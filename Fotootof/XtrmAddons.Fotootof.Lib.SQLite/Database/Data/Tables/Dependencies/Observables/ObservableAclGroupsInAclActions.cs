using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions.
    /// </summary>
    [JsonArray(Title = "AclGroups_AclActions")]
    public class ObservableAclGroupsInAclActions : ObservableDependenciesBase<AclGroupsInAclActions>
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservableAclGroupsInAclActions(string dependenciesPrimaryKeysName)
            : base(dependenciesPrimaryKeysName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableAclGroupsInAclActions(string dependenciesPrimaryKeysName, IEnumerable<AclGroupsInAclActions> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}