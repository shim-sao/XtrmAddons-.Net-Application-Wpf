namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Alias.</para>
    /// <para>Insures that the object has an Alias property according to the table schema.</para>
    /// </summary>
    public interface IDbColAlias
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Alias of the entity.
        /// </summary>
        string Alias { get; set; }

        #endregion
    }
}
