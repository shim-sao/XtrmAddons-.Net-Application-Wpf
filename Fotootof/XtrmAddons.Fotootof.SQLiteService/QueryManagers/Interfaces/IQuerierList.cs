using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace XtrmAddons.Fotootof.SQLiteService.QueryManagers.Interfaces
{
    interface IQuerierList<T, V>
    {
        #region Methods List

        /// <summary>
        /// Method to get a list of AclAction entities.
        /// </summary>
        /// <param name="op">AclAction list options to perform query.</param>
        /// <returns>A list of AclAction entities.</returns>
        ObservableCollection<T> List(V op);

        /// <summary>
        /// Method to get a list of AclAction entities.
        /// </summary>
        /// <param name="op">AclAction list options to perform query.</param>
        /// <returns>A list of AclAction entities.</returns>
        Task<ObservableCollection<T>> ListAsync(V op);

        #endregion
    }
}
