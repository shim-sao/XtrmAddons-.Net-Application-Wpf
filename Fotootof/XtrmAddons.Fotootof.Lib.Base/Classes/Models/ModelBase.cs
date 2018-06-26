using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Objects;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public class ModelBase<T> : ObjectBaseNotifier
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property alias to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

        /// <summary>
        /// Property to access to the owner object associated to the model.
        /// </summary>
        public T OwnerBase { get; protected set; }

        /// <summary>
        /// Property alias to access to the translation words.
        /// </summary>
        [System.Obsolete("use Translation.")]
        public dynamic Words => Culture.Translation.Words;

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        [System.Obsolete("use Translation.")]
        public dynamic DWords => Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the translation logs.
        /// </summary>
        [System.Obsolete("use Translation.")]
        public dynamic Logs => Culture.Translation.Logs;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        [System.Obsolete("use Translation.")]
        public dynamic DLogs => Culture.Translation.DLogs;

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
    }
}
