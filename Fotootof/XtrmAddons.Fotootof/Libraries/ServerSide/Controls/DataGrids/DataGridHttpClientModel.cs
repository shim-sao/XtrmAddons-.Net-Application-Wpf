using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridHttpClientModel<T> : DataGridBaseModel<T, ObservableCollection<Client>>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridHttpClientModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridHttpClientModel(T owner) : base(owner)
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
            Items = new ObservableCollection<Client>(ApplicationBase.Options.Remote.Clients);
        }

        #endregion
    }
}


