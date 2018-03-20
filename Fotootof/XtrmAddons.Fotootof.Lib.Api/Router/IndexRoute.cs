using System;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.Api.Router
{
    /// <summary>
    /// XtrmAddons PhotoAlbum Server Api Router Index Route.
    /// </summary>
    public class IndexRoute : Router
    {
        /// <summary>
        /// XtrmAddons PhotoAlbum Server Api Router Index Route constructor.
        /// </summary>
        public IndexRoute() : base() { }

        /// <summary>
        /// XtrmAddons PhotoAlbum Server Api Router Index Route constructor.
        /// </summary>
        /// <param name="uri"></param>
        public IndexRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to get index root of the server.
        /// </summary>
        /// <returns>The response data of the request.</returns>
        public override WebServerResponseData Index()
        {
            log.Info("Api : Serving root server prefix. Please wait !");

            try
            {
                return ResponseContentToJson();
            }
            catch(Exception e)
            {
                log.Fatal("Api Error : Serving root server prefix. Please wait !");
                log.Fatal(string.Format("Error : {0}",e.Message));
                log.Fatal(e.Source);
                log.Fatal(e.StackTrace);

                return null;
            }
        }
    }
}