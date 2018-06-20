using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService.
    /// </summary>
    public partial class QuerierSection : Querier
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods List

        /// <summary>
        /// Method to get a list of Section entity.
        /// </summary>
        /// <param name="op">Sections entities list options to perform query.</param>
        /// <returns>A list of Section entities.</returns>

        public ObservableCollection<SectionEntity> List(SectionOptionsList op)
        {
            using (Db.Context)
            {
                return new ObservableCollection<SectionEntity>(SectionManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of Section entities.
        /// </summary>
        /// <param name="op">Sections entities list options to perform query.</param>
        /// <returns>A list of Section entities.</returns>
        
        public Task<ObservableCollection<SectionEntity>> ListAsync(SectionOptionsList op)
            => Task.Run(() => List(op));

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select an Section entity.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        public SectionEntity SingleOrNull(SectionOptionsSelect op)
        {
            using (Db.Context)
            {
                return SectionManager.Select(op, true);
            }
        }

        /// <summary>
        /// Method to select asynchronously an Section entity.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        
        public Task<SectionEntity> SingleOrNullAsync(SectionOptionsSelect op)
            => Task.Run(() => SingleOrNullAsync(op));

        /// <summary>
        /// Method to select an Section entity.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        public SectionEntity SingleOrDefault(SectionOptionsSelect op)
        {
            using (Db.Context)
            {
                return SectionManager.Select(op, false);
            }
        }

        /// <summary>
        /// Method to select asynchronously an Section entity.
        /// </summary>
        /// <param name="op">Section entities select options to perform query.</param>
        /// <returns>An Section entity or null if not found.</returns>
        public Task<SectionEntity> SingleOrDefaultAsync(SectionOptionsSelect op)
            => Task.Run(() => SingleOrDefaultAsync(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new Section.
        /// </summary>
        /// <param name="Section">The Section entity to add.</param>
        /// <returns>The added Section entity.</returns>
        public SectionEntity Add(SectionEntity entity)
        {
            using (Db.Context)
            {
                entity = SectionManager.Add(entity);

                if (entity.IsDefault)
                {
                    SetDefault(entity.PrimaryKey);
                }

                return entity;
            }
        }

        /// <summary>
        /// Method to add new Section asynchronous.
        /// </summary>
        /// <param name="Section">The Section entity to add.</param>
        /// <returns>The added Section entity.</returns>
        public Task<SectionEntity> Section_Add_Async(SectionEntity entity)
        {
            return Task.Run(() => Add(entity));
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectionId"></param>
        /// <returns></returns>

        public SectionEntity Delete(int alGroupId)
        {
            SectionEntity item = SingleOrNull(new SectionOptionsSelect { PrimaryKey = alGroupId });

            using (Db.Context)
            {
                item = SectionManager.Delete(item);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        
        public Task<SectionEntity> DeleteAsync(int SectionId)
        {
            return Task.Run(() =>
            {
                return Delete(SectionId);
            });
        }

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update a Section entity asynchronously.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        /// <param name="save">Save database changes ?</param>
        /// <returns>The updated Section entity.</returns>
        public async Task<SectionEntity> UpdateAsync(SectionEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try
                {
                    Db.Context.Attach(entity);
                }
                catch (Exception e)
                {
                    log.Fatal(e.Output(), e);
                    throw e;
                }

                // Update entity.
                entity = SectionManager.Update(entity, save);

                // Check if entity is set to default.
                if (entity.IsDefault)
                {
                    SetDefault(entity.PrimaryKey);
                }

                // Hack to delete unassociated dependencies.
                await CleanDependenciesAsync("SectionsInACLGroups", "AclGroupId", entity.PrimaryKey, entity.AclGroupsPK);
                await CleanDependenciesAsync("AlbumsInSections", "AlbumId", entity.PrimaryKey, entity.AlbumsPKs);

                return entity;
            }
        }

        /// <summary>
        /// Method to set default Section.
        /// </summary>
        /// <param name="entityPK">A Section primary key.</param>
        public void SetDefault(int entityPK)
        {
            using (Db.Context)
            {
                SectionManager.SetDefaultAsync("Sections", "SectionId", entityPK);
            }
        }

        #endregion



        #region Methods Dependency

        /// <summary>
        /// Method to delete Section and AclGroup.
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="aclGroupIds"></param>
        /// <returns></returns>
        public async Task<int> DeleteDependenciesAsync(string entityPK, int sectionId, List<int> entityPKs)
        {
            using (Db.Context)
            {
                return await SectionManager.DeleteDependencyAsync
                    (
                        new EntityManagerDeleteDependency { Name = "", key = "SectionId", keyList = entityPK },
                        sectionId,
                        entityPKs
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="dependencyPKName"></param>
        /// <param name="sectionId"></param>
        /// <param name="dependenciesPKs"></param>
        /// <returns></returns>
        public async Task<int> CleanDependenciesAsync(string dependencyName, string dependencyPKName, int sectionId, IEnumerable<int> dependenciesPKs)
        {
            using (Db.Context)
            {
                return await SectionManager.CleanDependencyAsync
                    (
                        new EntityManagerDeleteDependency { Name = dependencyName, key = "SectionId", keyList = dependencyPKName },
                        sectionId,
                        dependenciesPKs
                    );
            }
        }

        #endregion
    }
}