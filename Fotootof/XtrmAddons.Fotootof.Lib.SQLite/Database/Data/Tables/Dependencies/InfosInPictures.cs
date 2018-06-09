using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Infos in Pictures Entity Object.
    /// </summary>
    [Serializable]
    [Table("InfosInPictures")]
    [JsonObject(MemberSerialization.OptIn, Title = "Infos_Pictures")]
    [XmlType(TypeName = "Infos_Pictures")]
    public class InfosInPictures
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
        /// Property the id of the Picture entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "PictureId")]
        [XmlAttribute(DataType = "int", AttributeName = "PictureId")]
        public int PictureId { get; set; }


        /// <summary>
        /// Property to access to the Info entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public InfoEntity InfoEntity { get; set; }

        /// <summary>
        /// Property to access to the Picture entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public PictureEntity PictureEntity { get; set; }

        #endregion
    }
}
