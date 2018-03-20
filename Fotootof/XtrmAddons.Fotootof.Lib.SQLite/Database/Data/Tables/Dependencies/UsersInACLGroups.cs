using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Users in AclGroups Entity Object.
    /// </summary>
    [Table("UsersInAclGroups")]
    public class UsersInAclGroups
    {
        #region Properties

        /// <summary>
        /// Property the id of the User entity.
        /// </summary>
        [Column(Order = 0)]
        public int UserId { get; set; }

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 1)]
        public int AclGroupId { get; set; }


        /// <summary>
        /// Property to access to the User entity.
        /// </summary>
        [NotMapped]
        public UserEntity UserEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
