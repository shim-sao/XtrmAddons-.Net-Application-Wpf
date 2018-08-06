using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using System.Reflection;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Manager.
    /// </summary>
    public abstract class EntitiesManager : EntitiesManagerBase<DatabaseContextCore>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Database Manager Constructor.
        /// </summary>
        /// <param name="connector"></param>
        public EntitiesManager(DatabaseContextCore connector) : base(connector) { }

        #endregion
    }
}
