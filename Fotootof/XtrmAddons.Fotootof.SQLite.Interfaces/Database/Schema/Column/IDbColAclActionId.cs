namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Column Action.</para>
    /// <para>Insures that the object has an Action property according to the table schema.</para>
    /// </summary>
    public interface IDbColAclActionId
    {
        #region Proprerties

        /// <summary>
        /// Property to accass to the Action of the entity.
        /// </summary>
        int AclActionId { get; set; }

        #endregion
    }
}
