using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entities Select Options.
    /// </summary>
    public class AclActionOptionsSelect : EntitiesOptionsSelect
    {
        #region Properties

        /// <summary>
        /// Property Action field.
        /// </summary>
        public string Action { get; set; } = "";

        #endregion
    }
}