using FotootofComponent.Browser.Controls.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Models;
using XtrmAddons.Net.Common.Extensions;

namespace FotootofComponent.Browser.Controls
{
    /// <summary>
    /// Class Fotootof Server Controls Storage System Model.
    /// </summary>
    internal class TreeViewSystemStorageModel : ModelBase<TreeViewSystemStorageControl>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store an <see cref="ObservableCollection{T}"/> of <see cref="DriveInfo"/>.
        /// </summary>
        public ObservableCollection<DriveInfo> drives
            = new ObservableCollection<DriveInfo>();

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the <see cref="ObservableCollection{T}"/> of <see cref="DriveInfo"/>.
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

        /// <summary>
        /// Property to access to the <see cref="TreeView"/> of the <see cref="UserControl"/>.
        /// </summary>
        public TreeView TreeViewDirectoryInfo 
            => OwnerBase.FindName("TreeViewDirectoryInfoName") as TreeView;

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Server Controls Storage System Model Constructor.
        /// </summary>
        public TreeViewSystemStorageModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class Fotootof Server Controls Storage System Model Constructor.
        /// </summary>
        /// <param name="owner">The <see cref="SystemStorageControl"/> owner of the model.</param>
        public TreeViewSystemStorageModel(TreeViewSystemStorageControl owner) : base(owner)
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

            foreach (DriveInfo driveInfo in Drives)
            {
                TreeViewDirectoryInfo.Items.Add(new TreeViewItemDriveInfo(driveInfo));
            }
        }

        /// <summary>
        /// Method to reinitialize the data of the model.
        /// </summary>
        public void Reinitialize()
        { 
            Drives.Clear();
            TreeViewDirectoryInfo.Items?.Clear();
            InitializeModel();
        }

        /// <summary>
        /// Method to expand a folder sub-directories tree.
        /// </summary>
        /// <param name="item">A <see cref="TreeViewItem"/> to expand.</param>
        public void ExpandTreeViewItem(TreeViewItem item)
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
                           item.Items.Add(new TreeViewItemDirectoryInfo(subDir));
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Output(), ex);
                    throw ex;
                }
            }
        }

        #endregion
    }
}
