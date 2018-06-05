using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewBrowser;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewLogs;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems.Log4net;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.NotifyIcons;

namespace XtrmAddons.Fotootof
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Main Window.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable memory log watcher.
        /// </summary>
        private static readonly MemoryLogWatcher logWatcher = new MemoryLogWatcher();

        /// <summary>
        /// Variable logs page.
        /// </summary>
        private static readonly PageLogs pageLogs = new PageLogs();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the logs page.
        /// </summary>
        public PageLogs BlockLogs { get; } = pageLogs;

        /// <summary>
        /// Property alias to access to the text block container of logs stack.
        /// </summary>
        public Border BlockContent => Block_Content;

        /// <summary>
        /// Property to access to the SQLite Service.
        /// </summary>
        public static SQLiteSvc Database
        {
            get => ApplicationSession.Properties.Database;
            set => ApplicationSession.Properties.Database = value;
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Main Window Constructor.
        /// </summary>
        public MainWindow()
        {
            // Merge application culture translation resources.
            Resources.MergedDictionaries.Add(Culture.Translation.Words);
            Resources.MergedDictionaries.Add(Culture.Translation.Logs);

            //var a = Properties;


            // Initialize window component.
            InitializeComponent();

            //log.Info(Translation.DLogs.InitializingApplicationWindowComponentDone);

            // Main Window to application session.
            ApplicationSession.Properties.MainWindow = this;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Method called on window load event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppOverwork.IsBusy = true;
            await Task.Delay(10);

            // Add application to system tray.
            NotifyIconManager.AddToTray();
            log.Info(Translation.DLogs.AddingApplicationToSystemTrayDone);

            // Add application log watcher event handler.
            AppLogger.UpdateLogTextbox(logWatcher.LogContent);
            logWatcher.LogContent = "";
            logWatcher.Updated += LogWatcher_Updated;

            // Initialize window content.
            await InitializeContentAsync();

            AppOverwork.IsBusy = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public void LogWatcher_Updated(object sender, EventArgs e)
        {
            AppLogger.UpdateLogTextbox(logWatcher.LogContent);
            logWatcher.LogContent = "";
        }

        /// <summary>
        /// Method to initialize application content.
        /// </summary>
        private async Task InitializeContentAsync()
        {
            await Task.Delay(10);

            // Assigned page frames.
            Frame_Logs.Navigate(BlockLogs);
            Frame_Content.Navigate(new PageBrowser());

            // Initialize items of Server Menu.
            AppMainMenu.InitializeMenuItemsServer();

            // Adjust frame logs content on resize. 
            SizeChanged += BlockLogs.Page_SizeChanged;
        }

        /// <summary>
        /// Method called on windows closing event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            NotifyIconManager.Dispose();
            log.Info(Translation.DLogs.ApplicationClosed);
        }

        /// <summary>
        /// Method to toggle logs window.
        /// </summary>
        public void LogsToggle()
        {
            // Set the row grid splitter Height.
            RowGridSplitter.Height =
                RowGridSplitter.Height == new GridLength(0)
                ? new GridLength(5) : new GridLength(0);

            // Set the grid row logs height.
            RowGridLogs.Height =
                RowGridLogs.Height == new GridLength(0)
                ? new GridLength(250) : new GridLength(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            #if DEBUG_SIZE

            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.WriteLine("MainWindow.ActualSize = [" + ActualWidth + "," + ActualHeight + "]");
            Trace.WriteLine("Block_Content.ActualSize = [" + Block_Content.ActualWidth + "," + Block_Content.ActualHeight + "]");
            Trace.WriteLine("Frame_Content.ActualSize = [" + Frame_Content.ActualWidth + "," + Frame_Content.ActualHeight + "]");
            Trace.WriteLine("RowGridMain.Height = [" + RowGridMain.Height + "]");
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            #endif
        }

        #endregion
    }
}