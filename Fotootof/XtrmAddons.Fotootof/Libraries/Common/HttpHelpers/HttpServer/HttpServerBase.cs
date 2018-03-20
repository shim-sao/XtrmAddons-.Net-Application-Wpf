using System;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Network;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.XmlRemote.Server;

namespace XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpServerBase
    {
        #region Properties

        private static event EventHandler OnServerStart = delegate { };

        private static event EventHandler OnServerStop = delegate { };

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected static void RaiseServerStart()
        {
            OnServerStart?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        protected static void RaiseServerStop()
        {
            OnServerStop?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// Method to start the server.
        /// </summary>
        public static void Start()
        {
            // Check if the server is started.
            if (!HttpWebServerApplication.IsStarted)
            {
                // Try to get server informations
                ServerData server = ApplicationBase.Options.Remote.Servers.FindDefault();

                if (server != null)
                {
                    HttpWebServerApplication.Start(server.Host, server.Port);
                    Logger.InfoAndClose(Logger.Translate("ServerStarted"), true);
                }

                RaiseServerStart();
            }
            else
            {
                Logger.Info("Server already started.");
            }
        }

        /// <summary>
        /// Method to stop the server.
        /// </summary>
        public static void Stop()
        {
            HttpWebServerApplication.Stop();
            Logger.InfoAndClose(Logger.Translate("ServerStarted"), true);

            RaiseServerStop();
        }

        /// <summary>
        /// Method called on add server to firewall click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        public static void AddNetworkAcl()
        {
            Logger.Info("Enabling external server access. Please wait.");

            Task.Run(() =>
            {
                // Try to get server informations
                ServerData server = ApplicationBase.Options.Remote.Servers.FindDefault();

                if (server != null)
                {
                    NetworkAclChecker.NetshAddAddress(NetworkAclChecker.FormatURL(server.Host, server.Port));
                }
            });

            Logger.InfoAndClose("Enabling external server access. Done.");
        }

        /// <summary>
        /// Method called on remove server from firwall click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        public static void RemoveNetworkAcl()
        {
            Logger.Info("Disabling external server access. Please wait.");
            
            Task.Run(() =>
            {
                // Try to get server informations
                ServerData server = ApplicationBase.Options.Remote.Servers.FindDefault();

                if (server != null)
                {
                    NetworkAclChecker.NetshDeleteAddress(NetworkAclChecker.FormatURL(server.Host, server.Port));
                }
            });

            Logger.Info("Disabling external server access. Done.");
        }


        public static void AddStartHandlerOnce(EventHandler handler)
        {

            OnServerStart -= handler;
            OnServerStart += handler;
        }

        public static void AddStopHandlerOnce(EventHandler handler)
        {
            OnServerStop -= handler;
            OnServerStop += handler;
        }

        #endregion
    }
}
