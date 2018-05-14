using Newtonsoft.Json;
using System.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Common Entity.
    /// </summary>
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
                    OriginalPath = (string)Application.Current.Resources["ImageAlbumDefaultOriginal"],
                    OriginalWidth = 512,
                    OriginalHeight = 512,
                    PicturePath = (string)Application.Current.Resources["ImageAlbumDefaultPicture"],
                    PictureWidth = 256,
                    PictureHeight = 256,
                    ThumbnailPath = (string)Application.Current.Resources["ImageAlbumDefaultThumbnail"],
                    ThumbnailWidth = 128,
                    ThumbnailHeight = 128
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