using Fotootof.Libraries.Windows;
using ServerInfo = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Layouts.Forms.Server
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Server Window Form Model.
    /// </summary>
    public class WindowFormServerModel : WindowLayoutFormModel<WindowFormServerLayout>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  Variable new server informations <see cref="ServerInfo"/>.
        /// </summary>
        private ServerInfo newFormData;

        /// <summary>
        /// Variable old server informations <see cref="ServerInfo"/>.
        /// </summary>
        private ServerInfo oldFormData;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the new server informations <see cref="ServerInfo"/>.
        /// </summary>
        public ServerInfo NewFormData
        {
            get => newFormData;
            set
            {
                if (newFormData != value)
                {
                    newFormData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the old server informations <see cref="ServerInfo"/>.
        /// </summary>
        public ServerInfo OldFormData
        {
            get => oldFormData;
            set
            {
                if (oldFormData != value)
                {
                    oldFormData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Server Window Form Model.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> owner associated to the model.</param>
        public WindowFormServerModel(WindowFormServerLayout controlView) : base(controlView) { }

        #endregion
    }
}