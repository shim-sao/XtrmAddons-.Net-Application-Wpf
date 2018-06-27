using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using XtrmAddons.Fotootof.Common.Collections;
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
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Album data entity.
        /// </summary>
        private AlbumEntity album = new AlbumEntity();

        /// <summary>
        /// Variable Album data entity.
        /// </summary>
        //private PictureEntityCollection pictures;
        private List<PictureEntity> pictures;

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the Album data entity.
        /// </summary>
        public AlbumEntity Album
        {
            get
            {
                Debug.WriteLine($"Getter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {album}");
                return album;
            }
            set
            {
                if(album != value)
                {
                    Debug.WriteLine($"Setter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {value}");
                    album = value;
                    pictures = value?.Pictures;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Pictures");
                }
            }
        }

        /// <summary>
        /// Property accessors to the Album content Pictures list.
        /// </summary>
        // public PictureEntityCollection Pictures
        //{
        //    get => new PictureEntityCollection(Album.Pictures);
        //    set
        //    {
        //        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {value}");
        //        if (new PictureEntityCollection(Album.Pictures) != value && Album != null)
        //        {
        //            if(value != null)
        //            {
        //                Album.Pictures = value.ToList();
        //                NotifyPropertyChanged();
        //                NotifyPropertyChanged("Album");
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Property accessors to the Album content Pictures list.
        /// </summary>
        public List<PictureEntity> Pictures
            => Album.Pictures;
        /*public List<PictureEntity> Pictures
        {
            get
            {
                if(pictures == null)
                {
                    pictures = Album.Pictures;
                }

                Debug.WriteLineIf(pictures != null, $"Getter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {pictures.Count}");
                return pictures;
            }
            set
            {
                if (pictures != value)
                {
                    Debug.WriteLine(value != null, $"Setter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {value.Count}");
                    pictures = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Pictures");
                }
            }
        }*/

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
