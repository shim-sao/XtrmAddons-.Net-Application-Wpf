using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLiteService : Versions.
    /// </summary>
    [System.Obsolete("Use others mechanisms. Table will be deleted.")]
    public partial class QuerierVersion : Queriers
    {
        #region Methods List

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<VersionEntity> List(VersionOptionsList op = null)
        {
            using (Db.Context)
            {
                return new ObservableCollection<VersionEntity>(VersionManager.List(op));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<ObservableCollection<VersionEntity>> ListAsync(VersionOptionsList op = null)
            => Task.Run(() => List(op));

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select an Version entity.
        /// </summary>
        /// <param name="op">Versions entities select options to perform query.</param>
        /// <returns>A version entity or null if not found.</returns>
        public VersionEntity SingleOrNull(VersionOptionsSelect op)
        {
            using (Db.Context)
            {
                return VersionManager.Select(op);
            }
        }

        /// <summary>
        /// Method to select an Version entity asynchronously.
        /// </summary>
        /// <param name="op">Versions entities select options to perform query.</param>
        /// <returns>A version entity or null if not found.</returns>
        public Task<VersionEntity> SingleOrNullAsync(VersionOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        #endregion



        #region Methods Add

        /// <summary>
        /// Method to insert a new Version.
        /// </summary>
        /// <param name="entity">The Version entity informations to insert.</param>
        /// <param name="save">Should save database changes ?</param>
        /// <returns></returns>
        public VersionEntity Add(VersionEntity entity, bool save = true)
        {
            using (Db.Context)
            {
                return VersionManager.Add(entity, save);
            }
        }

        /// <summary>
        /// Method to insert a new Version asynchronous.
        /// </summary>
        /// <param name="entity">The Version entity informations to insert.</param>
        /// <param name="save">Should save database changes ?</param>
        public Task<VersionEntity> AddAsync(VersionEntity entity, bool save = true)
            => Task.Run(() => Add(entity, save));

        #endregion



        #region Methods Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public VersionEntity Update(VersionEntity version)
        {
            using (Db.Context)
            {
                Db.Context.Versions.Attach(version);
                return VersionManager.Update(version);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<VersionEntity> UpdateAsync(VersionEntity version)
        {
            return await Task.Run(() =>
            {
                return Update(version);
            });
        }

        #endregion



        #region Methods Delete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public VersionEntity Delete(VersionEntity entity)
        {
            VersionEntity item;

            using (Db.Context)
            {
                item = VersionManager.Delete(entity);
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<VersionEntity> DeleteAsync(VersionEntity entity)
        {
            return Task.Run(() =>
            {
                return Delete(entity);
            });
        }

        #endregion
    }
}
