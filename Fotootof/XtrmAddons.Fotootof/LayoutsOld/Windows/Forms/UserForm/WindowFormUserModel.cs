using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;

namespace XtrmAddons.Fotootof.LayoutsOld.Windows.Forms.UserForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Window User Form Model.
    /// </summary>
    public class WindowFormUserModel : WindowBaseFormModel<WindowBaseForm>
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the <see cref="UserEntity"/>.
        /// </summary>
        protected UserEntity user;

        /// <summary>
        /// Variable to store the old <see cref="UserEntity"/>.
        /// </summary>
        protected UserEntity oldUser;

        /// <summary>
        /// Variable to store the collection of AclGroup entities <see cref="AclGroupEntityCollection"/>.
        /// </summary>
        protected AclGroupEntityCollection aclGroups;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the User <see cref="UserEntity"/>.
        /// </summary>
        public UserEntity User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the old User <see cref="UserEntity"/>.
        /// </summary>
        public UserEntity OldUser
        {
            get { return oldUser; }
            set
            {
                oldUser = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the AclGroup entities.
        /// </summary>
        public AclGroupEntityCollection AclGroups
        {
            get { return aclGroups; }
            set
            {
                aclGroups = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Window User Form Model Constructor.
        /// </summary>
        /// <param name="owner"></param>
        public WindowFormUserModel(WindowBaseForm owner) : base(owner) { }

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Window User Form Model Constructor.
        /// </summary>
        /// <param name="pBase"></param>
        public WindowFormUserModel(WindowBaseForm owner, int UserId) : this(owner)
        {
            LoadUser(UserId); ;

            // Set model entity to dependencies converters.
            IsAclGroupInUser.Entity = User;

            // Assign list of AclGroup to the model.
            AclGroups = new AclGroupEntityCollection(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        public void LoadUser(int UserId)
        {
            User = null;
            if (UserId > 0)
            {
                var op = new UserOptionsSelect
                {
                    PrimaryKey = UserId,
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                User = Db.Users.SingleOrNull(op);
            }

            User = User ?? new UserEntity();
            OldUser = User?.CloneJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        public bool IsUniqueEmail(string email)
        {
            if(email.IsNullOrWhiteSpace())
            {
                return false;
            }

            var op = new UserOptionsSelect { Email = email };
            var u = Db.Users.SingleOrNull(op);

            if(u != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        public bool IsUniqueName(string name)
        {
            if(name.IsNullOrWhiteSpace())
            {
                return false;
            }

            var op = new UserOptionsSelect { Name = name };
            var u = Db.Users.SingleOrNull(op);

            if(u != null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}