namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Comment.</para>
    /// <para>Insures that the object has an Comment property according to the table schema.</para>
    /// </summary>
    public interface IDbColComment
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the comment of the entity.
        /// </summary>
        string Comment { get; set; }

        #endregion
    }
}
