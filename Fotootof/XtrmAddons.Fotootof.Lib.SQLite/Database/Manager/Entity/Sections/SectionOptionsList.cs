using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Section Entities List Options.
    /// </summary>
    public class SectionOptionsList : EntitiesOptionsList
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<int> IncludeAlbumId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> ExcludeAlbumId { get; set; }

        #endregion
    }
}