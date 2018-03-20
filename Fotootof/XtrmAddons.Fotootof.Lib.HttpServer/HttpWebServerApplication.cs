using System;
using System.Net;
using XtrmAddons.Net.HttpWebServer.Events;
using XtrmAddons.Net.HttpWebServer.Session;

namespace XtrmAddons.Fotootof.Lib.HttpServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Library Http Web Server Application.
    /// </summary>
    public class HttpWebServerApplication
    {
        #region Variables

        /// <summary>
        /// Variable Http Web Server instance.
        /// </summary>
        private static HttpWebServer _ws = null;

        /// <summary>
        /// Variable Http Web Server prefixes.
        /// </summary>
        private static string _prefixes = "";

        #endregion Variables


        #region Property

        /// <summary>
        /// Property is server started.
        /// </summary>
        public static bool IsStarted
        {
            get
            {
                if (_ws == null)
                    return false;

                return _ws.IsStarted;
            }
        }

        public static WebServerSession Session
        {
            get
            {
                if (_ws == null)
                    return null;

                return _ws.Session;
            }
        }

        /// <summary>
        /// Variable envent handler on server start.
        /// </summary>
        public static event EventHandler<WebServerStartEventArgs> WebServerStartEventHandler
        {
            add { if (_ws != null) _ws.WebServerStartEventHandler += value;  }
            remove { if (_ws != null) _ws.WebServerStartEventHandler -= value; }
        }

        #endregion Property


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public static void Start(string host, string port )
        {
            _prefixes = "http://" + host + ":" + port + "/";
            _ws = new HttpWebServer(_prefixes);
            _ws.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Stop()
        {
            _ws.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
        }

        #endregion Methods
    }
}