using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Fotootof.SQLite.EntityManager.Data.Base;
using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies;
using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies.Observables;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.SQLite.EntityManager.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite AclGroup table entity.
    /// </summary>
    [Serializable]
    [Table("AclGroups")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AclGroupEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the item.
        /// </summary>
        [NotMapped]
        private string name = "";

        /// <summary>
        /// Variable alias of the item.
        /// </summary>
        [NotMapped]
        private string alias = "";

        /// <summary>
        /// Variable parent id of the item.
        /// </summary>
        [NotMapped]
        private int parentId = 0;

        /// <summary>
        /// Variable is default of the item.
        /// </summary>
        [NotMapped]
        private bool isDefault = false;

        /// <summary>
        /// Variable ordering of the item.
        /// </summary>
        [NotMapped]
        private int ordering = 0;

        /// <summary>
        /// Variable comment.
        /// </summary>
        [NotMapped]
        private string comment = "";

        #endregion



        #region Variables Dependencies AclAction

        /// <summary>
        /// Variable AclAction id (required for entity dependency).
        /// </summary>
        [NotMapped]
        private int aclActionId = 0;

        /// <summary>
        /// Variable Section primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        private int sectionId = 0;

        /// <summary>
        /// Variable primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        private int userId = 0;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int AclGroupId
        {
            get => PrimaryKey;
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
        /// Property to access to the name of the item.
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty]
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the alias of the item.
        /// </summary>
        [Column(Order = 2)]
        [JsonProperty]
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != alias)
                {
                    alias = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the parent id or primary key of the item.
        /// </summary>
        [Column(Order = 3)]
        [JsonProperty]
        public int ParentId
        {
            get { return parentId; }
            set
            {
                if (value != parentId)
                {
                    parentId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the is default of the item.
        /// </summary>
        [Column(Order = 4)]
        [JsonProperty]
        public bool IsDefault
        {
            get { return isDefault; }
            set
            {
                if (value != isDefault)
                {
                    isDefault = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the ordering of the item.
        /// </summary>
        [Column(Order = 5)]
        [JsonProperty]
        public int Ordering
        {
            get { return ordering; }
            set
            {
                if (value != ordering)
                {
                    ordering = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the created date.
        /// </summary>
        [Column(Order = 6)]
        [JsonProperty]
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



        #region Properties :  Dependencies AclAction

        /// <summary>
        /// Property to access to the AclAction id (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int AclActionId
        {
            get { return aclActionId; }
            set
            {
                if (value != aclActionId)
                {
                    aclActionId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the list of AclAction dependencies primary key.
        /// </summary>
        [NotMapped]
        [JsonProperty]
        public ObservableCollection<int> AclActionsPKeys
        {
            get
            {
                AclGroupsInAclActions.Populate();
                return AclGroupsInAclActions.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of AclAction associated to the item.
        /// </summary>
        [NotMapped]
        public ObservableCollection<AclActionEntity> AclActions
        {
            get
            {
                AclGroupsInAclActions.Populate();
                return AclGroupsInAclActions.DepReferences;
            }
            //set
            //{
            //    if (value != AclGroupsInAclActions.DepReferences)
            //    {
            //        AclGroupsInAclActions.DepReferences.ClearAndAdd(value);
            //        NotifyPropertyChanged();
            //    }
            //}
        }

        /// <summary>
        /// Property to access to the collection of relationship AclGroups in AclActions.
        /// </summary>
        public ObservableAclGroupsInAclActions<AclGroupEntity, AclActionEntity> AclGroupsInAclActions { get; set; }
            = new ObservableAclGroupsInAclActions<AclGroupEntity, AclActionEntity>();

        #endregion



        #region Properties : Dependencies Section

        /// <summary>
        /// Property to access to the Section primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int SectionId
        {
            get { return sectionId; }
            set
            {
                if (value != sectionId)
                {
                    sectionId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertiy to access to the list of Section dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> SectionsPKeys
        {
            get
            {
                SectionsInAclGroups.Populate();
                return SectionsInAclGroups.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Sections associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public ObservableCollection<SectionEntity> Sections
        {
            get
            {
                SectionsInAclGroups.Populate();
                return SectionsInAclGroups.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Sections in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableSectionsInAclGroups<AclActionEntity, SectionEntity> SectionsInAclGroups { get; set; }
            = new ObservableSectionsInAclGroups<AclActionEntity, SectionEntity>();

        #endregion



        #region Properties : Dependencies User

        /// <summary>
        /// Property to access to the User primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        public int UserId
        {
            get { return userId; }
            set
            {
                if (value != userId)
                {
                    userId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertiy to access to the list of User dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> UsersPKeys
        {
            get
            {
                UsersInAclGroups.Populate();
                return UsersInAclGroups.DepPKeys;
            }
        }

        /// <summary>
        /// Property to access to the list of Users associated to the item.
        /// </summary>
        [NotMapped]
        public ObservableCollection<UserEntity> Users
        {
            get
            {
                UsersInAclGroups.Populate();
                return UsersInAclGroups.DepReferences;
            }
        }

        /// <summary>
        /// Property to access to the collection of relationship Users in AclGroups.
        /// </summary>
        public ObservableUsersInAclGroups<AclGroupEntity, UserEntity> UsersInAclGroups { get; set; }
            = new ObservableUsersInAclGroups<AclGroupEntity, UserEntity>();
        
        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entity Constructor.
        /// </summary>
        public AclGroupEntity() { }

        #endregion



        #region Methods AclAction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aclActionId"></param>
        [System.Obsolete("Use => AclActionsPKeys.Add(AclActionPk);")]
        public void LinkAclAction(int aclActionId)
        {
            try
            {
                int index = AclGroupsInAclActions.ToList().FindIndex(o => o.AclActionId == aclActionId);

                if (index < 0)
                {
                    AclGroupsInAclActions.Add(new AclGroupsInAclActions { AclActionId = aclActionId });
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aclActionId"></param>
        [System.Obsolete("Use => AclActionsPKeys.Remove(AclActionPk);")]
        public void UnLinkAclAction(int aclActionId)
        {
            try
            {
                int index = AclGroupsInAclActions.ToList().FindIndex(o => o.AclActionId == aclActionId);
                AclGroupsInAclActions.RemoveAt(index);
            }
            catch { }
        }


        #endregion



        #region Methods Section

        /// <summary>
        /// Method to link Section to the AclGroup.
        /// </summary>
        /// <param name="sectionId">A Section primary key.</param>
        [System.Obsolete("Use => SectionsPKs.Add(SectionPk);")]
        public void LinkSection(int sectionId)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.SectionId == sectionId);

                if (index < 0)
                {
                    SectionsInAclGroups.Add(new SectionsInAclGroups { SectionId = sectionId });
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to unlink Section of the AclGroup.
        /// </summary>
        /// <param name="sectionId">A Section primary key.</param>
        [System.Obsolete("Use => SectionsPKs.Remove(SectionPk);")]
        public void UnLinkSection(int sectionId)
        {
            try
            {
                int index = SectionsInAclGroups.ToList().FindIndex(o => o.SectionId == sectionId);
                SectionsInAclGroups.RemoveAt(index);
            }
            catch { }
        }

        #endregion



        #region Methods User

        /// <summary>
        /// Method to link User to the AclGroup.
        /// </summary>
        /// <param name="userId">A User primary key.</param>
        [System.Obsolete("Use => UsersPKeys.Add(UserPk);")]
        public void LinkUser(int userId)
        {
            try
            {
                int index = UsersInAclGroups.ToList().FindIndex(o => o.UserId == userId);

                if (index < 0)
                {
                    UsersInAclGroups.Add(new UsersInAclGroups { UserId = userId });
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to unlink User of the AclGroup.
        /// </summary>
        /// <param name="userId">A User primary key.</param>
        [System.Obsolete("Use => UsersPKeys.Remove(UserPk);")]
        public void UnLinkUser(int userId)
        {
            try
            {
                int index = UsersInAclGroups.ToList().FindIndex(o => o.UserId == userId);
                UsersInAclGroups.RemoveAt(index);
            }
            catch { }
        }

        #endregion
    }
}
