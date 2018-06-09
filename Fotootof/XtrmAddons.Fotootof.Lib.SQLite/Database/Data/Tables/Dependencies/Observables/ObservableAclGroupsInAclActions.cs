using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable AclGroups In AclActions.
    /// </summary>
    [JsonArray(Title = "AclGroups_AclActions")]
    public class ObservableAclGroupsInAclActions : ObservableDependenciesBase<AclGroupsInAclActions>
    {
        public ObservableAclGroupsInAclActions(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}