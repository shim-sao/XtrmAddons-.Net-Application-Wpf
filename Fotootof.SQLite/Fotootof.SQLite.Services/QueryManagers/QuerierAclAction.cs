using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Services Query Manager AclAction.
    /// </summary>
    public class QuerierAclAction : Queriers
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods List

        /// <summary>
        /// Method to get a list of <see cref="AclActionEntity"/> entities.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsList"/> list of options to perform query.</param>
        /// <returns>A list of <see cref="AclActionEntity"/> entities.</returns>
        public ObservableCollection<AclActionEntity> List(AclActionOptionsList op = null)
        {
            using (Db.Context) { return new ObservableCollection<AclActionEntity>(AclActionManager.List(op)); }
        }

        /// <summary>
        /// Method to get a list of <see cref="AclActionEntity"/> entities.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsList"/> list of options to perform query.</param>
        /// <returns>A list of <see cref="AclActionEntity"/> entities.</returns>
        public Task<ObservableCollection<AclActionEntity>> ListAsync(AclActionOptionsList op = null)
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select an <see cref="AclActionEntity"/> entity.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsSelect"/> list of options to perform query.</param>
        /// <returns>An <see cref="AclActionEntity"/> entity or null if not found.</returns>
        public AclActionEntity SingleOrNull(AclActionOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(op), typeof(AclActionOptionsSelect));
                log.Error(e.Output());
                throw e;
            }

            using (Db.Context) { return AclActionManager.Select(op); }
        }

        /// <summary>
        /// Method to select an <see cref="AclActionEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclActionEntity"/> entity or null if not found.</returns>
        public Task<AclActionEntity> SingleOrNullAsync(AclActionOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        /// <summary>
        /// Method to select an <see cref="AclActionEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclActionEntity"/> entity or null if not found.</returns>
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
        /// Method to select an <see cref="AclActionEntity"/> entity asynchronously.
        /// </summary>
        /// <param name="op"><see cref="AclActionOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclActionEntity"/> entity or null if not found.</returns>
        public Task<AclActionEntity> SingleOrDefaultAsync(AclActionOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add a new <see cref="AclActionEntity"/>.
        /// </summary>
        /// <param item="AclAction">The <see cref="AclActionEntity"/> Entity to add.</param>
        /// <returns>The added <see cref="AclActionEntity"/> entity.</returns>
        public AclActionEntity Add(AclActionEntity item)
        {
            if (item == null)
            {
                throw Exceptions.GetArgumentNull(nameof(item), typeof(AclActionEntity));
            }

            using (Db.Context) { return AclActionManager.Add(item); }
        }

        /// <summary>
        /// Method to add a new <see cref="AclActionEntity"/> asynchronously.
        /// </summary>
        /// <param name="item">The <see cref="AclActionEntity"/> Entity to add.</param>
        /// <returns>The added <see cref="AclActionEntity"/> entity.</returns>
        public Task<AclActionEntity> AddAsync(AclActionEntity item)
            => Task.Run(() => { return Add(item); });

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an <see cref="AclActionEntity"/>.
        /// </summary>
        /// <param name="aclActionId">The <see cref="AclActionEntity"/> primary key to delete.</param>
        /// <returns>The deleted <see cref="AclActionEntity"/> entity.</returns>
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
        /// Method to delete an <see cref="AclActionEntity"/> asynchronously.
        /// </summary>
        /// <param name="aclActionId">The <see cref="AclActionEntity"/> primary key to delete.</param>
        /// <returns>The deleted <see cref="AclActionEntity"/> entity.</returns> 
        public Task<AclActionEntity> DeleteAsync(int aclActionId)
            => Task.Run(() => { return Delete(aclActionId); });

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update an <see cref="AclActionEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">An <see cref="AclActionEntity"/> to update.</param>
        /// <returns>The updated <see cref="AclActionEntity"/> entity.</returns>
        public Task<AclActionEntity> UpdateAsync(AclActionEntity entity)
            => Task.Run(() => { return UpdateAsync(entity); });

        #endregion
    }
}