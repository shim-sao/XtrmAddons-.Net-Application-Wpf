using log4net.Repository;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Settings;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Helpers;

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
            CultureInfo ci = new CultureInfo(ApplicationBase.Language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            //Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //{
            //    Source = new Uri("/../XtrmAddons.Fotootof.Template;component/Generic.xaml", UriKind.RelativeOrAbsolute)
            //});

        }

        #endregion



        #region Methods

        /// <summary>
        /// <para>Method to initialize log4net debugger on application contructor.</para>
        /// <para>Log4net configuration must be placed on top of the constructor instructions.</para>
        /// </summary>
        private static void InitializeLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();

            #if !DEBUG

            // Disable using DEBUG mode in Release mode.
            foreach (ILoggerRepository repository in log4net.LogManager.GetAllRepositories())
            {
                repository.Threshold = log4net.Core.Level.Info;
            }

            #endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.TraceInformation((string)Translation.DLogs.StartingApplicationWaiting);

            // Start application base manager.
            ApplicationBase.Debug();

            // Initialize application preferences.
            InitializePreferences();

            // Initialize application options.
            InitializeOptions();

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
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.TraceInformation((string)Translation.DLogs.SavingApplicationWaiting);
            ApplicationBase.Save();
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// <para>Method to reset the application.</para>
        /// <para>Reset application : delete user my documents application folder.</para>
        /// </summary>
        private void App_Reset()
        {
            if (Reset && System.IO.Directory.Exists(DirectoryHelper.UserMyDocuments))
            {
                Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

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
        /// Method example of custom preferences settings adding.
        /// </summary>
        public void InitializePreferences()
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            // Add application storage directories.
            SettingsPreferences.InitializeStorage();

            // Copy program files to My Documents user folder.
            Trace.WriteLine((string)Translation.DLogs.CopyingProgramFiles);
            ApplicationBase.Directories.CopyConfigFiles(true);

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Method example of custom options settings adding.
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