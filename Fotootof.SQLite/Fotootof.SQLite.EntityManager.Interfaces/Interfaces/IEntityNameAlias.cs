namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Database Data Interface for Base Entity.
    /// </summary>
    public interface IEntityNameAlias : IEntityPrimaryKey
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
