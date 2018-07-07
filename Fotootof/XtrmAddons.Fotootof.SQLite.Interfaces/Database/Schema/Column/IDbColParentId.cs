namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column ParentId.</para>
    /// <para>Insures that the object has an Action property according to the table schema.</para>
    /// </summary>
    public interface IDbColParentId
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Parent Id or Primary Key of the entity.
        /// </summary>
        int ParentId { get; set; }

        #endregion
    }
}
