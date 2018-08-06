using Fotootof.SQLite.EntityManager.Base;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Users Entities Select Options.
    /// </summary>
    public class UserOptionsSelect : EntitiesOptionsSelect
    {
        #region Properties
        
        /// <summary>
        /// Property Name of the entity.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Property Password of the entity.
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Property Email of the entity.
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Property must check password ?
        /// </summary>
        public bool CheckPassword { get; set; } = false;

        #endregion
    }
}