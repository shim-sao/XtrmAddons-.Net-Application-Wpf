using Fotootof.Collections.Entities;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Slideshow
{
    /// <summary>
    /// 
    /// </summary>
    public class PageSlideshowModel : ComponentModel<PageSlideshowLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private int albumPK;

        /// <summary>
        /// 
        /// </summary>
        private PictureEntityCollection pictures;

        /// <summary>
        /// 
        /// </summary>
        private PictureEntity currentPicture;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int AlbumPK
        {
            get => albumPK;
            set
            {
                albumPK = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PictureEntityCollection Pictures
        {
            get => pictures;
            set
            {
                pictures = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PictureEntity CurrentPicture
        {
            get => currentPicture;
            set
            {
                currentPicture = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartIndex { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int CurrentIndex { get; set; } = 0;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Page Slideshow Model Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public PageSlideshowModel(PageSlideshowLayout controlView) : base(controlView) { }

        #endregion



        #region Methods
        
        /// <summary>
        /// 
        /// </summary>
        public async void InitializeModelAsync()
        {
            try
            {
                if (AlbumPK > 0)
                {
                    AlbumEntity album = new AlbumEntity();

                    AlbumOptionsSelect options = new AlbumOptionsSelect
                    {
                        Dependencies = { EnumEntitiesDependencies.All },
                        PrimaryKey = AlbumPK
                    };
                    album = await Db.Albums.SingleOrNullAsync(options);

                    if (album != null && album?.Pictures != null)
                    {
                        Pictures = new PictureEntityCollection(album.Pictures);
                    }
                }

                if (CurrentPicture != null)
                {
                    CurrentPicture = CurrentPicture;
                }
                else if (Pictures.Count > 0)
                {
                    CurrentPicture = Pictures[0];
                }

            }
            catch (Exception ex)
            {
                log.Debug(ex.Output(), ex);
                throw;
            }
        }

        #endregion
    }
}