using Fotootof.Libraries.Windows;
using ServerInfo = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Layouts.Windows.Forms.Server
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Window Server Form Model.
    /// </summary>
    public class WindowFormServerModel : WindowLayoutFormModel<WindowFormServerLayout>
    {
        #region Variables

        /// <summary>
        /// Variable server.
        /// </summary>
        public ServerInfo server;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Client.
        /// </summary>
        public ServerInfo Server
        {
            get => server;
            set
            {
                server = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window Client Form Model Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public WindowFormServerModel(WindowFormServerLayout controlView) : base(controlView) { }

        #endregion
    }
}