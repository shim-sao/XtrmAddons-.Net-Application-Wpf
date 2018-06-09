using Newtonsoft.Json;
using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable UsersInAclGroups.
    /// </summary>
    [JsonArray(Title = "Users_AclGroups")]
    public class ObservableUsersInAclGroups : ObservableDependenciesBase<UsersInAclGroups>
    {
        public ObservableUsersInAclGroups(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
