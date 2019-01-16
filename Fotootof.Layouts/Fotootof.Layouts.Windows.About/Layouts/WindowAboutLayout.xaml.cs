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
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Track whether Dispose has been called.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    Model = null;
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.


                // Note disposing has been done.
                disposed = true;

            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~WindowAboutLayout()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
