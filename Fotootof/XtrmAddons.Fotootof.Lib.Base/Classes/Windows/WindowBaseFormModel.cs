namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Window Form Model.
    /// </summary>
    public class WindowBaseFormModel<T> : WindowBaseModel<T>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to define if form save is enabled.
        /// </summary>
        private bool isSaveEnabled = false;

        #endregion


        #region Properties

        /// <summary>
        /// Property to check if form save is enabled.
        /// </summary>
        public bool IsSaveEnabled
        {
            get { return isSaveEnabled; }
            set
            {
                if (value != isSaveEnabled)
                {
                    isSaveEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Window Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowBaseFormModel(T owner) : base(owner) { }

        #endregion
    }
}
