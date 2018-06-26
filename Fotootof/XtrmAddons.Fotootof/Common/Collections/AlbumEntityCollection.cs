using System;
using System.Collections.Generic;
using System.IO;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Net.Application;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using System.Linq;
using System.Threading.Tasks;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection.
    /// </summary>
    public class AlbumEntityCollection : CollectionBaseEntity<AlbumEntity, AlbumOptionsList>
    {
        #region Properties

        /// <summary>
        /// Property to set or check if auto load is enabled.
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public AlbumEntityCollection(bool autoLoad = false, AlbumOptionsList options = null) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A list of Album to paste in.</param>
        public AlbumEntityCollection(List<AlbumEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="collection">>A collection of Album to paste in.</param>
        public AlbumEntityCollection(IEnumerable<AlbumEntity> collection) : base(collection) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A list of Album to paste in.</param>
        public AlbumEntityCollection(List<AlbumJson> list) : base()
        {
            if (list == null)
            {
                ArgumentNullException ex = ExceptionBase.ArgNull(nameof(list), list);
                log.Warn(ex.Output(), ex);
                return;
            }

            if (list.Count == 0)
            {
                return;
            }

            foreach (AlbumJson item in list)
            {
                Add(item.ToEntity());
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Method to insert a list of Album entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static IEnumerable<AlbumEntity> DbInsert(List<AlbumEntity> newItems)
        {
            if (newItems == null)
            {
                ArgumentNullException e = ExceptionBase.ArgNull(nameof(newItems), newItems);
                log.Error(e.Output());
                return null;
            }

            IList<AlbumEntity> itemsAdded = new List<AlbumEntity>();

            try
            {
                log.Info($"Adding {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Please wait...");

                // Get all Album to check some properties before inserting new item.
                var items = MainWindow.Database.Albums.List(GetOptionsDefault());

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AlbumEntity entity in newItems)
                    {
                        FormatAlias(entity, items);

                        itemsAdded.Add(Db.Albums.Add(entity));

                        log.Info($"Album [{entity.PrimaryKey}:{entity.Name}] added.");
                    }
                }

                AppNavigator.Clear();
                log.Debug("Application navigator cleared.");
                log.Info($"Adding {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, $"Adding {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Failed.");
            }

            return itemsAdded;
        }

        /// <summary>
        /// Method to delete a list of Album entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<AlbumEntity> oldItems)
        {
            log.Info("Deleting Album(s). Please wait...");

            // Check for Removing items.
            try
            {
                log.Info("Deleting Album(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AlbumEntity entity in oldItems)
                    {
                        MainWindow.Database.Albums.Delete(entity);
                        log.Info($"Album [{entity.PrimaryKey}:{entity.Name}] deleted.");
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding Album(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Deleting Album(s) list failed !");
            }
        }

        /// <summary>
        /// Method to update a list of Album entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        public static async Task<IList<AlbumEntity>> DbUpdateAsync(IEnumerable<AlbumEntity> newItems, IEnumerable<AlbumEntity> oldItems)
        {
            if (newItems == null)
            {
                ArgumentNullException e = ExceptionBase.ArgNull(nameof(newItems), newItems);
                log.Error(e.Output());
                return null;
            }

            IList<AlbumEntity> itemsUpdated = new List<AlbumEntity>();

            try
            {
                log.Info($"Updating {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Please wait...");

                // Get all Album to check some properties before inserting new item.
                var items = MainWindow.Database.Albums.List(GetOptionsDefault());

                if (newItems != null && newItems.Count() > 0)
                {
                    foreach (AlbumEntity entity in newItems)
                    {
                        FormatAlias(entity, items);
                        itemsUpdated.Add(await MainWindow.Database.Albums.UpdateAsync(entity));
                        log.Info($"Album [{entity.PrimaryKey}:{entity.Name}] updated.");
                    }
                }

                AppNavigator.Clear();
                log.Info($"Updating {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, $"Updating {newItems.Count()} album{(newItems.Count() > 0 ? "s" : "")} : Failed.");
            }

            return itemsUpdated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private static AlbumEntity FormatAlias(AlbumEntity entity, IList<AlbumEntity> items = null)
        {
            items = items ?? MainWindow.Database.Albums.List(GetOptionsDefault());

            // Check if the alias is empty. Set name if required.
            if (entity.Alias.IsNullOrWhiteSpace())
            {
                entity.Alias = entity.Name;
            }

            // Check if another entity with the same alias is in database.
            int index = items.ToList().FindIndex(x => x.Alias.IsNotNullOrWhiteSpace() && x.Alias == entity.Alias && x.PrimaryKey != entity.PrimaryKey);
            if (index >= 0 || entity.Alias.IsNullOrWhiteSpace())
            {
                DateTime d = DateTime.Now;
                entity.Alias += "-" + d.ToString("yyyy-MM-dd") + "-" + d.ToString("HH-mm-ss-fff");
            }

            return entity;
        }

        #endregion
    }
}
