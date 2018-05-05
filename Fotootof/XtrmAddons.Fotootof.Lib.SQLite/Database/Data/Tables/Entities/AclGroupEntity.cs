﻿using Newtonsoft.Json;
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
    /// Class XtrmAddons Fotootof Libraries SQLite AclGroup table entity.
    /// </summary>
    [Table("AclGroups")]
    public partial class AclGroupEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of AclAction associated to the item.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AclActionEntity> aclActions;

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
        /// Property primary key auto incremented.
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
        /// Property name of the item.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 2)]
        public string Alias { get; set; }

        /// <summary>
        /// Property name of the item.
        /// </summary>
        [Column(Order = 3)]
        public int ParentId { get; set; }

        /// <summary>
        /// Property is default of the item.
        /// </summary>
        [Column(Order = 4)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Property ordering of the item.
        /// </summary>
        [Column(Order = 5)]
        public int Ordering { get; set; }

        /// <summary>
        /// Property created date.
        /// </summary>
        [Column(Order = 6)]
        public string Comment { get; set; }

        #endregion

        

        #region Properties : Dependencies

        /// <summary>
        /// Property to access to the AclAction id (required for entity dependency).
        /// </summary>
        [NotMapped]
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
        /// Property to access to the Section primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int SectionId { get; set; }

        /// <summary>
        /// Property to access to the User primary key (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int UserId { get; set; }

        /// <summary>
        /// Property to access to the list of AclAction associated to the item.
        /// </summary>
        [NotMapped]
        public List<AclActionEntity> AclActions
        {
            get => ListAclActions();
            set => aclActions = value;
        }

        /// <summary>
        /// Propertiy to access to the list of AclAction dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AclActionsPK => ListOfPrimaryKeys(AclGroupsInAclActions.ToList(), "AclActionId");

        /// <summary>
        /// Property to access to the list of Sections associated to the item.
        /// </summary>
        [NotMapped]
        public List<SectionEntity> Sections
        {
            get => ListSections();
            set => sections = value;
        }

        /// <summary>
        /// Propertiy to access to the list of Section dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> SectionsPK => ListOfPrimaryKeys(SectionsInAclGroups.ToList(), "SectionId");

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
        /// Propertiy to access to the list of User dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> UsersPK => ListOfPrimaryKeys(UsersInAclGroups.ToList(), "UserId");

        /// <summary>
        /// Property collection of relationship AclGroups in AclActions.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<AclGroupsInAclActions> AclGroupsInAclActions { get; set; }
            = new ObservableCollection<AclGroupsInAclActions>();

        /// <summary>
        /// Property collection of relationship Sections in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<SectionsInAclGroups> SectionsInAclGroups { get; set; }
            = new ObservableCollection<SectionsInAclGroups>();

        /// <summary>
        /// Property collection of relationship Users in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<UsersInAclGroups> UsersInAclGroups { get; set; }
            = new ObservableCollection<UsersInAclGroups>();
        
        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite AclAction Entity Constructor.
        /// </summary>
        public AclGroupEntity()
        {
            Initialize();

            AclGroupsInAclActions.CollectionChanged += (s, e) => { aclActions = null; };
            SectionsInAclGroups.CollectionChanged += (s, e) => { sections = null; };
            UsersInAclGroups.CollectionChanged += (s, e) => { users = null; };
        }

        #endregion

        

        #region Methods

        /// <summary>
        /// 
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
        /// Method to get the list of associated AclAction to the item.
        /// </summary>
        /// <returns></returns>
        private List<AclActionEntity> ListAclActions()
        {
            if (aclActions == null || aclActions.Count == 0)
            {
                aclActions = new List<AclActionEntity>();

                if (AclGroupsInAclActions != null)
                {
                    aclActions = ListEntities<AclActionEntity>(AclGroupsInAclActions);
                }
            }

            return aclActions;
        }

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
    }
}
