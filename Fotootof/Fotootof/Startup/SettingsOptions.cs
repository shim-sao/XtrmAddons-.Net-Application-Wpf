using Fotootof.HttpServer;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Helpers;
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

        /// <summary>
        /// 
        /// </summary>
        private static readonly Regex _ipAddress = new Regex(@"\b([0-9]{1,3}\.){3}[0-9]{1,3}$",
                                                           RegexOptions.Compiled | RegexOptions.ExplicitCapture);

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

            /*NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine(adapter.Description);
                Console.WriteLine(adapter.Id);
                Console.WriteLine(adapter.GetPhysicalAddress().ToString());

                // NetworkInformations.ShowIPAddresses(properties);

                Console.WriteLine("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);
                Console.WriteLine("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);

                server.Host = UnicastIPAddress(properties);

                if(server.Host != "0.0.0.0")
                {
                    break;
                }
            }*/

            server.Host = NetworkInformations.GetLocalNetworkIp(false);

            Trace.TraceInformation($"{Local.Properties.Logs.ServerAddress} : http://{server.Host}:{server.Port}");
            
            // Initialize web server host or ip address.
            if (server.Host.IsNullOrWhiteSpace())
            {
                server.Host = NetworkInformations.GetLocalNetworkIp(false);
                ApplicationBase.Options.Remote.Servers.AddKeySingle(server);
            }

            // Initialize web server port.
            if (server.Port.IsNullOrWhiteSpace())
            {
                server.Port = $"{NetworkInformations.GetAvailablePort(9293)}";
                ApplicationBase.Options.Remote.Servers.AddKeySingle(server);
            }

            server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();
            Trace.TraceInformation($"{Local.Properties.Logs.ServerAddress} : http://{server.Host}:{server.Port}");

            return server;
        }

        /// <summary>
        /// Method to initialize application Database.
        /// </summary>
        /// [SuppressMessage("Microsoft.Security", "CA2100", Justification = "Do not to fix it !", Scope = "Not supported by DLL")]
        public void InitializeDatabase()
        {
            log.Info(Local.Properties.Logs.DbInitializingConnection);

#if DEBUG
            string dbName = "default.debug.db3";
#else
            string dbName = "default.db3";
#endif

            Database database = GetDatabaseSettings(dbName);

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
            }

            // Catch connection to the database exceptions.
            catch (Exception e)
            {
                log.Fatal(Local.Properties.Logs.DbConnectionFail, e);
                MessageBox.Show
                (
                    Local.Properties.Logs.DbConnectionFail,
                    caption: Local.Properties.Translations.ApplicationName,
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error
                );
            }

            // End of database initilization.
            log.Info(Local.Properties.Logs.DbInitializingConnectionDone);
        }

        /// <summary>
        /// Method to get the database settings.
        /// Check if the file exists or create default db3 if t exists.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private static Database GetDatabaseSettings(string dbName)
        {
            // Get the default database in preferences if exists.
            Database database = ApplicationBase.Options.Data.Databases.FindDefaultFirst();

            log.Info($"database.Key = {database?.Key}");
            log.Info($"database.Source = {database?.Source}");

            string folder = "";

            if (Directory.Exists(ApplicationBase.Directories.Data))
            {
                folder = ApplicationBase.Directories.Data;
            }

            else
            {
                folder = Path.Combine(DirectoryHelper.UserMyDocuments, "Data");
            }

            log.Info($"folder = {folder}");

            // Create default database parameters if not exists.
            if (database == null || database.Key == null)
            {

                database = new Database
                {
                    Key = "default",
                    Name = dbName,
                    Type = DatabaseType.SQLite,
                    Source = Path.Combine(folder, dbName),
                    IsDefault = true,
                    Comment = "Default SQLite installed database."
                };

                log.Info(Local.Properties.Logs.DbAddingDefault);
                ApplicationBase.Options.Data.Databases.Add(database);
            }

            // Decide what to do.
            else
            {
                // Check if database file exists, if not... do nothing
                if (!File.Exists(database.Source))
                {
                    log.Info(Local.Properties.Logs.DbFileNotFound);

                    database.Source = Path.Combine(DirectoryHelper.UserMyDocuments, "Data", dbName);
                }
            }
            var a = database;

            log.Info($"database.Source = {database?.Source}");

            return database;
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
    }
}