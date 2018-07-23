using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.SideServer.Layouts.TreeViews.SystemStorage
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory.
    /// </summary>
    public partial class TreeViewSystemStorage : ControlBase
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private new static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public DriveInfo[] Drives { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Visibility IsHeaderVisible
        {
            get
            {
                return (FindName("StackPanelBlockHeaderName") as FrameworkElement).Visibility;
            }
            set
            {
                (FindName("StackPanelBlockHeaderName") as FrameworkElement).Visibility = value;
            }
        }
        
        #endregion


        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory Constructor.
        /// </summary>
        public TreeViewSystemStorage() : base ()
        {
            InitializeComponent();
            InitializeContent();
            Loaded += (s, e) => TreeView_Loaded();
        }
        

        /// <summary>
        /// Method to initialize control content.
        /// </summary>
        public void InitializeContent()
        {
            // Get list of computer drives.
            Drives = DriveInfo.GetDrives();

            // Add items to the tree view.
            foreach (DriveInfo driveInfo in Drives)
            {
                (FindName("TreeViewDirectoryInfoName") as TreeView).Items.Add(CreateTreeDriveInfo(driveInfo));
            }
        }

        /// <summary>
        /// Method to expand a folder sub-directories tree.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            // Get tree view item.
            TreeViewItem item = e.Source as TreeViewItem;

            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                DirectoryInfo expandedDir = null;

                if (item.Tag is DriveInfo)
                {
                    expandedDir = (item.Tag as DriveInfo).RootDirectory;
                }

                if (item.Tag is DirectoryInfo)
                {
                    expandedDir = (item.Tag as DirectoryInfo);
                }

                try
                {
                    foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                    {
                        if((subDir.Attributes & FileAttributes.System) != FileAttributes.System)
                        {
                            item.Items.Add(CreateTreeDirectoryInfo(subDir));
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Method to create a DriveInfo tree item.
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        private TreeViewItem CreateTreeDriveInfo(DriveInfo di)
        {
            return CreateTree(new StorageInfoModel(di));
        }

        /// <summary>
        /// Method to create a DriveInfo tree item.
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        private TreeViewItem CreateTree(StorageInfoModel model)
        {
            BitmapImage icon = Win32Icon.IconFromHandle(model.Name).ToBitmap().ToBitmapImage();

            Grid header = new Grid();
            header.Height = 20;
            ColumnDefinition gr1 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            header.ColumnDefinitions.Add(gr1);
            header.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            StackPanel title = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Height = 20,
                Children =
                {
                    new Border()
                    {
                        Child = new Image
                        {
                            Width = icon.Width,
                            Height = icon.Height,
                            Source = icon
                        }
                    },

                    new TextBlock
                    {
                        Text = model.Name.ToString(),
                        Margin = new Thickness(5,0,0,0)
                    }
                }
            };

            string inf = "NaN";
            try
            {
                inf = model.Files.Length.ToString();
                inf = $"{inf}/{model.Directories.Length.ToString()}";

                if (model.DriveInfo != null)
                {
                    inf = $"{inf} - {model.DriveInfo.AvailableFreeSpace / (1024 * 1024 * 1024)}/{model.DriveInfo.TotalSize / (1024 * 1024 * 1024)}Go";
                }
            }
            catch(Exception e)
            {
                log.Debug(e.Output(), e);
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {model?.GetType()}");
            }


            TextBlock count = new TextBlock
            {
                Text = inf,
                Margin = new Thickness(0, 0, 10, 0),
                FontStyle = FontStyles.Italic,
                FontSize = 10
            };

            Grid.SetColumn(title, 0);
            Grid.SetColumn(count, 1);
            header.Children.Add(title);
            header.Children.Add(count);
            
            TreeViewItem tv = new TreeViewItem
            {
                Header = header
            };

            if (model.DriveInfo != null)
            {
                tv.Tag = model.DriveInfo;
            }

            if (model.DirectoryInfo != null)
            {
                tv.Tag = model.DirectoryInfo ;
            }


            tv.Items.Add("Loading...");

            return tv;
        }

        /// <summary>
        /// Method to create a directory tree item.
        /// </summary>
        /// <param name="di">A directory informations.</param>
        /// <returns></returns>
        private TreeViewItem CreateTreeDirectoryInfo(DirectoryInfo di)
        {
            //return CreateTree(new StorageInfoModel(di));

            BitmapImage icon = Win32Icon.IconFromHandle(di.FullName).ToBitmap().ToBitmapImage();

            double opacity = 1;
            if((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                opacity = 0.5;
            }

            TreeViewItem tv = new TreeViewItem
            {
                Header = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = 20,
                    Children =
                    {
                        new Border()
                        {
                            Child = new Image
                            {
                                Width = icon.Width,
                                Height = icon.Height,
                                Source = icon,
                                Opacity = opacity
                            }
                        },

                        new TextBlock
                        {
                            Text = di.ToString(),
                            Margin = new Thickness(5,0,0,0),
                            Opacity = opacity,
                            HorizontalAlignment = HorizontalAlignment.Stretch
                        }
                    },
                    HorizontalAlignment = HorizontalAlignment.Stretch
                },
                Tag = di,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            tv.Items.Add("Loading...");

            return tv;
        }

        /// <summary>
        /// 
        /// </summary>
        private void TreeView_Loaded()
        {
            var root = (FindName("GridBlockRootName") as FrameworkElement);
            var header = (FindName("StackPanelBlockHeaderName") as FrameworkElement);
            var tv = (FindName("TreeViewDirectoryInfoName") as TreeView);

            tv.Height = root.ActualHeight;
            if(header.IsVisible)
            {
                tv.Height -= header.ActualHeight;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void TreeView_Resize(object sender, SizeChangedEventArgs e)
        {
            TreeView_Loaded();
        }

        /// <summary>
        /// Method called on user control size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
