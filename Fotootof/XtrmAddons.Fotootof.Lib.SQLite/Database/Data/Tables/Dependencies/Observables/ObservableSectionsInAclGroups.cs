using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups.
    /// </summary>
    [JsonArray(Title = "Sections_AclGroups")]
    public class ObservableSectionsInAclGroups : ObservableDependenciesBase<SectionsInAclGroups>
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservableSectionsInAclGroups(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }


        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Sections In AclGroups Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableSectionsInAclGroups(string dependenciesPrimaryKeysName, IEnumerable<SectionsInAclGroups> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}
