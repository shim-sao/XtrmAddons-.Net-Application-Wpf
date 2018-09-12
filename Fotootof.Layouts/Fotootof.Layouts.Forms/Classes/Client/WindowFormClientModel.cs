using RemoteClient = XtrmAddons.Net.Application.Serializable.Elements.Remote.Client;
using Fotootof.Libraries.Windows;

namespace Fotootof.Layouts.Forms.Client
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Window Client Form Model.
    /// </summary>
    public class WindowFormClientModel : WindowLayoutFormModel<WindowFormClientLayout>
    {
        #region Variables

        /// <summary>
        /// Variable Client.
        /// </summary>
        public RemoteClient client;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Client.
        /// </summary>
        public RemoteClient Client
        {
            get => client;
            set
            {
                client = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window Client Form Model Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public WindowFormClientModel(WindowFormClientLayout controlView) : base(controlView) { }

        #endregion
    }
}