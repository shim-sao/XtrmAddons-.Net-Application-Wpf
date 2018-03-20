using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.SQLiteService;
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
        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router User Constructor.
        /// </summary>
        public UserRoute() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router User Constructor.
        /// </summary>
        /// <param name="uri">A WPFWebServerRequestUrl object. The uri to route.</param>
        public UserRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to authenticate an User.
        /// </summary>
        /// <returns>A formated web server data response.</returns>
        public WebServerResponseData Authentication(string post)
        {
            log.Info(string.Format("Api User Authentication : post = {0}.", post));

            try
            {
                NameValueCollection nvc = HttpUtility.ParseQueryString(post);
                
                UserOptionsSelect options = new UserOptionsSelect
                {
                    Email = nvc["email"],
                    Password = nvc["password"],
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