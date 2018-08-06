using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityConverters.ValueConverters
{
    /// <summary>
    /// <para>Class to check dependencies of the set entity.</para>
    /// <para>Perform try to find dependency of an Section in a Album.</para>
    /// </summary>
    public class IsSectionInAlbum : IsDependency<AlbumEntity>
    {
        /// <summary>
        /// Property to access to the name of Sections dependencies primary keys.
        /// </summary>
        public override string DependenciesPKName => "SectionsPKs";
    }
}