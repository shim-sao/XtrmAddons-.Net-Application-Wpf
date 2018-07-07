namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Interface for Base Entity.
    /// </summary>
    public interface IAlias : IEntityBase
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
