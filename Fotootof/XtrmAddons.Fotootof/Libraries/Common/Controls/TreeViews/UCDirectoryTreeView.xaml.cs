using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.TreeViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory.
    /// </summary>
    public partial class UCDirectoryTreeView : ControlBase
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
                return SectionsDirectoriesHeader.Visibility;
            }
            set
            {
                SectionsDirectoriesHeader.Visibility = value;
            }
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control Browser Tree View Directory Constructor.
        /// </summary>
        public UCDirectoryTreeView() : base ()
        {
            InitializeComponent();
            InitializeContent();
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
                DirectoriesTreeView.Items.Add(CreateTreeDriveInfo(driveInfo));
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

        private void Window_Resize(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = this.Parent as FrameworkElement;
            Height = fe.ActualHeight - 25;

            /*var b = ActualHeight;
            var a = fe.ActualHeight;
            var c = Height;
            var d = 1;*/
        }
    }
}
