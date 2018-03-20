using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite ACL Actions Entities List Options.
    /// </summary>
    public class AclActionOptionsList : EntitiesOptionsList
    {
        #region Properties

        /// <summary>
        /// Property list of Action field.
        /// </summary>
        public List<string> Actions { get; set; } = new List<string>();

        #endregion
    }
}