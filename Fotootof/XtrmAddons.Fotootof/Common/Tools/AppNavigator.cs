using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Component.ClientSide.Views.ViewCatalog;
//using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewBrowser;
using FotootofComponent.Browser.Views;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewCatalog;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewSlideshow;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewUsers;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;

namespace XtrmAddons.Fotootof.Common.Tools
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server UI Navigation.</para>
    /// <para>Provide some page navigation management.</para>
    /// </summary>
    public class AppNavigator : AppNavigatorBase
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        #region Methods

        /// <summary>
        /// Method to navigate to the Album page.
        /// </summary>
        public static void NavigateToPageAlbumServer(int albumId) 
            => Navigate(new Component.ServerSide.Views.ViewAlbum.PageAlbum(albumId));

        /// <summary>
        /// Method to navigate to Album page.
        /// </summary>
        public static void NavigateToPageCatalogClient(ClientHttp server) 
            => Navigate(new PageCatalogClient(server));

        /// <summary>
        /// Method to navigate to Browser page.
        /// </summary>
        public static void NavigateToPageBrowser() 
            => Navigate(new PageBrowser());

        /// <summary>
        /// Method to navigate to the Server page.
        /// </summary>
        public static void NavigateToPageServer() 
            => Navigate(new PageServer());

        /// <summary>
        /// Method to navigate to User page.
        /// </summary>
        public static void NavigateToPageUsers() 
            => Navigate(new PageUsers());

        /// <summary>
        /// Method to navigate to the Sections page.
        /// </summary>
        public static void NavigateToPageCatalog()
            => Navigate(new PageCatalog());

        /// <summary>
        /// Method to navigate to the Slideshow Server page.
        /// </summary>
        public static void NavigateToPageSlideshowServer(PictureEntityCollection collection)
            => Navigate(new PageSlideshow(collection));

        /// <summary>
        /// Method to navigate to the Slideshow Server page.
        /// </summary>
        public static void NavigateToPageSlideshowServer(int albumPk)
            => Navigate(new PageSlideshow(albumPk));

        /// <summary>
        /// Method to navigate to the Plugin Server page.
        /// </summary>
        public static void NavigateToPagePluginServer(UserControl uc)
            => Navigate(new Component.ServerSide.Views.ViewPlugin.PagePlugin(uc));

        #endregion Methods
    }
}
