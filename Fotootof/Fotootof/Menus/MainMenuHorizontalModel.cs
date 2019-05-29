using Fotootof.HttpServer;
using Fotootof.Libraries.Controls;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using System;
using System.Windows.Controls;

namespace Fotootof.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Model.
    /// </summary>
    public class MainMenuHorizontalModel : ControlLayoutModel<MainMenuHorizontalLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable is Http server started.
        /// </summary>
        public bool isServerStarted;

        /// <summary>
        /// Variable <see cref="Grid"/> container for the plugin module.
        /// </summary>
        private Grid moduleContainer = new Grid();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="Grid"/> container for the plugin module.
        /// </summary>
        public Grid ModuleContainer
        {
            get => moduleContainer;
            set
            {
                if (moduleContainer != value)
                {
                    moduleContainer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to check if the Http server is started.
        /// </summary>
        public bool IsServerStarted
        {
            get => isServerStarted;
            set
            {
                if (isServerStarted != value)
                {
                    isServerStarted = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsServerStopped));
                }
            }
        }

        /// <summary>
        /// Property to check if the Http server is stopped.
        /// </summary>
        public bool IsServerStopped => !IsServerStarted;

        #endregion


        #region Constructor

        /// <summary>
        /// Class Fotootof Main Menu Horizontal Model Constructor.
        /// </summary>
        public MainMenuHorizontalModel()
        {
            HttpServerBase.NotifyServerStartedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            HttpServerBase.NotifyServerStoppedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            IsServerStarted = HttpWebServerApplication.IsStarted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlView"></param>
        public MainMenuHorizontalModel(MainMenuHorizontalLayout controlView) : base(controlView)
        {
            HttpServerBase.NotifyServerStartedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            HttpServerBase.NotifyServerStoppedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            IsServerStarted = HttpWebServerApplication.IsStarted;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to handle notification when theHttp server is started. 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/></param>
        private void HttpServerBase_NotifyServerHandlerOnce(object sender, EventArgs e)
        {
            IsServerStarted = HttpWebServerApplication.IsStarted;
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

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
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ModuleContainer = null;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.


                // Note disposing has been done.
                disposed = true;

                // Call base class implementation.
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}