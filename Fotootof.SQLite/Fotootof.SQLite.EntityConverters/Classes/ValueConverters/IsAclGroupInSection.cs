using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityConverters.ValueConverters
{
    /// <summary>
    /// <para>Class to check dependencies of the set entity.</para>
    /// <para>Perform try to find dependency of an AclGroup in a Section.</para>
    /// </summary>
    public class IsAclGroupInSection : IsDependency<SectionEntity>
    {
        /// <summary>
        /// Property to access to the name of AclGroups dependencies primary keys.
        /// </summary>
        public override string DependenciesPKName => "AclGroupsPKeys";
    }
}