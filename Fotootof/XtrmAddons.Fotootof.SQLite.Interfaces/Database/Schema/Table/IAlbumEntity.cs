using XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Column;

namespace XtrmAddons.Fotootof.SQLite.Interfaces.Database.Schema.Table
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof SQLite Database Schema Entity Album.</para>
    /// <para>Insures that the table Albums has some properties and method according to the table schema.</para>
    /// </summary>
    public interface IAlbumEntity : IEntityBase,
        IDbColAlias, IDbColName, IDbColDescription, IDbColOrdering, IDbColComment
    {
        #region Properties

        /// <summary>
        /// Property to access to the primary key auto incremented.
        /// </summary>
        int AlbumId { get; set; }

        #endregion

    }
}