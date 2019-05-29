using Fotootof.Collections.Entities;
using Fotootof.Components.Server.Users.Layouts;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fotootof.Components.Server.Users
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Models List Users.
    /// </summary>
    public class PageUsersModel : ComponentModel<PageUsersLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable data grid collection of AclGroups.
        /// </summary>
        private DataGridAclGroupsModel<DataGridAclGroupsControl> aclGroups = new DataGridAclGroupsModel<DataGridAclGroupsControl>();
        
        /// <summary>
        /// Variable data grid collection of Users.
        /// </summary>
        private DataGridUsersModel<DataGridUsersControl> users = new DataGridUsersModel<DataGridUsersControl>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the data grid collection of AclGroups.
        /// </summary>
        public DataGridAclGroupsModel<DataGridAclGroupsControl> AclGroups
        {
            get { return aclGroups; }
            set
            {
                if (aclGroups != value)
                {
                    aclGroups = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the data grid collection of Users.
        /// </summary>
        public DataGridUsersModel<DataGridUsersControl> Users
        {
            get { return users; }
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
        /// Class XtrmAddons Fotootof Component Server Models List Users Constructor.
        /// </summary>
        /// <param name="controlView">The page associated to the model.</param>
        public PageUsersModel(PageUsersLayout controlView) : base(controlView)
        {
            LoadAll();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to load all required page data from database.
        /// </summary>
        public void LoadAll()
        {
            LoadAclGroups();
            LoadUsers();
        }

        /// <summary>
        /// Method to load the list of AclGroups from the database.
        /// </summary>
        public void LoadAclGroups()
        {
            MessageBoxs.IsBusy = true;
            log.Info("Loading Acl Groups list : Start. Please wait...");

            try
            {
                AclGroups.Items = new AclGroupEntityCollection(true);
            }
            catch (Exception e)
            {
                string message = "Loading Acl Groups list failed !";
                log.Fatal(message, e);
                MessageBoxs.Fatal(e, message);
            }
            finally
            {
                log.Info("Loading Acl Groups list : End.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to load the list of users from the database.
        /// </summary>
        public void LoadUsers()
        {
            MessageBoxs.IsBusy = true;
            log.Warn(MessageBoxs.BusyContent = "Loading Users list. Please wait...");

            try
            {
                UserOptionsList op = new UserOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.UsersInAclGroups }
                };

                if (((DataGridAclGroupsLayout)(ControlView.FindName("UcDataGridAclGroupsServerName")))?.SelectedItem is AclGroupEntity a && a.PrimaryKey > 0)
                {
                    op.IncludeAclGroupKeys = new List<int>() { a.PrimaryKey };
                }

                UserEntityCollection users = new UserEntityCollection(op, false);
                users.Load();
                Users.Items = users;

                log.Info(MessageBoxs.BusyContent = "Loading Users list. Done.");
            }
            catch (Exception e)
            {
                MessageBoxs.BusyContent = "Loading Users list. failed !";
                log.Fatal(MessageBoxs.BusyContent, e);
                MessageBoxs.Fatal(e, (string)MessageBoxs.BusyContent);
            }
            finally
            {
                log.Warn(MessageBoxs.BusyContent = "Loading Users list. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to delete a user from the list and the database.
        /// </summary>
        /// <param name="item">The User to remove from database.</param>
        public void DeleleUser(UserEntity item)
        {
            // Remove item from the database.
            UserEntityCollection.DbDelete(new List<UserEntity> { item });

            // Remove item from the list.
            Users.Items.Remove(item);

            //Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddUserNew(UserEntity item)
        {
            Users.Items.Add(item);
            UserEntityCollection.DbInsert(new List<UserEntity> { item });
            LoadAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void SaveUserChanges(UserEntity item)
        {
            UserEntity old = Users.Items.Single(x => x.PrimaryKey == item.PrimaryKey);
            int index = Users.Items.IndexOf(old);

            UserEntityCollection.DbUpdate(new List<UserEntity> { item }, new List<UserEntity> { old });;
            Users.Items[index] = item;

            //Refresh();
        }

        #endregion
    }
}
