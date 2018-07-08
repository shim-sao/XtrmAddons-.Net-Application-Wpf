namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Interface for Base Entity.
    /// </summary>
    public interface IEntityBase
    {
        #region Proprerties

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        int PrimaryKey { get; set; }

        #endregion
    }
}