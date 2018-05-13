using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Manager.
    /// </summary>
    public abstract class EntitiesManager
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Variable database context entity core.
        /// </summary>
        public DatabaseContextCore Context { get; set; } = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Database Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public EntitiesManager(DatabaseContextCore context)
        {
            Context = context;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to select single item by id or primary key.
        /// </summary>
        /// <typeparam name="T">The Entity type.</typeparam>
        /// <param name="query">The query to search item.</param>
        /// <param name="exp">The expression to find the primary key.</param>
        /// <returns>An entity or null if not found.</returns>
        public T SingleIdOrNull<T>(IQueryable<T> query, Expression<Func<T, bool>> exp) where T : class
        {
            T item = null;

            try
            {
                item = query.SingleOrDefault(exp);
            }
            catch(Exception e)
            {
                log.Debug(e.Output());
                log.Debug(exp);

                return null;
            }

            // Check if item is found, return null instead of default.
            if (item != null && (int)item.GetPropertyValue("PrimaryKey") == 0)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        /// Method to save database context core changes.
        /// </summary>
        /// <param name="save">Process database context saving ?</param>
        /// <returns>The result of the database context saving process.</returns>
        public int Save(bool save = true)
        {
            if (save)
            {
                log.Debug("Saving database context set to true.");
                return Context.SaveChanges();
            }
            else
            {
                log.Debug("Saving database context set to false.");
            }

            return 0;
        }

        /// <summary>
        /// Method to add a new Entity.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="entity">An entity to add.</param>
        /// <param name="save">Process database context saving ?</param>
        /// <returns>The added Entity.</returns>
        public T Add<T>(T entity, bool save = true) where T : class
        {
            entity = Context.Add(entity).Entity;
            Save(save);
            return entity;
        }

        /// <summary>
        /// Method to delete an Entity of the database.
        /// </summary>
        /// <typeparam name="T">The Class of the entity.</typeparam>
        /// <param name="entity">The entity to remove from the database.</param>
        /// <param name="save">Process database context saving ?</param>
        /// <returns>The removed Entity.</returns>
        public T Delete<T>(T entity, bool save = true) where T : class
        {
            // Delete item informations.
            entity = Context.Remove(entity).Entity;
            Save(save);
            return entity;
        }

        /// <summary>
        /// Method to get the insert id (current + 1).
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="query">Queryable context entities.</param>
        /// <param name="exp">Delegate expression of Select.</param>
        /// <returns>Return the current inserted id of the entity type.</returns>
        public int InsertId<T>(IQueryable<T> query, Func<T, int> exp) where T : class
        {
            return query
                .Select(exp)
                .DefaultIfEmpty(0).Max()
                + 1; // current + 1
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="entity">An Entity to update.</param>
        /// <param name="save">Process database context saving ?</param>
        /// <returns>The updated Entity.</returns>
        public T Update<T>(T entity, bool save = true) where T : class
        {
            entity = Context.Update(entity).Entity;
            Save(save);
            return entity;
        }

        /// <summary>
        /// Method to update a list of T entities.
        /// </summary>
        /// <typeparam name="T">The Class of the entities.</typeparam>
        /// <param name="entities">A list of entities to update.</param>
        /// <param name="save">Process database context saving ?</param>
        /// <returns>The updated list of T entity.</returns>
        public List<T> Update<T>(List<T> entities, bool save = true) where T : class
        {
            // New list for updated T return.
            List<T> newEntities = new List<T>();

            // Loop over the list of T to update.
            foreach (T entity in entities)
            {
                newEntities.Add(Update(entity, false));
            }

            // Save changes if required.
            if (save)
            {
                Save();
            }

            // Return updates T.
            return newEntities;
        }

        /// <summary>
        /// Method to select an entity by keys | values.
        /// </summary>
        /// <typeparam name="T">The Class of the entity.</typeparam>
        /// <param name="entity">An Entity to select.</param>
        /// <param name="nullable">Return null instead of default entity ?</param>
        /// <returns>An entity or default or null if not found.</returns>
        public T SingleDefaultOrNull<T>(IQueryable<T> query, bool nullable = false) where T : class
        {
            T item = null;

            try
            {
                item = query.SingleOrDefault();
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return null;
            }

            // Check if user is found, return null instead of default.
            if ((int)item.GetPropertyValue("PrimaryKey") == 0 && nullable)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        /// Method to delete association between two entities.
        /// </summary>
        /// <param name="definition">Entity manager delete dependency definition.</param>
        /// <param name="entityPK">The id of the entity.</param>
        /// <param name="depdenciesPK">The list of id of another entity.</param>
        /// <returns>Async task int query result.</returns>
        public async Task<int> DeleteDependencyAsync(EntityManagerDeleteDependency definition, int entityPK, IEnumerable<int> depdenciesPK)
        {
            string pks = string.Join(",", depdenciesPK);
            string query = "DELETE FROM " + definition.Name
                    + string.Format(" WHERE " + definition.key + " = {0} AND " + definition.keyList + " IN ({1})", entityPK, pks);

            int result = await Context.Database.ExecuteSqlCommandAsync(query);

            Save();

            return result;
        }

        /// <summary>
        /// Method to clean association between two entities.
        /// </summary>
        /// <param name="definition">Entity manager delete dependency definition.</param>
        /// <param name="entityPK">The id of the entity.</param>
        /// <param name="depdenciesPK">The list of id of another entity.</param>
        /// <returns>Async task int query result.</returns>
        public async Task<int> CleanDependencyAsync(EntityManagerDeleteDependency definition, int entityPK, IEnumerable<int> depdenciesPK)
        {
            string pks = string.Join(",", depdenciesPK);
            string query = "DELETE FROM " + definition.Name
                    + string.Format(" WHERE " + definition.key + " = {0} AND " + definition.keyList + " NOT IN ({1})", entityPK, pks);

            int result = await Context.Database.ExecuteSqlCommandAsync(query);

            Save();

            return result;
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="item">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public async void SetDefaultAsync(string tableName, string pkName, int entityPK)
        {
            string query1 = "UPDATE " + tableName + " SET IsDefault = 0";
            string query2 = "UPDATE " + tableName + " SET IsDefault = 1 WHERE " + pkName + " = " + entityPK;
            int result = await Context.Database.ExecuteSqlCommandAsync(query1);
            result = await Context.Database.ExecuteSqlCommandAsync(query2);
        }

        #endregion
    }
}
