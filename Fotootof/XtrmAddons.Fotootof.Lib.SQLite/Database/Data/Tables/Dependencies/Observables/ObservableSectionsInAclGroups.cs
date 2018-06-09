using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups.
    /// </summary>
    [JsonArray(Title = "Sections_AclGroups")]
    public class ObservableSectionsInAclGroups : ObservableDependenciesBase<SectionsInAclGroups>
    {
        public ObservableSectionsInAclGroups(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
