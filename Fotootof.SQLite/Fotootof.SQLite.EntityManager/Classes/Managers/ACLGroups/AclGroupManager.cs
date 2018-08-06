using Fotootof.SQLite.EntityManager.Base;
using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Managers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite AclGroups Entities Manager.
    /// </summary>
    public partial class AclGroupManager : EntitiesManager
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public DatabaseContextCore Db => Connector as DatabaseContextCore;

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclGroups Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public AclGroupManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of AclGroup entities.
        /// </summary>
        /// <param name="op"></param>
        /// <returns>A list of AclGroup entities.</returns>
        public List<AclGroupEntity> List<T>(AclGroupOptionsList op = null) where T : AclGroupEntity
        {
            // Initialize default option list.
            op = op ?? new AclGroupOptionsList { };

            // Initialize query.
            IQueryable<AclGroupEntity> query = Connector.AclGroups as IQueryable<AclGroupEntity>;

            // Load dependencies if required.
            query = Query_Dependencies(query, op);

            query.QueryListInclude(op, "AclGroupId");
            query.QueryListExclude(op, "AclGroupId");

            // Set number elements to skip.
            if (op.Start > 0)
            {
                query = query.Skip(op.Start);
            }

            // Set the number elements to select.
            if (op.Limit > 0)
            {
                query = query.Take(op.Limit);
            }

            // Set order by.
            if (op.OrderBy.IsNullOrWhiteSpace() || op.OrderBy == "Name")
            {
                if (op.OrderDir == "Desc")
                {
                    query = query.OrderByDescending(x => x.Name);
                }
                else
                {
                    query = query.OrderBy(x => x.Name);
                }
            }

            return query.ToList();
        }

        /// <summary>
        /// Method to remove association between a AclAction and an AclGroup.
        /// </summary>
        /// <param name="aclGroupId">The id of the AclGroup.</param>
        /// <param name="aclActionId">The id of the AclAction.</param>
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async Task<int> DeleteAclActionDependencyAsync(int aclGroupId, List<int> aclActionId)
        {
            string action = string.Join(",", aclActionId);

            int result = await Connector.Database.ExecuteSqlCommandAsync (
                    $"DELETE FROM AclGroupsInAclActions WHERE AclGroupId = {aclGroupId} AND AclActionId IN ({action})"
                );

            Save();

            return result;
        }

        /// <summary>
        /// Method to remove association between a AclAction and an AclGroup.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async void CleanAclActionDependencyAsync(AclGroupEntity entity)
        {
            string action = string.Join(",", entity.AclActionsPKeys);

            int result = await Connector.Database.ExecuteSqlCommandAsync (
                    $"DELETE FROM AclGroupsInAclActions WHERE AclGroupId = {entity.PrimaryKey} AND AclActionId NOT IN ({action})"
                );

            Save();
        }

        /// <summary>
        /// Method to select an AclGroup.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <param name="nullable"></param>
        /// <returns>An AclGroup entity or null if not found.</returns>
        public AclGroupEntity Select(AclGroupOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<AclGroupEntity> query = Connector.AclGroups;

            // Load dependencies if required.
            query = Query_Dependencies(query, op);

            return SingleIdDefaultOrNull(query, op, nullable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private IQueryable<AclGroupEntity> Query_Dependencies(IQueryable<AclGroupEntity> query, EntitiesOptions op)
        {
            // Load AclActions dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.AclGroupsInAclActions))
            {
                query = query.Include(x => x.AclGroupsInAclActions);
            }

            // Load Users dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.UsersInAclGroups))
            {
                query = query.Include(x => x.UsersInAclGroups);
            }

            // Load Sections dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.SectionsInAclGroups))
            {
                query = query.Include(x => x.SectionsInAclGroups);
            }

            return query;
        }

        /// <summary>
        /// Method to select an AclGroup by id.
        /// </summary>
        /// <returns>An AclGroup entity or null if not found.</returns>
        private AclGroupEntity SingleIdDefaultOrNull(IQueryable<AclGroupEntity> query, AclGroupOptionsSelect op, bool nullable = false)
        {
            if (op.PrimaryKey <= 0)
            {
                if (nullable)
                    return null;
                else
                    return new AclGroupEntity();
            }

            AclGroupEntity item = query.SingleOrDefault(x => x.AclGroupId == op.PrimaryKey);

            // Check if user is found, return null instead of default.
            if (item == null || item.AclGroupId == 0)
            {
                if (nullable)
                    return null;
                else
                    return new AclGroupEntity();
            }

            return item;
        }

        /// <summary>
        /// Method to remove association between a AclGroup and an user.
        /// </summary>
        /// <param name="aclGroupId">The id of the AclGroup.</param>
        /// <param name="userId">The id of the user.</param>
        /// <param name="save"></param>
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async Task<int> RemoveUserDependencyAsync(int aclGroupId, int userId, bool save = true)
        {
            int result = await Connector.Database.ExecuteSqlCommandAsync(
                $"DELETE FROM UsersInAclGroups  WHERE AclGroupId = {aclGroupId} AND UserId = {userId}"
             );

            Save(save);
            return result;
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="entity">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public async Task<AclGroupEntity> UpdateAsync(AclGroupEntity entity)
        {
            // Remove Users In AclGroups dependencies.
            if (entity.UsersInAclGroups.DepPKeysRemoved.Count > 0)
            {
                await DeleteDependencyAsync(
                    new EntityManagerDeleteDependency { Name = "UsersInAclGroups", key = "AclGroupId", keyList = "UserId" },
                    entity.PrimaryKey,
                    entity.UsersInAclGroups.DepPKeysRemoved
                );
                entity.UsersInAclGroups.DepPKeysRemoved.Clear();
            }

            // Update item informations.
            entity = Connector.Update(entity).Entity;

            await SaveAsync();

            // Return the updated item.
            return entity;
        }

        /// <summary>
        /// <para>Method to inialize content of the table AclGroup after EnsureCreated()</para>
        /// <para>Call this method after database construction.</para>
        /// </summary>
        internal void InitializeTable()
        {
            try
            {
                log.Info("SQLite Initializing Table `AclGroups`. Please wait...");
                AclGroupEntity row;

                // INSERT Administrator Group.
                row = Connector.AclGroups.Add(new AclGroupEntity
                {
                    PrimaryKey = 1,
                    Name = Properties.Translations.Administrator,
                    Alias = Properties.Translations.Administrator.RemoveDiacritics().ToLower(),
                    Comment = Properties.Translations.AdministratorComment
                }).Entity;
                Save();
                row.AclActionsPKeys.Add(1);
                row.AclActionsPKeys.Add(2);
                row.AclActionsPKeys.Add(3);
                row.AclActionsPKeys.Add(4);
                row.AclActionsPKeys.Add(5);
                row.AclActionsPKeys.Add(6);
                row.AclActionsPKeys.Add(7);
                row.AclActionsPKeys.Add(8);
                Update(row);

                // INSERT Visitor Group.
                row = Connector.AclGroups.Add(new AclGroupEntity
                {
                    PrimaryKey = 2,
                    Name = Properties.Translations.Visitor,
                    Alias = Properties.Translations.Visitor.RemoveDiacritics().ToLower(),
                    Comment = Properties.Translations.VisitorComment,
                    IsDefault = true
                }).Entity;
                Save();
                row.AclActionsPKeys.Add(4);
                row.AclActionsPKeys.Add(8);
                Update(row);

                // Save content in database.
                int result = Save();
                log.Info($"SQLite Initializing Table `AclGroups`. {result} affected rows.");
            }
            catch (Exception ex)
            {
                log.Error("SQLite Initializing Table `AclGroups`. Exception.");
                log.Error(ex.Output(), ex);
            }
        }

        #endregion
    }
}
