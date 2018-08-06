using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries ACLGroups in ACLActions Entity Object.
    /// </summary>
    [Serializable]
    [Table("AclGroupsInAclActions")]
    [JsonObject(MemberSerialization.OptIn, Title = "AclGroups_AclActions")]
    [XmlType(TypeName = "AclGroups_AclActions")]
    public class AclGroupsInAclActions
    {
        #region Properties

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "AclGroupId")]
        [XmlAttribute(DataType = "int", AttributeName = "AclGroupId")]
        public int AclGroupId { get; set; }

        /// <summary>
        /// Property the id of the AclAction entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "AclActionId")]
        [XmlAttribute(DataType = "int", AttributeName = "AclActionId")]
        public int AclActionId { get; set; }



        /// <summary>
        /// Property to access to the AclAction entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AclActionEntity AclActionEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
