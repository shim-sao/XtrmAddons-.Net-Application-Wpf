using System;

namespace Fotootof.SQLite.EntityManager.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Entity Manager Table Column `Captured`.
    /// </summary>
    public interface IColumnCaptured
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the SQLite table column `Captured` definition of the entity.
        /// </summary>
        DateTime Captured { get; set; }

        #endregion
    }
}