using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column;

namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Table
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Entity AclGroup.</para>
    /// <para>Insures that the table AclGroups has some properties and method according to the table schema.</para>
    /// </summary>
    public interface IAclGroupEntity : IEntityBase,
        IDbColAlias, IDbColName, IDbColParentId, IDbColComment
    {
        #region Properties

        /// <summary>
        /// Property to access to the primary key auto incremented.
        /// </summary>
        int AclGroupId { get; set; }

        #endregion

    }
}