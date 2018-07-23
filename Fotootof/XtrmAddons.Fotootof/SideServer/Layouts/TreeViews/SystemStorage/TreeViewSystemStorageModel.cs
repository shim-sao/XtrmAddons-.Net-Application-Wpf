using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Models;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.SideServer.Layouts.TreeViews.SystemStorage
{
    internal class TreeViewSystemStorageModel : ModelBase<TreeViewSystemStorage>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Server.
        /// </summary>
        public ObservableCollection<StorageInfoModel> drives
            = new ObservableCollection<StorageInfoModel>();

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the Server.
        /// </summary>
        public ObservableCollection<StorageInfoModel> Drives
        {
            get { return drives; }
            set
            {
                drives = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model Constructor.
        /// </summary>
        /// <param name="pBase"></param>
        public TreeViewSystemStorageModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model Constructor.
        /// </summary>
        /// <param name="owner"></param>
        public TreeViewSystemStorageModel(TreeViewSystemStorage owner) : base(owner)
        {
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected void InitializeModel()
        {
            // Get list of computer drives.
            // Add items to the collection view.
            foreach (var driveInfo in DriveInfo.GetDrives())
            {
                Drives.Add(new StorageInfoModel(driveInfo));
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

            Binding myBinding = new Binding("ActualWidth")
            {
                Source = OwnerBase.FindName("TreeViewDirectoryInfoName")
            };

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

            /* (tv.Header as StackPanel).SetBinding(StackPanel.WidthProperty, myBinding);
             if ((tv.Header as StackPanel).Width > 25)
             {
                 (tv.Header as StackPanel).Width -= 25;
             }*/

            tv.SetBinding(StackPanel.WidthProperty, myBinding);

            tv.Items.Add("Loading...");

            return tv;
        }

        #endregion
    }
}
