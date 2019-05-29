using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using Fotootof.SQLite.Services.QueryManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Services Query Manager Users.
    /// </summary>
    public partial class QuerierUser : Queriers,
        IQuerierList<UserEntity, UserOptionsList>
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
        /// Method to get a list of <see cref="UserEntity"/>. 
        /// </summary>
        /// <param name="op"><see cref="UserOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{UserEntity}"/>.</returns>
        public ObservableCollection<UserEntity> List(UserOptionsList op = null)
        {
            using (Db.Context)
            {
                return new ObservableCollection<UserEntity>(UserManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of <see cref="UserEntity"/> asynchronously. 
        /// </summary>
        /// <param name="op"><see cref="UserOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{UserEntity}"/>.</returns>
        public Task<ObservableCollection<UserEntity>> ListAsync(UserOptionsList op = null)
            => Task.Run(() => List(op));

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to get a single <see cref="UserEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="UserOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="UserEntity"/> or null.</returns>
        public UserEntity SingleOrNull(UserOptionsSelect op)
        {
            using (Db.Context)
            {
                return UserManager.Select(op);
            }
        }

        /// <summary>
        /// Method to get a single <see cref="UserEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="UserOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="UserEntity"/> or null.</returns>
        public Task<UserEntity> SingleOrNullAsync(UserOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new <see cref="UserEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="UserEntity"/>.</param
        /// <param name="save">Should save database changes ?</param>
        /// <returns>The new <see cref="UserEntity"/>.</returns>
        public UserEntity Add(UserEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                return UserManager.Add(entity, save);
            }
        }

        /// <summary>
        /// Method to add new <see cref="UserEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">The <see cref="UserEntity"/>.</param
        /// <param name="save">Should save database changes ?</param>
        /// <returns>The new <see cref="UserEntity"/>.</returns>
        public Task<UserEntity> AddAsync(UserEntity entity, bool save = true)
            => Task.Run(() => Add(entity, save));

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update a <see cref="AlbumEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">An <see cref="AlbumEntity"/> to update.</param>
        /// <param name="autoDate">Auto initialize dates.</param>
        /// <returns>The updated <see cref="AlbumEntity"/>.</returns>
        public async Task<UserEntity> UpdateAsync(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try { Db.Context.Attach(entity); }
                catch (Exception e)
                {
                    InvalidOperationException i = new InvalidOperationException($"Error on database Context Attach {entity?.GetType()}", e);
                    log.Error(i.Output());
                    throw;
                }

                return await UserManager.UpdateAsync(entity);
            }
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an <see cref="UserEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="UserEntity"/> to delete.</param>
        /// <returns>The deleted <see cref="UserEntity"/>.</returns>
        public UserEntity Delete(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            using (Db.Context)
            {
                return UserManager.Delete(entity);
            }
        }

        /// <summary>
        /// Method to delete an <see cref="UserEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">The <see cref="UserEntity"/> to delete.</param>
        /// <returns>The deleted <see cref="UserEntity"/>.</returns>
        public Task<UserEntity> DeleteAsync(UserEntity entity)
            => Task.Run(() => { return Delete(entity); });
        #endregion



        #region Obsoletes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="aclGroupId"></param>
        /// <returns></returns>
        [System.Obsolete("to delete", true)]
        public async Task<int> DeleteAclGroupDependenciesAsync(int userId, List<int> aclGroupId)
        {
            using (Db.Context)
            {
                return await UserManager.DeleteAclGroupDependencyAsync(userId, aclGroupId);
            }
        }

        #endregion
    }
}
