using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewAlbum
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Album Model.
    /// </summary>
    public class PageAlbumModel<PageAlbum> : PageBaseModel<PageAlbum>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private AlbumEntity album;

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public AlbumEntity Album
        {
            get => album;
            set
            {
                album = value;
                NotifyPropertyChanged("Album");
                NotifyPropertyChanged("Pictures");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PictureEntity> Pictures
        {
            get => Album.Pictures;
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Album Model Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public PageAlbumModel(PageAlbum pageBase) : base(pageBase) { }

        #endregion
    }
}
