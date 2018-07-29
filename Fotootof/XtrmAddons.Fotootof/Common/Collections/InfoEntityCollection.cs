using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;

namespace XtrmAddons.Fotootof.Common.Collections
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Info Collections.
    /// </summary>
    [System.Obsolete("use XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities.InfoEntityCollection", true)]
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
        /// Class method to load a list of entities from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public override void Load()
        {
            base.Load();

            int i = 0;
            foreach (InfoEntity entity in this)
            {
                try
                {
                    string key = entity.Name.RemoveWhitespace().RemoveDiacritics();
                    //Trace.WriteLine(key);

                    if(Culture.Translation.Words.Contains(key))
                    {
                        this[i].Name = (string)Culture.Translation.Words[key];
                    }

                }
                catch (Exception ex)
                {
                    log.Error(ex.Output(), ex);
                }
                i++;
            }
        }

        /// <summary>
        /// Class method to load a list of entities from database.
        /// </summary>
        /// <param name="options">Options for query filters.</param>
        public new void Load(InfoOptionsList options)
        {
            base.Load(options);

            int i = 0;
            foreach (InfoEntity entity in this)
            {
                try
                {
                    this[i].Name = DWords[entity.Name.RemoveWhitespace().RemoveDiacritics()];

                }
                catch (Exception ex)
                {
                    log.Error(ex.Output());
                }
            }
            i++;
        }

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
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Adding Info(s) failed !");
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
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Deleting Section(s) list failed !");
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
                log.Error(ex.Output(), ex);
                MessageBase.Fatal(ex, "Replacing Info(s) failed !");
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
