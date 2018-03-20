using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Info Types Entities Select Options.
    /// </summary>
    public class InfoOptionsSelect : EntitiesOptionsSelect
    {
        #region Properties

        /// <summary>
        /// Property info types alias.
        /// </summary>
        public string Alias { get; set; }

        #endregion
    }
}