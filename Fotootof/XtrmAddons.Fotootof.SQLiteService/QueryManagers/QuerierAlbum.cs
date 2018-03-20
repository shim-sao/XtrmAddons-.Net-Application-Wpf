using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.SQLiteService.QueryManagers;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Service Albums.
    /// </summary>
    public partial class QuerierAlbum : Querier
    {
        #region Methods List

        /// <summary>
        /// Method to get a list of Album entities. 
        /// </summary>
        /// <param name="op">Album filters options for query.</param>
        /// <returns>A list of Album entities.</returns>
        public ObservableCollection<AlbumEntity> List(AlbumOptionsList op)
        {
            using (Db.Context)
            {
                return new ObservableCollection<AlbumEntity>(AlbumManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of Album entities asynchronous. 
        /// </summary>
        /// <param name="op">Album filters options for query.</param>
        /// <returns>A list of Album entities.</returns>
        public Task<ObservableCollection<AlbumEntity>> ListAsync(AlbumOptionsList op)
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to get a single Album entity.
        /// </summary>
        /// <param name="op">Album filters options for query.</param>
        /// <returns>An Album entity or null.</returns>
        public AlbumEntity SingleOrNull(AlbumOptionsSelect op)
        {
            using (Db.Context)
            {
                return AlbumManager.Select(op);
            }
        }

        /// <summary>
        /// Method to get a single Album entity.
        /// </summary>
        /// <param name="op">Album filters options for query.</param>
        /// <returns>An Album entity or null.</returns>
        public Task<AlbumEntity> SingleOrNullAsync(AlbumOptionsSelect op)
        {
            return Task.Run(() =>
            {
                return SingleOrNull(op);
            });
        }

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new album.
        /// </summary>
        /// <param name="entity">The Album Entity.</param>
        /// <returns>The new Album entity.</returns>
        public AlbumEntity Add(AlbumEntity entity)
        {
            using (Db.Context)
            {
                entity.Initialize();
                return AlbumManager.Add(entity);
            }
        }

        /// <summary>
        /// Method to add new album asynchronous.
        /// </summary>
        /// <param name="entity">The album Entity.</param>
        /// <returns>The new Album entity.</returns>
        
        public Task<AlbumEntity> AddAsync(AlbumEntity entity)
        {
            return Task.Run(() => Add(entity));
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an Album.
        /// </summary>
        /// <param name="album">The Album to delete.</param>
        /// <param name="save">Save changes ?</param>
        /// <returns>The deleted Album entity.</returns>
        public AlbumEntity Delete(AlbumEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                Db.Context.Albums.Attach(entity);
                return AlbumManager.Delete(entity);
            }
        }

        /// <summary>
        /// Method to delete an Album asynchronous.
        /// </summary
        /// <param name="album">The Album to delete.</param>
        /// <param name="save">Save changes ?</param>
        /// <returns>The deleted Album entity.</returns>
        public Task<AlbumEntity> DeleteAsync(AlbumEntity entity, bool save = true)
        {
            return Task.Run(() => Delete(entity));
        }

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update a album asynchronous.
        /// </summary>
        /// <param name="entity">The Album to update.</param>
        /// <param name="save">Save changes ?</param>
        /// <param name="autoDate">Auto initialize dates.</param>
        /// <returns>The updated Album entity.</returns>
        public async Task<AlbumEntity> UpdateAsync(AlbumEntity entity, bool save = true, bool autoDate = true)
        {
            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try { Db.Context.Attach(entity); } catch { throw new System.Exception("Error on database Context Attach Album"); }

                // Update entity.
                entity = AlbumManager.Update(entity, true);

                // Hack to delete unassociated dependencies. 
                await CleanDependenciesAsync("AlbumsInSections", "SectionId", entity.PrimaryKey, entity.SectionsPK);
                await CleanDependenciesAsync("InfosInAlbums", "InfoId", entity.PrimaryKey, entity.InfosPK);
                await CleanDependenciesAsync("PicturesInAlbums", "PictureId", entity.PrimaryKey, entity.PicturesPK);

                return entity;
            }
        }

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
        public async Task<int> CleanDependenciesAsync(string dependencyName, string dependencyPKName, int albumId, List<int> dependenciesPKs)
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

        #endregion
    }
}
