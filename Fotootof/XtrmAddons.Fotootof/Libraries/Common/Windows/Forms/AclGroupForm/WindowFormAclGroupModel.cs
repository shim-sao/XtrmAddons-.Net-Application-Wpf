using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AclGroupForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window AclGroup Form Model.
    /// </summary>
    public class WindowFormAclGroupModel<WindowBaseForm> : WindowBaseFormModel<WindowBaseForm>
    {
        #region Variables

        /// <summary>
        /// Variable AclGroup.
        /// </summary>
        private AclGroupEntity aclGroup;

        /// <summary>
        /// Variable observable collection of AclActions
        /// </summary>
        private ObservableCollection<AclActionEntity> aclActions;

        /// <summary>
        /// Variable observable collection of Users.
        /// </summary>
        private ObservableCollection<UserEntity> users;

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
                aclGroup = value;
                RaisePropertyChanged("AclGroup");
            }
        }

        /// <summary>
        /// Property to access to the observable collection of AclActions.
        /// </summary>
        public ObservableCollection<AclActionEntity> AclActions
        {
            get => aclActions;
            set
            {
                aclActions = value;
                RaisePropertyChanged("AclActions");
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Users.
        /// </summary>
        public ObservableCollection<UserEntity> Users
        {
            get => users;
            set
            {
                users = value;
                RaisePropertyChanged("Users");
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window AclGroup Form Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowFormAclGroupModel(WindowBaseForm windowBase) : base(windowBase) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to find an AclAction asynchronous.
        /// </summary>
        /// <param name="action">The action to find.</param>
        /// <returns>The AclAction according to the action.</returns>
        public async Task<AclActionEntity> AclAction_FindActionAsync(string action)
        {
            if(AclActions == null)
            {
                AclActions = await MainWindow.Database.AclActions.ListAsync();
            }

            return new List<AclActionEntity>(AclActions).Find(x => x.Action == action);
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
                if ((new List<AclGroupsInAclActions>(AclGroup.AclGroupsInAclActions).Exists(x => x.AclActionId == aclActionId)))
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
                AclGroup.AclGroupsInAclActions.Remove(new List<AclGroupsInAclActions>(AclGroup.AclGroupsInAclActions).Find(x => x.AclActionId == aclActionId));
            }
        }

        #endregion
    }
}