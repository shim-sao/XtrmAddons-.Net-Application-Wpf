using Fotootof.Libraries.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fotootof.Libraries.Collections
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Classes Collections Base.</para>
    /// </summary>
    public abstract class CollectionBase<T> : ObservableCollection<T>, IAutoLoader
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to define if the default items of the collection can be loaded.
        /// </summary>
        public abstract bool IsAutoloadEnabled { get; }

        /// <summary>
        /// Property alias to access to the translation words.
        /// </summary>
        public dynamic Words => XtrmAddons.Fotootof.Culture.Translation.Words;

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        public dynamic DWords => XtrmAddons.Fotootof.Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the translation logs.
        /// </summary>
        public dynamic Logs => XtrmAddons.Fotootof.Culture.Translation.Logs;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        public dynamic DLogs => XtrmAddons.Fotootof.Culture.Translation.DLogs;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Base Classes Collections Base Constructor.
        /// </summary>
        /// <param name="autoLoad">Auto load data from database ?</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollectionBase(bool autoLoad = false) : base()
        {
            Initialize(autoLoad);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Base Collections.
        /// </summary>
        /// <param name="list">A list of T to paste in.</param>
        public CollectionBase(List<T> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Collections.
        /// </summary>
        /// <param name="collection">>A collection of T to paste in.</param>
        public CollectionBase(IEnumerable<T> collection) : base(collection) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the collection.
        /// </summary>
        /// <param name="autoLoad">Auto load default items of the collection.</param>
        public void Initialize(bool autoLoad = false)
        {
            if (IsAutoloadEnabled && autoLoad)
            {
                Load();
            }
        }

        /// <summary>
        /// Method to load data from database.
        /// </summary>
        public abstract void Load();

        #endregion
    }
}