
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Settings;
using XtrmAddons.Net.Application;

namespace XtrmAddons.Fotootof
{
    public partial class App : System.Windows.Application
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// <para>Property to define if the application must be reset on start.</para>
        /// <para>Used as tool for quick development.</para>
        /// </summary>
        private bool Reset = false;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons.Fotootof Application Constructor.
        /// </summary>
        public App()
        {
            log4net.Config.XmlConfigurator.Configure();

            // Reset application : delete user my documents application folder.
            App_Reset();

            // Starting the application
            // The application loads options & parameters from files.
            // Create default files if not exists
            ApplicationBase.Start();
             
            // Initialize language.
            CultureInfo ci = new CultureInfo(ApplicationBase.Language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            // Startup the application.
            Startup += App_Startup;

            // Add automatic application saving before application closing.
            Exit += App_Exit;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.WriteLine((string)Translation.DLogs.StartingApplicationWaiting);
            ApplicationBase.Debug();
            InitializePreferencesAsync();
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

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
            mainWindow.Show();
        }

        /// <summary>
        /// Method called before the application closing.
        /// </summary>
        /// <param name="sender">The object sender of the event</param>
        /// <param name="e">Exit event arguments.</param>
        private void App_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.WriteLine((string)Translation.DLogs.SavingApplicationWaiting);
            ApplicationBase.Save();
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// <para>Method to reset the application.</para>
        /// <para>Reset application : delete user my documents application folder.</para>
        /// </summary>
        private void App_Reset()
        {
            if (Reset && System.IO.Directory.Exists(ApplicationBase.UserMyDocumentsDirectory))
            {
                Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

                Trace.WriteLine((string)Translation.DLogs.DeletingOptionsWaiting);
                System.IO.Directory.Delete(ApplicationBase.UserAppDataDirectory, true);
                Trace.WriteLine(string.Format((string)Translation.DLogs.ItemDeleted, ApplicationBase.UserAppDataDirectory));

                Trace.WriteLine((string)Translation.DLogs.DeletingUserOptionsWaiting);
                System.IO.Directory.Delete(ApplicationBase.UserMyDocumentsDirectory, true);
                Trace.WriteLine(string.Format((string)Translation.DLogs.ItemDeleted, ApplicationBase.UserMyDocumentsDirectory));

                Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Method example of custom preferences settings adding.
        /// </summary>
        public async void InitializePreferencesAsync()
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            // Add application storage directories.
            await SettingsPreferences.InitializeStorage();

            // Copy program files to My Documents user folder.
            Trace.WriteLine((string)Translation.DLogs.CopyingProgramFiles);
            await ApplicationBase.CopyConfigFiles(true);

            await SettingsOptions.InitializeDatabase();

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }
    }

    #endregion
}