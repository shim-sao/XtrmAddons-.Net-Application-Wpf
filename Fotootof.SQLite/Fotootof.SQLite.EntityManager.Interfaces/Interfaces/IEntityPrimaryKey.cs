namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Interface for Base Entity.
    /// </summary>
    public interface IEntityPrimaryKey
    {
        #region Proprerties

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        int PrimaryKey { get; set; }

        #endregion
    }
}