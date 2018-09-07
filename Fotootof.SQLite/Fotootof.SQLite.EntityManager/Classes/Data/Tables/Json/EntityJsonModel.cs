using Newtonsoft.Json.Linq;
using System;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class Fotootof Plugin Api Models Json Entity.
    /// </summary>
    public class EntityJsonModel<T> : JObject
    {
        /// <summary>
        /// Class Fotootof Plugin Api Models Json Album constructor.
        /// </summary>
        public EntityJsonModel()
        {
            FromObject(((T)Activator.CreateInstance(typeof(T))));
        }

        /// <summary>
        /// Class Fotootof Plugin Api Models Json Album constructor.
        /// </summary>
        /// <param name="album">An Album Entity.</param>
        public EntityJsonModel(T entity)
        {
            FromObject(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        public T ToEntity()
        {
            return ToObject<T>();
        }
    }
}
