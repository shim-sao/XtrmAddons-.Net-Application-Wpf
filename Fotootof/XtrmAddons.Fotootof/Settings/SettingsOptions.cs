using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlData;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Network;

namespace XtrmAddons.Fotootof.Settings
{
    /// <summary>
    /// Class XtrmAddons Fotootof Settings Preferences.
    /// </summary>
    public static class SettingsOptions
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        /// <summary>
        /// Method to initialize server application.
        /// </summary>
        public static async Task<Server> InitializeServer()
        {            
            return await Task.Run(() =>
            {
                Server server = ApplicationBase.Options.Remote.Servers.FindDefault();

                // Create default server parameters if not exists.
                if (server == null || server.Key == null)
                {
                    server = new Server
                    {
                        Key = "default",
                        Name = "Default Server",
                        Default = true
                    };

                    ApplicationBase.Options.Remote.Servers.AddDefaultUnique(server);
                }


                // Initialize web server host or ip address.
                if (server.Host.IsNullOrWhiteSpace())
                {
                    server.Host = NetworkInformations.GetLocalHostIp();
                    ApplicationBase.Options.Remote.Servers.ReplaceKeyDefaultUnique(server);
                }

                // Initialize web server port.
                if (server.Port.IsNullOrWhiteSpace())
                {
                    server.Port = NetworkInformations.GetAvailablePort(9293).ToString();
                    ApplicationBase.Options.Remote.Servers.ReplaceKeyDefaultUnique(server);
                }

                server = ApplicationBase.Options.Remote.Servers.FindDefault();
                Trace.WriteLine("Server address : http://" + server.Host + ":" + server.Host);

                return server;
            });
        }

        /// <summary>
        /// Method to initialize application database.
        /// </summary>
        public static async Task<SQLiteSvc> InitializeDatabase()
        {
            return await Task.Run(() =>
            {
                // Get the default database in preferences if exists.
                log.Info("Initializing database connection. Please wait...");

                Database database = ApplicationBase.Options.Data.Databases.FindDefault();

                // Create default database parameters if not exists.
                if (database == null || database.Key == null)
                {
                    database = new Database
                    {
                        Key = "default",
                        Name = "default.db3",
                        Type = DatabaseType.SQLite,
                        Source = Path.Combine(ApplicationBase.DataDirectory, "default.db3"),
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
                            log.Info("Database connection ready.");
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
                            log.Info("New database connection ready.");
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
                    log.Fatal("Connecting to the database failed !", e);
                    MessageBox.Show("Connecting to the database failed !", Translation.DWords.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // End of database initilization.
                log.Info("Initializing database connection. Done !");

                return MainWindow.Database;
            });
        }

        /// <summary>
        /// Method to initialize server application.
        /// </summary>
        public static async void AutoStartServerAsync()
        {
            // Get default server in preferences.
            log.Info("Auto start HTTP server connection. Please wait...");

            Server server = await InitializeServer();

            if(!server.AutoStart)
            {
                log.Debug("Auto start HTTP server connection. Aborted !");
                log.Info("Auto start HTTP server connection. Done !");
                return;
            }

            // Try to start server.
            try
            {
                if(server != null)
                {
                    HttpServerBase.AddNetworkAcl();
                    HttpServerBase.Start();

                    log.Info("Server started : [" + server.Host + ":" + server.Port + "]");
                }
                else
                {
                    log.Error("Server preferences not found !");
                }
            }

            // Catch server start exception.
            catch (Exception e)
            {
                log.Error("Auto start HTTP server connection failed : [" + server?.Host + ":" + server?.Port + "]", e);
                MessageBox.Show("Starting server : [" + server?.Host + ":" + server?.Port + "] failed !", Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            log.Info("Auto start HTTP server connection. Done !");
        }

        /// <summary>
        /// Method to add server mapping of DLL. 
        /// </summary>
        public static void AddServerMap()
        {
            log.Info("Adding API mapping to server.");

            // Add API mapping to server.
            HttpMapping.Load(
                Path.Combine(
                    ApplicationBase.Storage.Directories.FindKey("config.server").AbsolutePath,
                    "server-mapping.xml"
                )
            );
        }
    }
}
