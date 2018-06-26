using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite AclGroups Entities Manager.
    /// </summary>
    public partial class AclGroupManager : EntitiesManager
    {
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
        /// <param name="depencies">Load also dependencies.</param>
        /// <returns>A list of AclGroup entities.</returns>
        public List<AclGroupEntity> List<T>(AclGroupOptionsList op = null) where T : AclGroupEntity
        {
            // Initialize default option list.
            op = op ?? new AclGroupOptionsList { };

            // Initialize query.
            IQueryable<AclGroupEntity> query = Context.AclGroups as IQueryable<AclGroupEntity>;

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

            int result = await Context.Database.ExecuteSqlCommandAsync (
                    $"DELETE FROM AclGroupsInAclActions WHERE AclGroupId = {aclGroupId} AND AclActionId IN ({action})"
                );

            Save();

            return result;
        }

        /// <summary>
        /// Method to remove association between a AclAction and an AclGroup.
        /// </summary>
        /// <param name="aclGroupId">The id of the AclGroup.</param>
        /// <param name="aclActionId">The id of the AclAction.</param>
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async void CleanAclActionDependencyAsync(AclGroupEntity entity)
        {
            string action = string.Join(",", entity.AclActionsPK);

            int result = await Context.Database.ExecuteSqlCommandAsync (
                    $"DELETE FROM AclGroupsInAclActions WHERE AclGroupId = {entity.PrimaryKey} AND AclActionId NOT IN ({action})"
                );

            Save();
        }

        /// <summary>
        /// Method to select an AclGroup.
        /// </summary>
        /// <param name="op">AclGroup entities select options to perform query.</param>
        /// <returns>An AclGroup entity or null if not found.</returns>
        public AclGroupEntity Select(AclGroupOptionsSelect op, bool nullable = false)
        {
            // Initialize query.
            IQueryable<AclGroupEntity> query = Context.AclGroups;

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
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async Task<int> RemoveUserDependencyAsync(int aclGroupId, int userId, bool save = true)
        {
            int result = await Context.Database.ExecuteSqlCommandAsync(
                $"DELETE FROM UsersInAclGroups  WHERE AclGroupId = {aclGroupId} AND UserId = {userId}"
             );

            Save(save);
            return result;
        }

        #endregion
    }
}
