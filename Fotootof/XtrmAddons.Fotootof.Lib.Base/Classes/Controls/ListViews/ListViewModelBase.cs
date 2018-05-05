using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Models ListViews.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class ListViewBaseModel<T, U> : ControlBaseModel<T>
    {
        #region Variables

        /// <summary>
        /// Variable observable collection of items.
        /// </summary>
        private U items;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Models ListViews Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewBaseModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Models ListViews Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewBaseModel(T owner) : base(owner)
        {
            InitializeModel();
        }

        #endregion



        #region Properties

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



        #region Properties

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected virtual void InitializeModel()
        {

        }

        #endregion
    }
}