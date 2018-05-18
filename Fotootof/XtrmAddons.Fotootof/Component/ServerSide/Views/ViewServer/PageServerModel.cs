
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.XmlRemote.Server;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewServer
{
    /// <summary>
    /// 
    /// </summary>
    public class PageServerModel<PageServer> : PageBaseModel<PageServer>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private ServerData server;

        #endregion




        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageBase"></param>
        public PageServerModel(PageServer pageBase) : base(pageBase) { }
    }
}
