using System;
using System.Linq;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Manager Base Entities Query.
    /// </summary>
    public static class EntitiesQueryExtension
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods

        /// <summary>
        /// Method to filter a query by a primary key or property value. 
        /// </summary>
        /// <typeparam name="T">The Class type of entity.</typeparam>
        /// <param name="query">A query of entities.</param>
        /// <param name="op">Entities options list.</param>
        /// <param name="pk">The name of the column or property to filter.</param>
        public static void QueryListInclude<T>(this IQueryable<T> query, EntitiesOptionsList op, string pk = "PrimaryKey") where T : class
        {
            if (op.IncludePrimaryKeys != null && op.IncludePrimaryKeys.Count > 0)
            {
                query.Where(x => op.IncludePrimaryKeys.Contains((int)x.GetPropertyValue(pk, false)));
            }
        }

        /// <summary>
        /// Method to filter query by exclusion of a primary key or property value. 
        /// </summary>
        /// <typeparam name="T">The Class type of entity.</typeparam>
        /// <param name="query">A query of entities.</param>
        /// <param name="op">Entities options list.</param>
        /// <param name="pk">The name of the column or property to filter.</param>
        public static void QueryListExclude<T>(this IQueryable<T> query, EntitiesOptionsList op, string pk = "PrimaryKey") where T : class
        {
            if (op.ExcludePrimaryKeys != null && op.ExcludePrimaryKeys.Count > 0)
            {
                query = query.Where(x => !op.ExcludePrimaryKeys.Contains((int)x.GetPropertyValue(pk, false)));
            }
        }

        /// <summary>
        /// Method to filter by inclusion and exclusion of a primary key or property value.
        /// </summary>
        /// <typeparam name="T">The Class type of entity.</typeparam>
        /// <param name="query">A query of entities.</param>
        /// <param name="op">Entities options list.</param>
        /// <param name="pk">The name of the column or property to filter.</param>
        public static void QueryListFilter<T>(this IQueryable<T> query, EntitiesOptionsList op, string pk = "PrimaryKey") where T : class
        {
            query.QueryListInclude(op, pk);
            query.QueryListExclude(op, pk);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The Class type of entity.</typeparam>
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public static void QueryStartLimit<T>(this IQueryable<T> query, EntitiesOptionsList op) where T : class
        {
            query.QueryStartLimit(op.Start, op.Limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The Class type of entity.</typeparam>
        /// <param name="query"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static void QueryStartLimit<T>(this IQueryable<T> query, int start, int limit) where T : class
        {
            // Set number elements to skip.
            if (start > 0)
            {
                query = query.Skip(start);
            }

            // Set the number elements to select.
            if (limit > 0)
            {
                query = query.Take(limit);
            }
        }

        /// <summary>
        /// Method to select an entity or null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="exp"></param>
        /// <param name="pk"></param>
        /// <returns>An entity entity or null if not found.</returns>
        public static T SingleOrNull<T>(this IQueryable<T> query, Func<T, bool> exp, string pk = "PrimaryKey") where T : class
        {
            T item = query.SingleOrDefault(exp);

            // Check if user is found, return null instead of default.
            if (item == default(T))
            {
                return null;
            }

            return item;
        }

        #endregion
    }
}