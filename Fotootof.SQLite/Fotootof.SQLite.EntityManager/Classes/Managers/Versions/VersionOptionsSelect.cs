using Fotootof.SQLite.EntityManager.Base;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Versions Entities Select Options.
    /// </summary>
    public class VersionOptionsSelect : EntitiesOptionsSelect
    {
        #region Properties

        /// <summary>
        /// Property Assembly Version Min.
        /// </summary>
        public string AssemblyVersionMin { get; set; } = "";

        /// <summary>
        /// Property Assembly Version Max.
        /// </summary>
        public string AssemblyVersionMax { get; set; } = "";

        #endregion
    }
}