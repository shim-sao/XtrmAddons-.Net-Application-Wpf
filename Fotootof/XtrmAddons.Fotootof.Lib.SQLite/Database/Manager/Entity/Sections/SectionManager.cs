using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Sections Entities Manager.
    /// </summary>
    public partial class SectionManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Sections Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public SectionManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of Section entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of Section entities.</returns>
        public List<SectionEntity> List(SectionOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new SectionOptionsList { };

            // Initialize query.
            IQueryable<SectionEntity> query = Context.Sections;

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
        /// <param name="dependencies">Load also dependencies.</param>
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
        /// <returns>An Section entity or null if not found.</returns>
        public SectionEntity Select(SectionOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<SectionEntity> query = Context.Sections;

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

        #endregion
    }
}
