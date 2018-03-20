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
    /// Class XtrmAddons Fotootof Server SQLite User entity.
    /// </summary>
    [Table("Users")]
    public partial class UserEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable list of AclGroup associated to the User.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private List<AclGroupEntity> aclGroups;

        #endregion



        #region Properties

        /// <summary>
        /// Propmerty primary key auto incremented.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }

        /// <summary>
        /// Property name of the user.
        /// </summary>
        [Column(Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Property password of the user.
        /// </summary>
        [Column(Order = 2)]
        public string Password { get; set; }

        /// <summary>
        /// Property email of the user.
        /// </summary>
        [Column(Order = 3)]
        public string Email { get; set; }

        /// <summary>
        /// Property server owner.
        /// </summary>
        [Column(Order = 4)]
        public string Server { get; set; }

        /// <summary>
        /// Variable created date.
        /// </summary>
        [Column(Order = 5)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Variable modified date.
        /// </summary>
        [Column(Order = 6)]
        public DateTime Modified { get; set; }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// Property alias to access to the primary key of the entity.
        /// </summary>
        [NotMapped]
        public override int PrimaryKey { get => UserId; set => UserId = value; }

        /// <summary>
        /// Variable AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int AclGroupId { get; set; }

        /// <summary>
        /// Property to access to the list of AclGroup associated to the User.
        /// </summary>
        [NotMapped]
        public List<AclGroupEntity> AclGroups
        {
            get => ListAclGroups();
            set => aclGroups = value;
        }

        /// <summary>
        /// Propertiy to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        public List<int> AclGroupsPK => ListOfPrimaryKeys(UsersInAclGroups.ToList(), "AclGroupId");

        /// <summary>
        /// Variables collection of relationship Users in AclGroups.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<UsersInAclGroups> UsersInAclGroups { get; set; }
            = new ObservableCollection<UsersInAclGroups>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server SQLite User entity Constructor.
        /// </summary>
        public UserEntity()
        {
            Initialize();

            UsersInAclGroups.CollectionChanged += (s, e) => { aclGroups = null; };

        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize a User entity.
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
        /// Method to get list of associated AclGroup to the User.
        /// </summary>
        private List<AclGroupEntity> ListAclGroups()
        {
            if (aclGroups == null)
            {
                aclGroups = new List<AclGroupEntity>();

                if (UsersInAclGroups != null)
                {
                    aclGroups = ListEntities<AclGroupEntity>(UsersInAclGroups);
                }
            }

            return aclGroups;
        }

        /// <summary>
        /// Method to associate a AclGroup to the User.
        /// </summary>
        /// <param name="AclGroupId">An AclGroup primary key.</param>
        public void LinkAclGroup(int aclGroupId)
        {
            try
            {
                int index = UsersInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupId);

                if(index < 0)
                {
                    UsersInAclGroups.Add(new UsersInAclGroups { AclGroupId = aclGroupId });
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to unlink a AclGroup of the User.
        /// </summary>
        /// <param name="AclGroupId">An AclGroup primary key.</param>
        public void UnLinkAclGroup(int aclGroupId)
        {
            try
            {
                int index = UsersInAclGroups.ToList().FindIndex(o => o.AclGroupId == aclGroupId);
                UsersInAclGroups.RemoveAt(index);
            }
            catch { }
        }
        
        #endregion
    }
}