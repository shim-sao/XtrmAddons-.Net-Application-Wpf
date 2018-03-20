using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.HttpClient.WebAuth;

namespace XtrmAddons.Fotootof.Lib.HttpClient.WebClient
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Http Web Client.
    /// </summary>
    public class HttpWebClient : IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable format encoding.
        /// </summary>
        private Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// Variable url token.
        /// </summary>
        private string _urlToken = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the cookies container.
        /// </summary>
        public CookieContainer Cookies { get; private set; } = new CookieContainer();

        /// <summary>
        /// Property to access to the Http client handler
        /// </summary>
        public HttpClientHandler Handler { get; private set; } = new HttpClientHandler();

        /// <summary>
        /// Property to access to the server host name.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Property to access to the server port.
        /// </summary>
        public string Port { get; private set; }

        /// <summary>
        /// Property to access to the http user authenticator.
        /// </summary>
        public HttpWebAuth WebAuth { get; private set; }

        /// <summary>
        /// Property to access to the Http Client connector.
        /// </summary>
        public System.Net.Http.HttpClient Client { get; private set; }

        /// <summary>
        /// Property to access to the url token.
        /// </summary>
        public string Token { get =>_urlToken; private set => _urlToken = value; }

        /// <summary>
        /// Property to access to the server http response.
        /// </summary>
        public HttpResponseMessage Response { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Http Web Client Constructor.
        /// </summary>
        /// <param name="host">The server host name.</param>
        /// <param name="port">The server port.</param>
        /// <param name="webAuth">The web authorization informations.</param>
        public HttpWebClient(string host, string port, HttpWebAuth webAuth)
        {
            Host = host;
            Port = port;
            WebAuth = webAuth;

            Handler.CookieContainer = Cookies;
            Client = new System.Net.Http.HttpClient(Handler)
            {
                BaseAddress = new Uri("http://" + Host + ":" + Port + "/")
            };
        }

        #endregion



        #region Method

        /// <summary>
        /// Method to ping a remote server.
        /// </summary>
        /// <returns>An Http response message</returns>
        public HttpResponseMessage Ping()
        {
            try
            {
                return Client.GetAsync("api").Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Api web client server ping failed ArgumentNullException !", e);
                throw new HttpRequestException(e.Message, e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("ArgumentNullException : Api web client server ping failed ArgumentNullException !", e);
                throw new HttpRequestException(e.Message, e);
            }
            catch (AggregateException e)
            {
                log.Fatal("HttpRequestException : Api web client server ping failed HttpRequestException !", e);
                throw new AggregateException(e.Message, e);
            }
        }

        /// <summary>
        /// Method to read a response to string.
        /// </summary>
        /// <param name="response">An http response message.</param>
        /// <returns>A response in String format.</returns>
        public async Task<string> Read(HttpResponseMessage response)
        {
            try
            {
                HttpResponseHeaders header = response.Headers;
                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync(), _encoding, true))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                log.Fatal("Api web client read response failed !", e);
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// <para>Method to set Url cookies.</para>
        /// <para>Hack for cookies management because of CookieContainer bugs.</para>
        /// <para>Todo : Manage Cookies in the right way !</para>
        /// </summary>
        private void SetSessionToken()
        {
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseHeaders header = Response.Headers;
                string[] cookieHeader = (string[])header.GetValues("Set-Cookie");
                _urlToken = cookieHeader[0].Replace(", ", "&");
            }
        }

        /// <summary>
        /// Method to get server authentication.
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage Authentication()
        {
            Response = null;
            
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", WebAuth.Email),
                    new KeyValuePair<string, string>("password", WebAuth.Password)
                });
                Response = Client.PostAsync("api/user/authentication", content).Result;
                SetSessionToken();
            }
            catch (Exception e)
            {
                log.Fatal("FATAL : Http web client authentication exception !", e);
                throw new Exception("FATAL : Http web client authentication exception !", e);
            }

            return Response;
        }

        #endregion



        /// <summary>
        /// Method to get http request list of sections
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage ListSections()
        {
            try
            {
                return Client.GetAsync("api/sections?" + _urlToken).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client list sections ArgumentNullException !", e);
                throw new ArgumentNullException("ArgumentNullException : Http web client list sections ArgumentNullException !", e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client list sections HttpRequestException !", e);
                throw new Exception("HttpRequestException : Http web client list sections HttpRequestException !", e);
            }
            catch (Exception e)
            {
                log.Fatal("Exception : Http web client list sections Exception !", e);
                throw new Exception("Exception : Http web client list sections Exception !", e);
            }
        }
        
        /// <summary>
        /// Method to get http request list of albums
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage ListAlbums()
        {
            try
            {
                return Client.GetAsync("api/albums?" + _urlToken).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client list albums ArgumentNullException !", e);
                throw new ArgumentNullException("ArgumentNullException : Http web client list albums ArgumentNullException !", e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client list albums HttpRequestException !", e);
                throw new Exception("HttpRequestException : Http web client list albums HttpRequestException !", e);
            }
            catch (Exception e)
            {
                log.Fatal("Exception : Http web client list albums Exception !", e);
                throw new Exception("Exception : Http web client list albums Exception !", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage SingleSection(int pk)
        {
            try
            {
                return Client.GetAsync("api/section/get/" + pk.ToString() + "?" + _urlToken).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client single section ArgumentNullException !", e);
                throw new ArgumentNullException("ArgumentNullException : Http web client single section ArgumentNullException !", e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client single section HttpRequestException !", e);
                throw new Exception("HttpRequestException : Http web client single section HttpRequestException !", e);
            }
            catch (Exception e)
            {
                log.Fatal("Exception : Http web client single section Exception !", e);
                throw new Exception("Exception : Http web client single section Exception !", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage SingleAlbum(int pk)
        {
            try
            {
                return Client.GetAsync("api/album/get/" + pk.ToString() + "?" + _urlToken).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client single album ArgumentNullException !", e);
                throw new ArgumentNullException("ArgumentNullException : Http web client single album ArgumentNullException !", e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client single album HttpRequestException !", e);
                throw new Exception("HttpRequestException : Http web client single album HttpRequestException !", e);
            }
            catch (Exception e)
            {
                log.Fatal("FATAL : Http web client single album exception !", e);
                throw new Exception("FATAL : Http web client single album exception !", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage SinglePicture(string path)
        {
            try
            {
                return Client.GetAsync(path).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client single picture ArgumentNullException !", e);
                throw new ArgumentNullException("ArgumentNullException : Http web client single picture ArgumentNullException !", e);
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client single picture HttpRequestException !", e);
                throw new Exception("HttpRequestException : Http web client single picture HttpRequestException !", e);
            }
            catch (Exception e)
            {
                log.Fatal("FATAL : Http web client single picture exception !", e);
                throw new Exception("FATAL : Http web client single picture exception !", e);
            }
        }

        #region IDisposable Support

        /// <summary>
        /// Variable is disposed ?
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Variable Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Method to dispose the object.
        /// </summary>
        /// <param name="disposing">Dispose managed objects ?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed objects.
                    Client.Dispose();
                    Handler.Dispose();
                    handle.Dispose();
                    Response.Dispose();
                }

                // Dispose not managed objects & big size variables = null.
                Cookies = null;
                WebAuth = null;

                // Flag disposed value.
                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~HttpWebClient() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
