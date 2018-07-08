namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Parameters.</para>
    /// <para>Insures that the object has an Parameters property according to the table schema.</para>
    /// </summary>
    public interface IDbColParameters
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Parameters of the entity.
        /// </summary>
        string Parameters { get; set; }

        #endregion
    }
}
