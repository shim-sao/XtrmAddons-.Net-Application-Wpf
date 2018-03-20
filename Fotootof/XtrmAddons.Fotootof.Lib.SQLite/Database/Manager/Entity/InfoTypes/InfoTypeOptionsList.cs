using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite List Entities Select Options.
    /// </summary>
    public class InfoTypeOptionsList : EntitiesOptionsSelect
    {
        #region Properties

        /// <summary>
        /// Property list of info types alias.
        /// </summary>
        public List<string> Alias { get; set; }

        #endregion
    }
}