using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Interfaces
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof Lib SQLite Database Data Tables Section Entity.</para>
    /// <para>This interface is design to make sure a class provides access to a Section Entity.</para>
    /// </summary>
    public interface ISectionEntity
    {
        /// <summary>
        /// Property to access to a Section Entity
        /// </summary>
        SectionEntity SectionEntity { get; set; }        
    }
}
