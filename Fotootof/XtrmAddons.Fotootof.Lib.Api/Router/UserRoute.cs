using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using Fotootof.SQLite.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Fotootof.HttpServer;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.Api.Router
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Api Router User.
    /// </summary>
    public class UserRoute : Router
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router User Constructor.
        /// </summary>
        public UserRoute() : base() { }

        #endregion



        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router User Constructor.
        /// </summary>
        /// <param name="uri">A WPFWebServerRequestUrl object. The uri to route.</param>
        public UserRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to authenticate an User.
        /// </summary>
        /// <returns>A formated web server data response.</returns>
        public WebServerResponseData Authentication(NameValueCollection post)
        {
            log.Info($"Api WebServer Authentication : post => {post}.");

            try
            {
                //NameValueCollection nvc = HttpUtility.ParseQueryString(post);
                NameValueCollection nvc = post;

                log.Debug($"Api WebServer Authentication : Email => {nvc["email"]}.");
                log.Debug($"Api WebServer Authentication : Password => {nvc["email"].MD5Hash()}.");

                UserOptionsSelect options = new UserOptionsSelect
                {
                    Email = nvc["email"],
                    Password = nvc["password"].MD5Hash(),
                    CheckPassword = true,
                    Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                };

                UserEntity user = null;
                using (SQLiteSvc.Db.Context)
                {
                    user = Database.Users.SingleOrNull(options);
                }

                if (user == null)
                {
                    log.Info(string.Format("Api User Authentication : User not found [{0}].", post));

                    return ResponseNotAuth();
                }
                else
                {
                    // Generate sid
                    string sid = ("").GuidToBase64();

                    // Generate token
                    string stoken = ("").GuidToBase64();

                    Uri myUri = new Uri(Uri.AbsoluteUrl);

                    //Response.AddCookie("stoken", stoken);
                    Response.AddCookie("sid", sid + ":" + stoken, "/", myUri.Host);

                    HttpWebServerApplication.Session.Set(sid, "sid", stoken);
                    HttpWebServerApplication.Session.Set(sid, "UserId", user.UserId);

                    Content["Authentication"] = true;

                    return ResponseContentToJson();
                }
            }
            catch (Exception e)
            {
                log.Error("Api User Authentication failed !", e);
                return ResponseNotAuth();
            }
        }
    }
}