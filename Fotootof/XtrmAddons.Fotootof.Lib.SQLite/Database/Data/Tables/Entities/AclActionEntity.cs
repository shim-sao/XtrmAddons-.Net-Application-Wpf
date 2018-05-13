using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
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
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable action of the item.
        /// </summary>
        [NotMapped]
        private string action = "";

        /// <summary>
        /// Variable parameters of the item.
        /// </summary>
        [NotMapped]
        private string parameters = "";

        #endregion



        #region Variables Dependencies

        /// <summary>
        /// Variable AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        private int aclGroupId = 0;

        /// <summary>
        /// Variable list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        public IEnumerable<int> aclGroupsPK = null;

        /// <summary>
        /// Variable list of AclGroup associated to the AclAction.
        /// </summary>
        [NotMapped]
        private IEnumerable<AclGroupEntity> aclGroups = null;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
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
        /// Property to access to the action of the item.
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
        /// Property to access to the parameters of the item.
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
        /// Property to access to the collection of relationship AclGroup in AclAction.
        /// </summary>
        public ObservableAclGroupsInAclActions AclGroupsInAclActions { get; set; }
            = new ObservableAclGroupsInAclActions("AclActionId");

        /// <summary>
        /// Property to access to the AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
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
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        [JsonProperty]
        public IEnumerable<int> AclGroupsPK
        {
            get
            {
                if(aclGroupsPK == null)
                {
                    aclGroupsPK = ListOfPrimaryKeys(AclGroupsInAclActions, "AclGroupId");
                }
                return aclGroupsPK;
            }

            private set
            {
                if (aclGroupsPK != value)
                {
                    aclGroupsPK = value;
                }
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the list of AclGroup associated to the AclAction.
        /// </summary>
        [NotMapped]
        public IEnumerable<AclGroupEntity> AclGroups
        {
            get
            {
                if(aclGroups == null || aclGroups.Count() != AclGroupsInAclActions?.Count)
                {
                    aclGroups = ListEntities<AclGroupEntity>(AclGroupsInAclActions);
                }

                return aclGroups;
            }

            private set
            {
                if(aclGroups != value)
                {
                    aclGroups = value;
                }
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entity Constructor.
        /// </summary>
        public AclActionEntity()
        {
            // Manage Properties on AclGroup dependencies changes.
            AclGroupsInAclActions.CollectionChanged += (s, e)
                => {
                    aclGroups = null;
                    aclGroupsPK = null;
                    NotifyPropertyChanged("AclGroups");
                    NotifyPropertyChanged("AclGroupsPK");
                };
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to add a list of associated AclGroup to the AclAction.
        /// </summary>
        /// <param name="listGroups">A list of AclGroups</param>
        // todo : write public function to append a list and a function to create a new list.
        private void AddAclGroupsDependencies(List<AclGroupEntity> listGroups)
        {
            if (listGroups != null && listGroups.Count > 0)
            {
                // Link all AclGroup in the list.
                foreach(var group in listGroups)
                {
                    LinkAclGroup(group.PrimaryKey);
                }

                // Unlink AclGroup that are not in the List.
                foreach (var group in AclGroupsInAclActions)
                {
                    int index = listGroups.FindIndex(x => x.PrimaryKey == group.AclGroupId);
                    if (index < 0)
                    {
                        UnLinkAclGroup(group.AclGroupId);
                    }
                }
            }
        }

        /// <summary>
        /// Method to add a list of associated AclGroup.
        /// </summary>
        /// <param name="aclGroupsPk">A list of AclGroups</param>
        // todo : write public function to append a list and a function to create a new list.
        private bool CreateAclGroupsDependencies(IEnumerable<int> aclGroupsPk)
        {
            // Check if List is not null.
            if (aclGroupsPk == null)
            {
                log.Debug((new ArgumentNullException(nameof(aclGroupsPk)).Output()));
                return true;
            }

            // Check if List is not empty.
            if (aclGroupsPk.Count() == 0)
            {
                log.Debug((new ArgumentOutOfRangeException(nameof(aclGroupsPk) + " is empty !").Output()));
                return true;
            }

            // Proccess of the dependencies association.
            try
            {
                IEnumerable<int> depAclGroupsPk = ListOfPrimaryKeys(AclGroupsInAclActions, "AclGroupId");
                IEnumerable<int> difference = aclGroupsPk.Except(depAclGroupsPk);
                IEnumerable<int> intersection = aclGroupsPk.Intersect(depAclGroupsPk);

                // Link all new AclGroup in the list.
                if (difference.Count() > 0)
                {
                    foreach (var pk in difference)
                    {
                        LinkAclGroup(pk);
                    }
                }

                // Unlink all old AclGroup in the list.
                if (intersection.Count() > 0)
                {
                    foreach (var pk in intersection)
                    {
                        UnLinkAclGroup(pk);
                    }
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
        /// Method to associate an AclGroup to the AclAction.
        /// </summary>
        /// <param name="aclGroupPk">An AclGroup id or primary key to link.</param>
        /// <returns>True if link process is successful otherwise false.</returns>
        public bool LinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = FindAclGroupDependencyIndex(aclGroupPk);
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
        /// Method to dissociate an AclGroup of the AclAction.
        /// </summary>
        /// <param name="aclGroupPk">An AclGroup id or primary key to unlink.</param>
        /// <returns>True if unlink process is successful otherwise false.</returns>
        public bool UnLinkAclGroup(int aclGroupPk)
        {
            try
            {
                int index = FindAclGroupDependencyIndex(aclGroupPk);
                if (index > 0)
                {
                    AclGroupsInAclActions.RemoveAt(index);
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
        /// Method to an AclGroup dependency.
        /// </summary>
        /// <param name="aclGroupPk">An AclGroup id or primary key to find.</param>
        /// <returns>The index of the dependency or -1, on error or if it is not found.</returns>
        public int FindAclGroupDependencyIndex(int aclGroupPk)
        {
            try
            {
                return AclGroupsInAclActions.ToList().FindIndex(x => x.AclGroupId == aclGroupPk);
            }
            catch (Exception e)
            {
                log.Debug(e.Output());
                return -1;
            }
        }

        #endregion
    }
}