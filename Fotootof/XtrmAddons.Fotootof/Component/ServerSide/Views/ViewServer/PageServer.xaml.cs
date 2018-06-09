using System;
using System.Globalization;
using System.Windows;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Windows.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Server.
    /// </summary>
    public partial class PageServer : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        /// <summary>
        /// Property to access to the page browser model.
        /// </summary>
        public PageServerModel Model { get; private set; }




        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Server Constructor.
        /// </summary>
        public PageServer()
        {
            AppOverwork.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageWaiting, "Server"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageDone, "Server"));
            AppOverwork.IsBusy = false;
        }



        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

            // Add Server envents handlers.
            InitializeServer();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void InitializeModel()
        {
            Model = new PageServerModel(this)
            {
                Server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeServer()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                OnServerStart();
            }
            else
            {
                OnServerStop();
            }

            HttpServerBase.AddStartHandlerOnce(OnServerStart);
            HttpServerBase.AddStopHandlerOnce(OnServerStop);
        }

        /// <summary>
        /// Method called on server start event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        public void OnServerStart(object sender = null, EventArgs e = null)
        {
            UCServer.MainContainer.Background = WPFColorHex.ColorToBrush("#01CED1");
            UCServer.RefreshServerMenu();
        }

        /// <summary>
        /// Method called on server stop event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        public void OnServerStop(object sender = null, EventArgs e = null)
        {
            UCServer.MainContainer.Background = WPFColorHex.ColorToBrush("#FE003E");
            UCServer.RefreshServerMenu();
        }



        #region Methods Size Changed

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e) { }

        #endregion



        #region Obsoletes

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        [Obsolete("Will be remove. None sense...", true)]
        public override void Page_Loaded_Async(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
