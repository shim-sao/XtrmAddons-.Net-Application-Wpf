using Newtonsoft.Json;
using System;
using System.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Common Entity.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class CommonEntity : EntityBase
    {
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
                pe = new PictureEntity()
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

            else
            {
                pe = Db.Context.Pictures.FindAsync(pk).Result;
            }

            return pe;
        }

        #endregion
    }
}