namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column PrimaryKey.</para>
    /// <para>Insures that the object has an PrimaryKey property according to the table schema.</para>
    /// </summary>
    public interface IPrimaryKey
    {
        #region Proprerties

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        int PrimaryKey { get; set; }

        #endregion
    }
}