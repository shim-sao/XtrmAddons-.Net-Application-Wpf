using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Libraries.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users AclGroup Collections.
    /// </summary>
    public class AclGroupEntityCollection : CollectionBaseEntity<AclGroupEntity, AclGroupOptionsList>
    {
        #region Properties

        /// <summary>
        /// Property to define if the default items of the collection can be loaded.
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclGroup Collections Constructor.
        /// </summary>
        public AclGroupEntityCollection(bool autoLoad = false, AclGroupOptionsList options = null) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclGroup Collections Constructor.
        /// </summary>
        /// <param name="list">A list of AclGroup to paste in.</param>
        public AclGroupEntityCollection(List<AclGroupEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclGroup Collections Constructor.
        /// </summary>
        /// <param name="collection">>A collection of AclGroup to paste in.</param>
        public AclGroupEntityCollection(IEnumerable<AclGroupEntity> collection) : base(collection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to insert a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<AclGroupEntity> newItems)
        {
            try
            {
                Logger.Info("Adding AclGroup(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in newItems)
                    {
                        MainWindow.Database.AclGroups.Add_Async(entity);

                        Logger.Info(string.Format("AclGroup [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                Navigator.Clear();
                Logger.Info("Adding AclGroup(s). Done !");
            }
            catch (Exception e)
            {
                Logger.Fatal("Adding AclGroup(s) failed !", e);
            }
            finally
            {
                Logger.Close();
            }
        }

        /// <summary>
        /// Method to delete a list of AclGroup entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<AclGroupEntity> oldItems)
        {
            // Check for Removing items.
            try
            {
                Logger.Info("Deleting AclGroup(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in oldItems)
                    {
                        if (!entity.IsDefault)
                        {
                            MainWindow.Database.AclGroups.Delete(entity.PrimaryKey);
                        }

                        Logger.Info(string.Format("AclGroup [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                Navigator.Clear();
                Logger.Info("Adding AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Deleting AclGroup(s) list failed !", ex);
            }
            finally
            {
                Logger.Clear();
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        public static async void DbUpdateAsync(List<AclGroupEntity> newItems, List<AclGroupEntity> oldItems)
        {
            // Check for Replace | Edit items.
            try
            {
                Logger.Info("Replacing AclGroup. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in newItems)
                    {
                        //MainWindow.SQLiteService.CleanAclGroupDependenciesAclAction(entity);
                        await MainWindow.Database.AclGroups.Update_Async(entity);

                        if (entity.IsDefault)
                        {
                            MainWindow.Database.AclGroups.SetDefault(entity.PrimaryKey);
                        }

                        Logger.Info(string.Format("AclGroup [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                Navigator.Clear();
                Logger.Info("Replacing AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Replacing AclGroup(s) failed !", ex);
            }
            finally
            {
                Logger.Clear();
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void SetDefault(AclGroupEntity newItem)
        {
            try
            {
                Logger.Info("Setting default User Group. Please wait...");

                if (newItem != null)
                {
                    MainWindow.Database.AclGroups.SetDefault(newItem.PrimaryKey);
                }

                Navigator.Clear();
                Logger.Info("Setting default User Group. Done !");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Setting default User Group failed !", ex);
            }
            finally
            {
                Logger.Clear();
            }
        }

        #endregion
    }
}