using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Pictures in Albums Entity Object.
    /// </summary>
    [Serializable]
    [Table("PicturesInAlbums")]
    [JsonObject(MemberSerialization.OptIn, Title = "Pictures_Albums")]
    [XmlType(TypeName = "Pictures_Albums")]
    public class PicturesInAlbums
    {
        #region Properties

        /// <summary>
        /// Property the id of the Picture entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "PictureId")]
        [XmlAttribute(DataType = "int", AttributeName = "PictureId")]
        public int PictureId { get; set; }

        /// <summary>
        /// Property the id of the Album entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "AlbumId")]
        [XmlAttribute(DataType = "int", AttributeName = "AlbumId")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Property order place of the item.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty(PropertyName = "Ordering")]
        [XmlAttribute(DataType = "int", AttributeName = "Ordering")]
        public int Ordering { get; set; }


        /// <summary>
        /// Property to access to the Picture entity.
        /// </summary>
        [XmlIgnore]
        public PictureEntity PictureEntity { get; set; }

        /// <summary>
        /// Property to access to the Album entity.
        /// </summary>
        [XmlIgnore]
        public AlbumEntity AlbumEntity { get; set; }

        #endregion
    }
}