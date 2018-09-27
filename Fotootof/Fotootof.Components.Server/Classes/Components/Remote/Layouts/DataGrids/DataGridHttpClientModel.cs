using Fotootof.Libraries.Controls.DataGrids;
using System.Collections.ObjectModel;
using XtrmAddons.Net.Application;
using RemoteClient = XtrmAddons.Net.Application.Serializable.Elements.Remote.Client;

namespace Fotootof.Components.Server.Remote.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridHttpClientModel<T> : DataGridBaseModel<T, ObservableCollection<RemoteClient>> where T : class
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        public DataGridHttpClientModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public DataGridHttpClientModel(T controlView) : base(controlView)
        {
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected new void InitializeModel()
        {
            base.InitializeModel();

            Reset();
        }

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        public void Reset()
        {
            Items = new ObservableCollection<RemoteClient>(ApplicationBase.Options.Remote.Clients);
        }

        #endregion
    }
}


