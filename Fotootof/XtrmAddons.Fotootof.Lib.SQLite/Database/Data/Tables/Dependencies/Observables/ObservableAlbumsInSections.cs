using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Albums In Sections.
    /// </summary>
    [JsonArray(Title = "Albums_Sections")]
    public class ObservableAlbumsInSections : ObservableDependenciesBase<AlbumsInSections>
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Albums In Sections Constructor.
        /// </summary>
        /// <param name="dependencyPrimaryKeyName">The dependency primary key name.</param>
        public ObservableAlbumsInSections(string dependencyPrimaryKeyName)
            : base(dependencyPrimaryKeyName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Albums In Sections Constructor.
        /// </summary>
        /// <param name="dependencyPrimaryKeyName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableAlbumsInSections(string dependencyPrimaryKeyName, IEnumerable<AlbumsInSections> collection)
            : base(dependencyPrimaryKeyName, collection) { }
    }
}
