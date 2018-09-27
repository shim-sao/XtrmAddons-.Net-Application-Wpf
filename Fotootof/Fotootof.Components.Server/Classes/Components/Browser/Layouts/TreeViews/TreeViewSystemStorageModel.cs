using Fotootof.Components.Server.Browser.Layouts.Helpers;
using Fotootof.Libraries.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Browser.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Components Server Browser Storage System Tree View Model.
    /// </summary>
    internal class TreeViewSystemStorageModel : ControlLayoutModel<TreeViewSystemStorageLayout>
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
            => (TreeView)ControlView.FindName("TreeViewDirectoryInfoName");

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Components Server Browser Storage System Tree View Model Constructor.
        /// </summary>
        public TreeViewSystemStorageModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Components Server Browser Storage System Tree View Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="TreeViewSystemStorageLayout"/> owner of the model.</param>
        public TreeViewSystemStorageModel(TreeViewSystemStorageLayout controlView) : base(controlView)
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
                    throw;
                }
            }
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Track whether Dispose has been called.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    drives = null;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.


                // Note disposing has been done.
                disposed = true;
                    
                // Call base class implementation.
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}