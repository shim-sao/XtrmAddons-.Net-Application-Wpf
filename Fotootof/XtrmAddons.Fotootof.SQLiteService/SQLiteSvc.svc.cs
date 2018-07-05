using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Fotootof.SQLiteService.QueryManagers;

namespace XtrmAddons.Fotootof.SQLiteService
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

        #endregion



        #region Properties

        /// <summary>
        /// Property database connector core using EntityFramework.
        /// </summary>
        public static DatabaseCore Db { get => db; set { if (db is null) EntityBase.Db = db = value; } }

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

        /// <summary>
        /// 
        /// </summary>
        [System.Obsolete("Use others mechanisms. Table will be deleted.")]
        public QuerierVersion Versions { get; } = new QuerierVersion();
        
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
