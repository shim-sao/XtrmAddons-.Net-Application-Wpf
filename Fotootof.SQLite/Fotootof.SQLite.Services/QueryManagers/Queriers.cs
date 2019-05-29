using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Managers;
using System.Threading.Tasks;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class Fotootof.SQLite.Services.
    /// </summary>
    public abstract class Queriers
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
        public static VersionManager VersionManager => Db.Versions;

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DbSave()
        {
            using (Db.Context) { return Db.Context.SaveChanges(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> DbSaveAsync()
        {
            using (Db.Context) { return await Db.Context.SaveChangesAsync(); }
        }

        #endregion
    }
}