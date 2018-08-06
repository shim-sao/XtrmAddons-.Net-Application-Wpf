using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Base;
using Fotootof.SQLite.Services.QueryManagers;

namespace Fotootof.SQLite.Services
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Service.
    /// </summary>
    public partial class SQLiteSvc : ISQLiteSvc
    {
        #region Variables

        /// <summary>
        /// Variable database connector core using EntityFramework.
        /// </summary>
        private static DatabaseCore db;

        /// <summary>
        /// Variable to store the database file name.
        /// </summary>
        //private static string databaseFileName;

        #endregion



        #region Properties

        /// <summary>
        /// Property database connector core using EntityFramework.
        /// </summary>
        public static DatabaseCore Db { get => db; set { if (db is null) EntityBase.Db = db = value; } }
        // public static DatabaseCore Db { get => db; set { if (db is null) db = value; } }

        /// <summary>
        /// 
        /// </summary>
        public QuerierAclAction AclActions { get; } = new QuerierAclAction();

        /// <summary>
        /// 
        /// </summary>
        public QuerierAclGroup AclGroups { get; } = new QuerierAclGroup();

        /// <summary>
        /// 
        /// </summary>
        public QuerierAlbum Albums { get; } = new QuerierAlbum();

        /// <summary>
        /// 
        /// </summary>
        public QuerierInfo Infos { get; } = new QuerierInfo();

        /// <summary>
        /// 
        /// </summary>
        public QuerierPicture Pictures { get; } = new QuerierPicture();

        /// <summary>
        /// 
        /// </summary>
        public QuerierSection Sections { get; } = new QuerierSection();

        /// <summary>
        /// 
        /// </summary>
        public QuerierUser Users { get; } = new QuerierUser();

        #endregion



        #region Methods

        /// <summary>
        /// Class XtrmAddons Fotootof SQLite Service constructor.
        /// </summary>
        public SQLiteSvc() { }

        /// <summary>
        /// 
        /// </summary>
        public void Main() { }

        #endregion
    }
}
