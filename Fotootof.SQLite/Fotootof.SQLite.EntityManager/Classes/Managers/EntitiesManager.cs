using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System.Data.Entity;
using System.Linq;
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


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public UserEntity User { get; protected set; }

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Database Manager Constructor.
        /// </summary>
        /// <param name="connector"></param>
        public EntitiesManager(DatabaseContextCore connector) : base(connector) { }

        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected bool SetSafeUser(int id)
        {
            if (id > 0)
            {
                IQueryable<UserEntity> users = Connector.Users;
                users = users.Include(x => x.UsersInAclGroups);
                User = users.SingleOrNull(x => x.PrimaryKey == id);
                
                if (User == null)
                {
                    return false;
                }
            }

            return true;
        }
        
        #endregion
    }
}
