using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Pictures Manager.
    /// </summary>
    public partial class PictureManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Pictures Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public PictureManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to add a Picture entity.
        /// </summary>
        /// <param name="albumId">The album primary key for association.</param>
        /// <param name="picture">The Picture entity to add.</param>
        /// <returns>An added Picture entity.</returns>
        public PictureEntity Add(int albumId, PictureEntity picture, int insertId = 0)
        {
            // Search if item is already in database.
            PictureEntity item = Context.Pictures.Where(p => p.PicturePath == picture.PicturePath).SingleOrDefault();

            // Get the max order of pictures associated to the album.
            int maxorder = MaxOrder(albumId);

            // Initialize return entity.
            EntityEntry et = null;

            if (insertId == 0)
            {
                insertId = InsertId(Context.Pictures, x => x.PictureId);
            }

            if (picture == null || picture.PictureId == 0)
            {
                picture.PictureId = insertId;
                picture.PicturesInAlbums =
                    new ObservableCollection<PicturesInAlbums>
                    {
                        new PicturesInAlbums
                        {
                            AlbumId = albumId,
                            Ordering = ++maxorder
                        }
                    };

                et = Context.Pictures.Add(picture);
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

                    et = Context.Pictures.Update(picture);
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
        /// Method to add a list of Picture entities.
        /// </summary>
        /// <param name="albumId">The album primary key for association.</param>
        /// <param name="picture">The list of Picture entities to add.</param>
        /// <returns>A list of added Picture entities.</returns>
        public List<PictureEntity> Add(int AlbumId, List<PictureEntity> pictures)
        {
            List<PictureEntity> lp = new List<PictureEntity>();
            int insertId = InsertId(Context.Pictures, x => x.PictureId);

            foreach (PictureEntity p in pictures)
            {
                lp.Add(Add(AlbumId, p, insertId));
                insertId++;
            }

            return lp;
        }

        /// <summary>
        /// Method to get a list of picture entities.
        /// </summary>
        /// <param name="depencies">Load also dependencies.</param>
        /// <returns>A list of user entities.</returns>
        public List<PictureEntity> List(PictureOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new PictureOptionsList { };

            // Initialize query.
            IQueryable<PictureEntity> query = Context.Pictures;

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
        /// Method to get a list of Section entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of Section entities.</returns>
        private void QueryFilterAlbums(ref IQueryable<PictureEntity> query, PictureOptionsList op = null)
        {
            if (op.IncludeAlbumId != null && op.IncludeAlbumId.Count > 0)
            {
                query.Where(x => x.PicturesInAlbums.Any(y => op.IncludeAlbumId.Contains(y.AlbumId)));
            }

            if (op.ExcludeAlbumId != null && op.ExcludeAlbumId.Count > 0)
            {
                query.Where(x => !x.PicturesInAlbums.Any(y => op.IncludeAlbumId.Contains(y.AlbumId)));
            }
        }

        /// <summary>
        /// Method to get a list of Section entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of Section entities.</returns>
        private void QueryDependencies(ref IQueryable<PictureEntity> query, EntitiesOptions op = null)
        {
            // Load Album dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.PicturesInAlbums))
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
        /// Method to select a Picture entity.
        /// </summary>
        /// <param name="op">Picture entities select options to perform query.</param>
        /// <returns>An Picture entity or null if not found.</returns>
        public PictureEntity Select(PictureOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<PictureEntity> query = Context.Pictures;

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

            return SingleDefaultOrNull(query, nullable);
        }

        /// <summary>
        /// Method to select an Picture by id.
        /// </summary>
        /// <param name="query">A Pictures query.</param>
        /// <param name="op">A select picture options filters.</param>
        /// <returns>An Picture entity or null if not found.</returns>
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
        /// <param name="albumId">An album primary key.</param>
        public int MaxOrder(int albumId = 0)
        {
            return Context.PicturesInAlbums
                .Where(d => d.AlbumId == albumId)
                .Select(d => d.Ordering)
                .DefaultIfEmpty(0).Max();
        }

        #endregion
    }
}
