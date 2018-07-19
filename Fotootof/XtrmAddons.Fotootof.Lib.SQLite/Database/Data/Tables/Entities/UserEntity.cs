using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies.Observables;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server SQLite User entity.
    /// </summary>
    [Serializable]
    [Table("Users")]
    [JsonObject(MemberSerialization.OptIn, Title = "User")]
    [XmlType(TypeName = "User")]
    public partial class UserEntity : EntityBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable name of the User.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private string name = "";

        /// <summary>
        /// Variable password of the User.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private string password = "";

        /// <summary>
        /// Variable email of the User.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private string email = "";

        /// <summary>
        /// Variable server owner.
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private string server = "";

        /// <summary>
        /// Variable date of creation of the User
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private DateTime created = DateTime.Now;

        /// <summary>
        /// Variable date of modification of the User
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private DateTime modified = DateTime.Now;

        #endregion



        #region Variables Dependencies

        /// <summary>
        /// Variable AclGroup id (required for entity dependency).
        /// </summary>
        [NotMapped]
        [XmlIgnore]
        private int aclGroupId = 0;

        #endregion



        #region Properties

        /// <summary>
        /// <para>Propmerty primary key auto incremented.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [XmlIgnore]
        public int UserId
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

        /// <summary>
        /// <para>Property to access to the name of the user.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 1)]
        [JsonProperty(PropertyName = "Name")]
        [XmlAttribute(DataType = "string", AttributeName = "Name")]
        public string Name
        {
            get => name;
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
        /// <para>Property to access to the password of the user.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 2)]
        public string Password
        {
            get => password;
            set
            {
                if (value != password)
                {
                    password = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the email of the user.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 3)]
        public string Email
        {
            get => email;
            set
            {
                if (value != email)
                {
                    email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the server owner.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 4)]
        public string Server
        {
            get => server;
            set
            {
                if (value != server)
                {
                    server = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the date of creation of the User.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 5)]
        public DateTime Created
        {
            get => created;
            set
            {
                if (value != created)
                {
                    created = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the date of modification of the User.</para>
        /// <para>Notify on property changed.</para>
        /// </summary>
        [Column(Order = 6)]
        public DateTime Modified
        {
            get => modified;
            set
            {
                if (value != modified)
                {
                    modified = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Properties : Dependencies

        /// <summary>
        /// <para>Property to access to the AclGroup id (required for entity dependency).</para>
        /// <para>Notify on property changes.</para>
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
        /// Property to access to the list of AclGroup dependencies primary key.
        /// </summary>
        [NotMapped]
        public ObservableCollection<int> AclGroupsPKeys
        {
            get
            {
                //UsersInAclGroups.Populate();
                return UsersInAclGroups.DepPKeys;
            }
        }

        /// <summary>
        /// <para>Property to access to the list of AclGroup associated to the User.</para>
        /// <para>Notify on property changes.</para>
        /// </summary>
        [NotMapped]
        public ObservableCollection<AclGroupEntity> AclGroups
        {
            get
            {
                UsersInAclGroups.Populate();
                return UsersInAclGroups.DepReferences;
            }
        }

        /// <summary>
        /// <para>Property to access to the collection of relationship Users in AclGroups.</para>
        /// <para>Notify on property changes.</para>
        /// </summary>
        public ObservableUsersInAclGroups<UserEntity, AclGroupEntity> UsersInAclGroups { get; set; }
            = new ObservableUsersInAclGroups<UserEntity, AclGroupEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server SQLite User entity Constructor.
        /// </summary>
        public UserEntity() { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to associate a AclGroup to the User.
        /// </summary>
        /// <param name="AclGroupId">An AclGroup primary key.</param>
        [System.Obsolete("Use dependency references.")]
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
        [System.Obsolete("Use dependency references.")]
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