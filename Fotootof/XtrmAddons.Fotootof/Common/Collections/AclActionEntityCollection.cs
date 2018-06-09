using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users AclAction Collections.
    /// </summary>
    public class AclActionEntityCollection : CollectionBaseEntity<AclActionEntity, AclActionOptionsList>
    {
        #region Properties

        /// <summary>
        /// Property to define if the default items of the collection can be loaded.
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclAction Collections Constructor.
        /// </summary>
        public AclActionEntityCollection(bool autoLoad = false, AclActionOptionsList options = null)
            : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclAction Collections Constructor.
        /// </summary>
        /// <param name="list">A list of AclAction to paste in.</param>
        public AclActionEntityCollection(List<AclActionEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Component Users AclAction Collections Constructor.
        /// </summary>
        /// <param name="collection">>A collection of AclAction to paste in.</param>
        public AclActionEntityCollection(IEnumerable<AclActionEntity> collection)
            : base(collection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to insert a list of AclAction entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<AclActionEntity> newItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Adding AclAction(s). Please wait...");

            try
            {

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclActionEntity entity in newItems)
                    {
                        MainWindow.Database.AclActions.Add(entity);

                        log.Info(string.Format("AclAction [{0}:{1}] added.", entity.PrimaryKey, entity.Action));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding AclAction(s). Done !");
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBase.Fatal(e, "Adding AclAction(s) failed !");
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to delete a list of AclAction entities from the database.
        /// </summary>
        /// <param name="oldItems">The list of items to remove.</param>
        public static void DbDelete(List<AclActionEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Deleting AclAction(s). Please wait...");

            try
            {
                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AclActionEntity entity in oldItems)
                    {
                        MainWindow.Database.AclActions.Delete(entity.PrimaryKey);

                        log.Info(string.Format("AclAction [{0}:{1}] deleted.", entity.PrimaryKey, entity.Action));
                    }
                }

                AppNavigator.Clear();
                log.Info("Deleting AclAction(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Fatal(ex, "Deleting AclAction(s) list failed !");
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to update a list of AclAction entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
        public static void DbUpdate(List<AclActionEntity> newItems, List<AclActionEntity> oldItems)
        {
            AppOverwork.IsBusy = true;
            log.Info("Replacing AclAction. Please wait...");

            try
            {
                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AclActionEntity entity in newItems)
                    {
                        //MainWindow.SQLiteService.CleanAclActionDependenciesAclAction(entity);
                        //await MainWindow.Database.AclActions.Update(entity);

                        MessageBase.NotImplemented();

                        log.Info(string.Format("AclAction [{0}:{1}] updated.", entity.PrimaryKey, entity.Action));
                    }
                }

                AppNavigator.Clear();
                log.Info("Replacing AclAction(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Fatal(ex, "Replacing AclAction(s) failed !");
            }
            finally
            {
                AppOverwork.IsBusy = false;
            }
        }

        #endregion
    }
}