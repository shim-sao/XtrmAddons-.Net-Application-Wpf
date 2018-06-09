using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Albums.
    /// </summary>
    [JsonArray(Title = "Infos_Albums")]
    public class ObservableInfosInAlbums : ObservableDependenciesBase<InfosInAlbums>
    {
        public ObservableInfosInAlbums(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
