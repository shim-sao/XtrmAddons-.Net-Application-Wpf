using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite User Entities List Options.
    /// </summary>
    public class UserOptionsList : EntitiesOptionsList
    { 
        #region Properties

        /// <summary>
        /// Property main list of entity primary key.
        /// </summary>
        public List<int> IncludeAclGroupKeys { get; set; }

        /// <summary>
        /// Property main list of entity primary key.
        /// </summary>
        public List<int> ExcludeAclGroupKeys { get; set; }

        #endregion
    }
}