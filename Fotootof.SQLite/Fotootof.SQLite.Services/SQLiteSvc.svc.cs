using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Base;
using Fotootof.SQLite.Services.QueryManagers;
using System.Reflection;

namespace Fotootof.SQLite.Services
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Service.
    /// </summary>
    public partial class SQLiteSvc : ISQLiteSvc
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the SQLite Service instance <see cref="SQLiteSvc"/>.
        /// </summary>
        public static SQLiteSvc instance;

        /// <summary>
        /// Variable to store the database connector core <see cref="DatabaseCore"/> using EntityFramework.
        /// </summary>
        private static DatabaseCore db;

        /// <summary>
        /// Variable to store the database file name.
        /// </summary>
        //private static string databaseFileName;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the database connector core <see cref="DatabaseCore"/> using EntityFramework.
        /// </summary>
        public static DatabaseCore Db { get => db; set { if (db is null) EntityBase.Db = db = value; } }
        // public static DatabaseCore Db { get => db; set { if (db is null) db = value; } }

        /// <summary>
        /// Method to access to the AclActions Querier <see cref="QuerierAclAction"/>
        /// </summary>
        public QuerierAclAction AclActions { get; } = new QuerierAclAction();

        /// <summary>
        /// Method to access to the AclGroups Querier <see cref="QuerierAclGroup"/>
        /// </summary>
        public QuerierAclGroup AclGroups { get; } = new QuerierAclGroup();

        /// <summary>
        /// Method to access to the Albums Querier <see cref="QuerierAlbum"/>
        /// </summary>
        public QuerierAlbum Albums { get; } = new QuerierAlbum();

        /// <summary>
        /// Method to access to the Infos Querier <see cref="QuerierInfo"/>
        /// </summary>
        public QuerierInfo Infos { get; } = new QuerierInfo();

        /// <summary>
        /// Method to access to the Pictures Querier <see cref="QuerierPicture"/>
        /// </summary>
        public QuerierPicture Pictures { get; } = new QuerierPicture();

        /// <summary>
        /// Method to access to the Sections Querier <see cref="QuerierSection"/>
        /// </summary>
        public QuerierSection Sections { get; } = new QuerierSection();

        /// <summary>
        /// Method to access to the Users Querier <see cref="QuerierUser"/>
        /// </summary>
        public QuerierUser Users { get; } = new QuerierUser();

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof SQLite Service constructor.
        /// </summary>
        public SQLiteSvc()
        {
            log.Debug($"Initializing SQLite Service version {Assembly.GetAssembly(GetType()).GetName().Version}");
        }
        
        #endregion



        #region Methods

        /// <summary>
        /// Method Main Service contract.
        /// </summary>
        public string GetVersion()
        {
            return Assembly.GetAssembly(GetType()).GetName().Version.ToString();
        }

        /// <summary>
        /// <para>Method to get a service instance object <see cref="SQLiteSvc"/></para>
        /// <para>Return the SQLite Service instance or create a new one if no instance exists.</para>
        /// </summary>
        public static SQLiteSvc GetInstance()
        {
            if(instance == null)
            {
                instance = new SQLiteSvc();
            }

            return instance;
        }

        #endregion
    }
}
