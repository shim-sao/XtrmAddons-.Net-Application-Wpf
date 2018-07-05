using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService.
    /// </summary>
    public class Queriers
    {
        #region Properties

        /// <summary>
        /// Property proxy to access to the database connector core using EntityFramework.
        /// </summary>
        public static DatabaseCore Db => SQLiteSvc.Db;

        /// <summary>
        /// Property proxy to access to the database AclAction entities manager.
        /// </summary>
        public static AclActionManager AclActionManager => Db.AclActions;

        /// <summary>
        /// Property proxy to access to the database AclGroup entities manager.
        /// </summary>
        public static AclGroupManager AclGroupManager => Db.AclGroups;

        /// <summary>
        /// Property proxy to access to the database Album entities manager.
        /// </summary>
        public static AlbumManager AlbumManager => Db.Albums;

        /// <summary>
        /// Property proxy to access to the database Info entities manager.
        /// </summary>
        public static InfoManager InfoManager => Db.Infos;

        /// <summary>
        /// Property proxy to access to the database Picture entities manager.
        /// </summary>
        public static PictureManager PictureManager => Db.Pictures;

        /// <summary>
        /// Property proxy to access to the database Section entities manager.
        /// </summary>
        public static SectionManager SectionManager => Db.Sections;

        /// <summary>
        /// Property proxy to access to the database User entities manager.
        /// </summary>
        public static UserManager UserManager => Db.Users;

        /// <summary>
        /// Property proxy to access to the database Version entities manager.
        /// </summary>
        [System.Obsolete("Use others mechanisms. Table will be deleted.")]
        public static VersionManager VersionManager => Db.Versions;

        #endregion
    }
}