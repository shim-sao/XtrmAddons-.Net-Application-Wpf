using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewAlbum
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Album Model.
    /// </summary>
    public class PageAlbumModel<PageAlbum> : PageBaseModel<PageAlbum>
    {
        #region Variables

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
                RaisePropertyChanged("Album");
                RaisePropertyChanged("Pictures");
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
