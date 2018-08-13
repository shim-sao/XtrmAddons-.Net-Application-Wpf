using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Services Query Manager  Pictures.
    /// </summary>
    public partial class QuerierPicture : Queriers
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
        /// Method to get a list of <see cref="PictureEntity"/>. 
        /// </summary>
        /// <param name="op"><see cref="PictureOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{PictureEntity}"/>.</returns>
        public ObservableCollection<PictureEntity> List(PictureOptionsList op)
        {
            using (Db.Context)
            {
                return new ObservableCollection<PictureEntity>(PictureManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of <see cref="PictureEntity"/> asynchronously. 
        /// </summary>
        /// <param name="op"><see cref="PictureOptionsList"/> filters options for the query.</param>
        /// <returns>An <see cref="ObservableCollection{PictureEntity}"/>.</returns>
        public Task<ObservableCollection<PictureEntity>> ListAsync(PictureOptionsList options = null) 
            => Task.Run(() => { return List(options); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to get a single <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="PictureOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="PictureEntity"/> or null.</returns>
        public PictureEntity SingleOrNull(PictureOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(op), op);
                log.Error(e.Output());
                throw e;
            }

            using (Db.Context)
            {
                return PictureManager.Select(op);
            }
        }

        /// <summary>
        /// Method to get a single <see cref="PictureEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="PictureOptionsSelect"/> filters options for the query.</param>
        /// <returns>An <see cref="PictureEntity"/> or null.</returns>
        public Task<PictureEntity> SingleOrNullAsync(PictureOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public PictureEntity Add(PictureEntity picture)
        {
            using (Db.Context)
            {
                try { Db.Context.Attach(picture); } catch { }
                return PictureManager.Add(picture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public Task<PictureEntity> AddAsync(PictureEntity picture) => Task.Run(() => Add(picture));

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update a <see cref="PictureEntity"/> asynchronous.
        /// </summary>
        /// <param name="entity">An <see cref="PictureEntity"/> to update.</param>
        /// <returns>The updated <see cref="PictureEntity"/>.</returns>
        public Task<PictureEntity> UpdateAsync(PictureEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try { Db.Context.Attach(entity); } catch { throw new Exception($"Error on database Context Attach {entity?.GetType()}"); }

                return PictureManager.UpdateAsync(entity);
            }
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete a picture.
        /// </summary>
        /// <param name="picture">The Picture entity to delete.</param>
        /// <returns>The deleted Picture entity.</returns>
        public PictureEntity Delete(PictureEntity picture)
        {
            using (Db.Context)
            {
                Db.Context.Attach(picture);
                return PictureManager.Delete(picture);
            }
        }

        /// <summary>
        /// Method to delete a list of pictures.
        /// </summary>
        /// <param name="pictures">A list of Picture entity to delete.</param>
        /// <returns>The deleted Picture entity list.</returns>
        public List<PictureEntity> Delete(List<PictureEntity> pictures)
        {
            using (Db.Context)
            {
                foreach (PictureEntity p in pictures)
                {
                    Db.Context.Attach(p);
                }

                return PictureManager.Delete(pictures);
            }
        }

        /// <summary>
        /// Method to delete a picture asynchronously.
        /// </summary>
        /// <param name="picture">The Picture entity to delete.</param>
        /// <returns>>The deleted Picture entity.</returns>
        public Task<PictureEntity> DeleteAsync(PictureEntity picture)
            => Task.Run(() => { return Delete(picture); });

        /// <summary>
        /// Method to delete a list of pictures asynchronously.
        /// </summary>
        /// <param name="pictures">A list of Picture entity to delete.</param>
        /// <returns>The deleted Picture entity list.</returns>
        public Task<List<PictureEntity>> DeleteAsync(List<PictureEntity> pictures)
            => Task.Run(() => Delete(pictures));

        #endregion



        #region Methods Dependency

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="dependencyPKName"></param>
        /// <param name="aclGroupId"></param>
        /// <param name="dependenciesPKs"></param>
        /// <returns></returns>
        [System.Obsolete("", true)]
        public async Task<int> CleanDependencyAsync(string dependencyName, string dependencyPKName, int pictureId, IEnumerable<int> dependenciesPKs)
        {
            using (Db.Context)
            {
                return await PictureManager.CleanDependencyAsync
                    (
                        new EntityManagerDeleteDependency { Name = dependencyName, key = "PictureId", keyList = dependencyPKName },
                        pictureId,
                        dependenciesPKs
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        [System.Obsolete("", true)]
        public async void CleanDependencyAllAsync(PictureEntity entity)
        {
            // Hack to delete unassociated dependencies. 
            await CleanDependencyAsync("PicturesInAlbums", "AlbumId", entity.PrimaryKey, entity.AlbumsPKeys);
        }

        #endregion
    }
}
