using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class Fotootof.SQLite.Services Version Query Manager.
    /// </summary>
    public class QuerierVersion : Queriers
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods List

        /// <summary>
        /// Method to get an <see cref="ObservableCollection{T}"/> of <see cref="VersionEntity"/>.
        /// </summary>
        /// <param name="op">Version list options to perform query.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="VersionEntity"/>.</returns>
        public ObservableCollection<VersionEntity> List(VersionOptionsList op = null)
        {
            using (Db.Context) { return new ObservableCollection<VersionEntity>(VersionManager.List(op)); }
        }

        /// <summary>
        /// Method to get an <see cref="ObservableCollection{T}"/> of <see cref="VersionEntity"/> asynchronously.
        /// </summary>
        /// <param name="op">Version list options to perform query.</param>
        /// <returns>An <see cref="ObservableCollection{T}"/> of <see cref="VersionEntity"/>.</returns>
        public Task<ObservableCollection<VersionEntity>> ListAsync(VersionOptionsList op = null)
            => Task.Run(() => { return List(op); });

        #endregion



        #region Methods Single

        /// <summary>
        /// Method to select a <see cref="VersionEntity"/> or null.
        /// </summary>
        /// <param name="op">Version select options to perform query.</param>
        /// <returns>A <see cref="VersionEntity"/> or null if not found.</returns>
        public VersionEntity SingleOrNull(VersionOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context) { return VersionManager.Select(op); }
        }

        /// <summary>
        /// Method to select a <see cref="VersionEntity"/> or null asynchronously.
        /// </summary>
        /// <param name="op">Version select options to perform query.</param>
        /// <returns>A <see cref="VersionEntity"/> or null if not found.</returns>
        public Task<VersionEntity> SingleOrNullAsync(VersionOptionsSelect op)
            => Task.Run(() => SingleOrNull(op));

        /// <summary>
        /// Method to select a <see cref="VersionEntity"/> or default entity.
        /// </summary>
        /// <param name="op">Version select options to perform query.</param>
        /// <returns>A <see cref="VersionEntity"/> or default if not found.</returns>
        public VersionEntity SingleOrDefault(VersionOptionsSelect op)
        {
            if (op == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(op));
                log.Error(e.Output(), e);
                throw e;
            }

            using (Db.Context) { return VersionManager.Select(op); }
        }

        /// <summary>
        /// Method to select a <see cref="VersionEntity"/> or default entity asynchronously.
        /// </summary>
        /// <param name="op">Version select options to perform query.</param>
        /// <returns>A <see cref="VersionEntity"/> or default if not found.</returns>
        public Task<VersionEntity> SingleOrDefaultAsync(VersionOptionsSelect op)
            => Task.Run(() => SingleOrDefault(op));

        #endregion
    }
}