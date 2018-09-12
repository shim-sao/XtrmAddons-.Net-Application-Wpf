using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Fotootof.HttpClient.WebAuth;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.HttpClient.WebClient
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
        private readonly Encoding _encoding = Encoding.UTF8;

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
            log.Debug(MethodBase.GetCurrentMethod().Name);

            try
            {
                return Client.GetAsync("api").Result;
            }
            catch (ArgumentNullException e)
            {
                ArgumentNullException ex = new ArgumentNullException("Api web client server ping error.", e);
                log.Error(ex.Output());
                throw ex;
            }
            catch (HttpRequestException e)
            {
                HttpRequestException ex = new HttpRequestException("Api web client server ping error.", e);
                log.Error(ex.Output());
                throw ex;
            }
            catch (AggregateException e)
            {
                AggregateException ex = new AggregateException("Api web client server ping error.", e);
                log.Error(ex.Output());
                throw ex;
            }
        }

        /// <summary>
        /// Method to read a response to string.
        /// </summary>
        /// <param name="response">An http response message.</param>
        /// <returns>A response in String format.</returns>
        public async Task<string> Read(HttpResponseMessage response)
        {
            log.Debug(MethodBase.GetCurrentMethod().Name);

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
                AggregateException ex = new AggregateException("Api web client read response error.", e);
                log.Error(ex.Output());
                throw ex;
            }
        }

        /// <summary>
        /// <para>Method to set Url cookies.</para>
        /// <para>Hack for cookies management because of CookieContainer bugs.</para>
        /// <para>Todo : Manage Cookies in the right way !</para>
        /// </summary>
        private void SetSessionToken()
        {
            log.Debug($"Http Client method : {MethodBase.GetCurrentMethod().Name}");
            log.Debug($"Http Client Response.StatusCode : {Response.StatusCode}");

            if (Response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseHeaders header = Response.Headers;
                string[] cookieHeader = (string[])header.GetValues("Set-Cookie");

                #if DEBUG
                int i = 1;
                foreach(string s in cookieHeader)
                {
                    log.Debug($"cookie #{i} = {s}");
                    i++;
                }
                #endif

                string[] parts = cookieHeader[0].Replace(", ", "&").Split(new char[] { ';' });
                if(parts.Length > 1)
                {
                    _urlToken = parts[0];
                }
                
                log.Debug($"Url Token : {_urlToken}");
            }
        }

        /// <summary>
        /// Method to get server authentication.
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage Authentication()
        {
            log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Please wait...");

            Response = null;
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", WebAuth.Email),
                    new KeyValuePair<string, string>("password", WebAuth.Password)
                });

                using(Task<HttpResponseMessage> message = Client.PostAsync("api/user/authentication", content))
                {
                    Response = message.Result;
                    SetSessionToken();

                    return message.Result;
                }
            }
            catch (Exception e)
            {
                Exception ex = new Exception($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Exception in {e.TargetSite}", e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            finally
            {
                log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Done !");
            }
        }

        #endregion



        /// <summary>
        /// Method to get http request list of sections
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage ListSections()
        {
            log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Please wait...");

            Response = null;
            try
            {
                using (Task<HttpResponseMessage> message = Client.GetAsync("api/sections?" + _urlToken))
                {
                    return message.Result;
                }
            }
            catch (ArgumentNullException e)
            {
                ArgumentNullException ex = new ArgumentNullException($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. ArgumentNullException in {e.TargetSite}", e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            catch (HttpRequestException e)
            {
                HttpRequestException ex = new HttpRequestException($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. HttpRequestException in {e.TargetSite}", e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            catch (Exception e)
            {
                Exception ex = new Exception($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Exception in {e.TargetSite}", e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            finally
            {
                log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Done !");
            }
        }
        
        /*
        /// <summary>
        /// Method to get http request list of albums
        /// </summary>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage ListAlbums()
        {
            log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Please wait...");

            Response = null;
            try
            {
                using (Task<HttpResponseMessage> message = Client.GetAsync("api/albums?" + _urlToken))
                {
                    return message.Result;
                }
            }
            catch (ArgumentNullException e)
            {
                ArgumentNullException ex = ArgumentNullException(MethodBase.GetCurrentMethod().Name, e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            catch (HttpRequestException e)
            {
                HttpRequestException ex = HttpRequestException(MethodBase.GetCurrentMethod().Name, e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            catch (Exception e)
            {
                Exception ex = Exception(MethodBase.GetCurrentMethod().Name, e);
                log.Fatal(ex.Output(), e);
                throw ex;
            }
            finally
            {
                log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Done !");
            }
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage SingleSection(int pk)
        {
            log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Please wait...");

            Response = null;
            try
            {
                using (Task<HttpResponseMessage> message = Client.GetAsync($"api/section?id={pk.ToString()}&{_urlToken}"))
                {
                    return message.Result;
                }
            }
            catch (ArgumentNullException e)
            {
                ArgumentNullException ex = ArgumentNullException(MethodBase.GetCurrentMethod().Name, e);
                log.Error(ex);
                throw ex;
            }
            catch (HttpRequestException e)
            {
                HttpRequestException ex = HttpRequestException(MethodBase.GetCurrentMethod().Name, e);
                log.Error(ex);
                throw ex;
            }
            catch (Exception e)
            {
                Exception ex = Exception(MethodBase.GetCurrentMethod().Name, e);
                log.Error(ex);
                throw ex;
            }
            finally
            {
                log.Debug($"Http Client sending command : {MethodBase.GetCurrentMethod().Name}. Done !");
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
        /// <param name="pk"></param>
        /// <returns>An Http response message.</returns>
        public HttpResponseMessage SinglePicture(int pk)
        {
            try
            {
                return Client.GetAsync("api/picture/get/" + pk.ToString() + "?" + _urlToken).Result;
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("ArgumentNullException : Http web client single picture ArgumentNullException !", e);
                throw;
            }
            catch (HttpRequestException e)
            {
                log.Fatal("HttpRequestException : Http web client single picture HttpRequestException !", e);
                throw;
            }
            catch (Exception e)
            {
                log.Fatal("FATAL : Http web client single picture exception !", e);
                throw;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns>An Http response message.</returns>
        //public HttpResponseMessage SinglePicture(string path)
        //{
        //    try
        //    {
        //        return Client.GetAsync(path).Result;
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        log.Fatal("ArgumentNullException : Http web client single picture ArgumentNullException !", e);
        //        throw new ArgumentNullException("ArgumentNullException : Http web client single picture ArgumentNullException !", e);
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        log.Fatal("HttpRequestException : Http web client single picture HttpRequestException !", e);
        //        throw new Exception("HttpRequestException : Http web client single picture HttpRequestException !", e);
        //    }
        //    catch (Exception e)
        //    {
        //        log.Fatal("FATAL : Http web client single picture exception !", e);
        //        throw new Exception("FATAL : Http web client single picture exception !", e);
        //    }
        //}

        #region IDisposable Support

        /// <summary>
        /// Variable is disposed ?
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Method to dispose the object.
        /// </summary>
        /// <param name="disposing">Dispose managed objects ?</param>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "<Handler>k__BackingField")]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed objects.
                    Client.Dispose();
                    Handler.Dispose();
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
        // ~HttpWebClient()
        //{
        //    // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //    Dispose(false);
        //}

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            GC.SuppressFinalize(this);
        }

        #endregion



        #region Methods Exceptions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected static ArgumentNullException ArgumentNullException(string methodName, ArgumentNullException e)
        {
            return new ArgumentNullException(NewExceptionMessage(methodName, e), e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected static HttpRequestException HttpRequestException(string methodName, HttpRequestException e)
        {
            return new HttpRequestException(NewExceptionMessage(methodName, e), e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected static Exception Exception(string methodName, Exception e)
        {
            return new Exception(NewExceptionMessage(methodName, e), e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected static string NewExceptionMessage<T>(string methodName, T e)
        {
            return $"Http Client sending command : {methodName}. {e.GetType().Name} in {(e as Exception).TargetSite}";
        }

        #endregion
    }
}
