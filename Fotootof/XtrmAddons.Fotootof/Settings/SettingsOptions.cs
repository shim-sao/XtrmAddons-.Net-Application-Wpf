using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Threading;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlData;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Network;
using XtrmAddons.Net.SQLiteBundle;

namespace XtrmAddons.Fotootof.Settings
{
    /// <summary>
    /// Class XtrmAddons Fotootof Settings Preferences.
    /// </summary>
    public class SettingsOptionsInitializer
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
        public Server InitializeServer()
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
        }

        /// <summary>
        /// Method to initialize application database.
        /// </summary>
        public void InitializeDatabase()
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
                        log.Info("Database already exists. Connection ready.");

                        Version current = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                        Version old = new Version("1.0.18123.2149");

                        log.Info(string.Format(CultureInfo.InvariantCulture, "{0} ({1})", old, current));

                        if (current < old)
                        {
                            log.Info("Updating database 1.0.18123.0000 to 1.0.18123.2149");

                            string query = File.ReadAllText(Path.Combine(ApplicationBase.Storage.Directories.FindKey("config.database.scheme").AbsolutePath, "update.1.0.18123.2149.sqlite"));
                            using (TransactionScope tran = new TransactionScope())
                            {
                                SQLiteCommand command = db.CreateCommand();
                                command.CommandText = @query;
                                command.ExecuteNonQuery();
                            }
                        }
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

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, UpdateDatabaseDelegate());

            // Update the database.
            //UpdateDatabase();

            // End of database initilization.
            log.Info("Initializing database connection. Done !");
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
        //                updateFile = Path.Combine(ApplicationBase.Storage.Directories.FindKey("config.database.scheme").AbsolutePath, "update.1.0.18123.2149.sqlite");
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

            Server server = InitializeServer();

            // Try to start server.
            try
            {

                if(server != null)
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
                MessageBox.Show("Starting server : [" + server?.Host + ":" + server?.Port + "] failed !", Translation.DWords.Application, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            log.Info("Auto start HTTP server connection. Done !");
        }

        /// <summary>
        /// Method to add server mapping of DLL. 
        /// </summary>
        public void AddServerMap()
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
