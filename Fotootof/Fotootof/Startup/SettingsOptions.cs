using Fotootof.HttpServer;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Data;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Network;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Startup
{
    /// <summary>
    /// Class XtrmAddons Fotootof Startup Settings Options.
    /// </summary>
    public class SettingsOptions
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize application Server.
        /// </summary>
        public ServerData InitializeServer()
        {
            ServerData server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();

            // Create default server parameters if not exists.
            if (server == null || server.Key == null)
            {
                server = new ServerData
                {
                    Key = "default",
                    Name = "Default Server",
                    IsDefault = true
                };

                ApplicationBase.Options.Remote.Servers.AddDefaultSingle(server);
            }


            // Initialize web server host or ip address.
            if (server.Host.IsNullOrWhiteSpace())
            {
                server.Host = NetworkInformations.GetLocalHostIp();
                ApplicationBase.Options.Remote.Servers.AddKeySingle(server);
            }

            // Initialize web server port.
            if (server.Port.IsNullOrWhiteSpace())
            {
                server.Port = NetworkInformations.GetAvailablePort(9293).ToString();
                ApplicationBase.Options.Remote.Servers.AddKeySingle(server);
            }

            server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();
            Trace.TraceInformation($"{Local.Properties.Logs.ServerAddress} : http://{server.Host}:{server.Host}");

            return server;
        }

        /// <summary>
        /// Method to initialize application Database.
        /// </summary>
        /// [SuppressMessage("Microsoft.Security", "CA2100", Justification = "Do not to fix it !", Scope = "Not supported by DLL")]
        public void InitializeDatabase()
        {
            log.Info(Local.Properties.Logs.DbInitializingConnection);

            // Get the default database in preferences if exists.
            Database database = ApplicationBase.Options.Data.Databases.FindDefaultFirst();

#if DEBUG
            string dbName = "default.debug.db3";
#else
            string dbName = "default.db3";
#endif

            // Create default database parameters if not exists.
            if (database == null || database.Key == null)
            {
                database = new Database
                {
                    Key = "default",
                    Name = "default.db3",
                    Type = DatabaseType.SQLite,
                    Source = Path.Combine(ApplicationBase.Directories.Data, dbName),
                    IsDefault = true,
                    Comment = "Default SQLite installed database."
                };

                log.Info(Local.Properties.Logs.DbAddingDefault);
                ApplicationBase.Options.Data.Databases.Add(database);
            }

            // Try to connect to the database.
            try
            {
                // Check if default database exists, if not... do nothing
                // New database is auto created by EnsureCreated()
                // Check for the database updates.
                if (File.Exists(database.Source))
                {
                    log.Info(Local.Properties.Logs.DbFileFound);
                    log.Info(Local.Properties.Logs.DbConnectionReady);
                }

                // ... create new database from scheme.
                else
                {
                    log.Info(Local.Properties.Logs.DbFileNotFound);
                }

                // Add connection to SQLite Service.
                SQLiteSvc.Db = new DatabaseCore(database.Source);

                // Add SQLite Service to the main window | application session for dependencies..
                MainWindow.Database = SQLiteSvc.GetInstance();
            }

            // Catch connection to the database exceptions.
            catch (Exception e)
            {
                log.Fatal(Local.Properties.Logs.DbConnectionFail, e);
                MessageBox.Show
                (
                    Local.Properties.Logs.DbConnectionFail,
                    Local.Properties.Translations.ApplicationName,
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
            }

            // End of database initilization.
            log.Info(Local.Properties.Logs.DbInitializingConnectionDone);
        }

        /// <summary>
        /// Method to auto start the application Server.
        /// </summary>
        public void AutoStartServer()
        {
            // Get default server in preferences.
            log.Info("Auto start HTTP server connection. Please wait...");

            ServerData server = InitializeServer();

            // Try to start server.
            try
            {

                if (server != null)
                {
                    if (!server.AutoStart)
                    {
                        log.Debug("Auto start HTTP server connection. Aborted !");
                        log.Info("Auto start HTTP server connection. Done !");
                        return;
                    }

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
                MessageBox.Show("Starting server : [" + server?.Host + ":" + server?.Port + "] failed !", Local.Properties.Translations.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            log.Info("Auto start HTTP server connection. Done !");
        }

        #endregion



        #region Obsoletes

        /// <summary>
        /// Method to add server mapping of DLL. 
        /// </summary>
        // Todo : write contract plugin for it.
        [System.Obsolete("Do not use anymore.")]
        public void AddServerMap()
        {
            log.Info("Adding API mapping to server.");

            // Add API mapping to server.
            HttpMapping.Load(
                Path.Combine(
                    ApplicationBase.Storage.Directories.FindKeyFirst("data.server").AbsolutePath,
                    "server-mapping.xml"
                )
            );
        }

        #endregion
    }
}