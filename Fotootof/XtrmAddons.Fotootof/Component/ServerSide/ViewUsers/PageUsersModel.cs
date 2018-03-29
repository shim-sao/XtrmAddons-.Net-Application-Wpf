using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewUsers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Models List Users.
    /// </summary>
    public class PageUsersModel<PageUsers> : PageBaseModel<PageUsers>
    {
        #region Variables

        /// <summary>
        /// Variable observable collection of AclGroups.
        /// </summary>
        private DataGridAclGroupsModel<DataGridAclGroups> aclGroups;
        
        /// <summary>
        /// Variable observable collection of Users.
        /// </summary>
        private ObservableCollection<UserEntity> users;

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the observable collection of AclGroups.
        /// </summary>
        public DataGridAclGroupsModel<DataGridAclGroups> AclGroups
        {
            get { return aclGroups; }
            set
            {
                aclGroups = value;
                RaisePropertyChanged("AclGroups");
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Users.
        /// </summary>
        public ObservableCollection<UserEntity> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Users Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public PageUsersModel(PageUsers pageBase) : base(pageBase) { }

        #endregion
    }
}
