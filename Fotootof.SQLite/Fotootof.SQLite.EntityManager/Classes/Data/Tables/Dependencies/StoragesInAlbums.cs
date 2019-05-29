using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Entity Manager SQLite Storages in Albums Entity Object.
    /// </summary>
    [Serializable]
    [Table("StoragesInAlbums")]
    [JsonObject(MemberSerialization.OptIn, Title = "Storages_Albums")]
    [XmlType(TypeName = "Storages_Albums")]
    public class StoragesInAlbums
    {
        #region Properties

        /// <summary>
        /// Property the id of the  <see cref="StorageEntity"/>.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "StorageId")]
        [XmlAttribute(DataType = "int", AttributeName = "StorageId")]
        public int StorageId { get; set; }

        /// <summary>
        /// Property the id of the <see cref="AlbumEntity"/>.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "AlbumId")]
        [XmlAttribute(DataType = "int", AttributeName = "AlbumId")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property order place of the item <see cref="StorageEntity"/>.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty(PropertyName = "Ordering")]
        [XmlAttribute(DataType = "int", AttributeName = "Ordering")]
        public int Ordering { get; set; }



        /// <summary>
        /// Property to access to the <see cref="AlbumEntity"/>.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AlbumEntity AlbumEntity { get; set; }

        /// <summary>
        /// Property to access to the <see cref="StorageEntity"/>.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public StorageEntity StorageEntity { get; set; }

        #endregion
    }
}
