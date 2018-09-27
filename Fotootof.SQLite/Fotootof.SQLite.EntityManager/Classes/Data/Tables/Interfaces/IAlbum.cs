using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Interfaces
{
    /// <summary>
    /// <para>Interface Fotootof.SQLite.EntityManager Data Tables Album Entity.</para>
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
