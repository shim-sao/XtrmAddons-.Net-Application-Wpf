using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Memory;

namespace Fotootof.Navigator
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
        /// Property access to the main application selected tab.
        /// </summary>
        public static TabItem SelectedTabItem => GetSelectedTabItem();

        /// <summary>
        /// Property access to the main application selected frame.
        /// </summary>
        public static Frame SelectedFrame => GetSelectedFrame();

        /// <summary>
        /// Property access to the main application text box logs stack.
        /// </summary>
        public static TextBlock LogsStack
            => MainWindow.GetPropertyValue<Page>("BlockLogs").FindName("TextBlockLogsName") as TextBlock;

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
        /// Method to get the selected tab in the main window.
        /// </summary>
        /// <returns>The selected tab in the main window or null if none is selected.</returns>
        private static TabItem GetSelectedTabItem()
        {
            try
            {
                TabControl tabs = ((MainWindow as Window).FindName("BlockContentTabs") as TabControl);
                int index = tabs.SelectedIndex;

                if (index == -1)
                {
                    return null;
                }

                return tabs.SelectedItem as TabItem;
            }
            catch (Exception e)
            {
                log.Error(e.Output());
                MessageBoxs.Warning("Please selected a tab before continue.");
            }

            return null;
        }

        /// <summary>
        /// Method to get the selected tab in the main window.
        /// </summary>
        /// <returns>The selected tab content in the main window or null if none is selected.</returns>
        private static Frame GetSelectedFrame()
        {
            try
            {
                TabControl tabs = ((MainWindow as Window).FindName("BlockContentTabs") as TabControl);
                int index = tabs.SelectedIndex;

                if (index == -1)
                {
                    return null;
                }

                return tabs.SelectedContent as Frame;
            }
            catch (Exception e)
            {
                log.Error(e.Output());
                MessageBoxs.Warning("Please selected a tab before continue.");
            }

            return null;
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
            try
            {
                if (SelectedFrame == null)
                {
                    throw Exceptions.GetReferenceNull(nameof(page), typeof(Page));
                }

                // Navigate to the page on the selected tab.
                ((Label)SelectedTabItem.Header).Content = page.Title;

                if(SelectedFrame == null)
                {
                    throw Exceptions.GetReferenceNull(nameof(SelectedFrame), typeof(Frame));
                }

                SelectedFrame.Navigate(page);

                // Fix increase memory.
                MemoryManager.fixMemoryLeak();
                //GC.Collect();
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        #endregion
    }
}
