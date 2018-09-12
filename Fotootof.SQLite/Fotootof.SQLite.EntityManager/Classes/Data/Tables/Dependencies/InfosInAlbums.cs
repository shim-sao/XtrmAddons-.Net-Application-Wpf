using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Infos in Pictures Entity Object.
    /// </summary>
    [Serializable]
    [Table("InfosInAlbums")]
    [JsonObject(MemberSerialization.OptIn, Title = "Infos_Albums")]
    [XmlType(TypeName = "Infos_Albums")]
    public class InfosInAlbums
    {
        #region Properties

        /// <summary>
        /// Property the id of the Info entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "InfoId")]
        [XmlAttribute(DataType = "int", AttributeName = "InfoId")]
        public int InfoId { get; set; }

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "InfoId")]
        [XmlAttribute(DataType = "int", AttributeName = "InfoId")]
        public int AlbumId { get; set; }


        /// <summary>
        /// Property to access to the Info entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public InfoEntity InfoEntity { get; set; }

        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AlbumEntity AlbumEntity { get; set; }

        #endregion
    }
}