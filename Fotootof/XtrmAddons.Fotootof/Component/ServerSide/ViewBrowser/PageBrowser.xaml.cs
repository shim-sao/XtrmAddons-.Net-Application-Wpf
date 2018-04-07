using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View.</para>
    /// <para>This page is design to display a simple file browser for media manangement.</para>
    /// </summary>
    public partial class PageBrowser : PageBase
    {
        #region Properties

        /// <summary>
        /// Property to access to the page browser model.
        /// </summary>
        public PageBrowserModel<PageBrowser> Model { get; private set; }

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        public Stack<DirectoryInfo> PreviewDirectoryInfoStack { get; set; } = new Stack<DirectoryInfo>();

        /// <summary>
        /// Property to access to the current directory system informations.
        /// </summary>
        public DirectoryInfo CurrentDirectoryInfo { get; set; }

        /// <summary>
        /// Property to access to the preview directory system informations.
        /// </summary>
        public DirectoryInfo NextDirectoryInfo { get; set; }

        #endregion



        #region Constructors


        /// <summary>
        /// Class XtrmAddons Fotootof Server Side Component Browser View.
        /// </summary>
        public PageBrowser()
        {
            InitializeComponent();
            AfterInitializedComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            this.Width = Math.Max(fe.ActualWidth, 0);
            this.Height = Math.Max(fe.ActualHeight, 0);

            Block_MiddleContents.Width = Math.Max(this.Width, 0);
            Block_MiddleContents.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);

            UcTreeViewDirectories.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);
            UcListViewStoragesServer.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);

            Trace_Control_SizeChanged(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trace"></param>
        public void Trace_Control_SizeChanged(bool trace)
        {
            if (!trace) return;

            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            TraceSize(fe);
            TraceSize(this);
            TraceSize(Block_TopControls);
            TraceSize(Block_MiddleContents);
            TraceSize(UcTreeViewDirectories);
            TraceSize(UcListViewStoragesServer);
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }
        
        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContentAsync()
        {
            // Set busy indicator
            AppLogger.Info("Initializing page content. Please wait...", true);

            // Initialize associated view model of the page.
            Model = new PageBrowserModel<PageBrowser>(this)
            {
                FilesCollection = new ObservableCollection<StorageInfoModel>()
            };

            // Add action to the tree view item event handler.
            UcTreeViewDirectories.DirectoriesTreeView.SelectedItemChanged += TreeViewDirectories_SelectedItemChanged;
            UcListViewStoragesServer.ImageSize_SelectionChanged += ImageSize_SelectionChanged;
            
            // Reinitialize datacontext.
            DataContext = Model;

            // End of busy indicator.
            AppLogger.InfoAndClose("Initializing page content. Done.", true);
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
                }
            }

            return info;
        }

        /// <summary>
        /// Method to display header title of directory.
        /// </summary>
        /// <param name="dirInfo">A directory info.</param>
        private void DisplayHeaderDirectory(DirectoryInfo dirInfo)
        {
            if (dirInfo != null)
            {
                Breadcrumbs.Text = dirInfo.FullName;
            }
        }

        /// <summary>
        /// Method to clear files list view.
        /// </summary>
        private void ClearFilesCollection()
        {
            Model.FilesCollection.Clear();

            // Reset picture memory cache.
            if (PictureMemoryCache.MemCache != null)
            {
                PictureMemoryCache.MemCache.Dispose();
                PictureMemoryCache.MemCache = null;
            }

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewDirectories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)e.NewValue;
            TreeViewItem parent = item.GetTreeViewItemRoot();

            if (item.Tag.GetType() == typeof(DirectoryInfo))
            {
                DirectoryInfo di = (DirectoryInfo)item.Tag;

                PreviewDirectoryInfoStack.Push(CurrentDirectoryInfo);
                CurrentDirectoryInfo = di;
                Refresh_UcListViewStoragesServer();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_UcListViewStoragesServer()
        {
            DisplayHeaderDirectory(CurrentDirectoryInfo);
            ClearFilesCollection();

            LoadDirectoriesInfosToListView(GetDirectoriesInfos(CurrentDirectoryInfo));
            LoadInfoFilesToListViewAsync(GetInfoFiles(CurrentDirectoryInfo));

            UcListViewStoragesServer.Visibility = Visibility.Visible;
            UpdateLayout();
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
                StorageInfoModel item = new StorageInfoModel(dirInfo) { ImageSize = Model.ImageSize };
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
                StorageInfoModel item = new StorageInfoModel(fileInfo) { ImageSize = Model.ImageSize };
                Model.FilesCollection.Add(item);
            }

            UcListViewStoragesServer.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcListViewStoragesServer_ItemsCollection_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView list = sender as ListView;
            StorageInfoModel item = list.SelectedItem as StorageInfoModel;

            if(item != null)
            {
                if (item.IsPicture)
                {

                }
                else if (item.DirectoryInfo != null)
                {
                    CurrentDirectoryInfo = item.DirectoryInfo;
                    Refresh_UcListViewStoragesServer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcListViewStoragesServer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion



        #region Methods Size Changed

        #endregion
    }
}
