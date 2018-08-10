using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Connector
{
    /// <summary>
    /// Class Fotootof SQLite EntityManager Core Connector.
    /// </summary>
    public class DatabaseCore : IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the database context entity core SQLite connector.
        /// </summary>
        private DatabaseContextCore context = null;

        /// <summary>
        /// Variable to store the SQLite database file name.
        /// </summary>
        private readonly string dbname = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the AclAction entities query manager.
        /// </summary>
        public AclActionManager AclActions { get; private set; }

        /// <summary>
        /// Property to access to the AclGroup entities query manager.
        /// </summary>
        public AclGroupManager AclGroups { get; private set; }

        /// <summary>
        /// Property to access to the Album Entities query manager.
        /// </summary>
        public AlbumManager Albums { get; private set; }

        /// <summary>
        /// Property to access to the Info Entities query manager.
        /// </summary>
        public InfoManager Infos { get; private set; }

        /// <summary>
        /// Property to access to the Picture entities query manager.
        /// </summary>
        public PictureManager Pictures { get; private set; }

        /// <summary>
        /// Property to access to the Section entities query manager.
        /// </summary>
        public SectionManager Sections { get; private set; }

        /// <summary>
        /// Property to access to the User entities query manager.
        /// </summary>
        public UserManager Users { get; private set; }

        /// <summary>
        /// Property to access to the Version entities query manager.
        /// </summary>
        public VersionManager Versions { get; private set; }

        /// <summary>
        /// Property to access to the database context entity core connector.
        /// </summary>
        public DatabaseContextCore Context
        {
            get
            {
                if (dbname != "")
                {
                    // Create the connection to the database.
                    bool isCreated = Connect();

                    // Register all entities query managers with the database context.
                    RegisterContext();

                    // Populate database if is new.
                    if(isCreated == true)
                    {
                        PopulateDatabase();
                    }
                    else
                    {
                        Versions.CheckVersion();
                    }
                }
                
                return context;
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// <para>Class Fotootof.SQLite.EntityManager Core Connector constructor.</para>
        /// <para>This class provide database connection and data management.</para>
        /// </summary>
        /// <param name="filename">A database filename. Full path of the SQLite database.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if database file name is null.</exception>
        /// <exception cref="ArgumentException">Exception thrown if database file name is invalid.</exception>
        public DatabaseCore(string filename)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                throw Exceptions.GetArgumentNull(nameof(filename), filename);
            }

            // Store the file name of the database.
            dbname = filename;

            // try to create connection.
            Context.Dispose();
        }

        #endregion



        #region Methods

        /// <summary>
        /// <para>Method to create a connection to the to the database.</para>
        /// <para>The database is ensure to be created on connection request.</para>
        /// </summary>
        /// <exception cref="InvalidOperationException">Exception thrown if database connection fail.</exception>
        private bool Connect()
        {
            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContextCore>();
                options.UseSqlite(GetConnectionString());
                context = new DatabaseContextCore(options.Options);
                return context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                string s = $"Cannot open database : {dbname}";
                log.Fatal(s);
                log.Fatal(e.Output(), e);

                throw;
            }
        }

        /// <summary>
        /// Method to connect to the database.
        /// </summary>
        private string GetConnectionString()
        {
            return "Data Source=" + dbname;
        }

        /// <summary>
        /// Method to register entities context manager.
        /// </summary>
        protected void RegisterContext()
        {
            AclActions = new AclActionManager(context);
            AclGroups = new AclGroupManager(context);
            Albums = new AlbumManager(context);
            Infos = new InfoManager(context);
            Pictures = new PictureManager(context);
            Sections = new SectionManager(context);
            Users = new UserManager(context);
            Versions = new VersionManager(context);
        }

        /// <summary>
        /// Method to populate database after EnsureCreated()
        /// </summary>
        private void PopulateDatabase()
        {
            log.Warn($"SQLite Server Database version : {ServerVersion()}");
            log.Warn($"SQLite Initializing Tables content. Please wait...");

            try
            {
                AclActions.InitializeTable();
                AclGroups.InitializeTable();
                Infos.InitializeTable();
                Versions.InitializeTable();

                log.Warn($"SQLite Initializing Tables content. Done.");
            }
            catch (Exception ex)
            {
                log.Error("SQLite Initializing Tables content. Exception.");
                log.Error(ex.Output(), ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ServerVersion()
        {
            return context.Database.GetDbConnection().ServerVersion;
        }

        #endregion



        #region IDisposable Support

        /// <summary>
        /// Variable to detect redundant calls.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Method to dispose object and its resources. 
        /// </summary>
        /// <param name="disposing">Dispose object resources ?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    if (context != null)
                    {
                        context.Dispose();
                        context = null;
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~DatabaseCore() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }


        /// <summary>
        /// Method to dispose object and its resources. 
        /// </summary>
        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);// TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.// GC.SuppressFinalize(this);
        }

        #endregion
    }
}