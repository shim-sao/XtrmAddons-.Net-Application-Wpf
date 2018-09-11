using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Interfaces;
using Fotootof.SQLite.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Collections Base Entity.
    /// </summary>
    public abstract class CollectionBaseEntity<T, U> : CollectionBase<T>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property wrapper to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => SQLiteSvc.GetInstance();

        /// <summary>
        /// Property to access to the collection options filters.
        /// </summary>
        public U Options { get; set; }

        /// <summary>
        /// Property to access to the default collection options filters.
        /// </summary>
        public U OptionsDefault => GetOptionsDefault();


        /// <summary>
        /// Property to access to the  entity manager name.
        /// </summary>
        public string ManagerName => GetType().Name.Replace("EntityCollection", "") + "s";

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Collections Base Entity Constructor.
        /// </summary>
        /// <param name="autoLoad">Auto load data from database ?</param>
        /// <param name="options">Options for query filters.</param>
        public CollectionBaseEntity(bool autoLoad = false, U options = default(U))
        {
            if (options != null && !options.Equals(default(U)))
            {
                Options = options;
            }

            Initialize(autoLoad);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Collections Base Entity Constructor.
        /// </summary>
        /// <param name="list">A <see cref="List{T}"/> to paste in.</param>
        public CollectionBaseEntity(List<T> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Collections Base Entity Constructor.
        /// </summary>
        /// <param name="collection">A <see cref="IEnumerable{T}"/> to paste in.</param>
        public CollectionBaseEntity(IEnumerable<T> collection) : base(collection) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to create default entities collection filters for autoload.
        /// </summary>
        /// <returns>An object options list filters according to the entities collection.</returns>
        protected static U GetOptionsDefault()
        {
            U item = (U)Activator.CreateInstance(typeof(U));
            item.SetPropertyValue("Dependencies", new EnumEntitiesDependencies[] { EnumEntitiesDependencies.None });

            return item;
        }

        /// <summary>
        /// Class method to load a list of AclGroup from database.
        /// </summary>
        public override void Load()
        {
            LoadOptions(default(U));
        }

        /// <summary>
        /// Class method to load a list of AclGroup from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public void Load(U options)
        {
            LoadOptions(options);
        }

        /// <summary>
        /// Class method to load a list of entities from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        protected virtual void LoadOptions(U options = default(U))
        {
            log.Debug($"Auto loading Collection : {GetType().Name}");

            options = options == null || options.Equals(default(U)) ? Options : options;
            options = options == null || options.Equals(default(U)) ? OptionsDefault : options;

            string managerName = GetType().Name.Replace("EntityCollection", "") + "s";

            var manager = Db.GetPropertyValue(ManagerName);
            MethodInfo methodInfo = manager.GetMethod("List");
            IList items = methodInfo.Invoke(manager, new object[] { options }) as IList;

            log.Debug($"{items.Count} entity(ies) found !");

            foreach (T entity in items)
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Method to find an item by its primary key.
        /// </summary>
        /// <param name="pk">The item primary key or item id to find.</param>
        /// <returns>The item found or default if pk = 0.</returns>
        /// <exception cref="InvalidOperationException">Occurs when multiple items are found.</exception>
        public T FindPrimaryKey(int pk)
        {
            if (pk == 0)
            {
                log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {pk} => 0");
                return default(T);
            }

            return Items.ToList().Single(x => (int)x.GetPropertyValue("PrimaryKey") == pk);
        }

        /// <summary>
        /// Method to format the Alias property of an entity.
        /// </summary>
        /// <param name="entity">An entity with an Alias property derived from IAlias.</param>
        /// <param name="items">The list of entities to check in.</param>
        /// <returns></returns>
        protected static T FormatAlias(T entity, IList<IColumnNameAlias> items)
        {
            var obj = (IColumnNameAlias)entity;

            // Check if the alias is empty. Set name if required.
            if (obj.Alias.IsNullOrWhiteSpace())
            {
                obj.Alias = obj.Name;
            }

            // Check if another entity with the same alias is in database.
            int index = items.ToList().FindIndex(x => x.Alias.IsNotNullOrWhiteSpace() && x.Alias == obj.Alias && x.PrimaryKey != obj.PrimaryKey);
            if (index >= 0 || obj.Alias.IsNullOrWhiteSpace())
            {
                DateTime d = DateTime.Now;
                obj.Alias += "-" + d.ToString("yyyy-MM-dd") + "-" + d.ToString("HH-mm-ss-fff");
            }

            ((IColumnNameAlias)entity).Alias = obj.Alias;

            return entity;
        }

        #endregion
    }
}