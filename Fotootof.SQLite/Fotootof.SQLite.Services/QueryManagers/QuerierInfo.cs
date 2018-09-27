using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Managers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fotootof.SQLite.Services.QueryManagers
{
    /// <summary>
    /// Class Fotootof.SQLite.Services.
    /// </summary>
    public partial class QuerierInfo : Queriers
    {
        #region Methods List

        /// <summary>
        /// Method to get a list of Info entity.
        /// </summary>
        /// <param name="op">Infos entities list options to perform query.</param>
        /// <returns>A list of Info entities.</returns>
        public ObservableCollection<InfoEntity> List(InfoOptionsList op)
        {
            using (Db.Context)
            {
                return new ObservableCollection<InfoEntity>(InfoManager.List(op));
            }
        }

        /// <summary>
        /// Method to get a list of Info entities.
        /// </summary>
        /// <param name="op">Infos entities list options to perform query.</param>
        /// <returns>A list of Info entities.</returns>
        public Task<ObservableCollection<InfoEntity>> List_Async(InfoOptionsList op)
        {
            return Task.Run(() => List(op));
        }

        #endregion
    }
}