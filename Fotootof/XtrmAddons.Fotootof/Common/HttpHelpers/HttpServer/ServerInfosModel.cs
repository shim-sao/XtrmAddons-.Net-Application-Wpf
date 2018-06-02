using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace XtrmAddons.Fotootof.Common.HttpHelpers.HttpServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UC Server Infos Model.
    /// </summary>
    public class ServerInfosModel<UCServerInfos> : WindowBaseFormModel<UCServerInfos>
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
        public ServerInfosModel(UCServerInfos uc) : base(uc) { }

        #endregion
    }
}