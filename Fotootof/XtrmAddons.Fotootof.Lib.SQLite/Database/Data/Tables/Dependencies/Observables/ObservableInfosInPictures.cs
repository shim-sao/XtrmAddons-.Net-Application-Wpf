using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Pictures.
    /// </summary>
    [JsonArray(Title = "Infos_Pictures")]
    public class ObservableInfosInPictures : ObservableDependenciesBase<InfosInPictures>
    {

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Pictures Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservableInfosInPictures(string dependenciesPrimaryKeysName)
            : base(dependenciesPrimaryKeysName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Pictures Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableInfosInPictures(string dependenciesPrimaryKeysName, IEnumerable<InfosInPictures> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}
