using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using XtrmAddons.Net.Picture.Extensions;
using XtrmAddons.Net.Picture.ExtractLargeIconFromFile;
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

        #endregion



        #region Constructors


        /// <summary>
        /// Class XtrmAddons Fotootof Server Side Pages Browser Constructor.
        /// </summary>
        public PageBrowser()
        {
            InitializeComponent();
            AfterInitializedComponent();
        }

        #endregion



        #region Methods

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
            TreeViewDirectories.DirectoriesTreeView.SelectedItemChanged += TreeViewDirectories_SelectedItemChanged;

            // Reinitialize datacontext.
            Model.FilesCollection = new ObservableCollection<ListViewFilesItemModel>();
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
            Model.FilesCollection.Clear();
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
                DirectoryInfo dirInfo = (DirectoryInfo)item.Tag;

                DisplayHeaderDirectory(dirInfo);
                ClearFilesCollection();

                LoadDirectoriesInfosToListView(GetDirectoriesInfos(dirInfo));
                LoadInfoFilesToListViewAsync(GetInfoFiles(dirInfo));

                //UCBrowserPicture.Visibility = Visibility.Visible;
            }

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
                //Model.FilesCollection.Add(new ListViewFilesItemModel(dirInfo));
            }

            //UCBrowserPicture.Visibility = Visibility.Visible;
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
                Model.FilesCollection.Add(new ListViewFilesItemModel(fileInfo));
            }

            //UCBrowserPicture.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Resize(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = this.Parent as FrameworkElement;
            if (fe != null)
            {
                Height = fe.ActualHeight - 200;

                Breadcrumbs.Text = Height.ToString() + "  (" + fe.GetType().ToString() +")";
            }
        }

        #endregion
    }
}
