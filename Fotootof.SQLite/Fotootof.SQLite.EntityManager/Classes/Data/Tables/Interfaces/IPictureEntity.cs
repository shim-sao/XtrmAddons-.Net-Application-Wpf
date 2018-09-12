using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Interfaces
{
    /// <summary>
    /// <para>Interface Fotootof.SQLite.EntityManager Data Tables Picture Entity.</para>
    /// <para>This interface is design to make sure a class provides access to a Picture Entity.</para>
    /// </summary>
    public interface IPictureEntity
    {
        /// <summary>
        /// Property to access to a Picture Entity
        /// </summary>
        PictureEntity PictureEntity { get; set; }
    }
}
