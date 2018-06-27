using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base.Interfaces
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Interface for Base Entity.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public interface IAlias : IEntityBase
    {
        #region Proprerties

        /// <summary>
        /// Property to access to the column Name of the entity.
        /// </summary>
        [NotMapped]
        [JsonProperty]
        string Name { get; set; }

        /// <summary>
        /// Property alias of the entity.
        /// </summary>
        [NotMapped]
        [JsonProperty]
        string Alias { get; set; }

        #endregion
    }
}
