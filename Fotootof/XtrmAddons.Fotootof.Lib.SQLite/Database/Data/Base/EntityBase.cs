using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Base Entity.
    /// </summary>
    public abstract class EntityBase : IEntityBaseInterface, INotifyPropertyChanged
    {
        #region Variables

        /// <summary>
        /// Variable database connector.
        /// </summary>
        private static DatabaseCore db;

        #endregion



        #region Event Handler

        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion



        #region Proprerties

        /// <summary>
        /// Property to access to the application database connector.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public static DatabaseCore Db { get => db; set => db = value; }

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public abstract int PrimaryKey { get; set; }

        #endregion



        #region Methods

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method to get a list of primaries keys.
        /// </summary>
        /// <typeparam name="T">The Class of dependencies.</typeparam>
        /// <param name="dependencies">A list of Entity dependencies.</param>
        /// <param name="keyName">The associated key name of the dependencies.</param>
        /// <returns>A list of dependencies primary keys.</returns>
        public List<int> ListOfPrimaryKeys<T>(List<T> dependencies, string keyName) where T : class
        {
            List<int> ids = new List<int>();

            foreach(object o in dependencies)
            {
                ids.Add((int)((T)o).GetPropertyValue(keyName));
            }

            return ids;
        }

        /// <summary>
        /// Method to get a list of associated entities.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="dependencies">An collection of entities dependencies.</param>
        /// <returns>A list of AclAction associated to the dependencies.</returns>
        public static List<T> ListEntities<T>(IEnumerable dependencies) where T : class
        {
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

            return items;
        }

        #endregion
    }
}
