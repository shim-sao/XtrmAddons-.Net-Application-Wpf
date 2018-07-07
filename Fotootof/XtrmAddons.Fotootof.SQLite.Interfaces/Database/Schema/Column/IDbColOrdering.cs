namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Ordering.</para>
    /// <para>Insures that the object has an Ordering property according to the table schema.</para>
    /// </summary>
    public interface IDbColOrdering
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Ordering of the entity.
        /// </summary>
        int Ordering { get; set; }

        #endregion
    }
}
