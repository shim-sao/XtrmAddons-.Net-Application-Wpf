using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Lib.Api.Models.Json
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Api Models Json Info.
    /// </summary>
    public class InfoJson : EntityJson
    {
        #region Properties

        /// <summary>
        /// Variable primary key auto incremented.
        /// </summary>
        public int InfoId { get; set; }

        /// <summary>
        /// Variable Info Type primary key.
        /// </summary>
        public int InfoTypeId { get; set; }

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Variable Alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Property Description of the Info.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Variable created date.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Property ordering.
        /// </summary>
        public int Ordering { get; set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Info.
        /// </summary>
        public InfoJson() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Api Models Json Info.
        /// </summary>
        /// <param name="folder"></param>
        public InfoJson(InfoEntity info) : this()
        {
            InfoId = info.InfoId;
            InfoTypeId = info.InfoTypeId;

            Name = info.Name;
            Alias = info.Alias;
            Description = info.Description;

            IsDefault = info.IsDefault;
            Ordering = info.Ordering;
        }

        #endregion
    }
}
