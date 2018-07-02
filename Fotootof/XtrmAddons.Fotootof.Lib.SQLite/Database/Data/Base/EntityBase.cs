using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Common.Objects;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Lib SQLite Database Data Base Entity.</para>
    /// <para>This class provide base properties and methods for database Entity object.</para>
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public abstract class EntityBase : ObjectBaseNotifier, IEntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the database connector.
        /// </summary>
        [XmlIgnore]
        private static DatabaseCore db;

        /// <summary>
        /// Variable to store the primary key auto incremented value.
        /// </summary>
        [XmlIgnore]
        protected int primaryKey = 0;

        #endregion



        #region Proprerties

        /// <summary>
        /// Property to access to the application database connector.
        /// </summary>
        [NotMapped, XmlIgnore]
        public static DatabaseCore Db
        {
            get => db;
            set => db = value;
        }

        /// <summary>
        /// Property alias to access to the Primary Key (PK or Id) of the entity.
        /// </summary>
        [NotMapped]
        [JsonProperty(PropertyName = "Id")]
        [XmlAttribute(DataType = "int", AttributeName = "Id")]
        public int PrimaryKey
        {
            get => primaryKey;
            set
            {
                if (value != primaryKey)
                {
                    primaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get an observable collection of primaries keys.
        /// </summary>
        /// <typeparam name="T">The Class of dependencies.</typeparam>
        /// <param name="dependencies">A list of Entity dependencies.</param>
        /// <param name="primaryKeyName">The associated key name of the dependencies.</param>
        /// <returns>A list of dependencies primary keys.</returns>
        /// <exception cref="ArgumentNullException">Occurs if primaryKeyName argument is null, empty or whitespace.</exception>
        [Obsolete("Use property of observable dependency. Change dependency property to get new observable list.")]
        public IEnumerable<int> ListOfPrimaryKeys<T>(IEnumerable<T> dependencies, string primaryKeyName) where T : class
        {
            // Check if primary key name is valid, if not throw exception.
            if(primaryKeyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = ArgumentNullException(nameof(primaryKeyName));
                log.Fatal(ane.Output());
                throw ane;
            }

            // Initialize list of primary keys.
            List<int> ids = new List<int>();

            // Check if dependencies are not null, if not return empty list.
            if (dependencies == null)
            {
                log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {nameof(dependencies)} => {ids}");
                return ids;
            }

            // Convert dependencies into a list of primary keys.
            foreach (object o in dependencies)
            {
                ids.Add((int)((T)o).GetPropertyValue(primaryKeyName));
            }

            return ids;
        }

        /// <summary>
        /// Method to get a list of associated entities.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="dependencies">An collection of entities dependencies.</param>
        /// <returns>A list of AclAction associated to the dependencies.</returns>
        [Obsolete("Use property of observable dependency. Change dependency property to get new observable list.")]
        public static List<T> ListEntities<T>(IEnumerable dependencies) where T : class
        {
            log.Debug($"{typeof(EntityBase).Name}.{MethodBase.GetCurrentMethod().Name} : Object Type => {typeof(T)}");

            List<T> items = new List<T>();

            if (dependencies != null)
            {
                foreach (object dependency in dependencies)
                {
                    T dep = (T)dependency.GetPropertyValue(typeof(T).Name);

                    if (dep != null)
                    {
                        items.Add(dep);
                    }
                    else
                    {
                        T item = null;
                        int id = (int)dependency.GetPropertyValue(typeof(T).Name.Replace("Entity", "") + "Id");

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
            else
            {
                log.Warn($"{typeof(EntityBase).Name}.{MethodBase.GetCurrentMethod().Name} : {nameof(dependencies)} => {dependencies}");
            }

            return items;
        }

        #endregion



        #region Methods Exceptions

        /// <summary>
        /// Method to get a formated argument null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        protected static ArgumentNullException ArgumentNullException(string propertyName)
        {
            return new ArgumentNullException($"The argument {propertyName} must be not null, empty or whitespace !");
        }

        #endregion
    }
}
