using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService.
    /// </summary>
    public partial class QuerierAclGroup : Querier
    {
        #region Methods List

        /// <summary>
        /// Method to get a list of AclGroup entity.
        /// </summary>
        /// <param name="op">AclGroups entities list options to perform query.</param>
        /// <returns>A list of AclGroup entities.</returns>
        public ObservableCollection<AclGroupEntity> List(AclGroupOptionsList op)
        {
            using (Db.Context)
            {
                return new ObservableCollection<AclGroupEntity>(AclGroupManager.List<AclGroupEntity>(op));
            }
        }

        /// <summary>
        /// Method to get a list of AclGroup entities.
        /// </summary>
        /// <param name="op">AclGroups entities list options to perform query.</param>
        /// <returns>A list of AclGroup entities.</returns>
        public Task<ObservableCollection<AclGroupEntity>> List_Async(AclGroupOptionsList op)
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Select

        /// <summary>
        /// Method to select an AclGroup entity.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <returns>An AclGroup entity or null if not found.</returns>

        public AclGroupEntity SingleOrNull(AclGroupOptionsSelect op)
        {
            using (Db.Context)
            {
                return AclGroupManager.Select(op, true);
            }
        }

        /// <summary>
        /// Method to select asynchronously an AclGroup entity.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <returns>An AclGroup entity or null if not found.</returns>
        
        public Task<AclGroupEntity> SingleOrNull_Async(AclGroupOptionsSelect op)
        {
            return Task.Run(() =>
            {
                return SingleOrNull(op);
            });
        }

        /// <summary>
        /// Method to select an AclGroup entity.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <returns>An AclGroup entity or null if not found.</returns>
        
        public AclGroupEntity SingleOrDefault(AclGroupOptionsSelect op)
        {
            using (Db.Context)
            {
                return AclGroupManager.Select(op, false);
            }
        }

        /// <summary>
        /// Method to select asynchronously an AclGroup entity.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <returns>An AclGroup entity or null if not found.</returns>
        
        public Task<AclGroupEntity> SingleOrDefault_Async(AclGroupOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new AclGroup.
        /// </summary>
        /// <param name="AclGroup">The AclGroup entity to add.</param>
        /// <returns>The added AclGroup entity.</returns>
        public AclGroupEntity Add(AclGroupEntity entity)
        {
            using (Db.Context)
            {
                entity = AclGroupManager.Add(entity);
                
                if (entity.IsDefault)
                {
                    SetDefault(entity.PrimaryKey);
                }

                return entity;
            }
        }

        /// <summary>
        /// Method to add new AclGroup asynchronous.
        /// </summary>
        /// <param name="AclGroup">The AclGroup entity to add.</param>
        /// <returns>The added AclGroup entity.</returns>
        public Task<AclGroupEntity> Add_Async(AclGroupEntity entity)
        {
            return Task.Run(() =>
            {
                return Add(entity);
            });
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AclGroupId"></param>
        /// <returns></returns>
        public AclGroupEntity Delete(int alGroupId)
        {
            AclGroupEntity item = SingleOrNull(new AclGroupOptionsSelect { PrimaryKey = alGroupId });

            using (Db.Context)
            {
                item = AclGroupManager.Delete(item);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AclGroupId"></param>
        /// <returns></returns>
        
        public Task<AclGroupEntity> Delete_Async(int aclGroupId)
        {
            return Task.Run(() =>
            {
                return Delete(aclGroupId);
            });
        }

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update an AclGroup.
        /// </summary>
        /// <param name="AclGroup">The AclGroup Entity</param>
        /// <param name="save">Save database context changes ?</param>
        /// <returns>The updated AclGroup entity.</returns>
        public async Task<AclGroupEntity> Update_Async(AclGroupEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try { Db.Context.Attach(entity); } catch { throw new System.Exception("Error on database Context Attach AclGroup"); }

                // Update entity.
                entity = AclGroupManager.Update(entity, true);

                if (entity.IsDefault)
                {
                    SetDefault(entity.PrimaryKey);
                }

                // Hack to delete unassociated dependencies. 
                await CleanDependencies_Async("AclGroupsInAclActions", "AclActionId", entity.PrimaryKey, entity.AclActionsPK);
                await CleanDependencies_Async("SectionsInAclGroups", "SectionId", entity.PrimaryKey, entity.SectionsPK);
                await CleanDependencies_Async("UsersInAclGroups", "UserId", entity.PrimaryKey, entity.UsersPK);

                return entity;
            }
        }

        /// <summary>
        /// Method to set default AclGroup.
        /// </summary>
        /// <param name="entityPK">A AclGroup primary key.</param>
        public void SetDefault(int entityPK)
        {
            using (Db.Context)
            {
                AclGroupManager.SetDefaultAsync("AclGroups", "AclGroupId", entityPK);
            }
        }

        #endregion



        #region Methods Dependency

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyName"></param>
        /// <param name="dependencyPKName"></param>
        /// <param name="aclGroupId"></param>
        /// <param name="dependenciesPKs"></param>
        /// <returns></returns>
        public async Task<int> CleanDependencies_Async(string dependencyName, string dependencyPKName, int aclGroupId, List<int> dependenciesPKs)
        {
            using (Db.Context)
            {
                return await AclGroupManager.CleanDependencyAsync
                    (
                        new EntityManagerDeleteDependency { Name = dependencyName, key = "AclGroupId", keyList = dependencyPKName },
                        aclGroupId,
                        dependenciesPKs
                    );
            }
        }

        #endregion
    }
}