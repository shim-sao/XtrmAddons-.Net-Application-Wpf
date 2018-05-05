using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Version Entity.
    /// </summary>
    [Table("Versions")]
    public partial class VersionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable assembly version min.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string assemblyVersionMin = "";

        /// <summary>
        /// Variable assembly version max.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string assemblyVersionMax = "";


        /// <summary>
        /// Variable comment for the Version.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string comment = "";

        #endregion



        #region Proprerties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int VersionId
        {
            get { return primaryKey; }
            set
            {
                if (value != primaryKey)
                {
                    primaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the assembly version min.
        /// </summary>
        [Column(Order = 1)]
        public string AssemblyVersionMin
        {
            get { return assemblyVersionMin; }
            set
            {
                if (value != assemblyVersionMin)
                {
                    assemblyVersionMin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the assembly version max.
        /// </summary>
        [Column(Order = 2)]
        public string AssemblyVersionMax
        {
            get { return assemblyVersionMax; }
            set
            {
                if (value != assemblyVersionMax)
                {
                    assemblyVersionMax = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property comment for the Version.
        /// </summary>
        [Column(Order = 3)]
        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Version Entity Constructor.
        /// </summary>
        public VersionEntity() { }

        #endregion
    }
}
