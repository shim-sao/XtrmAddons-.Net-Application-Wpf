using log4net.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Threading;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Settings;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Helpers;
using XtrmAddons.Net.Application.Serializable.Elements.Base;

/// <summary>
/// Globals Conditionals :
/// - DEBUG_SIZE => Use this variable to enable some size trace in DEBUG mode.
/// </summary>
namespace XtrmAddons.Fotootof
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Application.</para>
    /// <para>This class defines the entire application</para>
    /// </summary>
    public partial class App : Application
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// <para>Variable to define if the application must be reset on start.</para>
        /// <para>Used as tool for quick development.</para>
        /// </summary>
        private bool Reset = false;

        /// <summary>
        /// Variable application settings options initilizer. 
        /// </summary>
        SettingsOptions options = new SettingsOptions();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons.Fotootof Application Constructor.
        /// </summary>
        public App()
        {
            // Must be placed at the top start of the application.
            InitializeLog4Net();

            // Reset application
            // delete user my documents application folder.
            App_Reset();

            // Starting the application
            // The application loads options & parameters from files.
            // Create default files if not exists
            ApplicationBase.Start();

            // Initialize language.
            // Must be placed in the top start of the application.
            Trace.TraceInformation((string)Translation.DLogs.AppIntLanguage);
            CultureInfo ci = new CultureInfo(ApplicationBase.Language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            
            /*ResourceDictionary rd = new ResourceDictionary
            {
                Source = new Uri("XtrmAddons.Fotootof.Template;component/Theme/Dark.xaml", UriKind.Relative)
            };
            Current.Resources.MergedDictionaries.Add(rd);*/
        }

        #endregion



        #region Methods

        /// <summary>
        /// <para>Method to initialize log4net debugger on application contructor.</para>
        /// <para>Log4net configuration must be placed on top of the constructor instructions.</para>
        /// <para><see href="https://logging.apache.org/log4net/release/manual/introduction.html"/></para>
        /// </summary>
        private static void InitializeLog4Net()
        {
            Trace.TraceInformation("Initializing Log4Net Configurator. Please wait... ");
            log4net.Config.XmlConfigurator.Configure();

#if DEBUG
            // Trace Log4net repositories for debug.
            int i = 0;
            foreach (log4net.Repository.ILoggerRepository repository in log4net.LogManager.GetAllRepositories())
            {

                Trace.TraceInformation($"{i} # Repository name : {repository.Name}");
                Trace.TraceInformation($"{i} # Repository is configured : {repository.Configured}");
                Trace.TraceInformation($"{i} # Repository levels map count : {repository.LevelMap.AllLevels.Count}");
                Trace.TraceInformation($"{i} # Repository threshold : {repository.Threshold}");

                Trace.TraceInformation($"{i} # Repository configuration messages : {repository.ConfigurationMessages.Count}");
                int j = 0;
                foreach (log4net.Util.LogLog message in repository.ConfigurationMessages)
                {
                    Trace.TraceInformation($"{i} # Repository message ({j}) : {message.Message}");
                    j++;
                }

                Trace.TraceInformation($"{i} # Repository plugin map : {repository.PluginMap.AllPlugins.Count}");
                j = 0;
                foreach (log4net.Plugin.PluginCollection plgCollec in repository.PluginMap.AllPlugins)
                {
                    int k = 0;
                    foreach (log4net.Plugin.PluginSkeleton plg in plgCollec)
                    {
                        Trace.TraceInformation($"{i} # Repository plugin ({j}:{k}) : {plg.Name}");
                        k++;
                    }
                    j++;
                }

                i++;
                repository.Threshold = log4net.Core.Level.All;
            }
#else
            // Disable logs under Info on release mode.
            foreach (log4net.Repository.ILoggerRepository repository in log4net.LogManager.GetAllRepositories())
            {
                repository.Threshold = log4net.Core.Level.Info;
            }
#endif
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Method called at the start up of the application.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Strart up event arguments.</param>
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Trace.TraceInformation((string)Translation.DLogs.AppStartWaiting);

            // Start application base manager.
            ApplicationBase.Debug();

            // Initialize application preferences.
            InitializePreferences();

            // Initialize application options.
            InitializeOptions();

            // Add Theme to Application UI Parmeters.
            //ApplicationBase.UI.AddParameter("ApplicationTheme", "Dark");

            // Application is running
            // Process command line args
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }

            string ws = ApplicationBase.UI.AddParameter("WindowState", "Maximized");
            switch (ws)
            {
                case "Maximized":
                    mainWindow.WindowState = WindowState.Maximized;
                    break;

                case "Minimized":
                    mainWindow.WindowState = WindowState.Minimized;
                    break;

                case "Normal":
                    mainWindow.WindowState = WindowState.Normal;
                    break;
            }
            mainWindow.Show();
        }

        /// <summary>
        /// Method called before the application closing.
        /// </summary>
        /// <param name="sender">The object sender of the event</param>
        /// <param name="e">Exit event arguments.</param>
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.TraceInformation((string)Translation.DLogs.AppSaveWaiting);
            ApplicationBase.Save();
            Trace.TraceInformation((string)Translation.DLogs.AppClosed);
        }

        /// <summary>
        /// <para>Method to reset the application.</para>
        /// <para>Reset application : delete user my documents application folder.</para>
        /// </summary>
        private void App_Reset()
        {
            if (Reset && System.IO.Directory.Exists(DirectoryHelper.UserMyDocuments))
            {
                Trace.TraceInformation((string)Translation.DLogs.DeletingOptionsWaiting);
                System.IO.Directory.Delete(DirectoryHelper.UserAppData, true);
                Trace.TraceInformation(string.Format(CultureInfo.CurrentCulture, (string)Translation.DLogs.ItemDeleted, DirectoryHelper.UserAppData));

                Trace.TraceInformation((string)Translation.DLogs.DeletingUserOptionsWaiting);
                System.IO.Directory.Delete(DirectoryHelper.UserMyDocuments, true);
                Trace.TraceInformation(string.Format(CultureInfo.CurrentCulture, (string)Translation.DLogs.ItemDeleted, DirectoryHelper.UserMyDocuments));

                Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Method to initialize application preferences before startup.
        /// </summary>
        public void InitializePreferences()
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            // Add application storage directories.
            SettingsPreferences.InitializeStorage();

            /*
            // Copy program files to My Documents user folder.
            Trace.WriteLine((string)Translation.DLogs.CopyingProgramFiles);
            ApplicationBase.Directories.CopyConfigFiles(true);
            */

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Method to initialize application options before startup.
        /// </summary>
        public void InitializeOptions()
        {
            Trace.WriteLine("Initializing Options ----------------------------------------------------------------------------------");
            
            options.InitializeDatabase();
            options.InitializeServer();
            options.AddServerMap();
            options.AutoStartServer();

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }
    }

    #endregion
}