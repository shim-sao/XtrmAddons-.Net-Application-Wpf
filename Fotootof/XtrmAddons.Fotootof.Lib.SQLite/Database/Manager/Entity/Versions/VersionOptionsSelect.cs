using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Versions Entities Select Options.
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