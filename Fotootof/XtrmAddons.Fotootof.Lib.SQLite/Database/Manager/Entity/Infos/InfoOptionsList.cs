using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Section Entities List Options.
    /// </summary>
    public class InfoOptionsList : EntitiesOptionsList
    {
        #region Properties

        /// <summary>
        /// Property list of info alias.
        /// </summary>
        public List<string> Alias { get; set; }

        /// <summary>
        /// Property list of info types alias.
        /// </summary>
        public List<string> InfoTypesAlias { get; set; }

        #endregion
    }
}