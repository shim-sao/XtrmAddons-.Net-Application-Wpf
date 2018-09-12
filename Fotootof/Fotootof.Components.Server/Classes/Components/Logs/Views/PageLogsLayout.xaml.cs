using Fotootof.Theme;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Server.Logs
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Views Logs Page.
    /// </summary>
    public partial class PageLogsLayout : Page
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable page width marging for content adjustement on size changed.
        /// </summary>
        protected double MargingWidth = 17;

        #endregion



        #region Properties

        /// <summary>
        /// Accessors to the application main window.
        /// </summary>
        protected Window AppWindow => Application.Current.MainWindow;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Views Logs Page Constructor.
        /// </summary>
        public PageLogsLayout()
        {
            ThemeLoader.MergeThemeTo(Resources);

            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = Math.Max(AppWindow.ActualWidth - MargingWidth, 0);
        }

        #endregion
    }
}
