using System.Collections.Generic;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Entities List Options.
    /// </summary>
    public class EntitiesOptionsList : EntitiesOptions
    {
        #region Properties

        /// <summary>
        /// Property main list of entity primary key.
        /// </summary>
        public List<int> IncludePrimaryKeys { get; set; }

        /// <summary>
        /// Property main list of entity primary key.
        /// </summary>
        public List<int> ExcludePrimaryKeys { get; set; }

        /// <summary>
        /// Property number to elements to skip.
        /// </summary>
        public int Start { get; set; } = 0;

        /// <summary>
        /// Property to set maximum number of elements in the list.
        /// </summary>
        public int Limit { get; set; } = 0;

        /// <summary>
        /// Property to order by elements.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Property order direction.
        /// </summary>
        public string OrderDir { get; set; }

        #endregion
    }
}