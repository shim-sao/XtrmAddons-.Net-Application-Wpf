using Fotootof.HttpServer;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.Services;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    /// Class XtrmAddons Fotootof Settings Preferences.
    /// </summary>
    public class SettingsOptions
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
        /// Method to initialize application database.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "Do not to fix it !", Scope = "Not supported by DLL")]
        public void InitializeDatabase()
        {
            log.Info(Local.Properties.Logs.DbInitializingConnection);

            // Get the default database in preferences if exists.
            Database database = ApplicationBase.Options.Data.Databases.FindDefaultFirst();

            // Create default database parameters if not exists.
            if (database == null || database.Key == null)
            {
                database = new Database
                {
                    Key = "default",
                    Name = "default.db3",
                    Type = DatabaseType.SQLite,
                    Source = Path.Combine(ApplicationBase.Directories.Data, "default.db3"),
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
                MessageBox.Show(Local.Properties.Logs.DbConnectionFail, Local.Properties.Translations.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, UpdateDatabaseDelegate());

            // Update the database.
            //UpdateDatabase();

            // End of database initilization.
            log.Info(Local.Properties.Logs.DbInitializingConnectionDone);
        }

        //public delegate void UpdateDatabaseDelegate(SQLiteConnection db, string updateFile);

        ///// <summary>
        ///// Method to update application database.
        ///// </summary>
        ///// <param name="db">The database to update.</param>
        //private void UpdateDatabase()
        //{
        //    log.Info("Updating database connection. Please wait...");

        //    using (SQLiteConnection db = SQLiteManager.Instance().Db)
        //    {
        //        if (db != null)
        //        {
        //            ObservableCollection<VersionEntity> databaseVersions = MainWindow.Database.Versions.List();
        //            string updateFile = "";

        //            if (databaseVersions == null || databaseVersions.Count == 0)
        //            {
        //                updateFile = Path.Combine(ApplicationBase.Storage.Directories.FindKey("data.database").AbsolutePath, "update.1.0.18123.2149.sqlite");
        //                Application.Current.Dispatcher.BeginInvoke(
        //                new UpdateDatabaseDelegate(SQLiteManagerUpdateDatabase), new object[] { db, updateFile });
        //                //WpfSQLiteData.RunFile(db, updateFile);
        //            }
        //        }
        //    }

        //    log.Info("Updating database connection. Done !");
        //}

        ///// <summary>
        ///// Method to update application database.
        ///// </summary>
        ///// <param name="db">The database to update.</param>
        //private void SQLiteManagerUpdateDatabase(SQLiteConnection db, string updateFile)
        //{
        //    WpfSQLiteData.RunFile(db, updateFile);
        //}

        /// <summary>
        /// Method to initialize server application.
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

        /// <summary>
        /// Method to add server mapping of DLL. 
        /// </summary>
        // Todo : write contract plugin for it.
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
    }
}