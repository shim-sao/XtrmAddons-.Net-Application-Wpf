using Fotootof.Collections.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Controls.ListViews;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Diagnostics;
using System.IO;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Album.Layouts
{
    /// <summary>
    /// Class Fotootof Components Server Album Layouts List View Pictures Model.
    /// </summary>
    internal class ListViewPicturesModel : ListViewBaseModel<ListViewPicturesLayout, PictureEntityCollection>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="AlbumEntity"/>.
        /// </summary>
        public AlbumEntity AlbumEntity
        {
            get { return ControlView.AlbumEntity; }
            set
            {
                if(ControlView.AlbumEntity != value)
                {
                    ControlView.AlbumEntity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Components Server Album Layouts List View Pictures Model.
        /// </summary>
        /// <param name="controlView">The <see cref="ListViewPicturesLayout"/> owner of the model.</param>
        public ListViewPicturesModel(ListViewPicturesLayout controlView) : base(controlView) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected override void InitializeModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="propertyPathName"></param>
        /// <param name="sDir"></param>
        /// <returns></returns>
        public bool FileSearch(PictureEntity pe, string propertyPathName, string sDir)
        {
            bool IsChanged = false;

            try
            {
                string filename = Path.Combine(sDir, Path.GetFileName(pe.GetPropertyValue(propertyPathName).ToString()));

                log.Info(string.Format("Searching Picture : {0}", filename));
                Trace.TraceInformation(filename);

                if (File.Exists(filename))
                {
                    FileInfo fi = new FileInfo(filename);
                    string lProp = propertyPathName.Replace("Path", "Length");
                    long? l = (long)pe.GetPropertyValue(lProp);

                    if (l == null || l == 0 || fi.Length == l)
                    {
                        pe.SetPropertyValue(propertyPathName, fi.FullName);
                        pe.SetPropertyValue(lProp, fi.Length);
                        return true;
                    }
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    IsChanged = FileSearch(pe, propertyPathName, d);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                log.Info(uae.Message);
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBoxs.Error(string.Format("Searching Picture {0} failed !\n\r {1}", propertyPathName, e.Message));
            }

            return IsChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="propertyPathName"></param>
        /// <param name="SelectedPath"></param>
        /// <returns></returns>
        public bool DirSearch(PictureEntity pe, string propertyPathName, string SelectedPath)
        {
            if (FileSearch(pe, propertyPathName, SelectedPath))
            {
                return true;
            }

            /*
             * Search on all drives : to be optimize something like that.
             * 
            string root = Path.GetPathRoot((string)pe.GetPropertyValue(propertyPathName));
            Trace.TraceInformation(root);

            if (FileSearch(pe, propertyPathName, root))
            {
                return true;
            }

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if(di.Name == root)
                {
                    continue;
                }

                Trace.TraceInformation(di.Name);
                return FileSearch(pe, propertyPathName, di.Name);
            }
            */

            return false;
        }

        #endregion

    }
}
