using Fotootof.Plugin.Api.Helpers;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace Fotootof.Plugin.Api.Router
{
    /// <summary>
    /// WPFXA PhotoAlbum Server Api Router Picture Route.
    /// </summary>
    public class PictureRoute : Router
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Picture Route constructor.
        /// </summary>
        public PictureRoute() : base() { }

        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Picture Route constructor.
        /// </summary>
        /// <param name="uri"></param>
        public PictureRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to get a list of associated Sections of an User.
        /// </summary>
        /// <returns>WPF Web Server response data of list of Sections.</returns>
        public override WebServerResponseData Index()
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            log.Info($"{MethodBase.GetCurrentMethod().Name} : Serving Picture request. Please wait...");

            if (!IsAuth())
            {
                log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response user not auth.");
                return ResponseNotAuth();
            }

            try
            {
                var a = Uri.QueryString;

                // Check if request params are correct.
                if (Uri.QueryString.Count == 0)
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : The Request is empty.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response => Picture not found or doesn't exists.");
                    return ResponseError404("Picture not found or doesn't exists.");
                }

                // Check if request params are correct.
                if (!Uri.QueryString.Keys.Cast<string>().Contains("id"))
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Request param [id] not found.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response => Picture not found or doesn't exists.");
                    return ResponseError404("Picture not found or doesn't exists.");
                }

                int id = int.Parse(Uri.QueryString["id"]);
                string type = Uri.QueryString["type"];

                // Get user and dependencies.
                UserEntity user = GetAuthUser();
                PictureEntity entity = RouteHelper.GetUserPicture(id, user);

                var aa = entity;

                // Check if request params are correct.
                if (entity == null)
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Request Picture not found.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response => Picture not found or doesn't exists.");
                    return ResponseError404("Picture not found or doesn't exists.");
                }

                Response = new WebServerResponseData(Uri.AbsoluteUrl);

                if (File.Exists(entity.PicturePath))
                {
                    Response.ServeFileUnSafe(entity.PicturePath);
                }
                else
                {
                    // Convert the image to byte[]
                    MemoryStream stream = new MemoryStream();
                    Properties.Images.error_404.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = stream.ToArray();

                    // Add the bytes to the response.
                    Response.SetContentType("error-404.png");
                    Response.ContentAppend(imageBytes);
                }
                
                return Response;
            }
            catch (Exception e)
            {
                log.Fatal($"{MethodBase.GetCurrentMethod().Name} : Serving picture request failed.", e);
                return ResponseError500();
            }


        }

            ///// <summary>
            ///// Method to get a picture path.
            ///// </summary>
            //public string GetPictureAlbumPath(string path)
            //{
            //    PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            //    string filename = Path.Combine(new string[]
            //           {
            //            props["Cache"],
            //            "albums",
            //            path
            //           }
            //        );

            //    return filename;
            //}

            ///// <summary>
            ///// Method to get a picture path.
            ///// </summary>
            //public string GetPictureCategoriesPath(string path)
            //{
            //    PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            //    string filename = Path.Combine(new string[]
            //           {
            //            props["Cache"],
            //            "categories",
            //            path
            //           }
            //        );

            //    return filename;
            //}

            ///// <summary>
            ///// Method to get a picture path.
            ///// </summary>
            //public string GetPicturePath(string path)
            //{
            //    PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            //    return Path.Combine(new string[]
            //        {
            //        props["Cache"],
            //        "albums",
            //        path
            //        }
            //    );
            //}

            ///// <summary>
            ///// 
            ///// </summary>
            ///// <returns></returns>
            //public WebServerResponseData Get()
            //{
            //    if (!IsAuth())
            //    {
            //        return ResponseNotAuth();
            //    }

            //    User user = GetAuthUser();
            //    Content["Authentication"] = true;

            //    try
            //    {
            //        // todo : check user in picture
            //        Picture item = SQLiteService.GetPicture(int.Parse(Uri.Params[0]), true);

            //        if (item == null)
            //        {
            //            return ResponseError404("Picture not found or doesn't exists.");
            //        }
            //        string er = GetPicturePath(item.ThumbnailPath);
            //        switch ((string)Uri.Params[1])
            //        {
            //            case "thumbnail":
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPicturePath(item.ThumbnailPath));
            //                break;

            //            default:
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPicturePath(item.PicturePath));
            //                break;
            //        }

            //        return Response;
            //    }
            //    catch (Exception e)
            //    {
            //        log.Fatal("Error : Api picture image failed.");
            //        log.Fatal(e.Message);
            //        log.Fatal(e.StackTrace);

            //        return ResponseError500();
            //    }
            //}

            ///// <summary>
            ///// 
            ///// </summary>
            ///// <returns></returns>
            //public WebServerResponseData Category()
            //{
            //    if (!IsAuth())
            //    {
            //        return ResponseNotAuth();
            //    }

            //    User user = GetAuthUser();
            //    Content["Authentication"] = true;

            //    try
            //    {
            //        // todo : check user in folder
            //        Category category = SQLiteService.GetCategory(int.Parse(Uri.Params[0]), true);

            //        if (category == null)
            //        {
            //            return ResponseError404("Category not found or doesn't exists.");
            //        }

            //        switch ((string)Uri.Params[1])
            //        {
            //            case "thumbnail":
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPictureCategoriesPath(category.ThumbnailPath));
            //                break;

            //            default:
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPictureCategoriesPath(category.PicturePath));
            //                break;
            //        }

            //        return Response;
            //    }
            //    catch (Exception e)
            //    {
            //        log.Fatal("Error : Api category image failed.");
            //        log.Fatal(e.Message);
            //        log.Fatal(e.StackTrace);

            //        return ResponseError500();
            //    }
            //}

            ///// <summary>
            ///// 
            ///// </summary>
            ///// <returns></returns>
            //public WebServerResponseData Album()
            //{
            //    if (!IsAuth())
            //    {
            //        return ResponseNotAuth();
            //    }

            //    User user = GetAuthUser();
            //    Content["Authentication"] = true;

            //    try
            //    {
            //        // todo : check user in folder
            //        Album album = SQLiteService.GetAlbum(int.Parse(Uri.Params[0]), true);

            //        if (album == null)
            //        {
            //            string defaultImage = Path.Combine(
            //                AppDomain.CurrentDomain.BaseDirectory,
            //                (string)AppSession.Get("properties.settings.default.AssetsImagesDefaultThumbnailPath")
            //            );
            //            Response.ServeFile(@defaultImage);

            //            return Response;
            //        }

            //        switch ((string)Uri.Params[1])
            //        {
            //            case "thumbnail":
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPictureAlbumPath(album.ThumbnailPath));
            //                break;

            //            default:
            //                Response.SetContentType(Uri.RelativeUrl);
            //                Response.ServeFile(GetPictureAlbumPath(album.PicturePath));
            //                break;
            //        }

            //        return Response;
            //    }
            //    catch (Exception e)
            //    {
            //        log.Fatal("Error : Api album image failed.");
            //        log.Fatal(e.Message);
            //        log.Fatal(e.StackTrace);

            //        return ResponseError500();
            //    }
            //}
        }
    }