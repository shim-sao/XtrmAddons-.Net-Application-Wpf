using Fotootof.SQLite.EntityManager.Base;
using System.Collections.Generic;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Picture Entities List Options.
    /// </summary>
    public class PictureOptionsList : EntitiesOptionsList
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