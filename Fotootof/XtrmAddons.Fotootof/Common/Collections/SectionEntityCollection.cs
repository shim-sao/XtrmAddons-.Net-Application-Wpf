using System;
using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users Section Collections.
    /// </summary>
    public class SectionEntityCollection : CollectionBaseEntity<SectionEntity, SectionOptionsList>
    {
        #region Properties

        public override bool IsAutoloadEnabled => true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public SectionEntityCollection(bool autoLoad = false, SectionOptionsList options = null) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="list">A list of Section to paste in.</param>
        public SectionEntityCollection(List<SectionEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="list">A list of Section to paste in.</param>
        public SectionEntityCollection(List<SectionJson> list) : base()
        {
            List<SectionEntity> entities = new List<SectionEntity>();
            
            if(list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            foreach(SectionJson sj in list)
            {
                Add(sj.ToEntity());
            }
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="collection">>A collection of Section to paste in.</param>
        public SectionEntityCollection(IEnumerable<SectionEntity> collection) : base(collection) { }

        #endregion



        #region Methods

        /// <summary>
        /// Class method to load a list of Section from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public override void Load()
        {
            LoadOptions(null);
        }

        /// <summary>
        /// Class method to load a list of Section from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public new void LoadOptions(SectionOptionsList options = null)
        {
            options = options ?? Options;
            options = options ?? OptionsDefault;

            var items = MainWindow.Database.Sections.List(options);
            foreach (SectionEntity entity in items)
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Method to insert a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<SectionEntity> newItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Adding Section(s) to database. Please wait...");

            try
            {
                // Get all Section to check some properties before inserting new item.
                var items = MainWindow.Database.Sections.List(GetOptionsDefault());

                // Define if it is first item insertion.
                bool firstItem = true;

                // Check if we have new items list to insert.
                if (newItems != null && newItems.Count > 0)
                {
                    int i = 0;
                    foreach (SectionEntity entity in newItems)
                    {
                        // Check it is the first entity to set IsDefault if required.
                        if (items.Count == 0 && firstItem)
                        {
                            entity.IsDefault = true;
                            firstItem = false;
                        }

                        FormatAlias(entity, items);

                        // Finally add the entity into the database.
                        MainWindow.Database.Sections.Add(entity);
                        i++;

                        log.Info(string.Format("Section [{0}:{1}] added to database.", entity.PrimaryKey, entity.Name));
                    }
                }

                // Clear navigation cache.
                AppNavigator.Clear();
                log.Info("Adding Section(s) to database. Done !");
            }
            catch (Exception e)
            {
                log.Error(e);
                AppLogger.Fatal("Adding Section(s) to database. Fail !", e);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private static SectionEntity FormatAlias(SectionEntity entity, IList<SectionEntity> items = null)
        {
            items = items ?? MainWindow.Database.Sections.List(GetOptionsDefault());

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

        /// <summary>
        /// Method to delete a list of Section entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<SectionEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Deleting Section(s). Please wait...");

            try
            {
                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (SectionEntity entity in oldItems)
                    {
                        MainWindow.Database.Sections.Delete(entity.PrimaryKey);

                        log.Info(string.Format("Section [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding Section(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Deleting Section(s) list failed !", ex);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<SectionEntity> newItems, List<SectionEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Replacing Section. Please wait...");

            try
            {
                if (newItems != null && newItems.Count > 0)
                {
                    foreach (SectionEntity entity in newItems)
                    {
                        FormatAlias(entity);

                        await MainWindow.Database.Sections.UpdateAsync(entity);

                        log.Info(string.Format("Section [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Fatal("Replacing Section(s) failed !", ex);
            }
            finally
            {
                log.Info("Replacing Section(s). Done !");
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void SetDefault(SectionEntity newItem)
        {
            AppOverwork.IsBusy = true;
            log.Info("Setting default Section. Please wait...");

            try
            {
                if (newItem != null)
                {
                    MainWindow.Database.Sections.SetDefault(newItem.PrimaryKey);
                }

                AppNavigator.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Fatal("Setting default Section. Failed !", ex);
            }
            finally
            {
                log.Info("Setting default Section. Done !");
                AppOverwork.IsBusy = false;
            }
        }

        #endregion
    }
}