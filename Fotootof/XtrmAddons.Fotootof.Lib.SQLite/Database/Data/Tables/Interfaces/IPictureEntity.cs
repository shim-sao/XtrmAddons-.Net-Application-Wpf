using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Interfaces
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof Lib SQLite Database Data Tables Picture Entity.</para>
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
