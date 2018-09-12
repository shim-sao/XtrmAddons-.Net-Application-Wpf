using Fotootof.Collections;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.Enums;
using Fotootof.Libraries.Models.Systems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Server.Browser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View.</para>
    /// <para>This page is design to display a simple file browser for media manangement.</para>
    /// </summary>
    public partial class PageBrowserLayout : ComponentView
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
        public PageBrowserLayout()
        {
            MessageBoxs.IsBusy = true;
            log.Warn(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageWaiting, "Browser"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageDone, "Browser"));
            MessageBoxs.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
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
            (FindName("TextBox_Breadcrumbs_Name") as TextBox).Text = Model.GetText(dirInfo);
        }

        /// <summary>
        /// Method to clear the <see cref="CollectionStorage"/> of files displayed in the view.
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
            (FindName("Button_Forward_Name") as Button).IsEnabled = false;

            Model.ChangeCurrentItem(((e.NewValue) as TreeViewItem)?.Tag);
            if (Model.PreviewStack.Count > 1)
            {
                (FindName("Button_Back_Name") as Button).IsEnabled = true;
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
            (FindName("Button_Forward_Name") as Button).IsEnabled = true;

            Model.CurrentItem = Model.PreviewStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (Model.PreviewStack.Count <= 1)
            {
                (FindName("Button_Back_Name") as Button).IsEnabled = false;
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
            (FindName("Button_Back_Name") as Button).IsEnabled = true;

            Model.CurrentItem = Model.NextStack.Pop();
            Refresh_UcListViewStoragesServer();

            if (Model.NextStack.Count == 0)
            {
                (FindName("Button_Forward_Name") as Button).IsEnabled = false;
            }
        }

        /// <summary>
        /// Method called on upward click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void OnUpward_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = Model.GetCurrentDirectoryInfo();

            if (di != null)
            {
                if(di.Parent != null)
                {
                    Model.PreviewStack.Push(Model.CurrentItem);
                    (FindName("Button_Back_Name") as Button).IsEnabled = true;

                    Model.NextStack.Clear();
                    (FindName("Button_Forward_Name") as Button).IsEnabled = false;

                    Model.CurrentItem = new DirectoryInfo(di.Parent.FullName);
                    Refresh_UcListViewStoragesServer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Refresh_UcListViewStoragesServer()
        {
            UcListViewStoragesServer?.Items?.Clear();
            Model.ClearFilesCollection();

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
        /// <param name="dirInfos">A list of files</param>
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
                    (FindName("Button_Forward_Name") as Button).IsEnabled = false;

                    Model.PreviewStack.Push(Model.CurrentItem);
                    if (Model.PreviewStack.Count > 1)
                    {
                        (FindName("Button_Back_Name") as Button).IsEnabled = true;
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
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = Math.Max(MainBlockContent.ActualWidth, 0);
            Height = Math.Max(MainBlockContent.ActualHeight, 0);

            double TopCtrlHeight = (FindName("Block_TopControls") as FrameworkElement).RenderSize.Height;

            BlockMiddleContentsName.Width = Math.Max(Width, 0);
            BlockMiddleContentsName.Height = Math.Max(Height - TopCtrlHeight, 0);

            UcTreeViewDirectories.Height = Math.Max(Height - TopCtrlHeight, 0);
            UcListViewStoragesServer.Height = Math.Max(Height - TopCtrlHeight, 0);

            TraceSize(MainBlockContent);
            TraceSize(this);
            TraceSize(Block_TopControls);
            TraceSize(BlockMiddleContentsName);
            TraceSize(UcTreeViewDirectories);
            TraceSize(UcListViewStoragesServer);
        }

        #endregion
    }
}
