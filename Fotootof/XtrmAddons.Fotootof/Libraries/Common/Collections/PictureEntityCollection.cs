using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Libraries.Common.Collections
{
    public class PictureEntityCollection : CollectionBaseEntity<PictureEntity, PictureOptionsList>
    {
        #region Properties

        public override bool IsAutoloadEnabled => true;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public PictureEntityCollection(PictureOptionsList options = null, bool autoLoad = false) : base(autoLoad, options) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="list">A list of Album to paste in.</param>
        public PictureEntityCollection(List<PictureEntity> list) : base(list) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="collection">>A collection of Album to paste in.</param>
        public PictureEntityCollection(IEnumerable<PictureEntity> collection) : base(collection) { }

        #endregion



        #region Methods

        /// <summary>
        /// Class method to load a list of Album from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public override void Load()
        {
            LoadOptions(null);
        }

        /// <summary>
        /// Class method to load a list of Album from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public new void LoadOptions(PictureOptionsList options = null)
        {
            options = options ?? Options;
            options = options ?? OptionsDefault;

            var items = MainWindow.Database.Pictures.List(options);
            foreach (PictureEntity entity in items)
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Method to insert a list of Picture entities into the database.
        /// </summary>
        /// <param name="newItems">Thee list of items to add.</param>
        public static void DbInsert(List<PictureEntity> newItems, List<AlbumEntity> albums = default(List<AlbumEntity>))
        {
            try
            {
                AppLogger.Info("Adding Picture(s). Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (PictureEntity entity in newItems)
                    {
                        // Process association for each albums.
                        foreach (AlbumEntity a in albums)
                        {
                            // Try to find Picture and Album dependency
                            PicturesInAlbums dependency = (new List<PicturesInAlbums>(entity.PicturesInAlbums)).Find(p => p.AlbumId == a.AlbumId);

                            // Associate Picture to the Album if not already set.
                            if (dependency == null)
                            {
                                entity.PicturesInAlbums.Add(
                                    new PicturesInAlbums
                                    {
                                        AlbumId = a.AlbumId,
                                        Ordering = entity.PicturesInAlbums.Count + 1
                                    }
                                );

                                AppLogger.Info(string.Format("Picture [{0}:{1}] associated to Album [{2}:{3}].", entity.PrimaryKey, entity.Name, a.PrimaryKey, a.Name));
                            }
                        }
                        
                        MainWindow.Database.Pictures.Add(entity);

                        AppLogger.Info(string.Format("Picture [{0}:{1}] added.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                AppLogger.Info("Adding Picture(s). Done !");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Adding Picture(s) failed !", e);
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
