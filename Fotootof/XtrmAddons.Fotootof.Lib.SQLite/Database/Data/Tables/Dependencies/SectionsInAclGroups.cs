using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Sections in AclGroups Entity Object.
    /// </summary>
    [Table("SectionsInAclGroups")]
    public class SectionsInAclGroups
    {
        #region Properties

        /// <summary>
        /// Property the id of the Section entity.
        /// </summary>
        [Column(Order = 0)]
        public int SectionId { get; set; }

        /// <summary>
        /// Property the id of the AclGroup entity.
        /// </summary>
        [Column(Order = 1)]
        public int AclGroupId { get; set; }


        /// <summary>
        /// Property to access to the Section entity.
        /// </summary>
        [NotMapped]
        public SectionEntity SectionEntity { get; set; }

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        [NotMapped]
        public AclGroupEntity AclGroupEntity { get; set; }

        #endregion
    }
}
