using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Entity;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Info Types Entities Manager.
    /// </summary>
    public partial class InfoTypeManager : EntitiesManager
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Info Types Entities Manager Constructor.
        /// </summary>
        /// <param name="context">A database context core.</param>
        public InfoTypeManager(DatabaseContextCore context) : base(context) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to get a list of InfoType entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of InfoType entities.</returns>
        public List<InfoType> List(InfoTypeOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new InfoTypeOptionsList { };

            // Initialize query.
            IQueryable<InfoType> query = Context.InfoTypes;

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
        private void QueryDependencies(ref IQueryable<InfoType> query, EntitiesOptions op = null)
        {
            // Load ACLGroup dependency if requered.
            if (op.IsDependOn(EnumEntitiesDependencies.InfosInAlbums))
            {
                query = query.Include(x => x.InfosInAlbums);
            }

            // Load Album dependency if requered.
            if (op.IsDependOn(EnumEntitiesDependencies.InfosInPictures))
            {
                query = query.Include(x => x.InfosInPictures);
            }
        }

        /// <summary>
        /// Method to select an Section.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        public Section Select(SectionOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<Section> query = Context.Sections;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

            List<string> keys = new List<string>();
            if (op.PrimaryKey > 0)
            {
                keys.Add("PrimaryKey");
            }

            if (op.Alias != "")
            {
                keys.Add("Alias");
            }
            
            return SingleDefaultOrNull(query, op, keys.ToArray(), nullable);
        }

        #endregion
    }
}
