using XtrmAddons.Fotootof.Lib.Base.Classes.Models;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.Servers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model.
    /// </summary>
    public class ServerInfosModel<ServerInfos> : ModelBase<ServerInfos>
    {
        #region Variables

        /// <summary>
        /// Variable Server.
        /// </summary>
        public Server server;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Server.
        /// </summary>
        public Server Server
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
        /// <param name="pBase"></param>
        public ServerInfosModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Server Side Server Infos Model Constructor.
        /// </summary>
        /// <param name="pBase"></param>
        public ServerInfosModel(ServerInfos pBase) : base(pBase)
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
            Server = ApplicationBase.Options.Remote.Servers.FindDefault();
        }

        #endregion
    }
}