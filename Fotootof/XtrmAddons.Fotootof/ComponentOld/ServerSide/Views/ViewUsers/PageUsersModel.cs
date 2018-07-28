using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Models.DataGrids;
using XtrmAddons.Fotootof.ComponentOld.ServerSide.Controls.DataGrids;
using XtrmAddons.Fotootof.Layouts.DataGrids.AclGroups;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;

namespace XtrmAddons.Fotootof.ComponentOld.ServerSide.Views.ViewUsers
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
        private DataGridAclGroupsModel<DataGridAclGroups> aclGroups = new DataGridAclGroupsModel<DataGridAclGroups>();
        
        /// <summary>
        /// Variable data grid collection of Users.
        /// </summary>
        private DataGridUsersModel<DataGridUsers> users = new DataGridUsersModel<DataGridUsers>();

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
        public PageUsersModel(PageUsers page) : base(page)
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
            MessageBase.IsBusy = true;
            log.Info("Loading Acl Groups list : Start. Please wait...");

            try
            {
                AclGroups.Items = new AclGroupEntityCollection(true);
            }
            catch (Exception e)
            {
                string message = "Loading Acl Groups list failed !";
                log.Fatal(message, e);
                MessageBase.Fatal(e, message);
            }
            finally
            {
                log.Info("Loading Acl Groups list : End.");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to load the list of users from the database.
        /// </summary>
        public void LoadUsers()
        {
            MessageBase.IsBusy = true;
            log.Warn(MessageBase.BusyContent = "Loading Users list. Please wait...");

            try
            {
                UserOptionsList op = new UserOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.UsersInAclGroups }
                };

                if (((DataGridAclGroupsServer)(OwnerBase.FindName("UcDataGridAclGroupsServerName")))?.SelectedItem is AclGroupEntity a && a.PrimaryKey > 0)
                {
                    op.IncludeAclGroupKeys = new List<int>() { a.PrimaryKey };
                }

                UserEntityCollection users = new UserEntityCollection(op, false);
                users.Load();
                Users.Items = users;

                log.Info(MessageBase.BusyContent = "Loading Users list. Done.");
            }
            catch (Exception e)
            {
                MessageBase.BusyContent = "Loading Users list. failed !";
                log.Fatal(MessageBase.BusyContent, e);
                MessageBase.Fatal(e, (string)MessageBase.BusyContent);
            }
            finally
            {
                log.Warn(MessageBase.BusyContent = "Loading Users list. Done.");
                MessageBase.IsBusy = false;
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
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
