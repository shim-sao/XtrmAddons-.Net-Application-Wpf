using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries ACLGroups in ACLActions Entity Object.
    /// </summary>
    [Table("AclGroupsInAclActions")]
    [JsonObject(MemberSerialization.OptIn)]
    public class AclGroupsInAclActions
    {
        #region Properties

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 0)]
        [JsonProperty]
        public int AclGroupId { get; set; }

        /// <summary>
        /// Property the id of the AclAction entity.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public int AclActionId { get; set; }



        /// <summary>
        /// Property to access to the AclAction entity.
        /// </summary>
        [NotMapped]
        public AclActionEntity AclActionEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
