using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Pictures in Albums Entity Object.
    /// </summary>
    [Table("PicturesInAlbums")]
    [JsonObject(MemberSerialization.OptIn)]
    public class PicturesInAlbums
    {
        #region Properties

        /// <summary>
        /// Property the id of the Picture entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty]
        public int PictureId { get; set; }

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty]
        public int Ordering { get; set; }


        /// <summary>
        /// Property to access to the Picture entity.
        /// </summary>
        public PictureEntity PictureEntity { get; set; }

        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        public AlbumEntity AlbumEntity { get; set; }

        #endregion
    }
}