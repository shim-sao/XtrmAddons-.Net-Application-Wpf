using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Acl Action Entity.
    /// </summary>
    [Table("AclActions")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AclActionEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [JsonIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable action of the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string action = "";

        /// <summary>
        /// Variable parameters of the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private string parameters = "";

        #endregion



        #region Variables : Dependencies

        /// <summary>
        /// Variable AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private int aclGroupId = 0;

        /// <summary>
        /// Variable list of AclGroup associated to the AclAction.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AclGroupEntity> aclGroups;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [JsonIgnore]
        public int AclActionId
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
        /// Property action of the item.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public string Action
        {
            get { return action; }
            set
            {
                if (value != action)
                {
                    action = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property parameters of the item.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty]
        public string Parameters
        {
            get { return parameters; }
            set
            {
                if (value != parameters)
                {
                    parameters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property to access to the  AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AclGroupId
        {
            get => aclGroupId;
            set
            {
                if (value != aclGroupId)
                {
                    aclGroupId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertiy to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        [JsonExtensionData]
        public List<int> AclGroupsPK
            => ListOfPrimaryKeys(AclGroupsInAclActions.ToList(), "AclGroupId");

        /// <summary>
        /// Property to access to the list of AclGroup associated to the AclAction.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public List<AclGroupEntity> AclGroups
        {
            get => ListAclGroups();
            set
            {
                AddAclGroupsDependencies(aclGroups);
                aclGroups = value;
                NotifyPropertyChanged();
            }
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
            AclGroupsInAclActions.CollectionChanged += (s, e)
                => {
                    aclGroups = null;
                    NotifyPropertyChanged("AclGroups");
                };
        }

        #endregion



        #region Methods
        
        /// <summary>
        /// Method to get the list of associated AclGroup.
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

        /// <summary>
        /// Method to add a list of associated AclGroup.
        /// </summary>
        /// <param name="aclGroups">A list of AclGroups</param>
        private void AddAclGroupsDependencies(List<AclGroupEntity> aclGroups)
        {
            if (aclGroups == null || aclGroups.Count == 0)
            {
                // Link all AclGroup in the list.
                foreach(var group in aclGroups)
                {
                    LinkAclGroup(group.PrimaryKey);
                }

                // Unlink AclGroup that are not in the List.
                foreach (var group in AclGroupsInAclActions)
                {
                    int index = aclGroups.FindIndex(x => x.PrimaryKey == group.AclGroupId);
                    if (index < 0)
                    {
                        UnLinkAclGroup(group.AclGroupId);
                    }
                }
            }
        }

        /// <summary>
        /// Method to associate an AclGroup to the Section.
        /// </summary>
        /// <param name="aclGroupPk">An AclGroup id or primary key to link.</param>
        public bool LinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = AclGroupsInAclActions.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);

                if (index < 0)
                {
                    AclGroupsInAclActions.Add(new AclGroupsInAclActions { AclGroupId = aclGroupPk });
                }

                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }

        /// <summary>
        /// Method to dissociate an AclGroup of the Section.
        /// </summary>
        /// <param name="aclGroupPk">An AclGroup id or primary key to unlink.</param>
        public bool UnLinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = AclGroupsInAclActions.ToList().FindIndex(o => o.AclGroupId == aclGroupPk);
                AclGroupsInAclActions.RemoveAt(index);
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return false;
            }
        }

        #endregion
    }
}