using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Versions Entities Manager.
    /// </summary>
    public partial class VersionManager : EntitiesManager
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private new static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



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

        /// <summary>
        /// Method to inialize content of the table AclGroup after EnsureCreated()
        /// </summary>
        internal void InitializeTable()
        {
            try
            {
                log.Info("SQLite Initializing Table `Versions`. Please wait...");
                Context.Versions.Add(new VersionEntity() { PrimaryKey = 0, AssemblyVersionMin = Assembly.GetExecutingAssembly().GetName().Version.ToString(), Comment = "Database creation." });
                int result = Save();
                log.Info($"SQLite Initializing Table `Versions`. {result} affected rows.");
            }
            catch (Exception ex)
            {
                log.Error("SQLite Initializing Table `Versions`. Exception.");
                log.Error(ex.Output(), ex);
                throw ex;
            }
        }

        /// <summary>
        /// Method to inialize content of the table AclGroup after EnsureCreated()
        /// </summary>
        internal async void CheckVersion()
        {
            try
            {
                // Get the latest update version inserted.
                var versionId = Context.Versions.Max(x => x.PrimaryKey);
                var version = Context.Versions.Single(x => x.PrimaryKey == versionId);

                // Create versions
                var currentVersion = new Version(version.AssemblyVersionMin);
                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

                log.Info($"Current Assembly Version : {currentVersion}");

                // Create list of update versions.
                Dictionary<string, string> versions = new Dictionary<string, string>()
                {
                    { "1.0.18123.2149", Properties.Database.SrcSQLiteDatabaseUpdate_1_0_18123_2149 },
                    { "1.0.18134.1044", Properties.Database.SrcSQLiteDatabaseUpdate_1_0_18134_1044 },
                    { "1.0.18137.1050", Properties.Database.SrcSQLiteDatabaseUpdate_1_0_18137_1050 },
                    { "1.0.18210.1228", Properties.Database.SrcSQLiteDatabaseUpdate_1_0_18210_1228 }
                };

                // Check for the latest version to skeep process
                // if not required at all connections.
                Version old = new Version(versions.Keys.Last());
                if(currentVersion >= old)
                {
                    return;
                }

                // Process to the updates
                foreach (var ver in versions)
                {
                    old = new Version(ver.Key);
                    if (currentVersion < old)
                    {
                        log.Info($"Updating Assembly Minimum Version : {old}");

                        await Context.Database.ExecuteSqlCommandAsync(ver.Value);
                        Context.Versions.Add(new VersionEntity() { PrimaryKey = 0, AssemblyVersionMin = old.ToString(), Comment = "Database auto update version." });
                        await SaveAsync();

                        log.Info($"Updating Assembly Minimum Version : {old}. Done !");
                    }
                    else
                    {
                        log.Info($"Updating Assembly Minimum Version : {old}. Skipped !");
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                throw ex;
            }
        }

        #endregion
    }
}