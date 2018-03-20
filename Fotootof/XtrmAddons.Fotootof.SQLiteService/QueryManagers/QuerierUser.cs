using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService : Users.
    /// </summary>
    public partial class QuerierUser : Querier
    {
        #region Methods List

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<UserEntity> List(UserOptionsList op = null)
        {
            using (Db.Context)
            {
                return new ObservableCollection<UserEntity>(UserManager.List(op));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ObservableCollection<UserEntity>> ListAsync(UserOptionsList op = null)
            => Task.Run(() => List(op));

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select an User entity.
        /// </summary>
        /// <param name="op">Users entities select options to perform query.</param>
        /// <returns>A user entity or null if not found.</returns>
        public UserEntity SingleOrNull(UserOptionsSelect op)
        {
            using (Db.Context)
            {
                return UserManager.Select(op);
            }
        }

        /// <summary>
        /// Method to select an User entity asynchronously.
        /// </summary>
        /// <param name="op">Users entities select options to perform query.</param>
        /// <returns>A user entity or null if not found.</returns>
        public Task<UserEntity> SingleOrNullAsync(UserOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to insert a new User.
        /// </summary>
        /// <param name="entity">The User entity informations to insert.</param>
        /// <param name="save">Should save database changes ?</param>
        /// <returns></returns>
        public UserEntity Add(UserEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                return UserManager.Add(entity, save);
            }
        }

        /// <summary>
        /// Method to insert a new User asynchronous.
        /// </summary>
        /// <param name="entity">The User entity informations to insert.</param>
        /// <param name="save">Should save database changes ?</param>
        public Task<UserEntity> AddAsync(UserEntity entity, bool save = true)
            => Task.Run(() => Add(entity, save));

        #endregion



        #region Methods Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserEntity> Update(UserEntity user)
        {
            using (Db.Context)
            {
                Db.Context.Users.Attach(user);
                return await UserManager.UpdateAsync(user);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            return await Update(user);
        }

        #endregion



        #region Methods Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserEntity Delete(UserEntity entity)
        {
            UserEntity item;

            using (Db.Context)
            {
                item = UserManager.Delete(entity);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<UserEntity> DeleteAsync(UserEntity entity)
        {
            return Task.Run(() =>
            {
                return Delete(entity);
            });
        }
        #endregion



        #region Methods Dependency AclGroup

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="aclGroupId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAclGroupDependenciesAsync(int userId, List<int> aclGroupId)
        {
            using (Db.Context)
            {
                return await UserManager.DeleteAclGroupDependencyAsync(userId, aclGroupId);
            }
        }

        #endregion
    }
}
