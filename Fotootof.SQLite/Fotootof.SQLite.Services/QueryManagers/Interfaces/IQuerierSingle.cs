using System.Threading.Tasks;

namespace Fotootof.SQLite.Services.QueryManagers.Interfaces
{
    interface IQuerierSingle<T, V>
    {
        #region Methods Single or Null

        /// <summary>
        /// Method to select an entity.
        /// </summary>
        /// <param name="op">Entity select options to perform query.</param>
        /// <returns>An entity or null if not found.</returns>
        T SingleOrNull(V op);

        /// <summary>
        /// Method to select asynchronously an entity.
        /// </summary>
        /// <param name="op">Entity select options to perform query.</param>
        /// <returns>An entity or null if not found.</returns>
        Task<T> SingleOrNullAsync(V op);

        #endregion


        #region Methods Single or Default

        /// <summary>
        /// Method to select an entity.
        /// </summary>
        /// <param name="op">Entity select options to perform query.</param>
        /// <returns>An entity or null if not found.</returns>
        T SingleOrDefault(V op);

        /// <summary>
        /// Method to select asynchronously an entity.
        /// </summary>
        /// <param name="op">Entity select options to perform query.</param>
        /// <returns>An entity or null if not found.</returns>
        Task<T> SingleOrDefaultAsync(V op);

        #endregion
    }
}
