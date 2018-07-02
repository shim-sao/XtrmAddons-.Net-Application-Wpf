using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Interfaces
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof Lib SQLite Database Data Tables Album Entity.</para>
    /// <para>This interface is design to make sure a class provides access to a Album Entity.</para>
    /// </summary>
    public interface IAlbumEntity
    {
        /// <summary>
        /// Property to access to a Album Entity
        /// </summary>
        AlbumEntity AlbumEntity { get; set; }
    }
}
