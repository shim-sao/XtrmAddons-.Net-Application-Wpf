using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        public static SQLiteSvc Database => (SQLiteSvc)ApplicationSession.Properties.Database;

        /// <summary>
        /// Property to access to the collection options filters.
        /// </summary>
        public U Options { get; set; }

        /// <summary>
        /// Property to access to the default collection options filters.
        /// </summary>
        public U OptionsDefault => GetOptionsDefault();


        string ManagerName => GetType().Name.Replace("EntityCollection", "") + "s";

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
        private static U GetOptionsDefault()
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
            options = options == null || options.Equals(default(U)) ? Options : options;
            options = options == null || options.Equals(default(U)) ? OptionsDefault : options;

            string managerName = GetType().Name.Replace("EntityCollection", "") + "s";

            var manager = Database.GetPropertyValue(ManagerName);
            MethodInfo methodInfo = manager.GetMethod("List");
            IList items = methodInfo.Invoke(manager, new object[] { options }) as IList;

            foreach (T entity in items)
            {
                Add(entity);
            }
        }

        #endregion
    }
}
