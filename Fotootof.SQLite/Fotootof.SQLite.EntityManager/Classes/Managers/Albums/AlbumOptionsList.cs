using Fotootof.SQLite.EntityManager.Base;
using System.Collections.Generic;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite ACL Actions Entities List Options.
    /// </summary>
    public class AlbumOptionsList : EntitiesOptionsList
    {
        #region Properties

        /// <summary>
        /// Property include filter list of Sections primary keys.
        /// </summary>
        public List<int> IncludeSectionKeys { get; set; }

        /// <summary>
        /// Property exclude filter list of Sections primary keys.
        /// </summary>
        public List<int> ExcludeSectionKeys { get; set; }

        /// <summary>
        /// Property include filter list of Infos primary keys.
        /// </summary>
        public List<int> IncludeInfoKeys { get; set; }

        /// <summary>
        /// Property exclude filter list of Infos primary keys.
        /// </summary>
        public List<int> ExcludeInfoKeys { get; set; }

        #endregion
    }
}