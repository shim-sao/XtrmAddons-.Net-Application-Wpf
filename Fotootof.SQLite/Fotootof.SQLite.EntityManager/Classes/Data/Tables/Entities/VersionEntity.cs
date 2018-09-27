using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Fotootof.SQLite.EntityManager.Data.Base;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Version Entity.
    /// </summary>
    [Serializable]
    [Table("Versions")]
    [JsonObject(MemberSerialization.OptIn, Title = "Version")]
    [XmlType(TypeName = "Version")]
    public partial class VersionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable assembly version min.
        /// </summary>
        [XmlIgnore]
        private string assemblyVersionMin = "";

        /// <summary>
        /// Variable assembly version max.
        /// </summary>
        [XmlIgnore]
        private string assemblyVersionMax = "";


        /// <summary>
        /// Variable comment for the Version.
        /// </summary>
        [XmlIgnore]
        private string comment = "";

        #endregion



        #region Proprerties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [XmlIgnore]
        public int VersionId
        {
            get { return primaryKey; }
            set
            {
                if (value != primaryKey)
                {
                    PrimaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the assembly version min.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "AssemblyVersionMin")]
        [XmlAttribute(DataType = "string", AttributeName = "AssemblyVersionMin")]
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
        [JsonProperty(PropertyName = "AssemblyVersionMax")]
        [XmlAttribute(DataType = "string", AttributeName = "AssemblyVersionMax")]
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
        [JsonProperty(PropertyName = "Comment")]
        [XmlAttribute(DataType = "string", AttributeName = "Comment")]
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
