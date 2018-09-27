
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using Fotootof.SQLite.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Fotootof.HttpServer;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;
using Fotootof.SQLite.EntityManager.Data.Tables.Json.Models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Fotootof.Plugin.Api.Router
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Plugin Api Router Constructor.</para>
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
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
        /// Property to access to the application database service <see cref="SQLiteSvc"/>.
        /// </summary>
        protected static SQLiteSvc Database
            => SQLiteSvc.GetInstance();

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Plugin Api Router Constructor.
        /// </summary>
        public Router() : base()
        {
            ResponseInitialize();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Plugin Api Router Constructor.
        /// </summary>
        /// <param name="uri"></param>
        public Router(WebServerRequestUrl uri) : base(uri)
        {
            Content.Add("Status", "Ready");
            Content.Add("Authentication", false);
            Content.Add("Response", "");
            Content.Add("Error", "");
            Content.Add("Version", Assembly.GetAssembly(GetType()).GetName().Version);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        private void ResponseInitialize()
        {
            Content.Add("Status", "Ready");
            Content.Add("Authentication", false);
            Content.Add("Response", "");
            Content.Add("Error", "");
            Content.Add("Version", Assembly.GetAssembly(GetType()).GetName().Version);
        }

        /// <summary>
        /// Method to format ResponseContent to Json.
        /// </summary>
        /// <returns></returns>
        protected WebServerResponseData ResponseContentToJson()
        {
            log.Info("Api : Creating json response. Please wait !");

#if DEBUG
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
#else
            Response.ContentAppend(
                    JsonConvert.SerializeObject(
                        Content,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        }
                    )
                ); 
#endif

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
        protected List<SectionJsonModel> ConvertJsonAuthSections(IEnumerable sections)
        {
            List<SectionJsonModel> items = new List<SectionJsonModel>();
            foreach (SectionEntity section in sections)
            {
                items.Add(new SectionJsonModel(section, false));
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       protected SectionJsonModel ConvertJsonAuthSection(SectionEntity entity, bool auth = true)
        {
            if (entity == null)
            {
                return null;
            }

            SectionJsonModel section = new SectionJsonModel(entity, auth);
            var keys = entity.AlbumsPKeys;

            if (keys != null)
            {
                section.Albums = new ObservableCollection<AlbumJsonModel>();

                foreach (int k in keys)
                {
                    var album = Database.Albums.SingleOrNull(
                        new AlbumOptionsSelect
                        {
                            PrimaryKey = k
                        });

                    var albJson = new AlbumJsonModel(album);

                    // Thumbnail picture informations
                    albJson.ThumbnailPicture = new PictureJsonModel(
                        Database.Pictures.SingleOrNull(
                            new PictureOptionsSelect
                            {
                                PrimaryKey = album.ThumbnailPictureId
                            })
                        );

                    albJson.ThumbnailPicture.ThumbnailPath = GetPicturePath(albJson.ThumbnailPicture, "thumbnail");
                    albJson.ThumbnailPicture.PicturePath = GetPicturePath(albJson.ThumbnailPicture, "picture");
                    albJson.ThumbnailPicture.OriginalPath = GetPicturePath(albJson.ThumbnailPicture, "original");

                    // Thumbnail picture informations
                    albJson.BackgroundPicture = new PictureJsonModel(
                        Database.Pictures.SingleOrNull(
                            new PictureOptionsSelect
                            {
                                PrimaryKey = album.BackgroundPictureId
                            })
                        );

                    albJson.BackgroundPicture.ThumbnailPath = GetPicturePath(albJson.BackgroundPicture, "thumbnail");
                    albJson.BackgroundPicture.PicturePath = GetPicturePath(albJson.BackgroundPicture, "picture");
                    albJson.BackgroundPicture.OriginalPath = GetPicturePath(albJson.BackgroundPicture, "original");

                    // Preview picture informations
                    albJson.PreviewPicture = new PictureJsonModel(
                        Database.Pictures.SingleOrNull(
                            new PictureOptionsSelect
                            {
                                PrimaryKey = album.PreviewPictureId
                            })
                        );

                    albJson.PreviewPicture.ThumbnailPath = GetPicturePath(albJson.PreviewPicture, "thumbnail");
                    albJson.PreviewPicture.PicturePath = GetPicturePath(albJson.PreviewPicture, "picture");
                    albJson.PreviewPicture.OriginalPath = GetPicturePath(albJson.PreviewPicture, "original");


                    section.Albums.Add(albJson);
                }
            }

            /*
            foreach (AlbumEntity album in entity.Albums)
            {
                //album.PicturePath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/album/" + album.PrimaryKey + "/picture?sid=" + CookieString;
                //album.ThumbnailPath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/album/" + album.PrimaryKey + "/thumbnail?sid=" + CookieString;
            }
            */

            return section;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pathType"></param>
        /// <returns></returns>
        private string GetPicturePath(PictureJsonModel p, string pathType)
        {
            return $"http://{Uri.Host}:{Uri.Port}/api/picture?id={p.PrimaryKey}&type={pathType}&sid={CookieString}";
        }
    }
}