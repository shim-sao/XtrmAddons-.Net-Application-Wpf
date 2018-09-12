using Fotootof.Libraries.Controls;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using System.Windows;
using Fotootof.HttpServer;
using XtrmAddons.Net.Application;

namespace Fotootof.Components.Server.Remote.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Server Infos User Control.
    /// </summary>
    public partial class ServerInfosLayout : ControlLayout
    {
        #region Properties

        /// <summary>
        /// Accessors to Window AclGroup Form model.
        /// </summary>
        public ServerInfosModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control Server Server Infos User Control Constructor.
        /// </summary>
        public ServerInfosLayout()
        {
            InitializeComponent();

            Model = new ServerInfosModel(this);
            DataContext = Model;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to refresh custom control data. 
        /// </summary>
        public void RefreshServerData()
        {
            // Try to get server informations
            Model.Server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();

            if (Model.server != null)
            {
                InputHost.Text = Model.server.Host;
                InputPort.Text = Model.server.Port;
            }

            RefreshServerMenu();
        }

        /// <summary>
        /// Method to refresh custom control data. 
        /// </summary>
        public void RefreshServerMenu()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                Button_Start.IsEnabled = false;
                Button_Stop.IsEnabled = true;
                Button_Restart.IsEnabled = true;
            }
            else
            {
                Button_Start.IsEnabled = true;
                Button_Stop.IsEnabled = false;
                Button_Restart.IsEnabled = false;
            }
        }

        /// <summary>
        /// Method called on server start click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerStart_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Start();
        }

        /// <summary>
        /// Method called on server stop click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerStop_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Stop();
        }

        /// <summary>
        /// Method called on server restart click.
        /// </summary>
        /// <param name="sender">The <see cref="DataObject"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnServerRestart_Click(object sender, RoutedEventArgs e)
        {
            //Overwork.IsBusy = true;
            OnServerStop_Click(sender, e);
            OnServerStart_Click(sender, e);
            //Overwork.IsBusy = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="DataObject"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }

    #endregion
}
