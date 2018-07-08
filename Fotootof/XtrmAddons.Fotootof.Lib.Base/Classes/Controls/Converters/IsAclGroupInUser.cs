using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters
{
    /// <summary>
    /// <para>Class to check dependencies of the set entity.</para>
    /// <para>Perform try to find dependency of an AclGroup in User.</para>
    /// </summary>
    public class IsAclGroupInUser : IsDependency<UserEntity>
    {
        /// <summary>
        /// Property to access to the name of Users dependencies primary keys.
        /// </summary>
        public override string DependenciesPKName => "AclGroupsPKeys";
    }
}