using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;

namespace XtrmAddons.Fotootof.ComponentOld.ServerSide.Views.ViewBrowser
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
        internal PageBrowserModel Model { get; private set; }

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        [Obsolete("Use Model.PreviewStack")]
        public Stack<object> PreviewStack => Model.PreviewStack;

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        [Obsolete("Use Model.PreviewStack")]
        public Stack<object> NextStack => Model.PreviewStack;

        /// <summary>
        /// Property to access to the current drive system informations.
        /// </summary>
        [Obsolete("Use Model.CurrentItem")]
        public object CurrentItem => Model.CurrentItem;

        private TreeView TreeViewStorage
            => (FindName("UcTreeViewDirectories") as UserControl).FindName("TreeViewDirectoryInfoName") as TreeView;


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
                FilesCollection = new CollectionStorage()
            };

            // Add action to the tree view item event handler.
            TreeViewStorage.SelectedItemChanged += TreeViewDirectories_SelectedItemChanged;
            UcListViewStoragesServer.ImageSize_SelectionChanged += ImageSize_SelectionChanged;
        }

        [Obsolete("Use Model.GetInfoFiles()")]
        private FileInfo[] GetInfoFiles(DirectoryInfo dirInfo, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return Model.GetInfoFiles(dirInfo);
        }

        [Obsolete("Use Model.GetInfoDirectories()")]
        private DirectoryInfo[] GetDirectoriesInfos(DirectoryInfo dirInfo)
        {
            return Model.GetInfoDirectories(dirInfo);
        }

        /// <summary>
        /// Method to display header title of directory.
        /// </summary>
        /// <param name="dirInfo">A directory info.</param>
        private void DisplayHeaderDirectory(object dirInfo)
        {
            Breadcrumbs.Text = Model.GetText(dirInfo);
        }

        /// <summary>
        /// Method to clear the <see cref="StorageCollection"/> of files displayed in the view.
        /// </summary>
        [Obsolete("use Model.ClearFilesCollection()")]
        private void ClearFilesCollection()
        {
            Model.ClearFilesCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void TreeViewDirectories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            (FindName("Button_Forward") as Button).IsEnabled = false;

            Model.ChangeCurrentItem(((e.NewValue) as TreeViewItem)?.Tag);
            if (Model.PreviewStack.Count > 1)
            {
                (FindName("Button_Back") as Button).IsEnabled = true;
            }
            
            Refresh_UcListViewStoragesServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnBack_Click(object sender, RoutedEventArgs e)
        {
            Model.NextStack.Push(Model.CurrentItem);
            Button_Forward.IsEnabled = true;

            Model.CurrentItem = Model.PreviewStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (Model.PreviewStack.Count <= 1)
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
            Model.PreviewStack.Push(Model.CurrentItem);
            Button_Back.IsEnabled = true;

            Model.CurrentItem = Model.NextStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (Model.NextStack.Count == 0)
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
            DirectoryInfo di = Model.GetCurrentDirectoryInfo();

            if (di != null)
            {
                if(di.Parent != null)
                {
                    Model.PreviewStack.Push(Model.CurrentItem);
                    Button_Back.IsEnabled = true;

                    Model.NextStack.Clear();
                    Button_Forward.IsEnabled = false;

                    Model.CurrentItem = new DirectoryInfo(di.Parent.FullName);
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
            UcListViewStoragesServer?.Items?.Clear();
            Model.ClearFilesCollection();

            DirectoryInfo di = null;
            if (Model.CurrentItem != null)
            {
                DisplayHeaderDirectory(Model.CurrentItem);

                Model.LoadInfos();

                UcListViewStoragesServer.Visibility = Visibility.Visible;
            }
            else
            {
                UcListViewStoragesServer.Visibility = Visibility.Hidden;
            }

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
        [Obsolete("use Model.GetCurrentDirectoryInfo()")]
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
        [Obsolete("use Model.LoadDirectoriesInfos();")]
        private void LoadDirectoriesInfosToListView(DirectoryInfo[] dirInfos)
        {
            Model.LoadDirectoriesInfo(dirInfos);

            //UcListViewStoragesServer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method to load files informations.
        /// </summary>
        /// <param name="infoFiles">A list of files</param>
        [Obsolete("use Model.LoadFilesInfo();")]
        private void LoadInfoFilesToListViewAsync(FileInfo[] infoFiles)
        {
            Model.LoadFilesInfo(infoFiles);
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
                    Model.NextStack.Clear();
                    Button_Forward.IsEnabled = false;

                    Model.PreviewStack.Push(Model.CurrentItem);
                    if (Model.PreviewStack.Count > 1)
                    {
                        Button_Back.IsEnabled = true;
                    }

                    Model.CurrentItem = item.DirectoryInfo;
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

            //UcTreeViewDirectories.Height = Math.Max(Height - TopCtrlHeight, 0);
            UcListViewStoragesServer.Height = Math.Max(Height - TopCtrlHeight, 0);

            TraceSize(MainBlockContent);
            TraceSize(this);
            TraceSize(Block_TopControls);
            TraceSize(Block_MiddleContents);
            //TraceSize(UcTreeViewDirectories);
            TraceSize(UcListViewStoragesServer);
        }

        #endregion
    }
}
