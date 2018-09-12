using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Services Query Manager AclGroup.
    /// </summary>
    public partial class QuerierAclGroup : Queriers
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
        /// Method to select an <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AclGroupOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclGroupEntity"/> or null if not found.</returns>
        public AclGroupEntity SingleOrNull(AclGroupOptionsSelect op)
        {
            using (Db.Context)
            {
                return AclGroupManager.Select(op, true);
            }
        }

        /// <summary>
        /// Method to select an <see cref="AclGroupEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="AclGroupOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclGroupEntity"/> or null if not found.</returns>
        public Task<AclGroupEntity> SingleOrNull_Async(AclGroupOptionsSelect op)
        {
            return Task.Run(() =>
            {
                return SingleOrNull(op);
            });
        }

        /// <summary>
        /// Method to select an <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="op"><see cref="AclGroupOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclGroupEntity"/> or null if not found.</returns>
        public AclGroupEntity SingleOrDefault(AclGroupOptionsSelect op)
        {
            using (Db.Context)
            {
                return AclGroupManager.Select(op, false);
            }
        }

        /// <summary>
        /// Method to select an <see cref="AclGroupEntity"/> asynchronously.
        /// </summary>
        /// <param name="op"><see cref="AclGroupOptionsSelect"/> options to perform query.</param>
        /// <returns>An <see cref="AclGroupEntity"/> or null if not found.</returns>
        public Task<AclGroupEntity> SingleOrDefault_Async(AclGroupOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to add new <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="AclGroup">The <see cref="AclGroupEntity"/> to add.</param>
        /// <returns>The added <see cref="AclGroupEntity"/>.</returns>
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
        /// Method to add new <see cref="AclGroupEntity"/> asynchronously.
        /// </summary>
        /// <param name="AclGroup">The <see cref="AclGroupEntity"/> to add.</param>
        /// <returns>The added <see cref="AclGroupEntity"/>.</returns>
        public Task<AclGroupEntity> AddAsync(AclGroupEntity entity)
            => Task.Run(() => { return Add(entity); });

        #endregion



        #region Methods Delete

        /// <summary>
        /// Method to delete an <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="aclGroupId">An <see cref="AclGroupEntity"/> primary key.</param>
        /// <returns>The deleted <see cref="AclGroupEntity"/>.</returns>
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
        /// Method to delete an <see cref="AclGroupEntity"/> asynchronously.
        /// </summary>
        /// <param name="aclGroupId">An <see cref="AclGroupEntity"/> primary key.</param>
        /// <returns>The deleted <see cref="AclGroupEntity"/>.</returns>
        public Task<AclGroupEntity> DeleteAsync(int aclGroupId)
            => Task.Run(() => { return Delete(aclGroupId); });

        #endregion



        #region Methods Update

        /// <summary>
        /// Method to update an <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="entity">The AclGroup Entity</param>
        /// <returns>The updated <see cref="AclGroupEntity"/>.</returns>
        public async Task<AclGroupEntity> UpdateAsync(AclGroupEntity entity)
        {
            using (Db.Context)
            {
                // Try to attach entity to the database context.
                try { Db.Context.Attach(entity); } catch { throw new System.Exception("Error on database Context Attach AclGroup"); }

                // Update entity.
                entity = await AclGroupManager.UpdateAsync(entity);

                if (entity.IsDefault)
                {
                    SetDefault(entity.PrimaryKey);
                }

                // Hack to delete unassociated dependencies. 
                //await CleanDependencies_Async("AclGroupsInAclActions", "AclActionId", entity.PrimaryKey, entity.AclActionsPKeys);
                //await CleanDependencies_Async("SectionsInAclGroups", "SectionId", entity.PrimaryKey, entity.SectionsPKs);
                //await CleanDependencies_Async("UsersInAclGroups", "UserId", entity.PrimaryKey, entity.UsersPKeys);

                return entity;
            }
        }

        /// <summary>
        /// Method to set default <see cref="AclGroupEntity"/>.
        /// </summary>
        /// <param name="entityPK">A <see cref="AclGroupEntity"/> primary key.</param>
        public async void SetDefault(int entityPK)
        {
            using (Db.Context)
            {
                await AclGroupManager.SetDefaultAsync("AclGroups", "AclGroupId", entityPK);
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
        [System.Obsolete("")]
        public async Task<int> CleanDependencies_Async(string dependencyName, string dependencyPKName, int aclGroupId, IEnumerable<int> dependenciesPKs)
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