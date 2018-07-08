using XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column;

namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Table
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Entity Base.</para>
    /// <para>Insures that the table entity has some properties and method according to the table schema.</para>
    /// </summary>
    public interface IAclActionEntity : IEntityBase, 
        IDbColAction, IDbColParameters
    {
        #region Properties

        /// <summary>
        /// Property to access to the primary key auto incremented.
        /// </summary>
        int AclActionId { get; set; }

        #endregion

    }
}