namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table Column Combo `Name` and  `Alias`.
    /// </summary>
    public interface IColumnNameAlias : IColumnPrimaryKey
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the column Name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Property alias of the entity.
        /// </summary>
        string Alias { get; set; }

        #endregion
    }
}
