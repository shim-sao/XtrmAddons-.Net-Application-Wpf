﻿using System;
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
        public AclGroupEntityCollection(bool autoLoad = false, AclGroupOptionsList options = null)
            : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclGroup Collections Constructor.
        /// </summary>
        /// <param name="list">A list of AclGroup to paste in.</param>
        public AclGroupEntityCollection(List<AclGroupEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclGroup Collections Constructor.
        /// </summary>
        /// <param name="collection">>A collection of AclGroup to paste in.</param>
        public AclGroupEntityCollection(IEnumerable<AclGroupEntity> collection)
            : base(collection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to insert a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<AclGroupEntity> newItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Adding AclGroup(s). Please wait...");

            try
            {

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in newItems)
                    {
                        MainWindow.Database.AclGroups.Add(entity);

                        log.Info(string.Format("AclGroup [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding AclGroup(s). Done !");
            }
            catch (Exception e)
            {
                log.Error(e);
                AppLogger.Fatal("Adding AclGroup(s) failed !", e);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to delete a list of AclGroup entities from the database.
        /// </summary>
        /// <param name="oldItems">The list of items to remove.</param>
        public static void DbDelete(List<AclGroupEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Deleting AclGroup(s). Please wait...");

            try
            {
                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in oldItems)
                    {
                        if (!entity.IsDefault)
                        {
                            MainWindow.Database.AclGroups.Delete(entity.PrimaryKey);
                        }

                        log.Info(string.Format("AclGroup [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Deleting AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Fatal("Deleting AclGroup(s) list failed !", ex);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<AclGroupEntity> newItems, List<AclGroupEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Replacing AclGroup. Please wait...");

            try
            {
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

                        log.Info(string.Format("AclGroup [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Replacing AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Fatal("Replacing AclGroup(s) failed !", ex);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void SetDefault(AclGroupEntity newItem)
        {
            AppOverwork.IsBusy = true;
            log.Info("Setting default User Group. Please wait...");

            try
            {
                if (newItem != null)
                {
                    MainWindow.Database.AclGroups.SetDefault(newItem.PrimaryKey);
                }

                AppNavigator.Clear();
                log.Info("Setting default User Group. Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Fatal("Setting default User Group failed !", ex);
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        #endregion
    }
}