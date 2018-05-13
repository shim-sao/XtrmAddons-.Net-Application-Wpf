using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Albums in Sections Entity Object.
    /// </summary>
    [Table("AlbumsInSections")]
    [JsonObject(MemberSerialization.OptIn)]
    public class AlbumsInSections
    {
        #region Properties

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property the id of the Section entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public int SectionId { get; set; }

        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty]
        public int Ordering { get; set; }



        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        public AlbumEntity AlbumEntity { get; set; }

        /// <summary>
        /// Property to access to the Section entity.
        /// </summary>
        public SectionEntity SectionEntity { get; set; }

        #endregion
    }
}
