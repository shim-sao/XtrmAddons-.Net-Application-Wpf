using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users Section Collections.
    /// </summary>
    [System.Obsolete("use XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities.SectionEntityCollection", true)]
    public class SectionEntityCollection : CollectionBaseEntity<SectionEntity, SectionOptionsList>
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="collection">>A collection of Section to paste in.</param>
        public SectionEntityCollection(IEnumerable<SectionEntity> collection) : base(collection) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="list">A list of Section to paste in.</param>
        public SectionEntityCollection(List<SectionJson> list) : base()
        {
            if(list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            foreach(SectionJson sj in list)
            {
                Add(sj.ToEntity());
            }
        }

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

                        FormatAlias(entity);

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
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Adding Section(s) to database. Fail !");
            }
        }

        /// <summary>
        /// Method to format the Alias property of an entity.
        /// </summary>
        /// <param name="entity">An entity with an Alias property derived from IAlias.</param>
        /// <param name="items">The list of entities to check in.</param>
        /// <returns></returns>
        protected static SectionEntity FormatAlias(SectionEntity entity)
        {
            var obj = (IAlias)entity;

            // Check if the alias is empty. Set name if required.
            if (obj.Alias.IsNullOrWhiteSpace())
            {
                obj.Alias = obj.Name;
            }

            // Check if another entity with the same alias is in database.
            var item = Db.Sections.SingleOrNull(
                new SectionOptionsSelect
                {
                    Alias = obj.Alias,
                    Dependencies = { EnumEntitiesDependencies.None }
                });

            if ((item != null && item.PrimaryKey != obj.PrimaryKey) || obj.Alias.IsNullOrWhiteSpace())
            {
                DateTime d = DateTime.Now;
                obj.Alias += "-" + d.ToString("yyyy-MM-dd") + "-" + d.ToString("HH-mm-ss-fff");
            }

            ((IAlias)entity).Alias = obj.Alias;

            return entity;
        }

        /// <summary>
        /// Method to delete a list of Section entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<SectionEntity> oldItems)
        {
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
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Deleting Section(s) list failed !");
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<SectionEntity> newItems, List<SectionEntity> oldItems)
        {
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
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Replacing Section(s) failed !");
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void SetDefault(SectionEntity newItem)
        {
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
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Setting default Section. Failed !");
            }
        }

        #endregion
    }
}