using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.ServerForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Window Server Form Model.
    /// </summary>
    public class WindowFormServerModel : WindowBaseFormModel<WindowServerForm>
    {
        #region Variables

        /// <summary>
        /// Variable server.
        /// </summary>
        public Server server;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Client.
        /// </summary>
        public Server Server
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
        /// <param name="pBase"></param>
        public WindowFormServerModel(WindowServerForm window) : base(window) { }

        #endregion
    }
}