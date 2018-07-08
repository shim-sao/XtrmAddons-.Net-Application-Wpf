using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
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
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static PictureEntity GetPicture(int pk)
        {
            PictureEntity pe = null;

            if (pk == 0)
            {
                pe = GetPictureDefault();
            }

            else
            {
                try
                {
                    using (Db.Context)
                    {
                        pe = Db.Context.Pictures.Find(pk);
                    }

                    if (pe == null)
                    {
                        pe = GetPictureDefault();
                    }
                }
                catch (Exception e)
                {
                    log.Debug($"{typeof(EntityBase).Name}.{MethodBase.GetCurrentMethod().Name} : Picture Primary Key");
                    log.Error(e.Output(), e);
                    pe = GetPictureDefault();
                }
            }

            return pe;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static PictureEntity GetPictureDefault()
        {
            return new PictureEntity()
            {
                OriginalPath = (string)Application.Current.Resources["ImageAlbumDefaultBackground"],
                OriginalWidth = 1920,
                OriginalHeight = 1080,

                PicturePath = (string)Application.Current.Resources["ImageAlbumDefaultPreview"],
                PictureWidth = 1920,
                PictureHeight = 720,

                ThumbnailPath = (string)Application.Current.Resources["ImageAlbumDefaultThumbnail"],
                ThumbnailWidth = 512,
                ThumbnailHeight = 512
            };
        }

        /// <summary>
        /// Method to get an observable collection of primaries keys.
        /// </summary>
        /// <typeparam name="T">The Class of dependencies.</typeparam>
        /// <param name="dependency">A list of Entity dependencies.</param>
        /// <param name="primaryKeyName">The associated key name of the dependencies.</param>
        /// <returns>A list of dependencies primary keys.</returns>
        /// <exception cref="ArgumentNullException">Occurs if primaryKeyName argument is null, empty or whitespace.</exception>
        public IEnumerable<int> ListOfPrimaryKeys<T>(IEnumerable<T> dependency, string primaryKeyName) where T : class
        {
            // Check if primary key name is valid, if not throw exception.
            if (primaryKeyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException e = new ArgumentNullException(nameof(primaryKeyName), $"The argument `Property Primary Key Name` must be not null, empty or whitespace !");
                log.Error(e.Output(), e);
                throw e;
            }

            // Initialize list of primary keys.
            IList<int> ids = new List<int>();

            // Check if dependencies are not null, if not return empty list.
            if (dependency == null)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {nameof(dependency)} => {ids.Count}");
                return ids;
            }

            try
            {
                // Convert dependencies into a list of primary keys.
                foreach (object o in dependency)
                {
                    ids.Add((int)((T)o).GetPropertyValue(primaryKeyName));
                }
            }
            catch (Exception e)
            {
                InvalidOperationException ex = new InvalidOperationException($"List '{typeof(T).Name}' dependency Primary Key failed.", e);
                log.Error(e.Output(), e);
                throw e;
            }

            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {nameof(dependency)} => {ids.Count}");
            return ids;
        }

        /// <summary>
        /// Method to get a list of associated entities.
        /// </summary>
        /// <typeparam name="T">The Class of entity.</typeparam>
        /// <param name="dependency">An collection of dependency entities.</param>
        /// <returns>A list of items associated to the given dependency.</returns>
        public static List<T> ListEntities<T>(IEnumerable dependency) where T : class
        {
            log.Debug($"{typeof(EntityBase).Name}.{MethodBase.GetCurrentMethod().Name} : Object Type => {typeof(T)}");

            List<T> items = new List<T>();

            if (dependency != null)
            {
                foreach (object link in dependency)
                {
                    T dep = (T)link.GetPropertyValue(typeof(T).Name);

                    if (dep != null)
                    {
                        items.Add(dep);
                    }
                    else
                    {
                        T item = null;
                        int id = (int)link.GetPropertyValue(typeof(T).Name.Replace("Entity", "") + "Id");

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
                log.Warn($"{typeof(EntityBase).Name}.{MethodBase.GetCurrentMethod().Name} : {nameof(dependency)} => {dependency}");
            }

            return items;
        }

        #endregion



        #region Methods Obsolete

        // Save not working at all.
        //public async Task<object> Save()
        //{
        //    try
        //    {
        //        using (Db.Context)
        //        {
        //            try { Db.Context.Attach(this); } catch (Exception e1) { log.Info(e1.Output(), e1); }

        //            if (PrimaryKey == 0)
        //            {
        //                this.Bind(Db.Context.Add(this));
        //                log.Info($"Adding {GetType().Name} [PrimaryKey:{PrimaryKey}] into database.");
        //            }
        //            else
        //            {
        //                this.Bind(Db.Context.Update(this));
        //                log.Info($"Updating {GetType().Name} [PrimaryKey:{PrimaryKey}] into database.");
        //            }
        //            /*
        //            using (Task<int> result = Db.Context.SaveChangesAsync())
        //            {
        //                if(result.IsCompleted)
        //                {
        //                    log.Warn($"Saving {GetType().Name} [PrimaryKey:{PrimaryKey}] into database : Done.");
        //                }

        //                if(result.IsCanceled)
        //                {
        //                    log.Warn($"Saving {GetType().Name} [PrimaryKey:{PrimaryKey}] into database : Canceled.");
        //                }

        //                if(result.IsFaulted)
        //                {
        //                    log.Warn($"Saving {GetType().Name} [PrimaryKey:{PrimaryKey}] into database : Error.");
        //                }
        //            }
        //            */
        //            int result = Db.Context.SaveChanges(true);
        //        }
        //    }
        //    catch (Exception e2)
        //    {
        //        log.Error(e2.Output(), e2);
        //    }

        //    return await Task.FromResult(this);
        //}

        #endregion
    }
}
