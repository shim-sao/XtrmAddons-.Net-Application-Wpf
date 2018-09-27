using Fotootof.HttpClient;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.HttpHelpers.HttpClient.Responses;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Libraries.HttpHelpers.HttpClient
{
    /// <summary>
    /// Class Fotootof Libraries Http Helpers Client.
    /// </summary>
    public class ClientHttp
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Http Helpers Client Constructor.
        /// </summary>
        /// <param name="server">A <see cref="Client"/> connector.</param>
        public ClientHttp(Client server)
        {
            Server = server;
            InitializeWebClient();
        }

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Client Server { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public HttpWebClientApplication WebClient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Sender"></param>
        public delegate void OnAuthenticationSuccessEventHandler<T>(object Sender);

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnAuthenticationSuccess = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnAuthenticationFailed = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnAuthenticationUnauthorized = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnListSectionsSuccess = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnListSectionsFailed = delegate { };
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnSingleSectionSuccess = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnSingleSectionFailed = delegate { };
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnSingleAlbumSuccess = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnSingleAlbumFailed = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnPingSuccess = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnPingFailed = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnServerStop = delegate { };

        #endregion




        /// <summary>
        /// Method to raise authentication success event.
        /// </summary>
        protected void RaiseAuthenticationSuccess<T>(Client server, T response = null) where T : class
        {
            OnAuthenticationSuccess?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication failed event.
        /// </summary>
        protected void RaiseAuthenticationFailed<T>(Client server, T response = null) where T : class
        {
            OnAuthenticationFailed?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication unauthorized event.
        /// </summary>
        protected void RaiseAuthenticationUnauthorized<T>(Client server, T response = null) where T : class
        {
            OnAuthenticationUnauthorized?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication success event.
        /// </summary>
        protected void RaiseListSectionsSuccess<T>(Client server, T response = null) where T : class
        {
            OnListSectionsSuccess?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication failed event.
        /// </summary>
        protected void RaiseListSectionsFailed<T>(Client server, T response = null) where T : class
        {
            OnListSectionsFailed?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication success event.
        /// </summary>
        protected void RaiseSingleSectionSuccess<T>(Client server, T response = null) where T : class
        {
            OnSingleSectionSuccess?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication failed event.
        /// </summary>
        protected void RaiseSingleSectionFailed<T>(Client server, T response = null) where T : class
        {
            OnSingleSectionFailed?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise single Album success event.
        /// </summary>
        protected void RaiseSingleAlbumSuccess<T>(Client server, T response = null) where T : class
        {
            OnSingleAlbumSuccess?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// Method to raise authentication failed event.
        /// </summary>
        protected void RaiseSingleAlbumFailed<T>(Client server, T response = null) where T : class
        {
            OnSingleAlbumFailed?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// 
        /// </summary>
        protected void RaisePingSuccess<T>(Client server, T response = null) where T : class
        {
            OnPingSuccess?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }

        /// <summary>
        /// 
        /// </summary>
        protected void RaisePingFailed<T>(Client server, T response = null) where T : class
        {
            OnPingFailed?.Invoke(this, new ClientHttpEventArgs<T>(server, response));
        }


        /// <summary>
        /// Method to ping a server.
        /// </summary>
        public void InitializeWebClient()
        {
            log.Info("Initializing server connection.");

            try
            {
                if(WebClient == null)
                {
                    WebClient = new HttpWebClientApplication(
                        Server.Host,
                        Server.Port,
                        Server.Email,
                        Server.Password,
                        Server.UserName
                    );
                }
            }
            catch (Exception e)
            {
                log.Error("Initializing server connection failed.", e);
                MessageBoxs.Fatal(e, "Initializing server connection failed.");
            }
        }

        /// <summary>
        /// <para>Method to send ping command to a server asynchronously.</para>
        /// <para>Command to check if the server is running.</para>
        /// </summary>
        public async Task Ping()
        {
            log.Info(string.Format(Properties.Logs.SendingClientCommand, MethodBase.GetCurrentMethod().Name, Server.Host, Server.Port));

            // Initialize server response.
            ServerResponse serverResponse = null;

            // Try to check if the server is running.
            try
            {
                // Send command Ping to the server.
                // Decode response as JSon format to exploit.
                HttpResponseMessage response = WebClient.Client.Ping();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponse>(message);

                // Raise ping success event on HTTP OK response.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    log.Info(Properties.Logs.SendingPingCommandResponseOk);
                    RaisePingSuccess(Server, serverResponse);
                }

                // Raise ping failed event on others HTTP response.
                // Todo : Adapt all http responses
                else
                {
                    log.Error(Properties.Logs.SendingPingCommandResponseFailed);
                    log.Error(response.StatusCode.ToString() + " : " + serverResponse.Error);

                    RaisePingFailed(Server, serverResponse);
                }
            }

            // Catch all exceptions.
            catch (Exception e)
            {
                log.Error(Properties.Logs.SendingPingCommandException);
                log.Fatal(e);

                RaisePingFailed(Server, serverResponse);
            }

            log.Info(Properties.Logs.SendingPingCommandDone);
        }


        /// <summary>
        /// <para>Method to send authentication command to a server asynchronously.</para>
        /// <para>Command to authenticate a user on a server.</para>
        /// </summary>
        public async Task<bool> Authentication()
        {
            log.Info(string.Format(Properties.Logs.SendingClientCommand, MethodBase.GetCurrentMethod().Name, Server.Host, Server.Port));

            // Initialize server response.
            ServerResponse serverResponse = null;

            // Try to check if the user can access to server content.
            try
            {
                // Send command Authentication to the server.
                // Decode response as JSon format to exploit.
                HttpResponseMessage response = WebClient.Client.Authentication();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponse>(message);

                // Raise authentication success event on HTTP OK response.
                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {
                    log.Debug(Properties.Logs.SendingAuthenticationCommandResponseOk);
                    RaiseAuthenticationSuccess(Server, serverResponse);
                }

                // Raise authentication unauthorized event unauthorized HTTP response.
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    log.Debug(Properties.Logs.SendingAuthenticationCommandResponseUnauthorized);
                    RaiseAuthenticationUnauthorized(Server, serverResponse);
                }

                // Raise ping failed event on others HTTP response.
                // Todo : Adapt all http responses
                else
                {
                    log.Error(Properties.Logs.SendingAuthenticationCommandResponseFailed);
                    log.Error(response.StatusCode.ToString() + " : " + serverResponse.Error);

                    RaiseAuthenticationFailed(Server, serverResponse);
                }

                log.Info(Properties.Logs.SendingAuthenticationCommandDone);

                return true;
            }

            // Catch Web exceptions.
            catch (WebException e)
            {
                log.Error(Properties.Logs.SendingAuthenticationCommandException);
                log.Fatal(e.Output());

                RaiseAuthenticationFailed(Server, serverResponse);

                return false;
            }

            // Catch all exceptions.
            catch (Exception e)
            {
                log.Error(Properties.Logs.SendingAuthenticationCommandException);
                log.Fatal(e.Output());

                RaiseAuthenticationFailed(Server, serverResponse);

                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> ListSections()
        {
            log.Info(string.Format(Properties.Logs.SendingClientCommand, MethodBase.GetCurrentMethod().Name, Server.Host, Server.Port));

            // Initialize sections server response.
            ServerResponseSections serverResponse = null;

            try
            {
                // Send command ListSections to the server.
                // Decode response as JSon format to exploit sections list.
                HttpResponseMessage response = WebClient.Client.ListSections();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponseSections>(message);

                // Raise ListSections success event on HTTP OK response.
                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {
                    log.Debug(Properties.Logs.SendingListSectionsCommandResponseOk);
                    RaiseListSectionsSuccess(Server, serverResponse);
                }

                // Raise authentication unauthorized event unauthorized HTTP response.
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    log.Debug(Properties.Logs.SendingListSectionsCommandUnauthorized);
                    RaiseAuthenticationUnauthorized(Server, serverResponse);
                }

                // Raise ping failed event on others HTTP response.
                // Todo : Adapt all http responses
                else
                {
                    log.Error(Properties.Logs.SendingListSectionsCommandResponseFailed);
                    log.Error(response.StatusCode.ToString() + " : " + serverResponse.Error);

                    RaiseListSectionsFailed(Server, serverResponse);
                }

                log.Info(Properties.Logs.SendingListSectionsCommandDone);

                return true;
            }

            // Catch all exceptions.
            catch (Exception e)
            {
                log.Error(Properties.Logs.SendingListSectionsCommandException);
                log.Fatal(e);

                RaiseListSectionsFailed(Server, serverResponse);

                return false;
            }
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        public async Task ListAlbums()
        {
            log.Info(string.Format(Properties.Logs.SendingAlbumsCommand.ToString(), Server.Host, Server.Port));

            ServerResponseSections serverResponse = null;

            try
            {
                HttpResponseMessage response = WebClient.Client.ListAlbums();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponseSections>(message);

                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {

                    RaiseListSectionsSuccess(Server, serverResponse);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    RaiseListSectionsFailed(Server, serverResponse);
                }
                else
                {
                    RaiseListSectionsFailed(Server, serverResponse);
                    MessageBase.Error($"Server list sections {Server.Host}:{Server.Port} failed !\n\r {response.StatusCode.ToString()} : {serverResponse.Error}");
                }
            }
            catch (Exception e)
            {
                RaiseListSectionsFailed(Server, serverResponse);
                MessageBase.Fatal(e, $"Server list sections {Server.Host}:{Server.Port} failed !");
            }

            log.Info(Properties.Logs.SendingAlbumsCommandDone);
        }
        */


        /// <summary>
        /// 
        /// </summary>
        public async Task SingleSection(int pk)
        {
            log.Info(string.Format(Properties.Logs.SendingClientCommand, MethodBase.GetCurrentMethod().Name, Server.Host, Server.Port));

            // Initialize sections server response.
            ServerResponseSection serverResponse = null;

            try
            {
                // Send command SingleSection to the server.
                // Decode response as JSon format to exploit sections list.
                HttpResponseMessage response = WebClient.Client.SingleSection(pk);
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponseSection>(message);

                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {
                    RaiseSingleSectionSuccess(Server, serverResponse);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    RaiseSingleSectionFailed(Server, serverResponse);
                }
                else
                {
                    RaiseSingleSectionFailed(Server, serverResponse);
                    MessageBoxs.Error($"Server single section {Server.Host}:{Server.Port} failed !\n\r {response.StatusCode.ToString()} : {serverResponse.Error}");
                }
            }
            catch (Exception e)
            {
                RaiseSingleSectionFailed(Server, serverResponse);
                MessageBoxs.Fatal(e, $"Server single section {Server.Host}:{Server.Port} failed !");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task SingleAlbum(int pk)
        {
            log.Info(string.Format(Properties.Logs.SendingClientCommand, MethodBase.GetCurrentMethod().Name, Server.Host, Server.Port));

            // Initialize sections server response.
            ServerResponseAlbum serverResponse = null;

            try
            {
                // Send command SingleSection to the server.
                // Decode response as JSon format to exploit sections list.
                HttpResponseMessage response = WebClient.Client.SingleAlbum(pk);
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponseAlbum>(message);

                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {
                    RaiseSingleAlbumSuccess(Server, serverResponse);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    RaiseSingleAlbumFailed(Server, serverResponse);
                }
                else
                {
                    RaiseSingleAlbumFailed(Server, serverResponse);
                    MessageBoxs.Error($"Server single section {Server.Host}:{Server.Port} failed !\n\r {response.StatusCode.ToString()} : {serverResponse.Error}");
                }
            }
            catch (Exception e)
            {
                RaiseSingleAlbumFailed(Server, serverResponse);
                MessageBoxs.Fatal(e, $"Server single section {Server.Host}:{Server.Port} failed !");
            }
        }
    }
}
