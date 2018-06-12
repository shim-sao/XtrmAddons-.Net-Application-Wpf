using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Collections
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Base Classes Collections Base Entity.</para>
    /// </summary>
    public abstract class CollectionBaseEntity<T, U> : CollectionBase<T>
    {
        #region Properties

        /// <summary>
        /// Property wrapper to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

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
        /// Class XtrmAddons Fotootof Libraries Base Collections Constructor.
        /// </summary>
        /// <param name="autoLoad">Auto load data from database ?</param>
        public CollectionBaseEntity(bool autoLoad = false, U options = default(U))
        {
            if(options != null && !options.Equals(default(U)))
            {
                Options = options;
            }

            Initialize(autoLoad);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Base Collections.
        /// </summary>
        /// <param name="list">A list of T to paste in.</param>
        public CollectionBaseEntity(List<T> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Base Collections.
        /// </summary>
        /// <param name="collection">>A collection of T to paste in.</param>
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
            item.SetPropertyValue("Dependencies", new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All });

            return item;
        }

        /// <summary>
        /// Class method to load a list of AclGroup from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
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
            if(pk == 0)
            {
                log.Warn($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {pk} => 0");
                return default(T);
            }

            return Items.ToList().Single(x => (int)x.GetPropertyValue("PrimaryKey") == pk);
        }

        #endregion
    }
}