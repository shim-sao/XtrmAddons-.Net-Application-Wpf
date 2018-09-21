using System.Collections;
using System.Reflection;
using System.Linq;

namespace Fotootof.Libraries.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Controlms List View Model.
    /// </summary>
    /// <typeparam name="T">The <see cref="System.Type"/> of inherited <see cref="System.Windows.Controls.ListView"/> layout owner of the model.</typeparam>
    /// <typeparam name="U">The <see cref="System.Type"/> of a observable collection of items.</typeparam>
    public abstract class ListViewBaseModel<T, U> : ControlLayoutModel<T> where T : class
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable observable collection of items.
        /// </summary>
        private U items;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Controlms List View Model Constructor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListViewBaseModel() : base()
        {
            InitializeModel();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Controlms List View Model Constructor.
        /// </summary>
        /// <param name="controlView">An inherited <see cref="System.Windows.Controls.ListView"/> layout owner of the model.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListViewBaseModel(T controlView) : base(controlView)
        {
            InitializeModel();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="System.Collections.ObjectModel.ObservableCollection{T}"/> of items.
        /// </summary>
        public U Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged();
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyPropertyChanged {items?.GetType()} => {items}");
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the .
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected virtual void InitializeModel()
        {
            log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {Local.Properties.Exceptions.OverrideMethodRequired}");
        }

        #endregion
    }
}