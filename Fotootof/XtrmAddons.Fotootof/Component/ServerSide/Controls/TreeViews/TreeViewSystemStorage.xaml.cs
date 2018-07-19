using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.TreeViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory.
    /// </summary>
    public partial class TreeViewSystemStorage : ControlBase
    {
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
                return StackPanelBlockHeaderName.Visibility;
            }
            set
            {
                StackPanelBlockHeaderName.Visibility = value;
            }
        }

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
            // Merge application resources.
            //Resources.MergedDictionaries.Add(Application.Current.Resources);

            // Get list of computer drives.
            Drives = DriveInfo.GetDrives();

            // Add items to the tree view.
            foreach (DriveInfo driveInfo in Drives)
            {
                TreeViewDirectoryInfoName.Items.Add(CreateTreeDriveInfo(driveInfo));
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
            BitmapImage icon = Win32Icon.IconFromHandle(di.Name).ToBitmap().ToBitmapImage();

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
                            Child = new System.Windows.Controls.Image
                            {
                                Width = icon.Width,
                                Height = icon.Height,
                                Source = icon
                            }
                        },

                        new TextBlock
                        {
                            Text = di.ToString(),
                            Margin = new Thickness(5,0,0,0)
                        }
                    }
                },

                Tag = di
            };

            tv.Items.Add("Loading...");

            return tv;
        }

        /// <summary>
        /// Method to create a directory tree item.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private TreeViewItem CreateTreeDirectoryInfo(DirectoryInfo di)
        {
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
                    Height = 15,
                    Children =
                    {
                        new Border()
                        {
                            Child = new System.Windows.Controls.Image
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
                            Opacity = opacity
                        }
                    }
                },

                Tag = di
            };

            tv.Items.Add("Loading...");

            return tv;
        }

        /// <summary>
        /// 
        /// </summary>
        private void TreeView_Loaded()
        {
            TreeViewDirectoryInfoName.Height = GridBlockRootName.ActualHeight;

            if(StackPanelBlockHeaderName.IsVisible)
            {
                TreeViewDirectoryInfoName.Height -= StackPanelBlockHeaderName.ActualHeight;
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
