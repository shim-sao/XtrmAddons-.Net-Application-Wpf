using System.Net;
using XtrmAddons.Net.HttpWebServer;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.HttpServer
{
    public class HttpWebServer : WebServer
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="prefix">The prefix of the web server.</param>
        public HttpWebServer(string prefix) : base(prefix) {}

        /// <summary>
        /// Method to output response of the server.
        /// </summary>
        public override WebServerResponseData GetResponse(HttpListenerContext ctx)
        {
            return new HttpRequest(ctx).Response;
        }
    }
}