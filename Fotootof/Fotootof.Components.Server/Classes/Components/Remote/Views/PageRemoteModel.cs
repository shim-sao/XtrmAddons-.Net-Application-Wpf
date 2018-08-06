
using Fotootof.Libraries.Components;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Components.Server.Remote
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component ServerSide Views Page Server Model.
    /// </summary>
    public class PageRemoteModel : PageBaseModel<PageRemoteLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable Server preferences.
        /// </summary>
        private ServerData server;

        #endregion



        #region Properties
        
        /// <summary>
        /// Property to access to the Server preferences.
        /// </summary>
        public ServerData Server
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
        /// Class XtrmAddons Fotootof Component ServerSide Views Page Server Model Constructor.
        /// </summary>
        /// <param name="page"></param>
        public PageRemoteModel(PageRemoteLayout page) : base(page) { }

        #endregion
    }
}
