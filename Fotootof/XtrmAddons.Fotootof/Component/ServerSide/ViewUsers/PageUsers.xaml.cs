using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;
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
               AclGroupEntity a = UcDataGridAclGroups.SelectedItem;

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
            AppOverwork.IsBusy = true;
            //await Task.Delay(1000);

            // Paste page to User list.
            model = model ?? new PageUsersModel<PageUsers>(this)
            {
                AclGroups = new DataGridAclGroupsModel<DataGridAclGroups>(),
                Users = new DataGridUsersModel<DataGridUsers>()
            };
            UcDataGridAclGroups.Tag = this;

            LoadAclGroups();
            LoadUsers();
            DataContext = model;

            UcDataGridAclGroups.OnAdd += UcDataGridAclGroups_AclGroupAdded;
            UcDataGridAclGroups.OnChange += UcDataGridAclGroups_AclGroupChanged;
            UcDataGridAclGroups.OnCancel += UcDataGridAclGroups_AclGroupCanceled;
            UcDataGridAclGroups.OnDelete += UcDataGridAclGroups_AclGroupDeleted;
            UcDataGridAclGroups.DefaultChanged += UcDataGridAclGroups_DefaultChanged;
            UcDataGridAclGroups.SelectionChanged += (s, e) => { LoadUsers(); };

            UcDataGridUsers.OnAdd += UcDataGridUsers_UserAdded;
            UcDataGridUsers.OnChange += UcDataGridUsers_UserChanged;
            UcDataGridUsers.OnCancel += UcDataGridUsers_UserCanceled;
            UcDataGridUsers.OnDelete += UcDataGridUsers_UserDeleled;

            AppOverwork.IsBusy = false;
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

                AppLogger.Info("Loading Acl Groups list. Please wait...");
                model.AclGroups.Items = new AclGroupEntityCollection(true);
                AppLogger.InfoAndClose("Loading Acl Groups list. Done.");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Loading Acl Groups list failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Method called on AclGroup view cancel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void UcDataGridAclGroups_AclGroupCanceled(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Adding or editing AclGroup operation canceled. Please wait...");
            LoadAclGroups();
            AppLogger.InfoAndClose("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view changes event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_AclGroupAdded(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving new AclGroup informations. Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;
            model.AclGroups.Items.Add(item);
            AclGroupEntityCollection.DbInsert(new List<AclGroupEntity> { item });

            Refresh();

            AppLogger.InfoAndClose("Saving new AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_AclGroupChanged(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Saving AclGroup informations. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntity old = model.AclGroups.Items.Single(x => x.AclGroupId == newEntity.AclGroupId);
            int index = model.AclGroups.Items.IndexOf(old);
            model.AclGroups.Items[index] = newEntity;
            AclGroupEntityCollection.DbUpdateAsync(new List<AclGroupEntity> { newEntity }, new List<AclGroupEntity> { old });

            Refresh();

            AppLogger.InfoAndClose("Saving AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view delete event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e"></param>
        private void UcDataGridAclGroups_AclGroupDeleted(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Deleting AclGroup(s). Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;

            // Remove item from list.
            model.AclGroups.Items.Remove(item);

            // Delete item from database.
            AclGroupEntityCollection.DbDelete(new List<AclGroupEntity> { item });

            Refresh();

            AppLogger.InfoAndClose("Deleting AclGroup(s). Done.");
        }

        /// <summary>
        /// Method called on AclGroup default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_DefaultChanged(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Setting default Section. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntityCollection.SetDefault(newEntity);
            LoadAclGroups();

            AppLogger.InfoAndClose("Setting default Section. Done.");
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
                AppLogger.Info("Loading Users list. Please wait...");
                UserEntityCollection Users = new UserEntityCollection(UserOptionsList, false);
                Users.Load();
                model.Users.Items = Users;

                AppLogger.InfoAndClose("Loading Users list. Done.");
            }
            catch (Exception e)
            {
                AppLogger.Fatal("Loading Users list failed : " + e.Message, e, true);
            }
        }

        /// <summary>
        /// Method called on User view cancel event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataGridUsers_UserCanceled(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Adding or editing User operation canceled. Please wait...");

            LoadUsers();

            AppLogger.InfoAndClose("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridUsers_UserAdded(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Adding or editing User informations. Please wait...");

            UserEntity entity = (UserEntity)e.NewEntity;

            model.Users.Items.Add(entity);
            UserEntityCollection.DbInsert(new List<UserEntity> { entity });

            Refresh();

            AppLogger.InfoAndClose("Adding or editing User informations. Done");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridUsers_UserChanged(object sender, EntityChangesEventArgs e)
        {
            UserEntity entity = (UserEntity)e.NewEntity;

            UserEntity old = model.Users.Items.Single(x => x.PrimaryKey == entity.PrimaryKey);
            int index = model.Users.Items.IndexOf(old);
            model.Users.Items[index] = entity;
            UserEntityCollection.DbUpdate(new List<UserEntity> { entity }, new List<UserEntity> { old });

            Refresh();

            AppLogger.Warning("UcDataGridUsers_UserChanged", false);
        }

        /// <summary>
        /// Method called on User view delete event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataGridUsers_UserDeleled(object sender, EntityChangesEventArgs e)
        {
            AppLogger.Info("Deleting User(s). Please wait...");

            UserEntity item = (UserEntity)e.NewEntity;

            // Remove item from list.
            model.Users.Items.Remove(item);

            // Delete item from database.
            UserEntityCollection.DbDelete(new List<UserEntity> { item });

            Refresh();

            AppLogger.InfoAndClose("Deleting User(s). Done.");
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeSize();
            ArrangeMiddleContents();
        }

        /// <summary>
        /// Method to arrange new page size.
        /// </summary>
        private void ArrangeSize()
        {
            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            this.Width = Math.Max(fe.ActualWidth, 0);
            this.Height = Math.Max(fe.ActualHeight, 0);

            TraceSize(fe);
            TraceSize(this);
        }

        /// <summary>
        /// Method to arrange new page middle content size.
        /// </summary>
        private void ArrangeMiddleContents()
        {
            Block_MiddleContents.Width = Math.Max(this.Width, 0);
            Block_MiddleContents.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);

            TraceSize(Block_TopControls);
            TraceSize(Block_MiddleContents);

            UcDataGridAclGroups.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);
            UcDataGridUsers.Height = Math.Max(this.Height - Block_TopControls.RenderSize.Height, 0);

            TraceSize(UcDataGridAclGroups);
            TraceSize(UcDataGridUsers);
        }

        #endregion
    }
}
