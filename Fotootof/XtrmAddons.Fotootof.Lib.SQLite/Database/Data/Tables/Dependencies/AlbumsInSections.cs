using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Albums in Sections Entity Object.
    /// </summary>
    [Serializable]
    [Table("AlbumsInSections")]
    [JsonObject(MemberSerialization.OptIn, Title = "Albums_Sections")]
    [XmlType(TypeName = "Albums_Sections")]
    public class AlbumsInSections
    {
        #region Properties

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "AlbumId")]
        [XmlAttribute(DataType = "int", AttributeName = "AlbumId")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property the id of the Section entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "SectionId")]
        [XmlAttribute(DataType = "int", AttributeName = "SectionId")]
        public int SectionId { get; set; }

        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty(PropertyName = "Ordering")]
        [XmlAttribute(DataType = "int", AttributeName = "Ordering")]
        public int Ordering { get; set; }



        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AlbumEntity AlbumEntity { get; set; }

        /// <summary>
        /// Property to access to the Section entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public SectionEntity SectionEntity { get; set; }

        #endregion
    }
}
