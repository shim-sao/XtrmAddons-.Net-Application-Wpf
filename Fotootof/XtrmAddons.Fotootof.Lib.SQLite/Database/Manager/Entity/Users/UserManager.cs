using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Scheme;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Users Entities Manager.
    /// </summary>
    public partial class UserManager : EntitiesManager
    {
        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Users Entities Manager Constructor.
        /// </summary>
        /// <param name="context"></param>
        public UserManager(DatabaseContextCore context) : base(context) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a list of User entities.
        /// </summary>
        /// <param name="dependencies">Load also dependencies.</param>
        /// <returns>A list of User entities.</returns>
        public List<UserEntity> List(UserOptionsList op = null)
        {
            // Initialize default option list.
            op = op ?? new UserOptionsList { };

            // Initialize query.
            IQueryable <UserEntity> query = Context.Users;

            // Load ACLGroup dependency if required.
            if (op.IsDependOn(EnumEntitiesDependencies.UsersInAclGroups))
            {
                query = query.Include(x => x.UsersInAclGroups);
            }

            // Check for include primary keys to search in.
            query.QueryListInclude(op);

            // Check for exclude primary keys in search.
            query.QueryListExclude(op);


            // Check for include AclGroup primary keys to search in.
            if (op.IncludeAclGroupKeys != null && op.IncludeAclGroupKeys.Count > 0)
            {
                query = query.Where(x => x.UsersInAclGroups.Any(y => op.IncludeAclGroupKeys.Contains(y.AclGroupId)));
            }

            // Check for exclude AclGroup primary keys in search.
            if (op.ExcludeAclGroupKeys != null && op.ExcludeAclGroupKeys.Count > 0)
            {
                query = query.Where(x => x.UsersInAclGroups.Any(y => !op.ExcludeAclGroupKeys.Contains(y.AclGroupId)));
            }
            

            // Set number elements to skip & the number elements to select.
            query.QueryStartLimit(op);

            // Return a list of entities.
            return query.ToList();
        }

        /// <summary>
        /// Method to remove association between an User and a list of AclGroup.
        /// </summary>
        /// <param name="userId">The id of the User.</param>
        /// <param name="aclGroupId">The list oft id of AclGoup.</param>
        /// <returns>Async task with modified AclGroup entity as result.</returns>
        public async Task<int> DeleteAclGroupDependencyAsync(int userId, List<int> aclGroupId)
        {
            string action = string.Join(",", aclGroupId);

            int result = await Context.Database.ExecuteSqlCommandAsync
                (
                    "DELETE FROM UsersInAclGroups"
                    + string.Format(" WHERE UserId = {0} AND AclGroupId IN ({1})", userId, action)
                );

            Save();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> CleanAclGroupDependencyAsync(UserEntity entity, bool save = true)
        {
            string ids = string.Join(",", entity.ListOfPrimaryKeys(entity.UsersInAclGroups.ToList(), "AclGroupId"));

            int result = await Context.Database.ExecuteSqlCommandAsync
                (
                    "DELETE FROM UsersInAclGroups"
                    + string.Format(" WHERE UserId = {0} AND AclGroupId NOT IN ({1})", entity.PrimaryKey, ids)
                );

            // Save changes on the database.
            if (save)
            {
                Save();
            }

            return result;
        }

        /// <summary>
        /// Method to update an Entity.
        /// </summary>
        /// <param name="item">An Entity to update.</param>
        /// <returns>The updated Entity.</returns>
        public async Task<UserEntity> UpdateAsync(UserEntity entity, bool save = true)
        {
            // Update item informations.
            entity = Context.Update(entity).Entity;
            await CleanAclGroupDependencyAsync(entity, save);

            // Save changes on the database.
            if (save)
            {
                Save();
            }

            // Return updated item.
            return entity;
        }

        /// <summary>
        /// Method to select an User.
        /// </summary>
        /// <param name="op">Users entities select options to perform query.</param>
        /// <returns>A User entity or null if not found.</returns>
        public UserEntity Select(UserOptionsSelect op)
        {
            // Initialize query.
            IQueryable<UserEntity> query = Context.Users;
            
            // Load AclGroups dependencies if required.
            if (op.IsDependOn(EnumEntitiesDependencies.UsersInAclGroups))
            {
                query = query.Include(x => x.UsersInAclGroups);
            }
                
            if (op.PrimaryKey > 0)
            {
                return SingleIdOrNull(query, op);
            }

            if (!op.Name.IsNullOrWhiteSpace())
            {
                return SingleNameOrNull(query, op);
            }

            if (!op.Email.IsNullOrWhiteSpace())
            {
                return SingleEmailOrNull(query, op);
            }

            return null;
        }

        /// <summary>
        /// Method to select single User by id.
        /// </summary>
        /// <returns>An User entity or null if not found.</returns>
        public UserEntity SingleIdOrNull(IQueryable<UserEntity> query, UserOptionsSelect op)
        {
            UserEntity item = null;

            try
            {
                item = query.SingleOrDefault(x => x.UserId == op.PrimaryKey);
            }
            catch (ArgumentNullException e)
            {
                log.Error("User single ID not found !", e);
                return null;
            }
            catch (Exception e)
            {
                log.Fatal("User single ID not found !", e);
                return null;
            }

            // Check if user is found, return null instead of default.
            if (item != null && item.UserId == 0)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        /// Method to select an User by name.
        /// </summary>
        /// <returns>An User entity or null if not found.</returns>
        public UserEntity SingleNameOrNull(IQueryable<UserEntity> query, UserOptionsSelect op)
        {
            // Select user by name.
            UserEntity item = null;
            try
            {
                item = query.SingleOrDefault(x => x.Name == op.Name);
            }
            catch (ArgumentNullException e)
            {
                log.Error("User single NAME not found !", e);
                return null;
            }
            catch (Exception e)
            {
                log.Fatal("User single NAME not found !", e);
                return null;
            }

            // Check if user is found, return null instead of default.
            if (item != null && item.UserId == 0)
            {
                return null;
            }

            // Check password if required.
            if (op.CheckPassword && (op.Password != item.Password))
            {
                return null;
            }

            return item;
        }

        /// <summary>
        /// Method to select an User by name.
        /// </summary>
        /// <returns>An User entity or null if not found.</returns>
        public UserEntity SingleEmailOrNull(IQueryable<UserEntity> query, UserOptionsSelect op)
        {
            // Select user by name.
            UserEntity item = null;
            try
            {
                item = query.SingleOrDefault(x => x.Email == op.Email);
            }
            catch (ArgumentNullException e)
            {
                log.Error("User single EMAIL not found !", e);
                return null;
            }
            catch (Exception e)
            {
                log.Fatal("User single EMAIL not found !", e);
                return null;
            }

            // Check if user is null.
            if (item == null)
            {
                return null;
            }

            // Check if user is found, return null instead of default.
            if (item.UserId == 0)
            {
                return null;
            }

            // Check password if required.
            if (op.CheckPassword && (op.Password != item.Password))
            {
                return null;
            }

            return item;
        }

        #endregion
    }
}