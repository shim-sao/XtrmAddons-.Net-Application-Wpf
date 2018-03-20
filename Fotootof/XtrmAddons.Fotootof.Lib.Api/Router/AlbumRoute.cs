using System;
using WPFXA.Net.HttpWebServer;
using WPFXA.PhotoAlbum.Server.Api.Models.Json;
using WPFXA.PhotoAlbum.Server.SQLite.Database.Entities;

namespace WPFXA.PhotoAlbum.Server.Api.Router
{
    /// <summary>
    /// WPFXA PhotoAlbum Server Api Router Album Route.
    /// </summary>
    public class AlbumRoute : Router
    {
        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Album Route constructor.
        /// </summary>
        public AlbumRoute() : base() { }

        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Album Route constructor.
        /// </summary>
        /// <param name="uri"></param>
        public AlbumRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebServerResponseData Get()
        {
            log.Info("Api : Serving Album request. Please wait !");

            if (!IsAuth())
            {
                log.Info("Api : Return response user not auth.");
                return ResponseNotAuth();
            }

            Content["Authentication"] = true;

            try
            {
                int cid = int.Parse(Uri.Params[0]);
                User user = GetAuthUser();

                Album item = null;
                Category parent = null;
                Folder root = null;

                log.Info("Api : Searching for album in user association. Please wait !");
                item = user.Albums.Find(a => a.AlbumId == cid);

                if (item == null)
                {
                    log.Info("Api : Searching for album in category association. Please wait !");
                    foreach (Category c in user.Categories)
                    {
                        parent = SQLiteService.GetCategory(c.CategoryId, true);
                        item = parent.Albums.Find(a => a.AlbumId == cid);

                        if (item != null)
                        {
                            break;
                        }
                    }
                }

                if (item == null)
                {
                    log.Info("Api : Searching for album in folder association. Please wait !");
                    foreach (Folder c in user.Folders)
                    {
                        root = SQLiteService.GetFolder(c.FolderId, true);
                        item = root.Albums.Find(a => a.AlbumId == cid);

                        if (item != null)
                        {
                            break;
                        }
                    }
                }

                
                if (item == null)
                {
                    log.Info("Api : Searching for album in folders association. Please wait !");
                    item = SQLiteService.GetAlbum(cid, true);

                    foreach (Category c in item.Categories)
                    {
                        parent = SQLiteService.GetCategory(c.CategoryId, true);

                        foreach (Folder f in parent.Folders)
                        {
                            root = user.Folders.Find(fiu => fiu.FolderId == f.FolderId);

                            if (root != null)
                            {
                                break;
                            }
                        }
                    }

                    if (root == null)
                    {
                        log.Info("Api : Folder root not founded. Return response user not auth.");
                        return ResponseNotAuth();
                    }
                }

                if (item == null)
                {
                    log.Error("Api : Album not found or doesn't exists.");
                    return ResponseError404("Album not found or doesn't exists.");
                }
                

                // Ensure to select dependencies.
                item = SQLiteService.GetAlbum(item.AlbumId, true);

                foreach (Picture a in item.Pictures)
                {
                    a.PicturePath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/get/" + a.PictureId + "/picture?sid=" + CookieString;
                    a.ThumbnailPath = "http://" + Uri.Host + ":" + Uri.Port + "/api/picture/get/" + a.PictureId + "/thumbnail?sid=" + CookieString;
                }

                AlbumJson itemJson = new AlbumJson(item, true);
                Content["Response"] = itemJson;

                return ResponseContentToJson();
            }
            catch (Exception e)
            {
                log.Fatal("ERROR : Api album details failed");
                log.Fatal(e.Message);
                log.Fatal(e.StackTrace);

                return ResponseError500();
            }
        }
    }
}