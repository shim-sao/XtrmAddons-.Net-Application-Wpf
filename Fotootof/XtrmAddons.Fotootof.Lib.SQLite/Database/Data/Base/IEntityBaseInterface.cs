using System.ComponentModel.DataAnnotations.Schema;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Interface for Base Entity.
    /// </summary>
    public interface IEntityBaseInterface
    {
        #region Proprerties

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        int PrimaryKey { get; set; }

        #endregion
    }
}
