using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Settings;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlData;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof
{
    public class MainSettings
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Variables


        #region Methods

        /// <summary>
        /// Method to initialize the main custom settings of the application.
        /// </summary>
        public static void Initialize()
        {

            InitializeDatabase();
            InitializeServerAsync();
        }

        /// <summary>
        /// Method to initialize server application.
        /// </summary>
        public static async void InitializeServerAsync()
        {
            // Get default server in preferences.
            AppLogger.Info("Initializing HTTP server connection. Please wait...");
            Server server = null;

            // Try to start server.
            try
            {
                // Add API mapping to server.
                HttpMapping.Load(
                    Path.Combine(
                        ApplicationBase.Storage.Directories.FindKey("config.server").AbsolutePath,
                        "server-mapping.xml"
                    )
                );

                server = await SettingsOptions.InitializeServerAsync();
                HttpServerBase.AddNetworkAcl();
                HttpServerBase.Start();

                // Start the http server.
                // HttpWebServerApplication.Start(server.Host, server.Port);

                AppLogger.Info("Initializing server connection. Done !", true);
                AppLogger.Info("Server started : [" + server.Host + ":" + server.Port + "]", true);
            }

            // Catch server start exception.
            catch (Exception e)
            {
                AppLogger.Fatal("Server initialization failed : [" + server?.Host + ":" + server?.Port + "]", e, true);
                MessageBox.Show("Starting server : [" + server?.Host + ":" + server?.Port + "] failed !", Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            AppLogger.InfoAndClose("Initializing HTTP server connection. Done.");
        }

        /// <summary>
        /// Method to initialize application database.
        /// </summary>
        public static void InitializeDatabase()
        {
            // Get the default database in preferences if exists.
            AppLogger.Info("Initializing database connection. Please wait...");
            Database database = ApplicationBase.Options.Data.Databases.FindDefault();

            // Create default database parameters if not exists.
            if(database == null || database.Key == null)
            {
                database = new Database
                {
                    Key = "default",
                    Name = "default.s3db",
                    Type = DatabaseType.SQLite,
                    Source = Path.Combine(ApplicationBase.DataDirectory, "default.s3db"),
                    Default = true,
                    Comment = "Default SQLite installed database."

                };

                ApplicationBase.Options.Data.Databases.Add(database);
            }

            // Try to connect to the database.
            try
            {
                // Check if default database exists.
                if (File.Exists(database.Source))
                {
                    using (SQLiteConnection db = SQLiteManager.Instance(database.Source).Db)
                    {
                        log.Debug("Database connection ready.");
                        AppLogger.Info("Database connection ready.");
                    }
                }

                // Create new database from scheme.
                else
                {
                    using (
                        SQLiteConnection db =
                            SQLiteManager.Instance(
                                database: database.Source,
                                createFile: true,
                                scheme: Path.Combine(ApplicationBase.Storage.Directories.FindKey("config.database.scheme").AbsolutePath, "scheme.sqlite")
                            ).Db
                    )
                    {
                        log.Debug("New database connection ready.");
                        AppLogger.Info("New database connection ready.");
                    }
                }

                // Add connection to SQLite Service.
                SQLiteSvc.Db = new DatabaseCore(database.Source);

                // Add SQLite Service to the main window | application session for depencies..
                MainWindow.Database = new SQLiteSvc();
            }

            // Catch connection to the database exceptions.
            catch (Exception e)
            {
                AppLogger.Fatal("Connecting to the database failed !", e, true);
                MessageBox.Show("Connecting to the database failed !", Translation.DWords.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // End of database initilization.
            AppLogger.Info("Initializing database connection. Done !");
        }

        #endregion Methods
    }
}
