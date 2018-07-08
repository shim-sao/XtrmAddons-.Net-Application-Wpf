using System;
using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Versions Entities Manager.
    /// </summary>
    [System.Obsolete("Use others mechanisms. Table will be deleted.")]
    public partial class VersionManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Users Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public VersionManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of Version entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of Version entities.</returns>
        public List<VersionEntity> List(VersionOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new VersionOptionsList { };

            // Initialize query.
            IQueryable <VersionEntity> query = Context.Versions;

            // Check for include primary keys to search in.
            query.QueryListInclude(op);

            // Check for exclude primary keys in search.
            query.QueryListExclude(op);            

            // Set number elements to skip & the number elements to select.
            query.QueryStartLimit(op);

            // Return a list of entities.
            return query.ToList();
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="item">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public VersionEntity Update(VersionEntity entity, bool save = true)
        {
            // Update item informations.
            entity = Context.Update(entity).Entity;

            // Save changes on the database.
            if (save)
            {
                Save();
            }

            // Return updated item.
            return entity;
        }

        /// <summary>
        /// Method to select an Version.
        /// </summary>
        /// <param name="op">Versions entities select options to perform query.</param>
        /// <returns>A Version entity or null if not found.</returns>
        public VersionEntity Select(VersionOptionsSelect op)
        {
            // Initialize query.
            IQueryable<VersionEntity> query = Context.Versions;

            // Initialize
            if (op.PrimaryKey > 0)
            {
                return SingleIdOrNull(query, op);
            }

            if (!op.AssemblyVersionMin.IsNullOrWhiteSpace())
            {
                return SingleAssemblyVersionMinOrNull(query, op);
            }

            if (!op.AssemblyVersionMax.IsNullOrWhiteSpace())
            {
                return SingleAssemblyVersionMaxOrNull(query, op);
            }

            return null;
        }

        /// <summary>
        /// Method to select single User by id.
        /// </summary>
        /// <returns>An User entity or null if not found.</returns>
        public VersionEntity SingleIdOrNull(IQueryable<VersionEntity> query, VersionOptionsSelect op)
        {
            return SingleIdOrNull(query, x => x.VersionId == op.PrimaryKey);
        }

        /// <summary>
        /// Method to select an Version by AssemblyVersionMin.
        /// </summary>
        /// <returns>An Version entity or null if not found.</returns>
        public VersionEntity SingleAssemblyVersionMinOrNull(IQueryable<VersionEntity> query, VersionOptionsSelect op)
        {
            // Select version by AssemblyVersionMin.
            VersionEntity item = null;
            try
            {
                item = query.SingleOrDefault(x => x.AssemblyVersionMin == op.AssemblyVersionMin);
            }
            catch(Exception e)
            {
                log.Error("Version single AssemblyVersionMin not found !", e);
                return null;
            }

            // Check if user is found, return null instead of default.
            if (item != null && item.PrimaryKey == 0)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        /// Method to select an Version by AssemblyVersionMin.
        /// </summary>
        /// <returns>An Version entity or null if not found.</returns>
        public VersionEntity SingleAssemblyVersionMaxOrNull(IQueryable<VersionEntity> query, VersionOptionsSelect op)
        {
            // Select version by AssemblyVersionMax.
            VersionEntity item = null;
            try
            {
                item = query.SingleOrDefault(x => x.AssemblyVersionMax == op.AssemblyVersionMax);
            }
            catch (Exception e)
            {
                log.Error("Version single AssemblyVersionMax not found !", e);
                return null;
            }

            // Check if user is found, return null instead of default.
            if (item != null && item.PrimaryKey == 0)
            {
                return null;
            }

            return item;
        }

        #endregion
    }
}