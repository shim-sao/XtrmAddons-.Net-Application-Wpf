using System;
using WPFXA.Net.HttpWebServer;
using WPFXA.PhotoAlbum.Server.SQLite.Database.Entities;

namespace WPFXA.PhotoAlbum.Server.Api.Router
{
    /// <summary>
    /// WPFXA PhotoAlbum Server Api Router Index Route.
    /// </summary>
    public class CategoryRoute : Router
    {
        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Index Route constructor.
        /// </summary>
        public CategoryRoute() : base() { }

        /// <summary>
        /// WPFXA PhotoAlbum Server Api Router Index Route constructor.
        /// </summary>
        /// <param name="uri"></param>
        public CategoryRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WebServerResponseData Get()
        {
            log.Info("Api : Serving category request. Please wait !");

            if (!IsAuth())
            {
                log.Info("Api : Return response user not auth.");
                return ResponseNotAuth();
            }
            
            try
            {
                // Check if request params are correct.
                if (Uri.Params.Length == 0)
                {
                    log.Info("Api : Params in the request are empty.");
                    log.Info(Uri);
                    log.Info("Api : Return response Category not found or doesn't exists.");
                    return ResponseError404("Category not found or doesn't exists.");
                }


                // Get user and dependencies.
                User user = GetAuthUser();

                // Load category
                // Ensure to select dependencies.
                Category category = SQLiteService.GetCategory(int.Parse(Uri.Params[0]), true);

                if (category == null)
                {
                    log.Info("Api : Params in the request are empty.");
                    log.Info(Uri);
                    log.Info("Api : Return response Category not found or doesn't exists.");
                    return ResponseError404("Category not found or doesn't exists.");
                }

                // Try to find categories in user dependencies.
                User userDep = category.Users.Find(c => c.UserId == user.UserId);

                // Check if category is associated to user
                // If not, check if category is in associated folder to user.
                if (userDep == null)
                {
                    Category folderDep = null;
                    foreach (Folder f in user.Folders)
                    {
                        Folder folder = SQLiteService.GetFolder(f.FolderId, true);
                        folderDep = folder.Categories.Find(c => c.CategoryId == category.CategoryId);

                        if (folderDep != null)
                            break;
                    }

                    if (folderDep == null)
                    {
                        return ResponseError404("Category not found or doesn't exists.");
                    }
                }
                
                // Format and return response.
                Content["Authentication"] = true;
                Content["Response"] = ConvertJsonAuthCategory(category);

                return ResponseContentToJson();
            }
            catch (Exception e)
            {
                log.Fatal("ERROR : Api list category album failed");
                log.Fatal(string.Format("Error : {0}", e.Message));
                log.Fatal(e.Source);
                log.Fatal(e.StackTrace);

                return ResponseError500();
            }
        }
    }
}