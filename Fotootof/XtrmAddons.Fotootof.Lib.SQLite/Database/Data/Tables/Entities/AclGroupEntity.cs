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
        /// Variable AclAction dependencies id or primary key.
        /// </summary>
        [NotMapped]
        private IEnumerable<int> aclActionsPK = null;

        /// <summary>
        /// Variable AclAction associated to the item.
        /// </summary>
        [NotMapped]
        private IEnumerable<AclActionEntity> aclActions = null;

        #endregion



        #region Variables : Dependencies


        #endregion



        #region Variables : Dependencies

        /// <summary>
        /// Variable Section primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private int sectionId = 0;

        /// <summary>
        /// Variable primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private int userId = 0;

        /// <summary>
        /// Variable list of Sections associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<SectionEntity> sections;

        /// <summary>
        /// Variable list of Users associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<UserEntity> users;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the id or primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int AclGroupId
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
        /// Property to access to the collection of relationship AclGroups in AclActions.
        /// </summary>
        public ObservableAclGroupsInAclActions<AclActionEntity> AclGroupsInAclActions { get; set; }
            = new ObservableAclGroupsInAclActions<AclActionEntity>();

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
        public IEnumerable<int> AclActionsPK
        {
            get
            {
                if (aclActionsPK == null)
                {
                    aclActionsPK = ListOfPrimaryKeys(AclGroupsInAclActions, "AclActionId");
                }
                return ListOfPrimaryKeys(AclGroupsInAclActions, "AclActionId");
            }

            private set
            {
                if (aclActionsPK != value)
                {
                    aclActionsPK = value;
                }
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the list of AclAction associated to the item.
        /// </summary>
        [NotMapped]
        public IEnumerable<AclActionEntity> AclActions
        {
            get
            {
                if (aclActions == null || aclActions.Count() != AclGroupsInAclActions?.Count)
                {
                    aclActions = ListEntities<AclActionEntity>(AclGroupsInAclActions);
                }

                return aclActions;
            }

            private set
            {
                if (aclActions != value)
                {
                    aclActions = value;
                }
                NotifyPropertyChanged();
            }
        }

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
        public IEnumerable<int> SectionsPK
            => ListOfPrimaryKeys(SectionsInAclGroups.ToList(), "SectionId");

        /// <summary>
        /// Property to access to the list of Sections associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public List<SectionEntity> Sections
        {
            get => ListSections();
            set => sections = value;
        }

        /// <summary>
        /// Property to access to the collection of relationship Sections in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<SectionsInAclGroups> SectionsInAclGroups { get; set; }
            = new ObservableCollection<SectionsInAclGroups>();

        #endregion



        #region Properties : Dependencies User

        /// <summary>
        /// Property to access to the User primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
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
        public IEnumerable<int> UsersPK
            => ListOfPrimaryKeys(UsersInAclGroups.ToList(), "UserId");

        /// <summary>
        /// Property to access to the list of Users associated to the item.
        /// </summary>
        [NotMapped]
        public List<UserEntity> Users
        {
            get => ListUsers();
            set => users = value;
        }

        /// <summary>
        /// Property to access to the collection of relationship Users in AclGroups.
        /// </summary>
        public ObservableCollection<UsersInAclGroups> UsersInAclGroups { get; set; }
            = new ObservableCollection<UsersInAclGroups>();
        
        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entity Constructor.
        /// </summary>
        public AclGroupEntity()
        {
            AclGroupsInAclActions.CollectionChanged += (s, e) => { aclActions = null; };
            SectionsInAclGroups.CollectionChanged += (s, e) => { sections = null; };
            UsersInAclGroups.CollectionChanged += (s, e) => { users = null; };
        }

        #endregion



        #region Methods AclAction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aclActionId"></param>
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
        /// <param name="AclGroupId"></param>
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
        /// Method to get the list of associated Sections to the item.
        /// </summary>
        /// <returns>A list of associated Sections.</returns>
        private List<SectionEntity> ListSections()
        {
            if (sections == null || sections.Count != SectionsInAclGroups.Count)
            {
                sections = new List<SectionEntity>();

                if (SectionsInAclGroups != null)
                {
                    sections = ListEntities<SectionEntity>(SectionsInAclGroups);
                }
            }

            return sections;
        }

        /// <summary>
        /// Method to link Section to the AclGroup.
        /// </summary>
        /// <param name="sectionId">A Section primary key.</param>
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
        /// Method to get the list of associated Users to the item.
        /// </summary>
        /// <returns>A list of associated Users.</returns>
        private List<UserEntity> ListUsers()
        {
            if (users == null)
            {
                users = new List<UserEntity>();

                if (UsersInAclGroups != null)
                {
                    users = ListEntities<UserEntity>(UsersInAclGroups);
                }
            }

            return users;
        }

        /// <summary>
        /// Method to link User to the AclGroup.
        /// </summary>
        /// <param name="sectionId">A User primary key.</param>
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
        /// <param name="sectionId">A User primary key.</param>
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
