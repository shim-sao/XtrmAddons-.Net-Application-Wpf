using System;
using System.Windows;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewLogs
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Views Logs Page.
    /// </summary>
    public partial class PageLogs : Page
    {
        #region Variables

        /// <summary>
        /// Variable logger.
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
        protected MainWindow AppWindow =>
            (MainWindow)Application.Current.MainWindow;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Views Logs Page Constructor.
        /// </summary>
        public PageLogs()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = AppWindow.ActualWidth - MargingWidth;
        }

        #endregion
    }
}
