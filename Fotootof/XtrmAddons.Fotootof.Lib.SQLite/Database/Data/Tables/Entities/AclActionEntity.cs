using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Acl Action Entity.
    /// </summary>
    [Table("AclActions")]
    public partial class AclActionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of AclGroups associated to the AclAction.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AclGroupEntity> aclGroups;

        #endregion



        #region Properties

        /// <summary>
        /// Property primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int AclActionId { get; set; }

        /// <summary>
        /// Property action of the item.
        /// </summary>
        [Column(Order = 1)]
        public string Action { get; set; }

        /// <summary>
        /// Property parameters of the item.
        /// </summary>
        [Column(Order = 2)]
        public string Parameters { get; set; }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public override int PrimaryKey { get => AclActionId; set => AclActionId = value; }

        /// <summary>
        /// Property to access to the  AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AclGroupId { get; set; }

        /// <summary>
        /// Property to access to the list of AclGroup associated to the AclAction.
        /// </summary>
        [NotMapped]
        public List<AclGroupEntity> AclGroups
        {
            get => ListAclGroups();
            set => aclGroups = value;
        }

        /// <summary>
        /// Property to access to the collection of relationship AclGroup in AclAction.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<AclGroupsInAclActions> AclGroupsInAclActions { get; set; }
            = new ObservableCollection<AclGroupsInAclActions>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entity Constructor.
        /// </summary>
        public AclActionEntity()
        {
            Initialize();

            AclGroupsInAclActions.CollectionChanged += (s, e) => { aclGroups = null; };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the AclAction.
        /// </summary>
        public void Initialize()
        {
            if (PrimaryKey <= 0)
            {
                PrimaryKey = 0;
            }

            this.InitializeNulls();
        }
        
        /// <summary>
        /// Method to get list of associated AclGroup.
        /// </summary>
        private List<AclGroupEntity> ListAclGroups()
        {
            if (aclGroups == null || aclGroups.Count == 0)
            {
                aclGroups = new List<AclGroupEntity>();

                if (AclGroupsInAclActions != null)
                {
                    aclGroups = ListEntities<AclGroupEntity>(AclGroupsInAclActions);
                }
            }

            return aclGroups;
        }

        #endregion
    }
}