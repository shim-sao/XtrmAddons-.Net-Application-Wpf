using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

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
        public ObservableCollection<DriveInfo> drives
            = new ObservableCollection<DriveInfo>();

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the Server.
        /// </summary>
        public ObservableCollection<DriveInfo> Drives
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
                Drives.Add(driveInfo);
            }
        }

        /// <summary>
        /// Method to expand a folder sub-directories tree.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public void ExpandTreeViewItem(TreeViewItemDriveInfo item)
        {
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
                        if ((subDir.Attributes & FileAttributes.System) != FileAttributes.System)
                        {
                           // item.Items.Add(CreateTreeDirectoryInfo(subDir));
                        }
                    }
                }
                catch { }
            }
        }

        #endregion
    }
}
