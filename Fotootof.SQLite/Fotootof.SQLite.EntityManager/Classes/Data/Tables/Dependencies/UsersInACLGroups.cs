using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Users in AclGroups Entity Object.
    /// </summary>
    [Serializable]
    [Table("UsersInAclGroups")]
    [JsonObject(MemberSerialization.OptIn, Title = "Users_AclGroups")]
    [XmlType(TypeName = "Users_AclGroups")]
    public class UsersInAclGroups
    {
        #region Properties

        /// <summary>
        /// Property the id of the User entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty(PropertyName = "UserId")]
        [XmlAttribute(DataType = "int", AttributeName = "UserId")]
        public int UserId { get; set; }

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "AclGroupId")]
        [XmlAttribute(DataType = "int", AttributeName = "AclGroupId")]
        public int AclGroupId { get; set; }


        /// <summary>
        /// Property to access to the User entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public UserEntity UserEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
