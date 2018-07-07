namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Table
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Entity Base.</para>
    /// <para>Insures that the table entity has some properties and method according to the table schema.</para>
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