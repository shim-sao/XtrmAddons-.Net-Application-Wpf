namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons SQLite Database Schema Column Name.</para>
    /// <para>Insures that the object has an Name property according to the table schema.</para>
    /// </summary>
    public interface IDbColName
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the Name of the entity.
        /// </summary>
        string Name { get; set; }

        #endregion
    }
}
