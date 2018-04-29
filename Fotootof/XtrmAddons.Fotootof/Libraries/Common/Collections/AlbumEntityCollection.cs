using System;
using System.Collections.Generic;
using System.IO;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Application;

namespace XtrmAddons.Fotootof.Libraries.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection.
    /// </summary>
    public class AlbumEntityCollection : CollectionBaseEntity<AlbumEntity, AlbumOptionsList>
    {
        #region Properties

        /// <summary>
        /// Property to set or check if auto load is enabled.
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public AlbumEntityCollection(bool autoLoad = false, AlbumOptionsList options = null) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A list of Album to paste in.</param>
        public AlbumEntityCollection(List<AlbumEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="collection">>A collection of Album to paste in.</param>
        public AlbumEntityCollection(IEnumerable<AlbumEntity> collection) : base(collection) { }

        #endregion


        #region Methods

        /// <summary>
        /// Class method to load a list of Album from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        protected override void LoadOptions(AlbumOptionsList options = default(AlbumOptionsList))
        {
            base.LoadOptions(options);

            string defaultImg = Path.Combine(ApplicationBase.AssetsImagesDefaultDirectory, "album-default.png");
            foreach (AlbumEntity entity in Items)
            {
                entity.InitializeImages();
            }
        }

        /// <summary>
        /// Method to insert a list of Album entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<AlbumEntity> newItems)
        {
            try
            {
                AppLogger.Info("Adding Album(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AlbumEntity entity in newItems)
                    {
                        MainWindow.Database.Albums.Add(entity);

                        AppLogger.Info(string.Format("Album [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Adding Album(s). Done !");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Adding Album(s) failed !", e);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        /// <summary>
        /// Method to delete a list of Album entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(List<AlbumEntity> oldItems)
        {
            // Check for Removing items.
            try
            {
                AppLogger.Info("Deleting Album(s). Please wait...");

                if (oldItems != null && oldItems.Count > 0)
                {
                    foreach (AlbumEntity entity in oldItems)
                    {
                        MainWindow.Database.Albums.Delete(entity);

                        AppLogger.Info(string.Format("Album [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Adding Album(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Deleting Album(s) list failed !", ex);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        /// <summary>
        /// Method to update a list of Album entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to update.</param>
        public static async void DbUpdateAsync(List<AlbumEntity> newItems, List<AlbumEntity> oldItems)
        {
            // Check for Replace | Edit items.
            try
            {
                AppLogger.Info("Replacing Album. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (AlbumEntity entity in newItems)
                    {
                        await MainWindow.Database.Albums.UpdateAsync(entity);
                        //await MainWindow.Database.Album_CleanDependencies_Async("AlbumsInACLGroups", "AclGroupId", entity.PrimaryKey, entity.AclGroupsPK);

                        AppLogger.Info(string.Format("Album [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Replacing Album(s). Done !");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Replacing Album(s) failed !", ex);
            }
            finally
            {
                AppLogger.Close();
            }
        }

        #endregion
    }
}
