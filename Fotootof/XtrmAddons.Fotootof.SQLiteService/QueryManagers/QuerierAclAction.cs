using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.SQLiteService.QueryManagers.Interfaces;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService.
    /// </summary>
    public class QuerierAclAction : Queriers,
        IQuerierList<AclActionEntity, AclActionOptionsList>,
        IQuerierSingle<AclActionEntity, AclActionOptionsSelect>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Methods List

        /// <summary>
        /// Method to get a list of AclAction entities.
        /// </summary>
        /// <param name="op">AclAction list options to perform query.</param>
        /// <returns>A list of AclAction entities.</returns>
        public ObservableCollection<AclActionEntity> List(AclActionOptionsList op = null)
        {
            using (Db.Context) { return new ObservableCollection<AclActionEntity>(AclActionManager.List(op)); }
        }

        /// <summary>
        /// Method to get a list of AclAction entities.
        /// </summary>
        /// <param name="op">AclAction list options to perform query.</param>
        /// <returns>A list of AclAction entities.</returns>
        public Task<ObservableCollection<AclActionEntity>> ListAsync(AclActionOptionsList op = null) 
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select an AclAction entity.
        /// </summary>
        /// <param name="op">AclAction select options to perform query.</param>
        /// <returns>An AclAction entity or null if not found.</returns>
        public AclActionEntity SingleOrNull(AclActionOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context) { return AclActionManager.Select(op); }
        }

        /// <summary>
        /// Method to select asynchronously an AclAction entity.
        /// </summary>
        /// <param name="op">AclAction select options to perform query.</param>
        /// <returns>An AclAction entity or null if not found.</returns>
        public Task<AclActionEntity> SingleOrNullAsync(AclActionOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        /// <summary>
        /// Method to select an AclAction entity.
        /// </summary>
        /// <param name="op">AclAction select options to perform query.</param>
        /// <returns>An AclAction entity or null if not found.</returns>
        public AclActionEntity SingleOrDefault(AclActionOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context) { return AclActionManager.Select(op); }
        }

        /// <summary>
        /// Method to select asynchronously an AclAction entity.
        /// </summary>
        /// <param name="op">AclAction select options to perform query.</param>
        /// <returns>An AclAction entity or null if not found.</returns>
        public Task<AclActionEntity> SingleOrDefaultAsync(AclActionOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add a new AclAction.
        /// </summary>
        /// <param name="AclAction">The AclAction Entity to add.</param>
        /// <returns>The added AclAction entity.</returns>
        public AclActionEntity Add(AclActionEntity item)
        {
            if (item == null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using (Db.Context) { return AclActionManager.Add(item); }
        }

        /// <summary>
        /// Method to add a new AclAction asynchronous.
        /// </summary>
        /// <param name="AclAction">The AclAction Entity to add.</param>
        /// <returns>The added AclAction entity.</returns>
        public Task<AclActionEntity> AddAsync(AclActionEntity item) => Task.Run(() => { return Add(item); });

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an AclAction.
        /// </summary>
        /// <param name="aclActionId">The AclAction Entity to delete.</param>
        /// <returns>The deleted AclAction entity.</returns>
        public AclActionEntity Delete(int aclActionId)
        {
            AclActionEntity item = SingleOrNull(new AclActionOptionsSelect { PrimaryKey = aclActionId });

            using (Db.Context)
            {
                item = AclActionManager.Delete(item);
            }

            return item;
        }

        /// <summary>
        /// Method to delete an AclAction asynchronous.
        /// </summary>
        /// <param name="aclActionId">The AclAction Entity to delete.</param>
        /// <returns>The deleted AclAction entity.</returns> 
        public Task<AclActionEntity> DeleteAsync(int aclActionId) => Task.Run(() => { return Delete(aclActionId); });

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update an AclAction.
        /// </summary>
        /// <param name="ACLGroup">The AclAction Entity to update.</param>
        /// <returns>The updated AclAction entity.</returns>
        public AclActionEntity Update(AclActionEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                try { Db.Context.AclActions.Attach(entity); } catch { }
                return AclActionManager.Update(entity);
            }
        }

        /// <summary>
        /// Method to update an AclAction asynchronous.
        /// </summary>
        /// <param name="AclAction"></param>
        /// <returns>The updated AclAction entity.</returns>
        public Task<AclActionEntity> UpdateAsync(AclActionEntity entity, bool save = true) => Task.Run(() => { return Update(entity); });

        #endregion
    }
}