using System;
using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Windows.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Server.
    /// </summary>
    public partial class PageServer : PageBase
    {
        /// <summary>
        /// Property to access to the page browser model.
        /// </summary>
        public PageServerModel<PageServer> Model { get; private set; }




        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Server Constructor.
        /// </summary>
        public PageServer()
        {
            InitializeComponent();
            AfterInitializedComponent();
        }



        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContentAsync()
        {
            // Try to get server informations
            InitializeServer();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeServer()
        {
            Model = new PageServerModel<PageServer>(this);
            Model.Server = ApplicationBase.Options.Remote.Servers.FindDefault();
            DataContext = Model;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
