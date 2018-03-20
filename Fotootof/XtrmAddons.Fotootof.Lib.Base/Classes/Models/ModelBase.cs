using System.ComponentModel;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public class ModelBase<T> : INotifyPropertyChanged
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Accessors owner base associated to the model.
        /// </summary>
        public T OwnerBase { get; protected set; }

        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Property to access to the dynamic translation words.
        /// </summary>
        public dynamic TranslationWords => Culture.Translation.Words;

        /// <summary>
        /// Property to access to the dynamic translation logs.
        /// </summary>
        public dynamic TranslationLogs => Culture.Translation.Logs;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Model Constructor.
        /// </summary>
        public ModelBase() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public ModelBase(T owner)
        {
            OwnerBase = owner;
        }

        #endregion



        #region Methods

        /// <summary>
        /// <para>Method to raise property changed.</para>
        /// <para>Send property changed event</para>
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
