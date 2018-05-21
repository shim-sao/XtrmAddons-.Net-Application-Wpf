using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Common.Windows.Forms
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Window Client Form Model.
    /// </summary>
    public class WindowFormClientModel : WindowBaseFormModel<WindowClientForm>
    {
        #region Variables

        /// <summary>
        /// Variable Client.
        /// </summary>
        public Client client;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Client.
        /// </summary>
        public Client Client
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
        /// <param name="pBase"></param>
        public WindowFormClientModel(WindowClientForm pBase) : base(pBase) { }

        #endregion
    }
}