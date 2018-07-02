using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Q(string s)
        {
            return $"'{s.Replace("'", "\'").Replace("\\'", "\'")}'";

            //if (s[0] == '\'')
            //    s = "'\\" + s;
            //else
            //    s = "'" + s;

            //if (s[s.Length - 1] == '\'')
            //{
            //    s = s.Remove(s.Length - 1, 1) + "\''";
            //}
            //else
            //    s += "'";

            //return s;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
        public string QN(string s)
        {
            return "`" + s.RemoveDiacritics() + "`";
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
        /// <param name="depdencyPKs">The list of id of another entity.</param>
        /// <returns>Asynchronous task int query result. The result is the number of affected rows.</returns>
        public async Task<int> DeleteDependencyAsync(EntityManagerDeleteDependency definition, int entityPK, IEnumerable<int> depdencyPKs, string search = "IN")
        {
            int result = 0;
            string query = "";
            string depTableName = definition.keyList.Replace("Id", "s");

            log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Deleting dependencies => {search} {definition?.Name}.{definition?.key}");
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Entity Primary Key => {entityPK}");
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Dependency Primary Keys => {depdencyPKs}");
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Dependency Table => {depTableName}");
            
            try
            {
                // Becareful : SQLiteException maybe occurs whithout '.
                query = $"DELETE FROM {QN(definition.Name)}"
                    + $" WHERE ({QN(definition.Name)}.{QN(definition.key)} = {Q(entityPK.ToString())}"
                    // Delete all dependencies link if no safe list of primary keys is past.
                    + ((depdencyPKs == null || depdencyPKs.Count() == 0) ? ")" : $" AND {QN(definition.Name)}.{QN(definition.keyList)} {search} ({string.Join(",", depdencyPKs)}))")
                    // Delete all dependencies link that not match to a table row.
                    + $" OR {QN(definition.Name)}.{QN(definition.keyList)} IN ("
                    + $"SELECT {QN(definition.Name)}.{QN(definition.keyList)} FROM {QN(definition.Name)}"
                    + $" LEFT JOIN {QN(depTableName)} ON ({QN(depTableName)}.{QN(definition.keyList)} = {QN(definition.Name)}.{QN(definition.keyList)})"
                    + $" WHERE  {QN(depTableName)}.{QN(definition.keyList)} IS NULL)";

                //log.Debug(query);

                #pragma warning disable EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.
                result = await Context.Database.ExecuteSqlCommandAsync(query);
                #pragma warning restore EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.

                Save();
            }
            catch (SQLiteException se)
            {
                log.Error(query);
                log.Error(se.Output(), se);
                return 0;
            }
            catch (Exception se)
            {
                log.Fatal(query);
                log.Fatal(se.Output(), se);
                return 0;
            }

            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Delete Affected Rows => {result}");
            return result;
        }

        /// <summary>
        /// Method to clean association between two entities asynchronously.
        /// </summary>
        /// <param name="definition">Entity manager delete dependency definition.</param>
        /// <param name="entityPK">The id of the entity.</param>
        /// <param name="depdencyPKs">The list of id of another entity.</param>
        /// <returns>Asynchronous task int query result. The result is the number of affected rows.</returns>
        public async Task<int> CleanDependencyAsync(EntityManagerDeleteDependency definition, int entityPK, IEnumerable<int> depdencyPKs)
        {
            return await DeleteDependencyAsync(definition, entityPK, depdencyPKs, "NOT IN");
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="item">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public async Task<int> SetDefaultAsync(string tableName, string pkName, int entityPK)
        {
            int result = 0;
            log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Setting Default => {tableName}.{pkName}");
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Entity Primary Key => {entityPK}");

            if (tableName.IsNullOrWhiteSpace())
            {
                ArgumentNullException e = new ArgumentNullException(nameof(tableName));
                log.Error(e.Output(), e);
                return 0;
            }

            if (pkName.IsNullOrWhiteSpace())
            {
                ArgumentNullException e = new ArgumentNullException(nameof(pkName));
                log.Error(e.Output(), e);
                return 0;
            }

            if (entityPK < 0)
            {
                IndexOutOfRangeException e = new IndexOutOfRangeException(nameof(pkName));
                log.Error(e.Output(), e);
                return 0;
            }

            string query = "";

            // Becareful : SQLiteException maybe occurs whithout '.
            //log.Debug(query = $"UPDATE `{tableName}` SET `IsDefault` = 0");
            log.Debug(query = $"UPDATE {QN(tableName)} SET {QN("IsDefault")} = {Q("0")}");
            try
            {
#pragma warning disable EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.
                result = await Context.Database.ExecuteSqlCommandAsync(query);
#pragma warning restore EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.
                Save();
            }
            catch (SQLiteException se)
            {
                log.Error(query);
                log.Error(se.Output(), se);
                return 0;
            }
            catch (Exception se)
            {
                log.Fatal(query);
                log.Fatal(se.Output(), se);
                return 0;
            }

            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Affected Rows => {result}");

            // Becareful : SQLiteException maybe occurs whithout '.
            log.Debug(query = $"UPDATE {QN(tableName)} SET {QN("IsDefault")} = {Q("1")} WHERE _rowid_ = {Q(entityPK.ToString())}");
            try
            {
#pragma warning disable EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.
                result = await Context.Database.ExecuteSqlCommandAsync(query);
#pragma warning restore EF1000 // Query is already generated by Linq. Becareful ! Linq on Linq cause SQLiteException.
                Save();
            }
            catch (SQLiteException se)
            {
                log.Error(query);
                log.Error(se.Output(), se);
                return 0;
            }
            catch (Exception se)
            {
                log.Fatal(query);
                log.Fatal(se.Output(), se);
                return 0;
            }

            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Affected Rows => {result}");
            return result;
        }

        #endregion
    }
}
