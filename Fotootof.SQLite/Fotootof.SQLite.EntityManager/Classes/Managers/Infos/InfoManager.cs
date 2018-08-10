using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Info Types Entities Manager.
    /// </summary>
    public partial class InfoManager : EntitiesManager
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



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
        /// <param name="op"></param>
        /// <returns>A list of InfoType entities.</returns>
        public List<InfoEntity> List(InfoOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new InfoOptionsList { };

            // Initialize query.
            IQueryable<InfoEntity> query = Connector.Infos;

            // Load dependencies if required.
            QueryDependencies(ref query, op);

            // Check for filter primary keys to search in.
            query.QueryListFilter(op);

            // Filter by info type alias.
            if(op.InfoTypesAlias != null && op.InfoTypesAlias.Count > 0)
            {
                List<InfoTypeEntity> types = Connector.InfoTypes.Where(x => op.InfoTypesAlias.Contains(x.Alias)).ToList();

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
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns>A list of Section entities.</returns>
        private void QueryDependencies(ref IQueryable<InfoEntity> query, EntitiesOptions op = null)
        {
        }

        /// <summary>
        /// Method to select an Section.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <param name="nullable"></param>
        /// <returns>An Section entity or null if not found.</returns>
        public InfoEntity Select(InfoOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<InfoEntity> query = Connector.Infos;

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
        /// Method to inialize content of the table AclGroup after EnsureCreated()
        /// </summary>
        internal void InitializeTable()
        {
            try
            {
                log.Info("SQLite Initializing Table `Infos`. Please wait...");

                // Picture Quality
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 1, InfoTypeId = 1, Name = "High", Alias = "high", Description = "Defines High Quality of pictures.", Ordering = 0, IsDefault = true });
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 2, InfoTypeId = 1, Name = "Medium", Alias = "medium", Description = "Defines Medium Quality of pictures.", Ordering = 1 });
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 3, InfoTypeId = 1, Name = "Low", Alias = "low", Description = "Defines Low Quality of pictures.", Ordering = 2 });
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 4, InfoTypeId = 1, Name = "Mixed", Alias = "mixed", Description = "Defines Various Quality of pictures.", Ordering = 3 });

                // Picture Color Type
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 5, InfoTypeId = 2, Name = "True Color", Alias = "true-color", Description = "Defines True Color pictures.", Ordering = 0, IsDefault = true });
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 6, InfoTypeId = 2, Name = "Black White", Alias = "black-white", Description = "Defines Black and White pictures.", Ordering = 1 });
                Connector.Infos.Add(new InfoEntity() { PrimaryKey = 7, InfoTypeId = 2, Name = "Mixed", Alias = "mixed", Description = "Defines mixed pictures color.", Ordering = 2 });

                int result = Save();
                log.Info($"SQLite Initializing Table `Info`. {result} affected rows.");
            }
            catch (Exception ex)
            {
                log.Error("SQLite Initializing Table `Infos`. Exception.");
                log.Error(ex.Output(), ex);
                throw;
            }

            // Infos Types
            try
            {
                log.Info("SQLite Initializing Table `InfoTypes`. Please wait...");
                Connector.InfoTypes.Add(new InfoTypeEntity() { PrimaryKey = 1, Name = "Quality", Alias = "quality", Description = "Defines pictures Quality." });
                Connector.InfoTypes.Add(new InfoTypeEntity() { PrimaryKey = 2, Name = "Color", Alias = "color", Description = "Defines pictures Color." });
                int result = Save();
                log.Info($"SQLite Initializing Table `InfoTypes`. {result} affected rows.");
            }
            catch (Exception ex)
            {
                log.Error("SQLite Initializing Table `InfoTypes`. Exception.");
                log.Error(ex.Output(), ex);
                throw;
            }
        }

        #endregion
    }
}