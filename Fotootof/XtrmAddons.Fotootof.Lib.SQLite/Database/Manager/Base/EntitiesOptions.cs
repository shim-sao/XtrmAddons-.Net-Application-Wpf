using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Entities Options Base.
    /// </summary>
    public class EntitiesOptions
    {
        #region Properties

        /// <summary>
        /// Property array of entity dependencies for process.
        /// </summary>
        public List<EnumEntitiesDependencies> Dependencies { get; set; } = new List<EnumEntitiesDependencies>();

        #endregion


        #region Methods

        /// <summary>
        /// Method to check if a required dependency is set.
        /// </summary>
        /// <param name="dependency">A dependency</param>
        /// <returns>True if the(all) dependency(ies) is(are) set.</returns>
        public bool IsDependOn(EnumEntitiesDependencies dependency)
        {
            if (
                Dependencies != null &&
                (
                    Dependencies.Contains(EnumEntitiesDependencies.All) ||
                    Dependencies.Contains(dependency)
                )
            ) {
                return true;
            }

            return false;
        }

        #endregion
    }
}