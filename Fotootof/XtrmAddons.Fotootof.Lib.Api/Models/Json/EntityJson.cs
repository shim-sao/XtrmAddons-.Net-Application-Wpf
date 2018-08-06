using Newtonsoft.Json;
using XtrmAddons.Net.Application;
using Fotootof.SQLite.Services;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Api Models Json Entity.
    /// </summary>
    public class EntityJson
    {
        /// <summary>
        /// Accessors database core manager.
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        protected SQLiteSvc Database => ApplicationSession.Properties.Database;

    }
}
