using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Entities Manager Sections.
    /// </summary>
    public partial class SectionManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof SQLite Entities Manager Sections Constructor.
        /// </summary>
        /// <param name="context">A database connector <see cref="DatabaseContextCore"/></param>
        public SectionManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of Section entities.
        /// </summary>
        /// <param name="op"></param>
        /// <returns>A list of Section entities.</returns>
        public List<SectionEntity> List(SectionOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new SectionOptionsList { };

            // Initialize query.
            IQueryable<SectionEntity> query = Connector.Sections;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

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
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns>A list of Section entities.</returns>
        private void QueryDependencies(ref IQueryable<SectionEntity> query, EntitiesOptions op = null)
        {
            // Load ACLGroup dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.SectionsInAclGroups))
            {
                query = query.Include(x => x.SectionsInAclGroups);
            }

            // Load Album dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.AlbumsInSections))
            {
                query = query.Include(x => x.AlbumsInSections);
            }
        }

        /// <summary>
        /// Method to select an Section.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <param name="nullable"></param>
        /// <returns>An Section entity or null if not found.</returns>
        public SectionEntity Select(SectionOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<SectionEntity> query = Connector.Sections;

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
            
            if(query.Count() > 0)
            {
                return SingleDefaultOrNull(query, nullable);
            }

            if(nullable)
            {
                return null;
            }

            return default(SectionEntity);
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="entity">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public async Task<SectionEntity> UpdateAsync(SectionEntity entity)
        {
            DeleteDependencyAclGroups(entity);
            DeleteDependencyAlbums(entity);

            // Update item informations.
            entity = Connector.Update(entity).Entity;

            await SaveAsync();

            // Return the updated item.
            return entity;
        }

        /// <summary>
        /// Method to delete <see cref="SectionsInAclGroups"/> associations.
        /// </summary>
        /// <param name="entity">The <see cref="SectionEntity"/> to process with.</param>
        private async void DeleteDependencyAclGroups(SectionEntity entity)
        {
            // Remove Users In AclGroups dependencies.
            if (entity.SectionsInAclGroups.DepPKeysRemoved.Count > 0)
            {
                await DeleteDependencyAsync(
                    new EntityManagerDeleteDependency { Name = "SectionsInAclGroups", key = "SectionId", keyList = "AclGroupId" },
                    entity.PrimaryKey,
                    entity.SectionsInAclGroups.DepPKeysRemoved
                );
                entity.SectionsInAclGroups.DepPKeysRemoved.Clear();
            }
        }

        /// <summary>
        /// Method to delete <see cref="AlbumsInSections"/> associations.
        /// </summary>
        /// <param name="entity">The <see cref="SectionEntity"/> to process with.</param>
        private async void DeleteDependencyAlbums(SectionEntity entity)
        {
            // Remove Users In Albums dependencies.
            if (entity.AlbumsInSections.DepPKeysRemoved.Count > 0)
            {
                await DeleteDependencyAsync(
                    new EntityManagerDeleteDependency { Name = "AlbumsInSections", key = "SectionId", keyList = "AlbumId" },
                    entity.PrimaryKey,
                    entity.AlbumsInSections.DepPKeysRemoved
                );
                entity.AlbumsInSections.DepPKeysRemoved.Clear();
            }
        }

        #endregion
    }
}
