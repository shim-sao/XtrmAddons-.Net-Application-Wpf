using Newtonsoft.Json;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Tables Dependencies Observable Albums In Sections.
    /// </summary>
    [JsonArray(Title = "Albums_Sections")]
    public class ObservableAlbumsInSections : ObservableDependenciesBase<AlbumsInSections>
    {
        public ObservableAlbumsInSections(string dependenciesPrimaryKeysName) : base(dependenciesPrimaryKeysName) { }
    }
}
