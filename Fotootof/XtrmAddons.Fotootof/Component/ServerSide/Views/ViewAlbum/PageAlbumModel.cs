using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewAlbum
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Album Model.
    /// </summary>
    public class PageAlbumModel : PageBaseModel<PageAlbum>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Album data entity.
        /// </summary>
        private AlbumEntity album;

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the Album data entity.
        /// </summary>
        public AlbumEntity Album
        {
            get => album;
            set
            {
                if(album != value)
                {
                    album = value;
                    NotifyPropertyChanged("Album");
                    NotifyPropertyChanged("Pictures");
                }
            }
        }

        /// <summary>
        /// Property accessors to the Album content Pictures list.
        /// </summary>
        public List<PictureEntity> Pictures => Album.Pictures;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Album Model Constructor.
        /// </summary>
        /// <param name="page">The page associated to the model.</param>
        public PageAlbumModel(PageAlbum page) : base(page) { }

        #endregion
    }
}
