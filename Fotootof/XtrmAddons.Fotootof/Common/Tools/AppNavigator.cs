using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Component.ClientSide.Views.ViewCatalog;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewBrowser;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewCatalog;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewSlideshow;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewUsers;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient;
using XtrmAddons.Net.Memory;

namespace XtrmAddons.Fotootof.Common.Tools
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server UI Navigation.</para>
    /// <para>Provide some page navigation management.</para>
    /// </summary>
    public static class AppNavigator
    {
        #region Properties

        /// <summary>
        /// Property cache dictionary container for pages.
        /// </summary>
        public static Dictionary<string, object> Pages { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Property access to the application main window.
        /// </summary>
        public static MainWindow MainWindow
            => (MainWindow)Application.Current.MainWindow;

        /// <summary>
        /// Property access to the main application frame.
        /// </summary>
        public static Frame MainFrame => MainWindow.Frame_Content;

        /// <summary>
        /// Property access to the main application text box logs stack.
        /// </summary>
        public static TextBlock LogsStack => MainWindow.BlockLogs.TextBlockLogsStack;

        #endregion Properties



        #region Methods

        /// <summary>
        /// Method to clear visited pages.
        /// </summary>
        public static void Clear()
            => Pages.Clear();

        /// <summary>
        /// Method to load a page.
        /// </summary>
        /// <param name="key">The key of the page to load.</param>
        /// <param name="page">The page to load.</param>
        /// <param name="clear">The page to navigate.</param>
        public static void LoadPage(string key, Page page, bool clear = true)
        {
            // Clear pages cache if required.
            if (clear)
            {
                Clear();
            }

            // Create page cache if required.
            if (!Pages.ContainsKey(key))
            {
                Pages.Add(key, page);
            }

            // Navigate to the page.
            Navigate((Page)Pages[key]);

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }


        /// <summary>
        /// Method to navigate to a page.
        /// <param name="page">The page to navigate.</param>
        /// </summary>
        public static void Navigate(Page page)
        {
            // Navigate to the page.
            MainFrame.Navigate(page);

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }

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
