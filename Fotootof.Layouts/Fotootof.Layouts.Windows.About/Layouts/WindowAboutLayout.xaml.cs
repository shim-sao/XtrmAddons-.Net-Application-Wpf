using Fotootof.Theme;
using System;
using System.Windows;

namespace Fotootof.Layouts.Windows.About
{
    /// <summary>
    /// Class Fotootof Layouts Windows About.
    /// </summary>
    public partial class WindowAboutLayout : Window, IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model <see cref="WindowAboutModel"/>.
        /// </summary>
        public WindowAboutModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Layouts Windows About Constructor.
        /// </summary>
        public WindowAboutLayout()
        {
            ThemeLoader.MergeThemeTo(Resources);

            InitializeComponent();
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="Window"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add data model to the window data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize window data model <see cref="WindowAboutModel"/>.
        /// </summary>
        protected void InitializeModel()
        {
            Model = new WindowAboutModel(this);
        }

        /// <summary>
        /// Method called on <see cref="System.Windows.Controls.Button"/> ok click event to close the <see cref="Window"/>.  
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Method to dispose the Window layout.
        /// </summary>
        public void Dispose()
        {
            Model = null;
        }

        #endregion
    }
}
