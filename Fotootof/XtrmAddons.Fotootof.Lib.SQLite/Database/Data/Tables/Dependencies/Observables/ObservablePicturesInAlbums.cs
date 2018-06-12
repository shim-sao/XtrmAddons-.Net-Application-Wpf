using Newtonsoft.Json;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Pictures In Albums.
    /// </summary>
    [JsonArray(Title = "Pictures_Albums")]
    public class ObservablePicturesInAlbums : ObservableDependenciesBase<PicturesInAlbums>
    {

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Pictures In Albums Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        public ObservablePicturesInAlbums(string dependenciesPrimaryKeysName)
            : base(dependenciesPrimaryKeysName) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Pictures In Albums Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName">The dependency primary key name.</param>
        /// <param name="collection">An enumerable collection of items.</param>
        public ObservablePicturesInAlbums(string dependenciesPrimaryKeysName, IEnumerable<PicturesInAlbums> collection)
            : base(dependenciesPrimaryKeysName, collection) { }
    }
}
