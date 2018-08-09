using Fotootof.Layouts.Dialogs;
using System;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Libraries.HttpHelpers.HttpServer
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpServerBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public static object NetworkAclChecker { get; private set; }

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private static event EventHandler NotifyServerStartedHandler = delegate { };

        /// <summary>
        /// 
        /// </summary>
        private static event EventHandler NotifyServerStoppedHandler = delegate { };

        /// <summary>
        /// 
        /// </summary>
        private static event EventHandler NotifyServerFailedHandler = delegate { };

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected static void NotifyServerStarted()
        {
            NotifyServerStartedHandler?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        protected static void NotifyServerStopped()
        {
            NotifyServerStoppedHandler?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        protected static void NotifyServerFailed()
        {
            NotifyServerFailedHandler?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// Method to start the server.
        /// </summary>
        public static void Start()
        {
            // Check if the server is started.
            if (!HttpWebServerApplication.IsStarted)
            {
                try
                {
                    // Try to get server informations
                    ServerData server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();

                    if (server != null)
                    {
                        HttpWebServerApplication.Start(server.Host, server.Port);
                        log.Info(Translation.DLogs.ServerStarted);
                    }

                    NotifyServerStarted();
                }
                catch (Exception ex)
                {
                    MessageBoxs.Warning(ex.Output(), ex.GetType().Name);
                    log.Error(ex.Output(), ex);

                    NotifyServerFailed();
                }
            }
            else
            {
                log.Warn("Server is already started.");
            }
        }

        /// <summary>
        /// Method to stop the server.
        /// </summary>
        public static void Stop()
        {
            HttpWebServerApplication.Stop();
            log.Info(Translation.DLogs.ServerStopped);

            NotifyServerStopped();
        }

        /// <summary>
        /// Method called on add server to firewall click.
        /// </summary>
        public static void AddNetworkAcl()
        {
            log.Info("Enabling external server access. Please wait.");

            Task.Run(() =>
            {
                // Try to get server informations
                ServerData server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();

                if (server != null)
                {
                    XtrmAddons.Net.Network.NetworkAclChecker.NetshAddAddress(XtrmAddons.Net.Network.NetworkAclChecker.FormatURL(server.Host, server.Port));
                }
            });

            log.Info("Enabling external server access. Done.");
        }

        /// <summary>
        /// Method called on remove server from firwall click.
        /// </summary>
        public static void RemoveNetworkAcl()
        {
            log.Info("Disabling external server access. Please wait.");
            
            Task.Run(() =>
            {
                // Try to get server informations
                ServerData server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();

                if (server != null)
                {
                    XtrmAddons.Net.Network.NetworkAclChecker.NetshDeleteAddress(XtrmAddons.Net.Network.NetworkAclChecker.FormatURL(server.Host, server.Port));
                }
            });

            log.Info("Disabling external server access. Done.");
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler NotifyServerStartedHandlerOnce
        {
            add
            {
                lock(NotifyServerStartedHandler)
                {
                    NotifyServerStartedHandler -= value;
                    NotifyServerStartedHandler += value;
                }
            }
            remove
            {
                lock (NotifyServerStartedHandler)
                {
                    NotifyServerStartedHandler -= value;
                }

            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler NotifyServerStoppedHandlerOnce
        {
            add
            {
                lock(NotifyServerStartedHandler)
                {
                    NotifyServerStoppedHandler -= value;
                    NotifyServerStoppedHandler += value;
                }
            }
            remove
            {
                lock (NotifyServerStartedHandler)
                {
                    NotifyServerStoppedHandler -= value;
                }

            }
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler">Event handler.</param>
        [System.Obsolete("use : NotifyServerStartedHandlerOnce")]
        public static void AddStartHandlerOnce(EventHandler handler)
        {

            NotifyServerStartedHandler -= handler;
            NotifyServerStartedHandler += handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler">Event handler.</param>
        [System.Obsolete("use : NotifyServerStoppedHandler")]
        public static void AddStopHandlerOnce(EventHandler handler)
        {
            NotifyServerStoppedHandler -= handler;
            NotifyServerStoppedHandler += handler;
        }

        #endregion
    }
}
