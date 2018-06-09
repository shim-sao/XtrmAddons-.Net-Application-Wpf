using System.Reflection;

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
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListViewBaseModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Models ListViews Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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



        #region Methods

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected virtual void InitializeModel()
        {
            log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} Child class can implement its own methods ! Use override if needeed.");
        }

        #endregion
    }
}