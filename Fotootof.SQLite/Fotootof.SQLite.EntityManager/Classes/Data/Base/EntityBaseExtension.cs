using Fotootof.SQLite.EntityManager.Connector;
using System.Collections;
using System.Collections.Generic;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Base Entity.
    /// </summary>
    [System.Obsolete("Use property of observable dependency. Change dependency property to get new observable list.")]
    public static class EntityBaseExtension
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the application database connector.
        /// </summary>
        public static DatabaseCore Db { get => EntityBase.Db; set => EntityBase.Db = value; }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of associated dependencies of an entity.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="dependencies">An collection of entities dependencies.</param>
        /// <returns>A list of AclAction associated to the dependencies.</returns>
        public static List<T> ListEntities<T>(ref T entity, IEnumerable dependencies) where T : class
        {
            List<T> items = new List<T>();

            if (dependencies != null)
            {
                foreach (object dependency in dependencies)
                {
                    T dep = (T)dependency.GetPropertyValue(entity.GetType().Name);

                    if (dep != null)
                    {
                        items.Add(dep);
                    }
                    else
                    {
                        T item = null;
                        int id = (int)dependency.GetPropertyValue(entity.GetType().Name + "Id");

                        using (Db.Context)
                        {
                            item = Db.Context.Find<T>(id);
                        }

                        if (item != null)
                        {
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        #endregion
    }
}
