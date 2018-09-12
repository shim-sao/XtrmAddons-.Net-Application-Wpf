using System;

namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table Column `Created`.
    /// </summary>
    public interface IColumnCreated
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the SQLite table column `Created` definition of the entity.
        /// </summary>
        DateTime Created { get; set; }

        #endregion
    }
}