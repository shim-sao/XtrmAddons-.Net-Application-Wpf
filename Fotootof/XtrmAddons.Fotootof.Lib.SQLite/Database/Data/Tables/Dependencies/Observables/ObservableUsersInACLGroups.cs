using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups.
    /// </summary>
    [JsonArray(Title = "Users_AclGroups")]
    public class ObservableUsersInAclGroups : ObservableDependenciesBase<UsersInAclGroups>
    {

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservableUsersInAclGroups(string dependenciesPrimaryKeysName)
            : base(dependenciesPrimaryKeysName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Users In AclGroups Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableUsersInAclGroups(string dependenciesPrimaryKeysName, IEnumerable<UsersInAclGroups> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}
