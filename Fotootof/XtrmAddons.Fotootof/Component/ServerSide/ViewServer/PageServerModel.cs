
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using ServerData = XtrmAddons.Net.Application.Serializable.Elements.XmlRemote.Server;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewServer
{
    /// <summary>
    /// 
    /// </summary>
    public class PageServerModel<PageServer> : PageBaseModel<PageServer>
    {
        /// <summary>
        /// 
        /// </summary>
        private ServerData server;

        /// <summary>
        /// 
        /// </summary>
        public ServerData Server
        {
            get => server;
            set
            {
                server = value;
                RaisePropertyChanged("Server");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageBase"></param>
        public PageServerModel(PageServer pageBase) : base(pageBase) { }
    }
}
