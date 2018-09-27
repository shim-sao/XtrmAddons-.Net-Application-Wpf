using Fotootof.Libraries.Windows;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Libraries.HttpHelpers.HttpServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UC Server Infos Model.
    /// </summary>
    public class ServerInfosModel<T> : WindowBaseFormModel<T>
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
        /// Class XtrmAddons Fotootof Server UC Server Infos Model.
        /// </summary>
        /// <param name="pBase"></param>
        public ServerInfosModel(T uc) : base(uc) { }

        #endregion
    }
}