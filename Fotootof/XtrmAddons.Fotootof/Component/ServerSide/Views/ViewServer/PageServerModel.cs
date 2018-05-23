
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.XmlRemote.Server;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component ServerSide Views Page Server Model.
    /// </summary>
    public class PageServerModel : PageBaseModel<PageServer>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
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
        public PageServerModel(PageServer page) : base(page) { }

        #endregion
    }
}
