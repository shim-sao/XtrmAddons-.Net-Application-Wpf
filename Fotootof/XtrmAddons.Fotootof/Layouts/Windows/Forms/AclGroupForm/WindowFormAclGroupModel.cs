using System.Linq;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.AclGroupForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window AclGroup Form Model.
    /// </summary>
    public class WindowFormAclGroupModel : WindowBaseFormModel<WindowFormAclGroup>
    {
        #region Variables

        /// <summary>
        /// Variable AclGroup.
        /// </summary>
        private AclGroupEntity aclGroup;

        /// <summary>
        /// Variable observable collection of AclActions
        /// </summary>
        private AclActionEntityCollection aclActions;

        /// <summary>
        /// Variable observable collection of Users.
        /// </summary>
        private UserEntityCollection users;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the AclGroup entity.
        /// </summary>
        public AclGroupEntity AclGroup
        {
            get => aclGroup;
            set
            {
                if (aclGroup != value)
                {
                    aclGroup = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of AclActions.
        /// </summary>
        public AclActionEntityCollection AclActions
        {
            get
            {
                if (aclActions == null)
                {
                    aclActions = new AclActionEntityCollection(true);
                }
                return aclActions;
            }
            set
            {
                if (aclActions != value)
                {
                    aclActions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Users.
        /// </summary>
        public UserEntityCollection Users
        {
            get
            {
                if (users == null)
                {
                    users = new UserEntityCollection(true);
                }
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window AclGroup Form Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowFormAclGroupModel(WindowFormAclGroup window) : base(window) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to find an AclAction asynchronous.
        /// </summary>
        /// <param name="action">The action to find.</param>
        /// <returns>The AclAction according to the action.</returns>
        public AclActionEntity AclAction_FindAction(string action)
        {
            return AclActions.ToList().Find(x => x.Action == action);
        }

        /// <summary>
        /// Method to check if an AclAction is associated to an AclGroup.
        /// </summary>
        /// <param name="aclActionId">An AclAction primary key.</param>
        /// <returns>True if the AclAction is associated otherwise false.</returns>
        public bool AclGroup_CanDoAclAction(int aclActionId)
        {
            if (AclGroup != null)
            {
                if (AclGroup.AclGroupsInAclActions.ToList().Exists(x => x.AclActionId == aclActionId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to add an AclAction associated to an AclGroup.
        /// </summary>
        public void AclGroup_AddAclAction(int aclActionId)
        {
            if (AclGroup != null && !AclGroup_CanDoAclAction(aclActionId))
            {
                AclGroup.AclGroupsInAclActions.Add(new AclGroupsInAclActions { AclActionId = aclActionId });
            }
        }

        /// <summary>
        /// Method to remove an AclAction associated to an AclGroup.
        /// </summary>
        public void AclGroup_RemoveAclAction(int aclActionId)
        {
            if (AclGroup != null && AclGroup_CanDoAclAction(aclActionId))
            {
                AclGroup.AclGroupsInAclActions.Remove(AclGroup.AclGroupsInAclActions.ToList().Find(x => x.AclActionId == aclActionId));
            }
        }

        #endregion
    }
}