using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Infos In Pictures.
    /// </summary>
    [JsonArray(Title = "Infos_Pictures")]
    public class ObservableInfosInPictures : ObservableDependenciesBase<InfosInPictures>
    {
        public ObservableInfosInPictures(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
