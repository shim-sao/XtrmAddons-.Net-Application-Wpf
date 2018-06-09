
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.Api.Router
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Lib Api Router.</para>
    /// <para>List of formatted routes :</para>
    /// <para>/index</para>
    /// </summary>
    public class Router : WebServerRequestRoute
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties
        
        /// <summary>
        /// Variable cookie session token.
        /// </summary>
        public string CookieString { get; private set; }

        /// <summary>
        /// Property dictionary of content.
        /// </summary>
        protected Dictionary<string, object> Content { get; set; }
            = new Dictionary<string, object>();

        /// <summary>
        /// Property dictionary of cookies authentication values.
        /// </summary>
        protected NameValueCollection CookAuth { get; set; }
            = new NameValueCollection { { "sid", "" }, { "token", "" }};

        /// <summary>
        /// Accessors database core manager.
        /// </summary>
        /// <returns></returns>
        protected SQLiteSvc Database => ApplicationSession.Properties.Database;

        #endregion



        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Router Constructor.
        /// </summary>
        public Router() : base()
        {
            Content.Add("Status", "Ready");
            Content.Add("Authentication", false);
            Content.Add("Response", "");
            Content.Add("Error", "");
            Content.Add("Version", "");
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Router Constructor.
        /// </summary>
        /// <param name="uri"></param>
        public Router(WebServerRequestUrl uri) : base(uri)
        {
            Content.Add("Status", "Ready");
            Content.Add("Authentication", false);
            Content.Add("Response", "");
            Content.Add("Error", "");
            Content.Add("Version", "");
        }

        /// <summary>
        /// Method to format ResponseContent to Json.
        /// </summary>
        /// <returns></returns>
        protected WebServerResponseData ResponseContentToJson()
        {
            log.Info("Api : Creating json response. Please wait !");

            Response.ContentAppend(
                JsonConvert.SerializeObject(
                    Content,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }
                )
            );

            log.Info("Api : Creating json response. Done !");

            return Response;
        }

        /// <summary>
        /// Method to return index root of the Api server.
        /// </summary>
        /// <returns>A WebServerResponseData indicate that the server is running.</returns>
        public virtual WebServerResponseData Index()
        {
            Content["Status"] = "Running";
            return ResponseContentToJson();
        }

        /// <summary>
        /// Method to return 404 file not found response.
        /// </summary>
        /// <returns>A WebServerResponseData indicate that the requested url is not found.</returns>
        protected virtual WebServerResponseData ResponseError404(string message = "Resource Not Found")
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Content["Error"] = message;
            return ResponseContentToJson();
        }

        /// <summary>
        /// Method to return 500 server error response.
        /// </summary>
        /// <returns>A WebServerResponseData indicate that the server encountered an error.</returns>
        protected virtual WebServerResponseData ResponseError500(string message = "Internal Server Error")
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            Content["Error"] = message;
            return ResponseContentToJson();
        }

        /// <summary>
        /// Method to return user not authorized response.
        /// </summary>
        /// <returns>User not authorized response</returns>
        protected virtual WebServerResponseData ResponseNotAuth(string message = "Authentication to server failed.")
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Content["Error"] = message;
            return ResponseContentToJson();
        }

        /// <summary>
        /// Method to get authenticate user.
        /// </summary>
        /// <returns></returns>
        protected UserEntity GetAuthUser()
        {
            if (IsAuth())
            {
                int sessionUserId = (int)HttpWebServerApplication.Session.Get(CookAuth["sid"], "UserId");

                UserOptionsSelect options = new UserOptionsSelect
                {
                    PrimaryKey = sessionUserId,
                    Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                };

                return Database.Users.SingleOrNull(options);
            }

            return null;
        }

        /// <summary>
        /// Method to check user authentication on server.
        /// </summary>
        /// <returns>true if user is authenticate otherwise false.</returns>
        protected bool IsAuth()
        {
            log.Info("Api : Checking user authentication on server. Please wait !");

            try
            {
                // Try to found sid cookie.
                // sid cookie contains at base 2 cookies but a big bug on Cookies sender append all cookies in a same string
                // so we use only 1 string as cookies container.
                // todo : Correct bug sender or rewrite all Http Server base with another solution more viable.
                string[] cook = new string[] { "", "" };

                CookieCollection cookies = Uri.Cookies;
                if(cookies != null && cookies.Count > 0)
                {
                    foreach (Cookie cookie in Uri.Cookies)
                    {
                        if (cookie.Path == "" || cookie.Path == "/api/user")
                        {
                            if (cookie.Name == "sid")
                            {
                                CookieString = cookie.Value;
                                cook = CookieString.Split(new char[] { ':' });
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        var a = Uri;
                        CookieString = Uri.QueryString["sid"];
                        cook = CookieString.Split(new char[] { ':' });
                    }
                    catch{}
                }
                string sid = cook[0];
                string stoken = cook[1];

                // todo : Alternative code with query params, use it if bug was corrected :
                // string sid = Uri.QueryString["sid"];
                // string stoken = Uri.QueryString["stoken"];

                // Get user token stored in the session.
                string token = (string)HttpWebServerApplication.Session.Get(sid, "sid");
                log.Debug(string.Format("Api : Authentication receive sid={0}, stoken={1}", sid, stoken));
                log.Debug(string.Format("Api : Authentication in session token={0}", token));

                if (token == null || token == "" || token != stoken)
                {
                    log.Info("Api : Checking user authentication on server. Unauthorized !");
                    return false;
                }

                CookAuth["sid"] = sid;
                CookAuth["token"] = stoken;

                log.Info("Api : Checking user authentication on server. Authorized !");
                return true;
            }
            catch(Exception e)
            {
                log.Error("Error : User authentication to server failed.");
                log.Error(string.Format("Error : {0}", e.Message));
                log.Error(e.StackTrace);

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected List<SectionJson> ConvertJsonAuthSections(IEnumerable sections)
        {
            List<SectionJson> items = new List<SectionJson>();
            foreach (SectionEntity section in sections)
            {
                items.Add(new SectionJson(section, false));
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       protected SectionJson ConvertJsonAuthSection(SectionEntity entity, bool auth = true)
        {
            foreach (AlbumEntity album in entity.Albums)
            {
                //album.PicturePath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/album/" + album.PrimaryKey + "/picture?sid=" + CookieString;
                //album.ThumbnailPath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/album/" + album.PrimaryKey + "/thumbnail?sid=" + CookieString;
            }

            return new SectionJson(entity, auth, true);
        }
    }
}