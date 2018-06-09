using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.UserForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Window User Form Model.
    /// </summary>
    public class WindowFormUserModel<WindowBaseForm> : WindowBaseFormModel<WindowBaseForm>
    {
        #region Variables

        /// <summary>
        /// Variable user entity.
        /// </summary>
        protected UserEntity user;

        /// <summary>
        /// Variable AclGroup entities.
        /// </summary>
        protected AclGroupEntityCollection aclGroups;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the User.
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
        /// Class XtrmAddons Fotootof Server Window User Form Model Constructor.
        /// </summary>
        /// <param name="pBase"></param>
        public WindowFormUserModel(WindowBaseForm windowBase) : base(windowBase) { }

        #endregion
    }
}