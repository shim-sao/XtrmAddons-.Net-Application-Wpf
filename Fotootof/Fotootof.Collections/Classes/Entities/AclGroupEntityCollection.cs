using Fotootof.Layouts.Dialogs;
using Fotootof.Navigator;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Collections.Entities
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
            log.Info("Adding AclGroup(s). Please wait...");

            try
            {

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in newItems)
                    {
                        Db.AclGroups.Add(entity);

                        log.Info($"AclGroup [{entity.PrimaryKey}:{entity.Name}] added.");
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Adding AclGroup(s). Done !");
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                MessageBoxs.Fatal(e, "Adding AclGroup(s) failed !");
            }
        }

        /// <summary>
        /// Method to delete a list of AclGroup entities from the database.
        /// </summary>
        /// <param name="oldItems">The list of items to remove.</param>
        public static void DbDelete(List<AclGroupEntity> oldItems)
        {
            log.Info("Deleting AclGroup(s). Please wait...");

            try
            {
                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in oldItems)
                    {
                        if (!entity.IsDefault)
                        {
                            Db.AclGroups.Delete(entity.PrimaryKey);
                        }

                        log.Info(string.Format("AclGroup [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Deleting AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Deleting AclGroup(s) list failed !");
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static async void DbUpdateAsync(List<AclGroupEntity> newItems, List<AclGroupEntity> oldItems)
        {
            log.Info("Replacing AclGroup. Please wait...");

            try
            {
                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclGroupEntity entity in newItems)
                    {
                        await Db.AclGroups.Update_Async(entity);

                        if (entity.IsDefault)
                        {
                            Db.AclGroups.SetDefault(entity.PrimaryKey);
                        }

                        log.Info(string.Format("AclGroup [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Replacing AclGroup(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Replacing AclGroup(s) failed !");
            }
        }

        /// <summary>
        /// Method to update a list of AclGroup entities into the database.
        /// </summary>
        /// <param name="newItem"></param>
        public static void SetDefault(AclGroupEntity newItem)
        {
            log.Info("Setting default User Group. Please wait...");

            try
            {
                if (newItem != null)
                {
                    Db.AclGroups.SetDefault(newItem.PrimaryKey);
                }

                AppNavigatorBase.Clear();
                log.Info("Setting default User Group. Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Setting default User Group failed !");
            }
        }

        #endregion
    }
}