using Fotootof.Libraries.Models;
using XtrmAddons.Net.Application;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Components.Server.Remote.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model.
    /// </summary>
    public class ServerInfosModel : ModelBase<ServerInfosLayout>
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
    }
}