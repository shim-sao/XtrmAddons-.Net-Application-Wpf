using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Tables Sections in AclGroups Entity Dependency.
    /// </summary>
    [Serializable]
    [Table("SectionsInAclGroups")]
    [JsonObject(MemberSerialization.OptIn, Title = "Sections_AclGroups")]
    [XmlType(TypeName = "Sections_AclGroups")]
    public class SectionsInAclGroups
    {
        #region Properties

        /// <summary>
        /// Property the id of the Section entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "SectionId")]
        [XmlAttribute(DataType = "int", AttributeName = "SectionId")]
        public int SectionId { get; set; }

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "AclGroupId")]
        [XmlAttribute(DataType = "int", AttributeName = "AclGroupId")]
        public int AclGroupId { get; set; }


        /// <summary>
        /// Property to access to the Section entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public SectionEntity SectionEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
