using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewUsers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Models List Users.
    /// </summary>
    public class PageUsersModel : PageBaseModel<PageUsers>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable data grid collection of AclGroups.
        /// </summary>
        private DataGridAclGroupsModel<DataGridAclGroups> aclGroups;
        
        /// <summary>
        /// Variable data grid collection of Users.
        /// </summary>
        private DataGridUsersModel<DataGridUsers> users;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the data grid  collection of AclGroups.
        /// </summary>
        public DataGridAclGroupsModel<DataGridAclGroups> AclGroups
        {
            get { return aclGroups; }
            set
            {
                if (aclGroups == value)
                {
                    aclGroups = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the data grid collection of Users.
        /// </summary>
        public DataGridUsersModel<DataGridUsers> Users
        {
            get { return users; }
            set
            {
                if (users == value)
                {
                    users = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Models List Users Constructor.
        /// </summary>
        /// <param name="page">The page associated to the model.</param>
        public PageUsersModel(PageUsers page) : base(page) { }

        #endregion
    }
}
