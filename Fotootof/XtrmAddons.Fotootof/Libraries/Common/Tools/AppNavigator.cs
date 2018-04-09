using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Memory;
using XtrmAddons.Fotootof.Component.ClientSide.ViewCatalog;
using XtrmAddons.Fotootof.Component.ServerSide.ViewAlbum;
using XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser;
using XtrmAddons.Fotootof.Component.ServerSide.ViewCatalog;
using XtrmAddons.Fotootof.Component.ServerSide.ViewServer;
using XtrmAddons.Fotootof.Component.ServerSide.ViewUsers;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient;

namespace XtrmAddons.Fotootof.Libraries.Common.Tools
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
        public static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        /// <summary>
        /// Property access to the main application frame.
        /// </summary>
        public static Frame MainFrame => MainWindow.Frame_Content;

        /// <summary>
        /// Property access to the main application text box logs stack.
        /// </summary>
        public static TextBlock LogsStack => MainWindow.LogsStack;

        #endregion Properties



        #region Methods

        /// <summary>
        /// Method to clear visited pages.
        /// </summary>
        public static void Clear()
            => Pages.Clear();

        /// <summary>
        /// Method to load a page.
        /// <paramref name="key"/>The key of the page to load.</param>
        /// <paramref name="page"/>The page to load.</param>
        /// <paramref name="clear"/>The page to navigate.</param>
        /// </summary>
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
        /// <paramref name="page"/>The page to navigate.</param>
        /// </summary>
        public static void Navigate(Page page)
        {
            // Navigate to the page.
            MainFrame.Navigate(page);

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }

        /// <summary>
        /// Method to navigate to Album page.
        /// </summary>
        public static void NavigateToPageAlbum(int albumId) 
            => Navigate(new PageAlbum(albumId));

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
        /// Method to navigate to Control Panel page.
        /// </summary>
        public static void NavigateToPageServer() 
            => Navigate(new PageServer());

        /// <summary>
        /// Method to navigate to User page.
        /// </summary>
        public static void NavigateToPageUsers() 
            => Navigate(new PageUsers());

        /// <summary>
        /// Method to navigate to Sections page.
        /// </summary>
        public static void NavigateToPageCatalog()
            => Navigate(new PageCatalog());

        #endregion Methods
    }
}
