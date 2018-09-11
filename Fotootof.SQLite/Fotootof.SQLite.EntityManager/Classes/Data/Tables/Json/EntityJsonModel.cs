using Fotootof.SQLite.EntityManager.Connector;
using Fotootof.SQLite.EntityManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Common.Objects;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Json.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof SQLite Entity Manager Data Tables Json Model Entity.
    /// </summary>
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class EntityJsonModel<T> : ObjectBaseNotifier, IColumnPrimaryKey
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        [XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the database connector.
        /// </summary>
        [XmlIgnore]
        private static DatabaseCore db;

        /// <summary>
        /// Variable to store the primary key auto incremented value.
        /// </summary>
        [XmlIgnore]
        protected int primaryKey = 0;

        #endregion



        #region Proprerties

        /// <summary>
        /// Property to access to the application database connector.
        /// </summary>
        [XmlIgnore]
        public static DatabaseCore Db
        {
            get => db;
            set => db = value;
        }

        /// <summary>
        /// Property alias to access to the Primary Key (PK or Id) of the entity.
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        [XmlAttribute(DataType = "int", AttributeName = "Id")]
        public int PrimaryKey
        {
            get => primaryKey;
            set
            {
                if (value != primaryKey)
                {
                    primaryKey = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructors

        /// <summary>
        /// Class Fotootof Plugin Api Models Json Album constructor.
        /// </summary>
        public EntityJsonModel() { }
        
        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="auth"></param>
        public virtual void FromEntity(T entity, bool auth = false) { }

        /// <summary>
        /// 
        /// </summary>
        public virtual T ToEntity() { return default(T); }

        #endregion
    }
}
