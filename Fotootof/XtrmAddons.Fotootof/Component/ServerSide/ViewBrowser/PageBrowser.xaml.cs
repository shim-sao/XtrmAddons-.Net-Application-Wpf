using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Side Pages Browser.
    /// </summary>
    public partial class PageBrowser : PageBase
    {
        #region Properties

        /// <summary>
        /// Property to access to the page browser model.
        /// </summary>
        public PageBrowserModel<PageBrowser> Model { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Size ImageSize { get; set; } = new Size { Height = 32, Width = 32 };

        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo CurrentDirectoryInfo { get; set; }

        #endregion



        #region Constructors


        /// <summary>
        /// Class XtrmAddons Fotootof Server Side Pages Browser Constructor.
        /// </summary>
        public PageBrowser()
        {
            InitializeComponent();
            AfterInitializedComponent();
            Loaded += (s, e) => Page_Loaded();
        }

        #endregion



        #region Methods

        public void Page_Loaded()
        {
            MiddleContents.MinHeight = GridRoot.ActualHeight - TopControls.ActualHeight;
            MiddleContents.Height = GridRoot.ActualHeight - TopControls.ActualHeight;

            UcTreeViewDirectories.MinHeight = GridRoot.ActualHeight - TopControls.ActualHeight;
            UcTreeViewDirectories.Height = GridRoot.ActualHeight - TopControls.ActualHeight;
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
            Model = new PageBrowserModel<PageBrowser>(this);

            // Add action to the tree view item event handler.
            UcTreeViewDirectories.DirectoriesTreeView.SelectedItemChanged += TreeViewDirectories_SelectedItemChanged;
            UcListViewStoragesServer.ImageSize_SelectionChanged += ImageSize_SelectionChanged;

            // Reinitialize datacontext.
            Model.StoragesCollection = new ObservableCollection<StorageInfoModel>();
            DataContext = Model;

            // End of busy indicator.
            AppLogger.InfoAndClose("Initializing page content. Done.", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        private FileInfo[] GetInfoFiles(DirectoryInfo dirInfo)
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
            Model.StoragesCollection.Clear();
            //UCBrowserPicture.UpdateLayout();
            //UCBrowserPicture.Visibility = Visibility.Hidden;

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
                CurrentDirectoryInfo = (DirectoryInfo)item.Tag;
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
                StorageInfoModel item = new StorageInfoModel(dirInfo) { ImageSize = ImageSize };
                Model.StoragesCollection.Add(item);
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
                StorageInfoModel item = new StorageInfoModel(fileInfo) { ImageSize = ImageSize };
                Model.StoragesCollection.Add(item);
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
            int dim = 32;
            int index = ((ComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    dim = 32;
                    break;

                case 1:
                    dim = 64;
                    break;

                case 2:
                    dim = 96;
                    break;

                case 3:
                    dim = 128;
                    break;

                case 4:
                    dim = 256;
                    break;
            }

            ImageSize = new Size(dim, dim);
            Refresh_UcListViewStoragesServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Resize(object sender, SizeChangedEventArgs e)
        {
            Page_Loaded();
        }

        #endregion
    }
}
