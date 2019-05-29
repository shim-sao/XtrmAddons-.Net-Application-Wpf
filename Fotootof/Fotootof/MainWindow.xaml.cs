using Fotootof.Components.Server.Logs;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Logs.Log4net;
using Fotootof.SQLite.Services;
using Fotootof.Theme;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.NotifyIcons;

namespace Fotootof
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Main Window.</para>
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable memory log watcher <see cref="MemoryLogWatcher"/> for log4net memory appender management.
        /// </summary>
        private static readonly MemoryLogWatcher logWatcher = new MemoryLogWatcher();

        /// <summary>
        /// Variable to store logs page <see cref="PageLogsLayout"/>.
        /// </summary>
        private static readonly PageLogsLayout pageLogs = new PageLogsLayout();

        private bool blnLeftDown = false;
        Point pStart = new Point();


        //private int nextWaiting = 0;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the logs page.
        /// </summary>
        public PageLogsLayout BlockLogs => pageLogs;

        /// <summary>
        /// Property alias to the busy indicator <see cref="Xceed.Wpf.Toolkit.BusyIndicator"/>.
        /// </summary>
        public Xceed.Wpf.Toolkit.BusyIndicator BusyIndicator => XCTKBusyIndicator;

        /// <summary>
        /// Property to access to the SQLite Service.
        /// </summary>
        [System.Obsolete("Use SQLiteSvc.GetInstance()")]
        public static SQLiteSvc Database
        {
            get => ApplicationSession.Properties.Database;
            set => ApplicationSession.Properties.Database = value;
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Main Window Constructor.
        /// </summary>
        public MainWindow()
        {
            // Merge dynamic custom 
            ThemeLoader.MergeThemeTo(Resources, false);

            // Initialize window component.
            InitializeComponent();

            log.Info(Local.Properties.Logs.InitializingApplicationWindowComponent);

            // Main Window to application session.
            ApplicationSession.Properties.MainWindow = this;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize application content asynchronously.
        /// </summary>
        private async Task InitializeContentAsync()
        {
            await Task.Delay(5000).ConfigureAwait(true);

            // Assigned page frames.
            FrameBlockLogsName.Navigate(BlockLogs);
            //Frame_Content.Navigate(new PageBrowser());

            // Adjust frame logs content on resize. 
            SizeChanged += BlockLogs.Page_SizeChanged;

            // Show frame logs according to the preferences.
            if (Settings.Controls.Default.MainMenuMenuItemDisplayLogsIsChecked)
            {
                ToggleLogs();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BlockContentTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        private void TabPlusLabel_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                //TabControl tc = (TabControl)sender;
                //int index = tc.SelectedIndex;

                //if (index != -1)
                //{
                //    if (IsTabPlus(tc.SelectedItem as TabItem) && !blnLeftDown)
                //    {
                //        TabItem tiplus = (TabItem)tc.SelectedItem;
                //        int newIndex = index + 1;

                //        Label lbl = new Label();
                //        lbl.Content = "New Tab #" + newIndex.ToString();

                //        lbl.MouseLeftButtonDown += TabItemHeaderLabel_MouseLeftButtonDown;
                //        lbl.MouseLeftButtonUp += TabItemHeaderLabel_MouseLeftButtonUp;
                //        lbl.MouseLeave += TabItemHeaderLabel_MouseLeave;
                //        lbl.MouseEnter += lTabItemHeaderLabel_MouseEnter;

                //        TabItem ti = new TabItem
                //        {
                //            Name = "NewTab" + newIndex.ToString(),
                //            Header = lbl,
                //            Content = new Frame
                //            {
                //                Name = "Frame_Content" + newIndex.ToString(),
                //                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                //                HorizontalAlignment = HorizontalAlignment.Stretch,
                //                VerticalAlignment = VerticalAlignment.Stretch
                //            }
                //        };

                //        tc.Items.Remove(tiplus);
                //        tc.Items.Add(ti);
                //        tc.Items.Add(tiplus);
                //    }
                //}

                TabControl tc = (TabControl)FindName("BlockContentTabs");
                TabItem tiplus = (TabItem)FindName("TabPlus");
                int newIndex = tc?.Items?.Count - 1 ?? 0;

                Label lbl = new Label();
                lbl.Content = "New Tab #" + newIndex.ToString();

                lbl.MouseLeftButtonDown += TabItemHeaderLabel_MouseLeftButtonDown;
                lbl.MouseLeftButtonUp += TabItemHeaderLabel_MouseLeftButtonUp;
                lbl.MouseLeave += TabItemHeaderLabel_MouseLeave;
                lbl.MouseEnter += lTabItemHeaderLabel_MouseEnter;

                TabItem ti = new TabItem
                {
                    Name = "NewTab" + newIndex.ToString(),
                    Header = lbl,
                    Content = new Frame
                    {
                        Name = "Frame_Content" + newIndex.ToString(),
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    }
                };

                tc.Items.Remove(tiplus);
                tc.Items.Add(ti);
                tc.Items.Add(tiplus);

            }
            catch (Exception ex)
            {
                log.Error(ex.Output());
                MessageBoxs.Error(ex.Output());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ti">A <see cref="TabItem"/> of the main content <see cref="TabControl"/>.</param>
        private bool IsTabPlus(TabItem ti)
        {
            return ti?.Name as string == "TabPlus" && ti?.Tag as string == "TabPlus";
        }

        ///// <summary>
        ///// Method to initialize application content asynchronously.
        ///// </summary>
        //private async Task InitializeContentAsync()
        //{
        //    await Task.Delay(10);

        //    // Assigned page frames.
        //    FrameBlockLogsName.Navigate(BlockLogs);
        //    //Frame_Content.Navigate(new PageBrowser());

        //    // Adjust frame logs content on resize. 
        //    SizeChanged += BlockLogs.Page_SizeChanged;

        //    // Show frame logs according to the preferences.
        //    if (Settings.Controls.Default.MainMenuMenuItemDisplayLogsIsChecked)
        //    {
        //        ToggleLogs();
        //    }
        //}

        #endregion



        #region Methods Windows Events

        /// <summary>
        /// Method called on <see cref="Window"/> load event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            //await Task.Delay(5000).ConfigureAwait(true); ;

            // Add application to system tray.
            NotifyIconManager.AddToTray();
            log.Info(Local.Properties.Logs.ApplicationToSystemTray);

            // Add application log watcher event handler.
            AppLogger.UpdateLogTextbox(logWatcher.LogContent);
            logWatcher.LogContent = "";
            logWatcher.Updated += LogWatcher_Updated;

            // Initialize window content.
            await InitializeContentAsync().ConfigureAwait(false);

            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on <see cref="Window"/> closing event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The cancel event arguments <see cref="CancelEventArgs"/>.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            NotifyIconManager.Dispose();
            log.Info(string.Format(Local.Properties.Logs.WindowClosing, GetType().Name));
        }

        /// <summary>
        /// Method called on file exit click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The executed routed event arguments <see cref="ExecutedRoutedEventArgs"/>.</param>
        private void FileExit_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Method called on <see cref="Window"/> size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
#if DEBUG_SIZE
            FrameworkElement fe = FindName("BlockContent") as FrameworkElement;
            Trace.TraceInformation("-------------------------------------------------------------------------------------------------------");
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            Trace.TraceInformation("MainWindow.ActualSize = [" + ActualWidth + "," + ActualHeight + "]");
            Trace.TraceInformation("fe.ActualSize = [" + fe.ActualWidth + "," + fe.ActualHeight + "]");
            Trace.TraceInformation("Frame_Content.ActualSize = [" + Frame_Content.ActualWidth + "," + Frame_Content.ActualHeight + "]");
            Trace.TraceInformation("RowGridMain.Height = [" + RowGridMain.Height + "]");
            Trace.TraceInformation("-------------------------------------------------------------------------------------------------------");
#endif
        }

        #endregion


        #region Methods Logs Watcher

        /// <summary>
        /// Method to watch logs for console and application page logs.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Event arguments <see cref="EventArgs"/>.</param>
        public void LogWatcher_Updated(object sender, EventArgs e)
        {
            AppLogger.UpdateLogTextbox(logWatcher.LogContent);
            logWatcher.LogContent = "";
        }

        /// <summary>
        /// Method to toggle logs frame.
        /// </summary>
        public void ToggleLogs()
        {
            // Set the row grid splitter Height.
            RowGridSplitter.Height =
                RowGridSplitter.Height == new GridLength(0)
                ? new GridLength(6) : new GridLength(0);

            // Set the grid row logs height.
            RowGridLogs.Height =
                RowGridLogs.Height == new GridLength(0)
                ? new GridLength(Settings.Controls.Default.MainWindowRowGridLogs) : new GridLength(0);
        }

        #endregion





        private void TabItemHeaderLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pStart = e.GetPosition((TabControl)FindName("BlockContentTabs"));
            blnLeftDown = true;
        }

        void TabItemHeaderLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            blnLeftDown = false;
        }


        void lTabItemHeaderLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (blnLeftDown)
            {
                pStart = e.GetPosition((TabControl)FindName("BlockContentTabs"));
            }
        }

        void TabItemHeaderLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            var tabControl = (TabControl)FindName("BlockContentTabs");

            Label lbl = (Label)sender;
            Point pLeave = e.GetPosition(tabControl);
            var pDelta = pLeave - pStart;
            // if ".Y" changes less then width of label calculate direction base on ".X"

            int iS = tabControl.SelectedIndex;
            int iCount = tabControl.Items.Count-1;

            if (blnLeftDown)
            {
                if (Math.Abs(pDelta.Y) <= lbl.ActualHeight / 3.0)      // Mouse MUST leave label from left or right side 
                {
                    bool blnDirection = false;             // move right  
                    if (pDelta.X < 0) blnDirection = true; // move left  ; (mosuse moved from left side)

                    if (blnDirection && iS > 0)  // move left
                    {
                        var temp = tabControl.SelectedItem;
                        tabControl.Items.RemoveAt(iS);
                        tabControl.Items.Insert(iS - 1, temp);
                        tabControl.SelectedIndex = iS - 1;

                    }

                    if (!blnDirection && iS < iCount - 1)  // move right - if last one do not move also...
                    {
                        var temp = tabControl.SelectedItem;
                        tabControl.Items.RemoveAt(iS);
                        tabControl.Items.Insert(iS + 1, temp);
                        tabControl.SelectedIndex = iS + 1;

                    }
                }
                else
                {
                    // More information about "DragDrop" see below links ...

                    TabItem ti = tabControl.Items[iS] as TabItem;
                    if (ti != null && e.LeftButton == MouseButtonState.Pressed && !IsTabPlus(ti))
                    {
                        DataObject data = new DataObject();
                        data.SetData(ti.Name, ti);

                        DragDropEffects effect = DragDrop.DoDragDrop(this, data, DragDropEffects.All);

                    }

                    blnLeftDown = false;
                }
            }
        }
    }
}