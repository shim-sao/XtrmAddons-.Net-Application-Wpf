using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Info Types Entities Manager.
    /// </summary>
    public partial class InfoManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Infos Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public InfoManager(DatabaseContextCore context) : base(context) { }

        #endregion


        
        #region Methods

        /// <summary>
        /// Method to get a list of InfoType entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of InfoType entities.</returns>
        public List<InfoEntity> List(InfoOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new InfoOptionsList { };

            // Initialize query.
            IQueryable<InfoEntity> query = Context.Infos;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

            // Check for filter primary keys to search in.
            query.QueryListFilter(op);

            // Filter by info type alias.
            if(op.InfoTypesAlias != null && op.InfoTypesAlias.Count > 0)
            {
                List<InfoTypeEntity> types = Context.InfoTypes.Where(x => op.InfoTypesAlias.Contains(x.Alias)).ToList();

                if (types != null && types.Count > 0)
                {
                    int[] ids = new int[types.Count];

                    int i = 0;
                    foreach (InfoTypeEntity t in types)
                    {
                        ids[i] = t.PrimaryKey;
                        i++;
                    }

                    query = query.Where(x => ids.Contains(x.InfoTypeId));
                }
            }

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
        private void QueryDependencies(ref IQueryable<InfoEntity> query, EntitiesOptions op = null)
        {
        }

        /// <summary>
        /// Method to select an Section.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        public InfoEntity Select(InfoOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<InfoEntity> query = Context.Infos;

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

        #endregion
    }
}