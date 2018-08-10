using Fotootof.Libraries.Controls;
using XtrmAddons.Net.Application;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Components.Server.Remote.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model.
    /// </summary>
    public class ServerInfosModel : ControlLayoutModel<ServerInfosLayout>
    {
        #region Variables

        /// <summary>
        /// Variable Server.
        /// </summary>
        public ServerData server;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Server.
        /// </summary>
        public ServerData Server
        {
            get { return server; }
            set
            {
                server = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model Constructor.
        /// </summary>
        public ServerInfosModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public ServerInfosModel(ServerInfosLayout controlView) : base(controlView)
        {
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected void InitializeModel()
        {
            Server = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();
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
        protected new void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    Server = null;
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