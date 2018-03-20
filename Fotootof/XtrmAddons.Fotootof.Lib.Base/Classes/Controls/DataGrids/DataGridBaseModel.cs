using System.Dynamic;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids.
    /// </summary>
    /// <typeparam name="DataGridBase">The Data Grid Base User Control.</typeparam>
    /// <typeparam name="T">The type of data grid base child.</typeparam>
    public class DataGridBaseModel<T, U> : ControlBaseModel<T>
    {
        #region Variables

        /// <summary>
        /// Variable observable collection of items.
        /// </summary>
        private U items;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public DataGridBaseModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public DataGridBaseModel(T owner) : base(owner)
        {
            InitializeModel();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected virtual void InitializeModel()
        {
            Columns.Visibility = new ExpandoObject();
            Rows.IsEnabled = new ExpandoObject();
        }

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
                RaisePropertyChanged("Items");
            }
        }

        #endregion
    }
}