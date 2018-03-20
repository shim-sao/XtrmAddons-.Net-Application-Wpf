using System.Windows;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Component.Logs.Pages
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Logs Page.
    /// </summary>
    public partial class PageLogs : Page
    {
        #region Properties

        /// <summary>
        /// Variable page width marging for content adjustement on size changed.
        /// </summary>
        protected double MargingWidth = 55;

        #endregion


        #region Properties

        /// <summary>
        /// Accessors to the application main window.
        /// </summary>
        protected MainWindow AppWindow
        {
            get
            {
                return (MainWindow)Application.Current.MainWindow;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Logs Page Constructor.
        /// </summary>
        public PageLogs()
        {
            // Initialize page component.
            InitializeComponent();

            // Method called for size adjustement.
            SizeChanged += Window_SizeChanged;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = AppWindow.ActualWidth - MargingWidth;
        }

        #endregion
    }
}
