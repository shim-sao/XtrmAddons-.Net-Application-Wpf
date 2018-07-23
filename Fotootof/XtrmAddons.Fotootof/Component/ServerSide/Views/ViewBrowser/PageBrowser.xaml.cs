using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using XtrmAddons.Net.Windows.Converter.Picture;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View.</para>
    /// <para>This page is design to display a simple file browser for media manangement.</para>
    /// </summary>
    public partial class PageBrowser : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page browser model.
        /// </summary>
        public PageBrowserModel Model { get; private set; }

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        public Stack<object> PreviewStack { get; set; } = new Stack<object>();

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        public Stack<object> NextStack { get; set; } = new Stack<object>();

        /// <summary>
        /// Property to access to the current drive system informations.
        /// </summary>
        public object CurrentItem { get; set; }

        #endregion



        #region Constructors


        /// <summary>
        /// Class XtrmAddons Fotootof Server Side Component Browser View.
        /// </summary>
        public PageBrowser()
        {
            MessageBase.IsBusy = true;
            log.Warn(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageWaiting, "Browser"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageDone, "Browser"));
            MessageBase.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }
        
        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeModel()
        {
            // Initialize associated view model of the page.
            Model = new PageBrowserModel(this)
            {
                FilesCollection = new StorageCollection()
            };

            // Add action to the tree view item event handler.
            UcTreeViewDirectories.TreeViewDirectoryInfoName.SelectedItemChanged += TreeViewDirectories_SelectedItemChanged;
            UcListViewStoragesServer.ImageSize_SelectionChanged += ImageSize_SelectionChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        private FileInfo[] GetInfoFiles(DirectoryInfo dirInfo, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            FileInfo[] info = null;

            if (dirInfo != null)
            {
                try
                {
                    info = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    MessageBase.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        private DirectoryInfo[] GetDirectoriesInfos(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] info = null;

            if (dirInfo != null)
            {
                try
                {
                    info = dirInfo.GetDirectories();
                }
                catch(Exception e)
                {
                    log.Error(e);
                    MessageBase.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// Method to display header title of directory.
        /// </summary>
        /// <param name="dirInfo">A directory info.</param>
        private void DisplayHeaderDirectory(object dirInfo)
        {
            if (dirInfo != null)
            {
                if (dirInfo.GetType().Equals(typeof(DirectoryInfo)))
                {
                    Breadcrumbs.Text = ((DirectoryInfo)dirInfo).FullName;
                }

                if(dirInfo.GetType().Equals(typeof(DriveInfo)))
                {
                    Breadcrumbs.Text = ((DriveInfo)dirInfo).Name;
                }
            }
        }

        /// <summary>
        /// Method to clear files list view.
        /// </summary>
        private void ClearFilesCollection()
        {
            // Clear model files collection.
            Model.FilesCollection.Clear();

            // Clear picture memory cache.
            PictureMemoryCache.Clear();

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void TreeViewDirectories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            NextStack.Clear();
            Button_Forward.IsEnabled = false;

            PreviewStack.Push(CurrentItem);
            if (PreviewStack.Count > 1)
            {
                Button_Back.IsEnabled = true;
            }

            CurrentItem = ((TreeViewItem)e.NewValue).Tag;
            Refresh_UcListViewStoragesServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnBack_Click(object sender, RoutedEventArgs e)
        {
            NextStack.Push(CurrentItem);
            Button_Forward.IsEnabled = true;

            CurrentItem = PreviewStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (PreviewStack.Count <= 1)
            {
                Button_Back.IsEnabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnForward_Click(object sender, RoutedEventArgs e)
        {
            PreviewStack.Push(CurrentItem);
            Button_Back.IsEnabled = true;

            CurrentItem = NextStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (NextStack.Count == 0)
            {
                Button_Forward.IsEnabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnUpward_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = GetCurrentDirectoryInfo();

            if (di != null)
            {
                if(di.Parent != null)
                {
                    PreviewStack.Push(CurrentItem);
                    Button_Back.IsEnabled = true;

                    NextStack.Clear();
                    Button_Forward.IsEnabled = false;

                    CurrentItem = new DirectoryInfo(di.Parent.FullName);
                    Refresh_UcListViewStoragesServer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Refresh_UcListViewStoragesServer()
        {
            DirectoryInfo di = null;
            if (CurrentItem != null)
            {
                DisplayHeaderDirectory(CurrentItem);

                ClearFilesCollection();
                di = GetCurrentDirectoryInfo();

                LoadDirectoriesInfosToListView(GetDirectoriesInfos(di));
                LoadInfoFilesToListViewAsync(GetInfoFiles(di));
            }
            else
            {
                UcListViewStoragesServer.Visibility = Visibility.Hidden;
            }

            UcListViewStoragesServer.Visibility = Visibility.Visible;
            UcListViewStoragesServer.CounterTotalImages.Text = Model.FilesCollection.ImagesCount.ToString();
            UcListViewStoragesServer.CounterTotalDirectories.Text = Model.FilesCollection.DirectoriesCount.ToString();

            //((TextBlock)UcListViewStoragesServer.GetPropertyValue("CounterTotalImages")).Text = Model.FilesCollection.ImagesCount.ToString();
            //((TextBlock)UcListViewStoragesServer.GetPropertyValue("CounterTotalDirectories")).Text = Model.FilesCollection.DirectoriesCount.ToString();

            UpdateLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DirectoryInfo GetCurrentDirectoryInfo()
        {
            DirectoryInfo di = null;

            if(CurrentItem != null)
            {
                if (CurrentItem.GetType().Equals(typeof(DriveInfo)))
                {
                    di = new DirectoryInfo(((DriveInfo)CurrentItem).RootDirectory.FullName);
                }
                else
                {
                    di = (DirectoryInfo)CurrentItem;
                }
            }

            return di;
        }

        /// <summary>
        /// Method to load files informations.
        /// </summary>
        /// <param name="infoFiles">A list of files</param>
        private void LoadDirectoriesInfosToListView(DirectoryInfo[] dirInfos)
        {
            if (dirInfos == null)
            {
                return;
            }

            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                var item = new StorageInfoModel(dirInfo) { ImageSize = Model.ImageSize };
                Model.FilesCollection.Add(item);
            }

            UcListViewStoragesServer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method to load files informations.
        /// </summary>
        /// <param name="infoFiles">A list of files</param>
        private void LoadInfoFilesToListViewAsync(FileInfo[] infoFiles)
        {
            if (infoFiles == null)
            {
                return;
            }

            foreach (FileInfo fileInfo in infoFiles)
            {
                if (ConverterPictureBase.Extensions.Contains(fileInfo.Extension.ToLower()))
                { 
                    StorageInfoModel item = new StorageInfoModel(fileInfo) { ImageSize = Model.ImageSize };
                    Model.FilesCollection.Add(item);
                }
            }

            UcListViewStoragesServer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void ImageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            ImageSize dim = ImageSizeExtensions.Index(index);

            Model.ImageSize = dim.ToSize();
            Refresh_UcListViewStoragesServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void UcListViewStoragesServer_ItemsCollection_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView list = sender as ListView;

            if (list.SelectedItem is StorageInfoModel item)
            {
                if (item.IsPicture)
                {

                }
                else if (item.DirectoryInfo != null)
                {
                    NextStack.Clear();
                    Button_Forward.IsEnabled = false;

                    PreviewStack.Push(CurrentItem);
                    if (PreviewStack.Count > 1)
                    {
                        Button_Back.IsEnabled = true;
                    }

                    CurrentItem = item.DirectoryInfo;
                    Refresh_UcListViewStoragesServer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void UcListViewStoragesServer_Loaded(object sender, RoutedEventArgs e)
        {

        }


        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = Math.Max(MainBlockContent.ActualWidth, 0);
            Height = Math.Max(MainBlockContent.ActualHeight, 0);

            double TopCtrlHeight = ((FrameworkElement)FindName("Block_TopControls")).RenderSize.Height;

            Block_MiddleContents.Width = Math.Max(Width, 0);
            Block_MiddleContents.Height = Math.Max(Height - TopCtrlHeight, 0);

            UcTreeViewDirectories.Height = Math.Max(Height - TopCtrlHeight, 0);
            UcListViewStoragesServer.Height = Math.Max(Height - TopCtrlHeight, 0);

            TraceSize(MainBlockContent);
            TraceSize(this);
            TraceSize(Block_TopControls);
            TraceSize(Block_MiddleContents);
            TraceSize(UcTreeViewDirectories);
            TraceSize(UcListViewStoragesServer);
        }

        #endregion
    }
}
