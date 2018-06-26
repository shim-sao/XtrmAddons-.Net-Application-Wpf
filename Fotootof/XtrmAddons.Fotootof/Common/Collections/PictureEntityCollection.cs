using System;
using System.Linq;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Common.Collections
{
    public class PictureEntityCollection : CollectionBaseEntity<PictureEntity, PictureOptionsList>
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override bool IsAutoloadEnabled => true;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Albums Collection Constructor.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public PictureEntityCollection(PictureOptionsList options = null, bool autoLoad = false)
            : base(autoLoad, options) { }

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
        public static IList<PictureEntity> DbInsert(IEnumerable<PictureEntity> newItems, IEnumerable<AlbumEntity> albums = default(IEnumerable<AlbumEntity>))
        {
            if (newItems == null)
            {
                ArgumentNullException e = ExceptionBase.ArgNull(nameof(newItems), newItems);
                log.Error(e.Output());
                return null;
            }

            if (albums == null)
            {
                ArgumentNullException e = ExceptionBase.ArgNull(nameof(albums), albums);
                log.Error(e.Output());
                return null;
            }

            IList<PictureEntity> itemsAdded = new List<PictureEntity>();

            try
            {
                log.Info($"Adding {newItems.Count()} picture{(newItems.Count() > 0 ? "s" : "")} : Please wait...");

                if (newItems != null && newItems.Count() > 0)
                {
                    foreach (PictureEntity entity in newItems)
                    {
                        // Process association for each albums.
                        if(albums != null)
                        {
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

                                    log.Info(string.Format("Picture [{0}:{1}] associated to Album [{2}:{3}].", entity.PrimaryKey, entity.Name, a.PrimaryKey, a.Name));
                                }
                            }
                        }
                        
                        // Add picture strored in database to the return list.
                        itemsAdded.Add(MainWindow.Database.Pictures.Add(entity));

                        log.Info($"Picture [{entity.PrimaryKey}:{entity.Name}] added.");
                    }
                }

                AppNavigator.Clear();
                log.Debug("Application navigator cleared.");
                log.Info($"Adding {newItems.Count()} picture{(newItems.Count() > 0 ? "s" : "")} : Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, $"Adding {newItems.Count()} picture{(newItems.Count() > 0 ? "s" : "")} : Failed.");
            }

            return itemsAdded;
        }

        /// <summary>
        /// Method to delete a list of Picture entities from the database.
        /// </summary>
        /// <param name="newItems">The list of items to remove.</param>
        public static void DbDelete(IEnumerable<PictureEntity> oldItems)
        {
            if(oldItems == null)
            {
                ArgumentNullException e = ExceptionBase.ArgNull(nameof(oldItems), oldItems);
                log.Error(e.Output());
                return;
            }

            try
            {
                log.Info($"Deleting {oldItems.Count()} picture{(oldItems.Count() > 0 ? "s" : "")} : Please wait...");

                if (oldItems != null && oldItems.Count() > 0)
                {
                    foreach (PictureEntity entity in oldItems)
                    {
                        MainWindow.Database.Pictures.Delete(entity);

                        log.Info(string.Format("Album [{0}:{1}] deleted.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info($"Deleting {oldItems.Count()} picture{(oldItems.Count() > 0 ? "s" : "")} : Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, $"Deleting {oldItems.Count()} picture{(oldItems.Count() > 0 ? "s" : "")} : failed !");
            }
        }

        /// <summary>
        /// Method to update a list of Picture entities into the database.
        /// </summary>
        /// <param name="newItems">The list of items to update.</param>
        public static async void DbUpdateAsync(List<PictureEntity> newItems, List<PictureEntity> oldItems)
        {
            // Check for Replace | Edit items.
            try
            {
                log.Info("Replacing Picture. Please wait...");

                if (newItems != null && newItems.Count > 0)
                {
                    foreach (PictureEntity entity in newItems)
                    {
                        await MainWindow.Database.Pictures.UpdateAsync(entity);

                        log.Info(string.Format("Picture [{0}:{1}] updated.", entity.PrimaryKey, entity.Name));
                    }
                }

                AppNavigator.Clear();
                log.Info("Replacing Picture(s). Done !");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Replacing Picture(s) failed !");
            }
        }

        #endregion
    }
}