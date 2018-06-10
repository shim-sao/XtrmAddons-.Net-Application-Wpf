using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users Collections.
    /// </summary>
    public class UserEntityCollection : CollectionBaseEntity<UserEntity, UserOptionsList>
    {
        #region Properties

        public override bool IsAutoloadEnabled => true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Users Collections Constructor.
        /// </summary>
        public UserEntityCollection(bool autoLoad = false) : base(autoLoad) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Users Collections Constructor.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public UserEntityCollection(UserOptionsList options = null, bool autoLoad = false) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Users Collections Constructor.
        /// </summary>
        /// <param name="list">A list of User to paste in.</param>
        public UserEntityCollection(List<UserEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Users Collections Constructor.
        /// </summary>
        /// <param name="collection">>A collection of User to paste in.</param>
        public UserEntityCollection(IEnumerable<UserEntity> collection) : base(collection) { }

        #endregion



        #region Methods

        /// <summary>
        /// Class method to load a list of AclGroup from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public override void Load()
        {
            LoadOptionsAsync(null);
        }

        /// <summary>
        /// Class method to load a list of User from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        private async void LoadOptionsAsync(UserOptionsList options = null)
        {
            options = options ?? Options;
            options = options ?? OptionsDefault;

            var entities = await MainWindow.Database.Users.ListAsync(options);
            foreach(UserEntity entity in entities)
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Method called asynchronously on User entities collection changed.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<UserEntity> newItems)
        {
            MessageBase.IsBusy = true;
            log.Info("Adding User(s). Please wait...");

            try
            {
                log.Info("Adding User(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (UserEntity entity in newItems)
                    {
                        MainWindow.Database.Users.Add(entity);

                        log.Info(string.Format("User [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding User(s). Done !");
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBase.Fatal(e, "Adding User(s) failed !");
            }
            finally
            {
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called asynchronously on User entities collection changed.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<UserEntity> oldItems)
        {
            log.Info("Deleting User(s). Please wait...");

            try
            {
                log.Info("Deleting User(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (UserEntity entity in oldItems)
                    {
                        MainWindow.Database.Users.Delete(entity);

                        log.Info(string.Format("User [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Adding User(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Deleting User(s) list failed !");
            }
        }

        /// <summary>
        /// Method to update User entities database collection asynchronously.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        public async static void DbUpdate(List<UserEntity> newItems, List<UserEntity> oldItems)
        {
            log.Info("Updating User(s). Please wait...");

            // Check for Replace | Edit items.
            try
            {
                log.Info("Replacing User. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (UserEntity entity in newItems)
                    {
                        await MainWindow.Database.Users.UpdateAsync(entity);

                        log.Info(string.Format("User [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Updating User(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Updating User(s) failed !");
            }
        }

        #endregion
    }
}
