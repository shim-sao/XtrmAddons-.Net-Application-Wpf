using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Pictures In Albums.
    /// </summary>
    [JsonArray(Title = "Pictures_Albums")]
    public class ObservablePicturesInAlbums : ObservableDependenciesBase<PicturesInAlbums>
    {
        public ObservablePicturesInAlbums(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
