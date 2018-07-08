using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Libraries SQLite Albums Entities Manager.
    /// </summary>
    public partial class AlbumManager : EntitiesManager
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Albums Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public AlbumManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to add new album.
        /// </summary>
        /// <param name="item">A album entity to add.</param>
        /// <returns>The added album entity.</returns>
        public AlbumEntity Add(AlbumEntity entity, int sectionId = 0, bool save = true)
        {
            // Check if Section id is present.
            if(sectionId > 0)
            {
                // Check if Album contains associated Sections
                /*if (entity.AlbumsInSections == null)
                {
                    // Add new dependency.
                    entity.AlbumsInSections = new ObservableAlbumsInSections<AlbumEntity, SectionEntity>
                    {
                        new AlbumsInSections { SectionId = sectionId }
                    };
                    
                }
                else if(entity.AlbumsInSections.SingleOrDefault(x => x.SectionId == sectionId).SectionId == 0)
                {
                    entity.AlbumsInSections.Add(new AlbumsInSections { SectionId = sectionId });
                }*/
                entity.SectionsPKs.Add(sectionId);
            }

            entity = Context.Albums.Add(entity).Entity;

            if(save)
            {
                Save();
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private IQueryable<AlbumEntity> Query_Dependencies(IQueryable<AlbumEntity> query, EntitiesOptions op)
        {
            /*if (op.IsDependOn(EnumEntitiesDependencies.None))
            {
                log.Debug($"{typeof(EntitiesQueryExtension).Name}.{MethodBase.GetCurrentMethod().Name} : EnumEntitiesDependencies.None");
                return query;
            }*/

            // Load Pictures dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.PicturesInAlbums))
            {
                log.Debug($"{typeof(EntitiesQueryExtension).Name}.{MethodBase.GetCurrentMethod().Name} : EnumEntitiesDependencies.PicturesInAlbums");
                query = query.Include(x => x.PicturesInAlbums);
            }

            // Load Sections dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.AlbumsInSections))
            {
                log.Debug($"{typeof(EntitiesQueryExtension).Name}.{MethodBase.GetCurrentMethod().Name} : EnumEntitiesDependencies.AlbumsInSections");
                query = query.Include(x => x.AlbumsInSections);
            }

            // Load Infos dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.InfosInAlbums))
            {
                log.Debug($"{typeof(EntitiesQueryExtension).Name}.{MethodBase.GetCurrentMethod().Name} : EnumEntitiesDependencies.InfosInAlbums");
                query = query.Include(x => x.InfosInAlbums);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private IQueryable<AlbumEntity> Query_FilterSections(IQueryable<AlbumEntity> query, AlbumOptionsList op)
        {

            // Check for include Section primary keys to search in.
            if (op.IncludeSectionKeys != null && op.IncludeSectionKeys.Count > 0)
            {
                query = query.Where(x => x.AlbumsInSections.Any(y => op.IncludeSectionKeys.Contains(y.SectionId)));
            }

            // Check for exclude Section primary keys in search.
            if (op.ExcludeSectionKeys != null && op.ExcludeSectionKeys.Count > 0)
            {
                query = query.Where(x => x.AlbumsInSections.Any(y => !op.ExcludeSectionKeys.Contains(y.SectionId)));
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private IQueryable<AlbumEntity> Query_FilterInfos(IQueryable<AlbumEntity> query, AlbumOptionsList op)
        {
            // Check for include Info primary keys to search in.
            if (op.IncludeInfoKeys != null && op.IncludeInfoKeys.Count > 0)
            {
                query = query.Where(x => x.InfosInAlbums.Any(y => op.IncludeInfoKeys.Contains(y.InfoId)));
            }

            // Check for exclude Info primary keys in search.
            if (op.ExcludeInfoKeys != null && op.ExcludeInfoKeys.Count > 0)
            {
                query = query.Where(x => x.InfosInAlbums.Any(y => !op.ExcludeInfoKeys.Contains(y.InfoId)));
            }

            return query;
        }

        /// <summary>
        /// Method to list albums entities.
        /// </summary><param name="depencies">Load also dependencies.</param>
        /// <returns>A list of albums entities.</returns>
        public IList<AlbumEntity> List(AlbumOptionsList op)
        {
            if(op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            // Initialize query.
            IQueryable<AlbumEntity> query = Context.Albums;

            query = Query_Dependencies(query, op);

            // Filter by entity primary keys.
            query.QueryListInclude(op, "AlbumId");
            query.QueryListExclude(op, "AlbumId");


            // Check Section filters.
            query = Query_FilterSections(query, op);
            query = Query_FilterInfos(query, op);

            // Set number elements to skip.
            if (op.Start > 0)
            {
                query = query.Skip(op.Start);
            }

            // Set the number elements to select.
            if (op.Limit > 0)
            {
                query = query.Take(op.Limit);
            }

            return query.ToList();
        }

        /// <summary>
        /// Method to get the min Picture created date of an Album.
        /// </summary>
        /// <param name="name"></param>
        public DateTime MinCreated(int albumId)
        {
            try
            {
                return
                    Context.PicturesInAlbums
                    .Where(d => d.AlbumId == albumId)
                    .Select(d => d.PictureEntity.Created)
                    .DefaultIfEmpty(DateTime.Now).Min();
            }
            catch { }

            return DateTime.Now;
        }

        /// <summary>
        /// Method to get the min Picture created date of an Album.
        /// </summary>
        /// <param name="name"></param>
        public DateTime MaxCreated(int albumId)
        {
            try
            {
                return
                    Context.PicturesInAlbums
                    .Where(d => d.AlbumId == albumId)
                    .Select(d => d.PictureEntity.Created)
                    .DefaultIfEmpty(DateTime.Now).Max();
            }
            catch { }

            return DateTime.Now;
        }

        /// <summary>
        /// Method to remove picture dependencies of an album asynchronous.
        /// </summary>
        /// <param name="albumId">The id of the album.</param>
        /// <param name="pictureId">The id of the picture.</param>
        /// <returns></returns>
        public AlbumEntity RemovePictureDependency(int albumId, int pictureId, bool save = true)
        {
            AlbumOptionsSelect options = new AlbumOptionsSelect { PrimaryKey = albumId };
            options.Dependencies.Add(EnumEntitiesDependencies.PicturesInAlbums);

            AlbumEntity album = Select(options);
            PicturesInAlbums dependency = album.PicturesInAlbums.SingleOrDefault(c => c.PictureId == pictureId);
            album.PicturesInAlbums.Remove(dependency);

            return Update(album, save);
        }

        /// <summary>
        /// Method to remove association between an album and a picture.
        /// </summary>
        /// <param name="albumId">The id of the albumId.</param>
        /// <param name="pictureId">The id of the picture.</param>
        /// <returns>Async task with modified Album entity as result.</returns>
        public async Task<AlbumEntity> RemovePictureDependenciesAsync(int albumId, int pictureId, bool save = true)
        {
            int result = await Context.Database.ExecuteSqlCommandAsync(
                $"DELETE FROM PicturesInAlbums WHERE AlbumId = {albumId} AND PictureId = {pictureId}"
             );

            Save(save);
            
            AlbumOptionsSelect options = new AlbumOptionsSelect { PrimaryKey = albumId };
            options.Dependencies.Add(EnumEntitiesDependencies.PicturesInAlbums);

            return Select(options);
        }

        /// <summary>
        /// Method to remove Section dependencies of an Album.
        /// </summary>
        /// <param name="albumId">The id of the Album.</param>
        /// <param name="sectionId">The id of the Section.</param>
        /// <returns>The modified Album entity as result.</returns>
        public AlbumEntity RemoveSectionDependency(int albumId, int sectionId, bool save = true)
        {
            AlbumOptionsSelect options = new AlbumOptionsSelect { PrimaryKey = albumId };
            options.Dependencies.Add(EnumEntitiesDependencies.AlbumsInSections);

            AlbumEntity album = Select(options);
            album.AlbumsInSections.Remove(album.AlbumsInSections.SingleOrDefault(x => x.SectionId == sectionId));

            return Update(album, save);
        }

        /// <summary>
        /// Method to remove association between an Album and a Section asynchronous.
        /// </summary>
        /// <param name="albumId">The id of the Album.</param>
        /// <param name="sectionId">The id of the Section.</param>
        /// <returns>Async task with modified Album entity as result.</returns>
        public async Task<AlbumEntity> RemoveSectionDependenciesAsync(int albumId, int sectionId, bool save = true)
        {
            int result = await Context.Database.ExecuteSqlCommandAsync(
                $"DELETE FROM AlbumsInSections WHERE AlbumId = {albumId} AND SectionId = {sectionId}"
             );

            Save(save);

            AlbumOptionsSelect options = new AlbumOptionsSelect { PrimaryKey = albumId };
            options.Dependencies.Add(EnumEntitiesDependencies.AlbumsInSections);

            return Select(options);
        }

        /// <summary>
        /// Method to select an Album.
        /// </summary>
        /// <param name="albumId">The id of the album.</param>
        /// <param name="depencies">Load also dependencies.</param>
        /// <returns>An album entity.</returns>
        public AlbumEntity Select(AlbumOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            // Initialize query.
            IQueryable<AlbumEntity> query = Context.Albums;

            // Load dependencies if required.
            query = Query_Dependencies(query, op);

            AlbumEntity entity = null;

            // Initialize Album
            if(op.PrimaryKey != 0)
            {
                entity = SingleIdOrNull(query, op);
            }

            if(op.Alias.IsNotNullOrWhiteSpace())
            {
                entity = SingleAliasOrNull(query, op);
            }
            
            if (entity != null)
            {
                if (op.IsDependOn(EnumEntitiesDependencies.PicturesInAlbums))
                {
                    foreach (PictureEntity item in entity.Pictures)
                    {
                        if (item != null)
                        {
                            List<PicturesInAlbums> l = Context.PicturesInAlbums.Where(d => d.PictureId == item.PictureId && d.AlbumId == op.PrimaryKey).ToList();
                            if (l.Count > 0)
                            {
                                item.Ordering = l[0].Ordering;
                            }
                        }
                    }
                }

                return entity;
            }

            return null;
        }

        /// <summary>
        /// Method to select an Album by id.
        /// </summary>
        /// <returns>An Album entity or null if not found.</returns>
        private AlbumEntity SingleIdOrNull(IQueryable<AlbumEntity> query, AlbumOptionsSelect op)
        {
            AlbumEntity entity = query.SingleOrDefault(x => x.AlbumId == op.PrimaryKey);

            // Check if user is found, return null instead of default.
            if (entity == null || entity.AlbumId == 0)
            {
                return null;
            }

            return entity;
        }

        /// <summary>
        /// Method to select an Album by id.
        /// </summary>
        /// <returns>An Album entity or null if not found.</returns>
        private AlbumEntity SingleAliasOrNull(IQueryable<AlbumEntity> query, AlbumOptionsSelect op)
        {
            AlbumEntity entity = query.SingleOrDefault(x => x.Alias == op.Alias);

            // Check if user is found, return null instead of default.
            if (entity == null || entity.AlbumId == 0)
            {
                return null;
            }

            return entity;
        }

        /// <summary>
        /// Method to update an album.
        /// </summary>
        /// <param name="album">A album entity to update.</param>
        /// <param name="save">Save changes after update ?</param>
        /// <param name="autoDate">Automatic set default dates ?</param>
        /// <returns>The updated Album entity.</returns>
        public AlbumEntity UpdateAlbum(AlbumEntity entity, bool save = true, bool autoDate = true)
        {
            // Update dates according picture list.
            if (autoDate)
            {
                entity.DateStart = MinCreated(entity.AlbumId);
                entity.DateEnd = MaxCreated(entity.AlbumId);
            }

            entity.Modified = DateTime.UtcNow;

            return Update(entity, save);
        }

        /// <summary>
        /// Method to update a list of albums.
        /// </summary>
        /// <param name="album">A list of Album entities to update.</param>
        /// <param name="save">Save changes after update ?</param>
        /// <param name="autoDate">Automatic set default dates ?</param>
        /// <returns>The updated list of Album entity.</returns>
        public List<AlbumEntity> UpdateAlbum(List<AlbumEntity> albums, bool save = true, bool autoDate = true)
        {
            // New list for updated albums return.
            List<AlbumEntity> entities = new List<AlbumEntity>();

            // Loop over the list of albums to update.
            foreach (AlbumEntity entity in albums)
            {
                entities.Add(UpdateAlbum(entity, false, autoDate));
            }

            Save(save);
            
            // Return updates albums.
            return entities;
        }

        #endregion
    }
}