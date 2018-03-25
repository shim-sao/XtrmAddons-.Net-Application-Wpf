using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Libraries.Common.Collections
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
            try
            {
                AppLogger.Info("Adding Section(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (SectionEntity entity in newItems)
                    {
                        entity.Initialize();
                        MainWindow.Database.Sections.Add(entity);

                        AppLogger.Info(string.Format("Section [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Adding Section(s). Done !");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Adding Section(s) failed !", e);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        /// <summary>
        /// Method to delete a list of Section entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<SectionEntity> oldItems)
        {
            // Check for Removing items.
            try
            {
                AppLogger.Info("Deleting Section(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (SectionEntity entity in oldItems)
                    {
                        MainWindow.Database.Sections.Delete(entity.PrimaryKey);

                        AppLogger.Info(string.Format("Section [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Adding Section(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Deleting Section(s) list failed !", ex);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<SectionEntity> newItems, List<SectionEntity> oldItems)
        {
            // Check for Replace | Edit items.
            try
            {
                AppLogger.Info("Replacing Section. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (SectionEntity entity in newItems)
                    {
                        await MainWindow.Database.Sections.UpdateAsync(entity);

                        AppLogger.Info(string.Format("Section [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Replacing Section(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Replacing Section(s) failed !", ex);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        /// <summary>
        /// Method to update a list of Section entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void SetDefault(SectionEntity newItem)
        {
            try
            {
                AppLogger.Info("Setting default Section. Please wait...");

                if (newItem != null)
                {
                    MainWindow.Database.Sections.SetDefault(newItem.PrimaryKey);
                }

                AppNavigator.Clear();
                AppLogger.Info("Setting default Section. Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Setting default Section failed !", ex);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        #endregion
    }
}