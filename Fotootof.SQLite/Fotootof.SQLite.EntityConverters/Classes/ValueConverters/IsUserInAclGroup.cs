using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.SQLite.EntityConverters.ValueConverters
{
    /// <summary>
    /// <para>Class to check dependencies of the set entity.</para>
    /// <para>Perform try to find dependency of an User In AclGroup.</para>
    /// </summary>
    public class IsUserInAclGroup : IsDependency<AclGroupEntity>
    {
        /// <summary>
        /// Property to access to the name of Users dependencies primary keys.
        /// </summary>
        public override string DependenciesPKName => "UsersPKeys";
    }
}