namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Description.</para>
    /// <para>Insures that the object has an Comment property according to the table schema.</para>
    /// </summary>
    public interface IDbColDescription
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the description of the entity.
        /// </summary>
        string Description { get; set; }

        #endregion
    }
}
