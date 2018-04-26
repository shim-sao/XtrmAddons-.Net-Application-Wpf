using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using XtrmAddons.Fotootof.Lib.HttpClient;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient.Responses;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient
{
    //[System.Obsolete("use => XtrmAddons.Fotootof.Lib.Base.Classes.HttpClient.ClientHttp")]
    public class ClientHttp
    {
        #region Variable
        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        public ClientHttp(Client server)
        {
            Server = server;
            InitializeWebClient();
        }

        #endregion



        #region Properties

        public Client Server { get; private set; }
        
        public HttpWebClientApplication WebClient { get; private set; }

        public event EventHandler OnAuthenticationSuccess = delegate { };

        public event EventHandler OnAuthenticationFailed = delegate { };

        public event EventHandler OnListSectionsSuccess = delegate { };

        public event EventHandler OnListSectionsFailed = delegate { };

        public event EventHandler OnSingleSectionSuccess = delegate { };

        public event EventHandler OnSingleSectionFailed = delegate { };

        public event EventHandler OnPingSuccess = delegate { };

        public event EventHandler OnPingFailed = delegate { };

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
        /// <param name="ServerInfos"></param>
        public void InitializeWebClient()
        {
            AppLogger.Info("Initializing server connection.", true);

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
                AppLogger.Fatal("Initializing server connection failed.", e, true);
            }
        }

        /// <summary>
        /// Method to ping a server.
        /// </summary>
        /// <param name="ServerInfos"></param>
        public async void Ping()
        {
            AppLogger.Info(string.Format("Ping server {0}:{1}", Server.Host, Server.Port), true);

            ServerResponse serverResponse = null;

            try
            {
                HttpResponseMessage response = WebClient.Client.Ping();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponse>(message);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    RaisePingSuccess(Server, serverResponse);
                }
                else
                {
                    RaisePingFailed(Server, serverResponse);
                    AppLogger.Error(string.Format("Ping server {0}:{1} failed !", Server.Host, Server.Port), true);
                    AppLogger.Error(response.StatusCode.ToString() + " : " + serverResponse.Error, true);
                }
            }
            catch (Exception e)
            {
                RaisePingFailed(Server, serverResponse);
                AppLogger.Fatal(string.Format("Ping server {0}:{1} failed !", Server.Host, Server.Port), e, true);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public async void Authentication()
        {
            AppLogger.Info("Initializing server authentication.", true);

            ServerResponse serverResponse = null;

            try
            {
                HttpResponseMessage response = WebClient.Client.Authentication();
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponse>(message);

                if (response.StatusCode == HttpStatusCode.OK && serverResponse.Authentication)
                {

                    RaiseAuthenticationSuccess(Server, serverResponse);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    RaiseAuthenticationFailed(Server, serverResponse);
                }
                else
                {
                    RaiseAuthenticationFailed(Server, serverResponse);
                    AppLogger.Error(string.Format("Server authentication on server [{0}:{1}] failed !", Server.Host, Server.Port), true);
                    AppLogger.Error(response.StatusCode.ToString() + " : " + serverResponse.Error, true);
                }
            }
            catch (Exception e)
            {
                RaiseAuthenticationFailed(Server, serverResponse);
                AppLogger.Fatal(string.Format("Server authentication on server[{0}:{1}] failed !", Server.Host, Server.Port), e, true);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public async void ListSections()
        {
            AppLogger.Info("Initializing server list sections.", true);

            ServerResponseSections serverResponse = null;

            try
            {
                HttpResponseMessage response = WebClient.Client.ListSections();
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
                    AppLogger.Error(string.Format("Server list sections {0}:{1} failed !", Server.Host, Server.Port), true);
                    AppLogger.Error(response.StatusCode.ToString() + " : " + serverResponse.Error, true);
                }

                AppLogger.InfoAndClose("Initializing server list sections done.", true);
            }
            catch (Exception e)
            {
                RaiseListSectionsFailed(Server, serverResponse);
                AppLogger.Fatal(string.Format("Server list sections {0}:{1} failed !", Server.Host, Server.Port), e, true);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public async void ListAlbums()
        {
            AppLogger.Info("Initializing server list albums.", true);

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
                    AppLogger.Error(string.Format("Server list sections {0}:{1} failed !", Server.Host, Server.Port), true);
                    AppLogger.Error(response.StatusCode.ToString() + " : " + serverResponse.Error, true);
                }
            }
            catch (Exception e)
            {
                RaiseListSectionsFailed(Server, serverResponse);
                AppLogger.Fatal(string.Format("Server list sections {0}:{1} failed !", Server.Host, Server.Port), e, true);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public async void SingleSection(int pk)
        {
            AppLogger.Info("Initializing server single section.", true);
            ServerResponseSections serverResponse = null;

            try
            {
                HttpResponseMessage response = WebClient.Client.SingleSection(pk);
                string message = await WebClient.Client.Read(response);
                serverResponse = JsonConvert.DeserializeObject<ServerResponseSections>(message);

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
                    AppLogger.Error(string.Format("Server list albums {0}:{1} failed !", Server.Host, Server.Port), true);
                    AppLogger.Error(response.StatusCode.ToString() + " : " + serverResponse.Error, true);
                }
            }
            catch (Exception e)
            {
                RaiseSingleSectionFailed(Server, serverResponse);
                AppLogger.Fatal(string.Format("Server list sections {0}:{1} failed !", Server.Host, Server.Port), e, true);
            }
        }
    }
}
