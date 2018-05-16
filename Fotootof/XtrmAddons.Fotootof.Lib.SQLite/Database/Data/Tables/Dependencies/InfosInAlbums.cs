using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Infos in Pictures Entity Object.
    /// </summary>
    [Table("InfosInAlbums")]
    [JsonObject(MemberSerialization.OptIn)]
    public class InfosInAlbums
    {
        #region Properties

        /// <summary>
        /// Property the id of the Info entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty]
        public int InfoId { get; set; }

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public int AlbumId { get; set; }


        /// <summary>
        /// Property to access to the Info entity.
        /// </summary>
        [NotMapped]
        public InfoEntity InfoEntity { get; set; }

        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        [NotMapped]
        public AlbumEntity AlbumEntity { get; set; }

        #endregion
    }
}