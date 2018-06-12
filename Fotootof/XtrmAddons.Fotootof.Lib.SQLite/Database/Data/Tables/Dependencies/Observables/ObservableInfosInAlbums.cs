using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Albums.
    /// </summary>
    [JsonArray(Title = "Infos_Albums")]
    public class ObservableInfosInAlbums : ObservableDependenciesBase<InfosInAlbums>
    {

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Albums Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservableInfosInAlbums(string dependenciesPrimaryKeysName)
            : base(dependenciesPrimaryKeysName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Albums Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservableInfosInAlbums(string dependenciesPrimaryKeysName, IEnumerable<InfosInAlbums> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}
