using Fotootof.HttpServer;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using System;
using System.Globalization;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Windows.Tools;

namespace Fotootof.Components.Server.Remote
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Server.
    /// </summary>
    public partial class PageRemoteLayout : ComponentView
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
        public PageRemoteModel Model { get; private set; }




        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Server Constructor.
        /// </summary>
        public PageRemoteLayout()
        {
            MessageBoxs.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageWaiting, "Server Server"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageDone, "Server Server"));
            MessageBoxs.IsBusy = false;
        }



        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
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
            Model = new PageRemoteModel(this)
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

            HttpServerBase.NotifyServerStartedHandlerOnce += OnServerStart;
            HttpServerBase.NotifyServerStoppedHandlerOnce += OnServerStop;
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
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e) { }

        #endregion
    }
}
