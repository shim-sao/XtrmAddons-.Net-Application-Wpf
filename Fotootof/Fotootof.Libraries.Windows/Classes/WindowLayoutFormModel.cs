namespace Fotootof.Libraries.Windows
{
    /// <summary>
    /// Class Fotootof Libraries Window Form Model Base.
    /// </summary>
    public class WindowLayoutFormModel<T> : WindowLayoutModel<T>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net"/>.
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
        /// Class Fotootof Libraries Window Form Model Base Constructor.
        /// </summary>
        /// <param name="owner">The <see cref="object"/> owner associated to the model.</param>
        public WindowLayoutFormModel(T owner) : base(owner) { }

        #endregion
    }
}
