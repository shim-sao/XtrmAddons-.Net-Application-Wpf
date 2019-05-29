using System;

namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table Column `Modified`.
    /// </summary>
    public interface IColumnModified
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the SQLite table column `Modified` definition of the entity.
        /// </summary>
        DateTime Modified { get; set; }

        #endregion
    }
}