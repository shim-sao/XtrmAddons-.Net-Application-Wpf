using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Base;
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
    /// Class XtrmAddons Fotootof SQLite Services Query Manager Albums.
    /// </summary>
    public partial class QuerierAlbum : Queriers,
        IQuerierList<AlbumEntity, AlbumOptionsList>,
        IQuerierSingle<AlbumEntity, AlbumOptionsSelect>
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
        /// Method to get a list of <see cref="AlbumEntity"/>. 
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{AlbumEntity}"/>.</returns>
        public ObservableCollection<AlbumEntity> List(AlbumOptionsList op)
        {
            if (op == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(op), typeof(AlbumOptionsList));
                log.Error(e.Output());
                throw e;
            }

            using (Db.Context)
            {
                return new ObservableCollection<AlbumEntity>(AlbumManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of <see cref="AlbumEntity"/> asynchronously. 
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{AlbumEntity}"/>.</returns>
        public Task<ObservableCollection<AlbumEntity>> ListAsync(AlbumOptionsList op)
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to get a single <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="AlbumEntity"/> or null.</returns>
        public AlbumEntity SingleOrNull(AlbumOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context)
            {
                return AlbumManager.Select(op);
            }
        }

        /// <summary>
        /// Method to get a single <see cref="AlbumEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="AlbumEntity"/> or null.</returns>
        public Task<AlbumEntity> SingleOrNullAsync(AlbumOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        /// <summary>
        /// Method to get a single <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="AlbumEntity"/> or a dafault entity.</returns>
        public AlbumEntity SingleOrDefault(AlbumOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context)
            {
                return AlbumManager.Select(op) ?? default(AlbumEntity);
            }
        }

        /// <summary>
        /// Method to get a single <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AlbumOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="AlbumEntity"/> or a dafault entity.</returns>
        public Task<AlbumEntity> SingleOrDefaultAsync(AlbumOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="AlbumEntity"/>.</param>
        /// <returns>The new <see cref="AlbumEntity"/>.</returns>
        public AlbumEntity Add(AlbumEntity entity)
        {
            using (Db.Context)
            {
                return AlbumManager.Add(entity);
            }
        }

        /// <summary>
        /// Method to add new <see cref="AlbumEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">The <see cref="AlbumEntity"/>.</param>
        /// <returns>The new <see cref="AlbumEntity"/>.</returns>
        public Task<AlbumEntity> AddAsync(AlbumEntity entity)
        {
            return Task.Run(() => Add(entity));
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an <see cref="AlbumEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="AlbumEntity"/> to delete.</param>
        /// <param name="save">Save changes ?</param>
        /// <returns>The deleted <see cref="AlbumEntity"/>.</returns>
        public AlbumEntity Delete(AlbumEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            using (Db.Context)
            {
                Db.Context.Albums.Attach(entity);
                return AlbumManager.Delete(entity);
            }
        }

        /// <summary>
        /// Method to delete an <see cref="AlbumEntity"/> asynchronously.
        /// </summary
        /// <param name="album">The <see cref="AlbumEntity"/> to delete.</param>
        /// <param name="save">Save changes ?</param>
        /// <returns>The deleted <see cref="AlbumEntity"/>.</returns>
        public async Task<AlbumEntity> DeleteAsync(AlbumEntity entity)
        {
            return await Task.Run(() => Delete(entity));
        }

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update a <see cref="AlbumEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">An <see cref="AlbumEntity"/> to update.</param>
        /// <param name="autoDate">Auto initialize dates.</param>
        /// <returns>The updated <see cref="AlbumEntity"/>.</returns>
        public async Task<AlbumEntity> UpdateAsync(AlbumEntity entity, bool autoDate = true)
        {
            if(entity == null)
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

                // Update entity.
                return await AlbumManager.UpdateAsync(entity, autoDate);
            }
        }

        #endregion



        #region Obsoletes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="dependencyPKName"></param>
        /// <param name="aclGroupId"></param>
        /// <param name="dependenciesPKs"></param>
        /// <returns></returns>
        [System.Obsolete("", true)]
        public async Task<int> CleanDependencyAsync(string dependencyName, string dependencyPKName, int albumId, IEnumerable<int> dependenciesPKs)
        {
            using (Db.Context)
            {
                return await AlbumManager.CleanDependencyAsync
                    (
                        new EntityManagerDeleteDependency { Name = dependencyName, key = "AlbumId", keyList = dependencyPKName },
                        albumId,
                        dependenciesPKs
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        [System.Obsolete("", true)]
        public async void CleanDependencyAllAsync(AlbumEntity entity)
        {
            // Hack to delete unassociated dependencies. 
            await CleanDependencyAsync("AlbumsInSections", "SectionId", entity.PrimaryKey, entity.SectionsPKs);
            await CleanDependencyAsync("InfosInAlbums", "InfoId", entity.PrimaryKey, entity.InfosPKs);
            await CleanDependencyAsync("PicturesInAlbums", "PictureId", entity.PrimaryKey, entity.PicturesPKs);
        }

        #endregion
    }
}
