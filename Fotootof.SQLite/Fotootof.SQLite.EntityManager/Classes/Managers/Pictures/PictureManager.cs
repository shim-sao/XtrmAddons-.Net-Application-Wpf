using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Entity Manager Pictures.
    /// </summary>
    public partial class PictureManager : EntitiesManager
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof SQLite Entity Manager Pictures Constructor.
        /// </summary>
        /// <param name="context">A database connector <see cref="DatabaseContextCore"/></param>
        public PictureManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add a <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="albumId">The <see cref="AlbumEntity"/> primary key for association filter.</param>
        /// <param name="picture">The <see cref="PictureEntity"/> to add.</param>
        /// <param name="insertId"></param>
        /// <returns>The added <see cref="PictureEntity"/>.</returns>
        public PictureEntity Add(int albumId, PictureEntity picture, int insertId = 0)
        {
            // Search if item is already in database.
            PictureEntity item = Connector.Pictures.Where(p => p.PicturePath == picture.PicturePath).SingleOrDefault();

            // Get the max order of pictures associated to the album.
            int maxorder = MaxOrder(albumId);

            // Initialize return entity.
            EntityEntry et = null;

            if (insertId == 0)
            {
                insertId = InsertId(Connector.Pictures, x => x.PictureId);
            }

            if (picture == null || picture.PictureId == 0)
            {
                picture.PictureId = insertId;
                //picture.PicturesInAlbums =
                //    new ObservablePicturesInAlbums<PictureEntity, AlbumEntity>
                //    {
                //        new PicturesInAlbums
                //        {
                //            AlbumId = albumId,
                //            Ordering = ++maxorder
                //        }
                //    };
                picture.PicturesInAlbums.Add(
                    new PicturesInAlbums
                    {
                        AlbumId = albumId,
                        Ordering = ++maxorder
                    });

                et = Connector.Pictures.Add(picture);
            }
            else
            {
                PicturesInAlbums dependency = (new List<PicturesInAlbums>(picture.PicturesInAlbums)).Find(p => p.AlbumId == albumId);
                if (dependency == null)
                {
                    picture.PicturesInAlbums.Add(
                        new PicturesInAlbums
                        {
                            AlbumId = albumId,
                            Ordering = ++maxorder
                        }
                    );

                    et = Connector.Pictures.Update(picture);
                }
                else
                {
                    (new List<PicturesInAlbums>(picture.PicturesInAlbums)).Find(p => p.AlbumId == albumId).Ordering = ++maxorder;
                }
            }

            Save();

            if (et != null)
            {
                return (PictureEntity)et.Entity;
            }
            else
            {
                return picture;
            }
        }

        /// <summary>
        /// Method to add an <see cref="IEnumerable{T}"/> list of <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="albumId">The <see cref="AlbumEntity"/> primary key for association.</param>
        /// <param name="pictures"><see cref="IEnumerable{T}"/> list of <see cref="PictureEntity"/> to add.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> list of <see cref="PictureEntity"/>.</returns>
        public IEnumerable<PictureEntity> Add(int albumId, IEnumerable<PictureEntity> pictures)
        {
            IList<PictureEntity> lp = new List<PictureEntity>();
            int insertId = InsertId(Connector.Pictures, x => x.PictureId);

            foreach (PictureEntity p in pictures)
            {
                lp.Add(Add(albumId, p, insertId));
                insertId++;
            }

            return lp;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a <see cref="IEnumerable{T}"/> list of <see cref="PictureEntity"/> from database.
        /// </summary>
        /// <param name="op">The <see cref="PictureOptionsList"/> list of options for filter the query.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> list of <see cref="PictureEntity"/>.</returns>
        public IEnumerable<PictureEntity> List(PictureOptionsList op = default(PictureOptionsList))
        {
            // Initialize default option list.
            op = op ?? new PictureOptionsList { };

            // Initialize query.
            IQueryable<PictureEntity> query = Connector.Pictures;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

            // Load dependencies if required.
            QueryFilterAlbums(ref query, op);

            // Check for filter primary keys to search in.
            query.QueryListFilter(op);

            // Set number elements to skip & the number elements to select.
            query.QueryStartLimit(op);



            // Return a list of entities.
            return query.ToList();
        }

        /// <summary>
        /// Method to filter by <see cref="AlbumEntity"/> a <see cref="IQueryable"/> query of <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable"/> query of <see cref="PictureEntity"/>.</param>
        /// <param name="op">The <see cref="PictureOptionsList"/> list of options for filter the query.</param>
        /// <returns>A list of Section entities.</returns>
        private void QueryFilterAlbums(ref IQueryable<PictureEntity> query, PictureOptionsList op = default(PictureOptionsList))
        {
            // Initialize default option list.
            op = op ?? new PictureOptionsList { };

            // Check for the desired album associations.
            if (op.IncludeAlbumId != null && op.IncludeAlbumId.Count > 0)
            {
                query.Where(x => x.PicturesInAlbums.Any(y => op.IncludeAlbumId.Contains(y.AlbumId)));
            }

            // Check for the undesired album association.
            if (op.ExcludeAlbumId != null && op.ExcludeAlbumId.Count > 0)
            {
                query.Where(x => !x.PicturesInAlbums.Any(y => op.IncludeAlbumId.Contains(y.AlbumId)));
            }
        }

        /// <summary>
        /// Method to include dependencies to a <see cref="IQueryable"/> query of <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable"/> query of <see cref="PictureEntity"/>.</param>
        /// <param name="op">The <see cref="EntitiesOptions"/> list of options for filter the query.</param>
        /// <returns>A list of Section entities.</returns>
        private void QueryDependencies(ref IQueryable<PictureEntity> query, EntitiesOptions op = default(PictureOptionsList))
        {
            // Initialize default option select.
            op = op ?? new PictureOptionsSelect();

            // Load Album dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.PicturesInAlbums) || User != null)
            {
                query.Include(x => x.PicturesInAlbums);
            }

            // Load Info dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.InfosInPictures))
            {
                query.Include(x => x.InfosInPictures);
            }
        }

        /// <summary>
        /// Method to select a <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="op">Picture entities select options to perform query.</param>
        /// <param name="nullable"></param>
        /// <returns>An Picture entity or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Occurs if Picture Select Options are null <see cref="PictureOptionsSelect"/></exception>
        public PictureEntity Select(PictureOptionsSelect op, bool nullable = false)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {op?.GetType()}");

            if (op == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(op), typeof(PictureOptionsSelect));
                log.Error($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {e.Output()}");
                throw e;
            }

            if (!SetSafeUser(op.UserId)) return null;

            // Initialize query.
            IQueryable<PictureEntity> query = Connector.Pictures;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

            if (op.PrimaryKey > 0)
            {
                query = query.Where(x => x.PrimaryKey == op.PrimaryKey);
            }

            if (!op.Alias.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.Alias == op.Alias);
            }

            // Filter by User. 
            //if (entity != null && user != null)
            //{
            //    SectionEntity section = null;

            //    foreach (AlbumsInSections sectDep in entity.AlbumsInSections)
            //    {
            //        IQueryable<SectionEntity> sections = Connector.Sections;
            //        sections = sections.Include(x => x.SectionsInAclGroups);
            //        SectionEntity s = sections.SingleOrDefault(x => x.PrimaryKey == sectDep.SectionId);

            //        if (s.PrimaryKey == 0)
            //        {
            //            return null;
            //        }

            //        foreach (SectionsInAclGroups aclgDep in s.SectionsInAclGroups)
            //        {
            //            section = GetUserSection(aclgDep.SectionId, user);
            //            if (section != null && section.PrimaryKey > 0) break;
            //        }

            //        if (section != null && section.PrimaryKey > 0) break;
            //    }

            //    if (section == null)
            //    {
            //        return null;
            //    }
            //}
            
            return SingleDefaultOrNull(query, nullable);
        }

        /// <summary>
        /// Method to select a <see cref="PictureEntity"/> by its unique identifier or primar key.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable"/> query of <see cref="PictureEntity"/>.</param>
        /// <param name="op">The <see cref="PictureOptionsSelect"/> options filters.</param>
        /// <returns>A <see cref="PictureEntity"/> or null if not found.</returns>
        private PictureEntity SingleIdOrNull(IQueryable<PictureEntity> query, PictureOptionsSelect op)
        {
            PictureEntity entity = query.SingleOrDefault(x => x.PictureId == op.PrimaryKey);

            // Check if user is found, return null instead of default.
            if (entity != null && entity.AlbumId == 0)
            {
                return null;
            }

            return entity;
        }

        /// <summary>
        /// Method to get max pictures ordering.
        /// </summary>
        /// <param name="albumId">An <see cref="AlbumEntity"/> unique identifier or primary key.</param>
        public int MaxOrder(int albumId = 0)
        {
            return Connector.PicturesInAlbums
                .Where(d => d.AlbumId == albumId)
                .Select(d => d.Ordering)
                .DefaultIfEmpty(0).Max();
        }
        
        /// <summary>
        /// Method to update an <see cref="PictureEntity"/>.
        /// </summary>
        /// <param name="entity">An <see cref="PictureEntity"/> to update.</param>
        /// <param name="autoDate">Automatic set default dates ?</param>
        /// <returns>The updated <see cref="PictureEntity"/>.</returns>
        public async Task<PictureEntity> UpdateAsync(PictureEntity entity, bool autoDate = true)
        {
            // Remove Albums in Sections dependencies.
            DeleteDependencyAlbums(entity);
            DeleteDependencyInfos(entity);

            // Update item informations.
            entity = Connector.Update(entity).Entity;

            await SaveAsync();

            // Return the updated item.
            return entity;
        }

        /// <summary>
        /// Method to delete <see cref="PicturesInAlbums"/> associations.
        /// </summary>
        /// <param name="entity">The <see cref="PictureEntity"/> to process with.</param>
        private async void DeleteDependencyAlbums(PictureEntity entity)
        {
            if (entity.PicturesInAlbums.DepPKeysRemoved.Count > 0)
            {
                await DeleteDependencyAsync(
                    new EntityManagerDeleteDependency { Name = "PicturesInAlbums", key = "PictureId", keyList = "AlbumId" },
                    entity.PrimaryKey,
                    entity.PicturesInAlbums.DepPKeysRemoved
                );
                entity.PicturesInAlbums.DepPKeysRemoved.Clear();

                log.Debug("Delete Pictures in Albums associations. Done.");
            }
        }

        /// <summary>
        /// Method to delete <see cref="PicturesInAlbums"/> associations.
        /// </summary>
        /// <param name="entity">The <see cref="PictureEntity"/> to process with.</param>
        private async void DeleteDependencyInfos(PictureEntity entity)
        {
            if (entity.InfosInPictures.DepPKeysRemoved.Count > 0)
            {
                await DeleteDependencyAsync(
                    new EntityManagerDeleteDependency { Name = "InfosInPictures", key = "PictureId", keyList = "InfoId" },
                    entity.PrimaryKey,
                    entity.InfosInPictures.DepPKeysRemoved
                );
                entity.InfosInPictures.DepPKeysRemoved.Clear();

                log.Debug("Delete Infos in Pictures associations. Done.");
            }
        }

        #endregion
    }
}
