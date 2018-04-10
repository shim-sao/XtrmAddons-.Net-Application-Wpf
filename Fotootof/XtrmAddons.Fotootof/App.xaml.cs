
using System.Diagnostics;
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
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.WriteLine("Starting the application options & parameters. Please wait...");
            ApplicationBase.Start();
            ApplicationBase.Debug();
            InitializePreferencesAsync();
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            // Add automatic application saving before application closing.
            Exit += App_Exit;
        }

        #endregion



        #region Methods

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

                Trace.WriteLine("Deleting the application options & parameters. Please wait...");
                System.IO.Directory.Delete(ApplicationBase.UserAppDataDirectory, true);
                Trace.WriteLine(ApplicationBase.UserAppDataDirectory + " deleted !");

                Trace.WriteLine("Deleting the user options & parameters. Please wait...");
                System.IO.Directory.Delete(ApplicationBase.UserMyDocumentsDirectory, true);
                Trace.WriteLine(ApplicationBase.UserMyDocumentsDirectory + " deleted !");

                Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Method example of custom preferences settings adding.
        /// </summary>
        public async void InitializePreferencesAsync()
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            // Initialize language.
            SettingsPreferences.InitializeLanguage();

            // Add application storage directories.
            await SettingsPreferences.InitializeStorage();

            // Copy program files to My Documents user folder.
            Trace.WriteLine("Copy program files to My Documents user folder. Please wait...");
            await ApplicationBase.CopyConfigFiles(true);

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }
    }

    #endregion
}