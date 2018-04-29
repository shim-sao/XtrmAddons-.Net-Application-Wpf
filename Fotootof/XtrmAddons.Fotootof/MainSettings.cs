using System;
using System.IO;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Settings;
using XtrmAddons.Net.Application;
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

            //InitializeDatabase();
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

                server = await SettingsOptions.GetDefaultServerOptions();
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

        #endregion Methods
    }
}
