using Fotootof.SQLite.Services;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Objects;

namespace Fotootof.Libraries.Models
{
    /// <summary>
    /// Class Fotootof Libraries Model Base.
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
        public T ControlView { get; protected set; }

        #endregion
        


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Model Constructor.
        /// </summary>
        public ModelBase() { }

        /// <summary>
        /// Class Fotootof Libraries Model Base.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> view associated to the model.</param>
        public ModelBase(T controlView)
        {
            ControlView = controlView;
        }

        #endregion
    }
}
