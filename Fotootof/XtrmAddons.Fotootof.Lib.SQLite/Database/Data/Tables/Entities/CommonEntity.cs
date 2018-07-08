using Newtonsoft.Json;
using System;
using System.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Common Entity.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class CommonEntity : EntityBase
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static PictureEntity GetPicture(int pk)
        {
            PictureEntity pe = null;

            if (pk == 0)
            {
                pe = GetPictureDefault();
            }

            else
            {
                try
                {
                    pe = Db.Context.Pictures.Find(pk);
                    if(pe == null)
                    {
                        pe = GetPictureDefault();
                    }
                }
                catch(Exception e)
                {
                    log.Error(e.Output(), e);
                    pe = GetPictureDefault();
                }
                
            }

            return pe;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static PictureEntity GetPictureDefault()
        {
            return new PictureEntity()
            {
                OriginalPath = (string)Application.Current.Resources["ImageAlbumDefaultBackground"],
                OriginalWidth = 1920,
                OriginalHeight = 1080,

                PicturePath = (string)Application.Current.Resources["ImageAlbumDefaultPreview"],
                PictureWidth = 1920,
                PictureHeight = 720,

                ThumbnailPath = (string)Application.Current.Resources["ImageAlbumDefaultThumbnail"],
                ThumbnailWidth = 512,
                ThumbnailHeight = 512
            };
        }

        #endregion
    }
}