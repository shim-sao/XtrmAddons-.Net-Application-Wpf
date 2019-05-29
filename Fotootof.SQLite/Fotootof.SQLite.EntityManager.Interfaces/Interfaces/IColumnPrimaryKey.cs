namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table Column `PrimaryKey`.
    /// </summary>
    public interface IColumnPrimaryKey
    {
        #region Proprerties

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        int PrimaryKey { get; set; }

        #endregion
    }
}