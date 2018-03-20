using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters
{
    /// <summary>
    /// <para>Class to check dependencies of the set entity.</para>
    /// <para>Perform try to find dependency of an Album in a Section.</para>
    /// </summary>
    public class IsAlbumInSection : IsDependency<SectionEntity>
    {
        /// <summary>
        /// Property to access to the name of Albums dependencies primary keys.
        /// </summary>
        public override string DependenciesPKName => "AlbumsPK";
    }
}