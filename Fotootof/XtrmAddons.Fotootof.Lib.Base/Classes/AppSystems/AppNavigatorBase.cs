using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Memory;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server UI Navigation.</para>
    /// <para>Provide some page navigation management.</para>
    /// </summary>
    public abstract class AppNavigatorBase
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Properties

        /// <summary>
        /// Property cache dictionary container for pages.
        /// </summary>
        public static Dictionary<string, object> Pages { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Property access to the application main window.
        /// </summary>
        public static object MainWindow => Application.Current.MainWindow;

        /// <summary>
        /// Property access to the main application frame.
        /// </summary>
        public static Frame MainFrame => (MainWindow as Window).FindName("Frame_Content") as Frame;

        /// <summary>
        /// Property access to the main application text box logs stack.
        /// </summary>
        public static TextBlock LogsStack
            => MainWindow.GetPropertyValue<FrameworkElement>("BlockLogs").FindName("TextBlockLogsName") as TextBlock;

        #endregion Properties



        #region Methods

        /// <summary>
        /// Method to clear visited pages.
        /// </summary>
        public static void Clear()
        {
            Pages.Clear();
            log.Debug("Application pages navigator cleared !");
        }

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

        #endregion Methods
    }
}
