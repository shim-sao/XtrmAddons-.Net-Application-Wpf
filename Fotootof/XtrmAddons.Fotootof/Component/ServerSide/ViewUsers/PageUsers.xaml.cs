using System;
using System.Collections.Generic;
using System.Linq;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewUsers
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Pages Users View Users List.
    /// </summary>
    public partial class PageUsers : PageBase
    {
        #region Variable

        /// <summary>
        /// 
        /// </summary>
        private PageUsersModel<PageUsers> model;

        #endregion



        #region Properties

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public PageUsersModel<PageUsers> Model
        {
            get => model;
            private set { model = value; }

        }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public UserOptionsList UserOptionsList
        {
            get
            {
                AclGroupEntity a = AclGroupsDataGrid.SelectedItem;

                UserOptionsList op = new UserOptionsList
                {
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                if (a != null)
                {
                    op.IncludeAclGroupKeys = new List<int>() { a.PrimaryKey };
                }

                return op;
            }
        }

        #endregion



        #region constructor

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Views Users Page constructor.
        /// </summary>
        public PageUsers()
        {
            InitializeComponent();
            AfterInitializedComponent();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContentAsync()
        {
            Overwork.IsBusy = true;
            //await Task.Delay(1000);

            // Paste page to User list.
            model = model ?? new PageUsersModel<PageUsers>(this);
            AclGroupsDataGrid.Tag = this;

            LoadAclGroups();
            LoadUsers();
            DataContext = model;

            AclGroupsDataGrid.OnAdd += AclGroupsDataGrid_AclGroupAdded;
            AclGroupsDataGrid.OnChange += AclGroupsDataGrid_AclGroupChanged;
            AclGroupsDataGrid.OnCancel += AclGroupsDataGrid_AclGroupCanceled;
            AclGroupsDataGrid.OnDelete += AclGroupsDataGrid_AclGroupDeleted;
            AclGroupsDataGrid.DefaultChanged += AclGroupsDataGrid_DefaultChanged;
            AclGroupsDataGrid.SelectionChanged += (s, e) => { LoadUsers(); };

            UsersListView.OnAdd += UsersListView_UserAdded;
            UsersListView.OnChange += UsersListView_UserChanged;
            UsersListView.OnCancel += UsersListView_UserCanceled;
            UsersListView.OnDelete += UsersListView_UserDeleled;

            Overwork.IsBusy = false;
        }

        #endregion



        #region Methods : AclGroup

        /// <summary>
        /// Method to load the list of AclGroup from database.
        /// </summary>
        private void Refresh()
        {
            LoadAclGroups();
            LoadUsers();
        }

        /// <summary>
        /// Method to load the list of AclGroup from database.
        /// </summary>
        private void LoadAclGroups()
        {
            try
            {

                Logger.Info("Loading Acl Groups list. Please wait...");
                model.AclGroups = new AclGroupEntityCollection(true);
                Logger.Info("Loading Acl Groups list. Done.");
            }
            catch (Exception e)
            {
                Logger.Fatal("Loading Acl Groups list failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Method called on AclGroup view cancel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void AclGroupsDataGrid_AclGroupCanceled(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Adding or editing AclGroup operation canceled. Please wait...");

            LoadAclGroups();

            Logger.Info("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view changes event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AclGroupsDataGrid_AclGroupAdded(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Saving new AclGroup informations. Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;
            model.AclGroups.Add(item);
            AclGroupEntityCollection.DbInsert(new List<AclGroupEntity> { item });

            Refresh();

            Logger.Info("Saving new AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AclGroupsDataGrid_AclGroupChanged(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Saving AclGroup informations. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntity old = model.AclGroups.Single(x => x.AclGroupId == newEntity.AclGroupId);
            int index = model.AclGroups.IndexOf(old);
            model.AclGroups[index] = newEntity;
            AclGroupEntityCollection.DbUpdateAsync(new List<AclGroupEntity> { newEntity }, new List<AclGroupEntity> { old });

            Refresh();

            Logger.Info("Saving AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view delete event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e"></param>
        private void AclGroupsDataGrid_AclGroupDeleted(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Deleting AclGroup(s). Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;

            // Remove item from list.
            model.AclGroups.Remove(item);

            // Delete item from database.
            AclGroupEntityCollection.DbDelete(new List<AclGroupEntity> { item });

            Refresh();

            Logger.Info("Deleting AclGroup(s). Done.");
        }

        /// <summary>
        /// Method called on AclGroup default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AclGroupsDataGrid_DefaultChanged(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Setting default Section. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntityCollection.SetDefault(newEntity);
            LoadAclGroups();

            Logger.Info("Setting default Section. Done.");
        }

        #endregion



        #region Methods : User

        /// <summary>
        /// Method to load users list.
        /// </summary>
        private void LoadUsers()
        {
            try
            {
                Logger.Info("Loading Users list. Please wait...");
                UserEntityCollection Users = new UserEntityCollection(UserOptionsList, false);
                Users.Load();

                model.Users = Users;
                Logger.Info("Loading Users list. Done.");
            }
            catch (Exception e)
            {
                Logger.Fatal("Loading Users list failed : " + e.Message, e, true);
            }
        }

        /// <summary>
        /// Method called on User view cancel event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersListView_UserCanceled(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Adding or editing User operation canceled. Please wait...");

            LoadUsers();

            Logger.Info("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UsersListView_UserAdded(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Adding or editing User informations. Please wait...");

            UserEntity entity = (UserEntity)e.NewEntity;

            model.Users.Add(entity);
            UserEntityCollection.DbInsert(new List<UserEntity> { entity });

            Refresh();

            Logger.Info("Adding or editing User informations. Done");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UsersListView_UserChanged(object sender, EntityChangesEventArgs e)
        {
            UserEntity entity = (UserEntity)e.NewEntity;

            UserEntity old = model.Users.Single(x => x.PrimaryKey == entity.PrimaryKey);
            int index = model.Users.IndexOf(old);
            model.Users[index] = entity;
            UserEntityCollection.DbUpdate(new List<UserEntity> { entity }, new List<UserEntity> { old });

            Refresh();

            Logger.Warning("UsersListView_UserChanged", false);
        }

        /// <summary>
        /// Method called on User view delete event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersListView_UserDeleled(object sender, EntityChangesEventArgs e)
        {
            Logger.Info("Deleting User(s). Please wait...");

            UserEntity item = (UserEntity)e.NewEntity;

            // Remove item from list.
            model.Users.Remove(item);

            // Delete item from database.
            UserEntityCollection.DbDelete(new List<UserEntity> { item });

            Refresh();

            Logger.Info("Deleting User(s). Done.");
        }

        #endregion
    }
}
