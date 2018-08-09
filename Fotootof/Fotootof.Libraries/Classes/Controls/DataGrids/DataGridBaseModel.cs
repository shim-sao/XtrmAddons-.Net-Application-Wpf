using System.Dynamic;

namespace Fotootof.Libraries.Controls.DataGrids
{
    /// <summary>
    /// Class Fotootof Libraries Control DataGrid Model.
    /// </summary>
    /// <typeparam name="U">The <see cref="object"/> User Control.</typeparam>
    /// <typeparam name="T">The Type of data grid base child.</typeparam>
    public class DataGridBaseModel<T, U> : ControlBaseModel<T>
    {
        #region Variables

        /// <summary>
        /// Variable observable collection of items.
        /// </summary>
        private U items;

        #endregion



        #region Properties

        /// <summary>
        /// Variable columns settings.
        /// </summary>
        public dynamic Columns { get; set; } = new ExpandoObject();

        /// <summary>
        /// Variable rows settings.
        /// </summary>
        public dynamic Rows { get; set; } = new ExpandoObject();
        
        /// <summary>
        /// Property to access to the observable collection of items.
        /// </summary>
        public U Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Constructor.
        /// </summary>
        public DataGridBaseModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> view associated to the model.</param>
        public DataGridBaseModel(T controlView) : base(controlView)
        {
            InitializeModel();
        }

        #endregion



        #region Method

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected void InitializeModel()
        {
            Columns.Visibility = new ExpandoObject();
            Rows.IsEnabled = new ExpandoObject();
        }

        #endregion
    }
}