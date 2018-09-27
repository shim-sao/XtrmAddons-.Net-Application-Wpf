using Fotootof.SQLite.EntityManager.Base;

namespace Fotootof.SQLite.EntityManager.Managers
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