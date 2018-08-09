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
    /// Class XtrmAddons Fotootof Server Component Users Collections.
    /// </summary>
    public class UserEntityCollection : CollectionBaseEntity<UserEntity, UserOptionsList>
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="autoLoad"></param>
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

            var entities = await Db.Users.ListAsync(options);
            foreach (UserEntity entity in entities)
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
            MessageBoxs.IsBusy = true;
            log.Info("Adding User(s). Please wait...");

            try
            {
                log.Info("Adding User(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (UserEntity entity in newItems)
                    {
                        Db.Users.Add(entity);

                        log.Info(string.Format("User [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Adding User(s). Done !");
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBoxs.Fatal(e, "Adding User(s) failed !");
            }
            finally
            {
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called asynchronously on User entities collection changed.
        /// </summary>
        /// <param name="oldItems">The list of items to remove.</param>
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
                        Db.Users.Delete(entity);

                        log.Info(string.Format("User [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Adding User(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Deleting User(s) list failed !");
            }
        }

        /// <summary>
        /// Method to update User entities database collection asynchronously.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        /// <param name="oldItems"></param>
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
                        await Db.Users.UpdateAsync(entity);

                        log.Info(string.Format("User [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigatorBase.Clear();
                log.Info("Updating User(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Updating User(s) failed !");
            }
        }

        #endregion
    }
}
