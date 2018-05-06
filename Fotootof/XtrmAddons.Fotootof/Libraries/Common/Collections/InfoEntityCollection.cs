using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Libraries.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Info Collections.
    /// </summary>
    public class InfoEntityCollection : CollectionBaseEntity<InfoEntity, InfoOptionsList>
    {
        #region Properties

        public override bool IsAutoloadEnabled => true;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public InfoEntityCollection(bool autoLoad = false, InfoOptionsList options = null) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="list">A list of Section to paste in.</param>
        public InfoEntityCollection(List<InfoEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Section Collections.
        /// </summary>
        /// <param name="collection">>A collection of Section to paste in.</param>
        public InfoEntityCollection(IEnumerable<InfoEntity> collection) : base(collection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to insert a list of Info entities into the database.
        /// </summary>
        /// <param name="newItems">The list of items to add.</param>
        public static void DbInsert(List<InfoEntity> newItems)
        {
            try
            {
                log.Info("Adding Info(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (InfoEntity entity in newItems)
                    {
                        //entity.Initialize();
                        //MainWindow.Database.Info_Add(entity);

                        throw new NotImplementedException();

                        //Logger.Info(string.Format("Info [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding Info(s). Done !");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Adding Info(s) failed !", e);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to delete a list of Info entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<InfoEntity> oldItems)
        {
            // Check for Removing items.
            try
            {
                log.Info("Deleting Section(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (InfoEntity entity in oldItems)
                    {
                        //MainWindow.Database.Info_Delete(entity.PrimaryKey);

                        throw new NotImplementedException();

                        //Logger.Info(string.Format("Section [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
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
        /// Method to update a list of Info entities into the database.
        /// </summary>
        /// <param name="newItems">The list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<InfoEntity> newItems, List<InfoEntity> oldItems)
        {
            // Check for Replace | Edit items.
            try
            {
                log.Info("Replacing Info. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (InfoEntity entity in newItems)
                    {
                        //await MainWindow.Database.Infos.UpdateAsync(entity);
                        await Task.Delay(10);
                        throw new NotImplementedException();

                        //Logger.Info(string.Format("Info [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Replacing Info(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Replacing Info(s) failed !", ex);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to load list of Info of type alias "quality".
        /// </summary>
        public static InfoEntityCollection TypesQuality()
        {
            return new InfoEntityCollection
            (
                true,
                new InfoOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    InfoTypesAlias = new List<string>() { "quality" },
                    OrderBy = "Ordering"
                }
            );
        }

        /// <summary>
        /// Method to load list of Info of type alias "quality".
        /// </summary>
        public static InfoEntityCollection TypesColor()
        {
            return new InfoEntityCollection
            (
                true,
                new InfoOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    InfoTypesAlias = new List<string>() { "color" },
                    OrderBy = "Ordering"
                }
            );
        }

        #endregion
    }
}
