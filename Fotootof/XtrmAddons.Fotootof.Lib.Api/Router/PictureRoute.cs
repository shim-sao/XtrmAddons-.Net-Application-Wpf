using System;
using System.IO;
using WPFXA.Common.Classes.Configuration.Properties.Application;
using WPFXA.Common.Session;
using WPFXA.Net.HttpWebServer;
using WPFXA.PhotoAlbum.Server.SQLite.Database.Entities;

namespace WPFXA.PhotoAlbum.Server.Api.Router
{
    /// <summary>
    /// WPFXA PhotoAlbum Server Api Router Picture Route.
    /// </summary>
    public class PictureRoute : Router
    {
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
        /// Method to get a picture path.
        /// </summary>
        public string GetPictureAlbumPath(string path)
        {
            PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            string filename = Path.Combine(new string[]
                   {
                    props["Cache"],
                    "albums",
                    path
                   }
                );

            return filename;
        }

        /// <summary>
        /// Method to get a picture path.
        /// </summary>
        public string GetPictureCategoriesPath(string path)
        {
            PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            string filename = Path.Combine(new string[]
                   {
                    props["Cache"],
                    "categories",
                    path
                   }
                );

            return filename;
        }

        /// <summary>
        /// Method to get a picture path.
        /// </summary>
        public string GetPicturePath(string path)
        {
            PropertiesApplication props = (PropertiesApplication)AppSession.Get("SettingsApplication");

            return Path.Combine(new string[]
                {
                props["Cache"],
                "albums",
                path
                }
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebServerResponseData Get()
        {
            if (!IsAuth())
            {
                return ResponseNotAuth();
            }

            User user = GetAuthUser();
            Content["Authentication"] = true;

            try
            {
                // todo : check user in picture
                Picture item = SQLiteService.GetPicture(int.Parse(Uri.Params[0]), true);

                if (item == null)
                {
                    return ResponseError404("Picture not found or doesn't exists.");
                }
                string er = GetPicturePath(item.ThumbnailPath);
                switch ((string)Uri.Params[1])
                {
                    case "thumbnail":
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPicturePath(item.ThumbnailPath));
                        break;

                    default:
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPicturePath(item.PicturePath));
                        break;
                }

                return Response;
            }
            catch (Exception e)
            {
                log.Fatal("Error : Api picture image failed.");
                log.Fatal(e.Message);
                log.Fatal(e.StackTrace);

                return ResponseError500();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebServerResponseData Category()
        {
            if (!IsAuth())
            {
                return ResponseNotAuth();
            }

            User user = GetAuthUser();
            Content["Authentication"] = true;

            try
            {
                // todo : check user in folder
                Category category = SQLiteService.GetCategory(int.Parse(Uri.Params[0]), true);

                if (category == null)
                {
                    return ResponseError404("Category not found or doesn't exists.");
                }

                switch ((string)Uri.Params[1])
                {
                    case "thumbnail":
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPictureCategoriesPath(category.ThumbnailPath));
                        break;

                    default:
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPictureCategoriesPath(category.PicturePath));
                        break;
                }

                return Response;
            }
            catch (Exception e)
            {
                log.Fatal("Error : Api category image failed.");
                log.Fatal(e.Message);
                log.Fatal(e.StackTrace);

                return ResponseError500();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebServerResponseData Album()
        {
            if (!IsAuth())
            {
                return ResponseNotAuth();
            }

            User user = GetAuthUser();
            Content["Authentication"] = true;

            try
            {
                // todo : check user in folder
                Album album = SQLiteService.GetAlbum(int.Parse(Uri.Params[0]), true);

                if (album == null)
                {
                    string defaultImage = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        (string)AppSession.Get("properties.settings.default.AssetsImagesDefaultThumbnailPath")
                    );
                    Response.ServeFile(@defaultImage);

                    return Response;
                }
                
                switch ((string)Uri.Params[1])
                {
                    case "thumbnail":
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPictureAlbumPath(album.ThumbnailPath));
                        break;

                    default:
                        Response.SetContentType(Uri.RelativeUrl);
                        Response.ServeFile(GetPictureAlbumPath(album.PicturePath));
                        break;
                }

                return Response;
            }
            catch (Exception e)
            {
                log.Fatal("Error : Api album image failed.");
                log.Fatal(e.Message);
                log.Fatal(e.StackTrace);

                return ResponseError500();
            }
        }
    }
}