using Fotootof.Collections.Entities;
using Fotootof.Components.Server.Users.Layouts;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Event;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Users
{
    /// <summary>
    /// Class Server Components Users Layout.
    /// </summary>
    public partial class PageUsersLayout : ComponentView
    {
        #region Variable

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the model <see cref="PageUsersModel"/>.
        /// </summary>
        public PageUsersModel Model { get; private set; }

        #endregion



        #region constructor

        /// <summary>
        /// Class XtrmAddons PhotoAlbum Server Views Users Page constructor.
        /// </summary>
        public PageUsersLayout()
        {
            MessageBoxs.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageWaiting, "Users"));

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Translation.DLogs.InitializingPageDone, "Users"));
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            InitializeEvents();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeModel()
        {
            // Paste page to User list.
            Model = new PageUsersModel(this);
        }

        /// <summary>
        /// Method to initialize events handlers.
        /// </summary>
        private void InitializeEvents()
        {
            var groups = (FindName("UcDataGridAclGroupsServerName") as DataGridAclGroupsLayout);
            groups.Added += UcDataGridAclGroups_AclGroupAdded;
            groups.Changed += UcDataGridAclGroups_AclGroupChanged;
            groups.Canceled += UcDataGridAclGroups_AclGroupCanceled;
            groups.Deleted += UcDataGridAclGroups_AclGroupDeleted;
            groups.DefaultChanged += UcDataGridAclGroups_DefaultChanged;
            groups.SelectionChanged += (s, ec) => { Model.LoadUsers(); };

            var users = (FindName("UcDataGridUsers") as DataGridUsersLayout);
            users.Added += UcDataGridUsers_UserAdded;
            users.Changed += UcDataGridUsers_UserChanged;
            users.Canceled += UcDataGridUsers_UserCanceled;
            users.Deleted += UcDataGridUsers_UserDeleled;
        }

        #endregion



        #region Methods : AclGroup

        /// <summary>
        /// Method called on AclGroup view cancel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Entity changes event arguments.</param>
        private void UcDataGridAclGroups_AclGroupCanceled(object sender, EntityChangesEventArgs e)
        {
            log.Info("Adding or editing AclGroup operation canceled. Please wait...");

            Model.LoadAclGroups();

            log.Info("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view changes event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_AclGroupAdded(object sender, EntityChangesEventArgs e)
        {
            log.Info("Saving new AclGroup informations. Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;
            Model.AclGroups.Items.Add(item);
            AclGroupEntityCollection.DbInsert(new List<AclGroupEntity> { item });

            Model.LoadAll();

            log.Info("Saving new AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view add event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_AclGroupChanged(object sender, EntityChangesEventArgs e)
        {
            log.Info("Saving AclGroup informations. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntity old = Model.AclGroups.Items.Single(x => x.AclGroupId == newEntity.AclGroupId);
            int index = Model.AclGroups.Items.IndexOf(old);
            Model.AclGroups.Items[index] = newEntity;
            AclGroupEntityCollection.DbUpdateAsync(new List<AclGroupEntity> { newEntity }, new List<AclGroupEntity> { old });

            Model.LoadAll();

            log.Info("Saving AclGroup informations. Done.");
        }

        /// <summary>
        /// Method called on AclGroup view delete event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e"></param>
        private void UcDataGridAclGroups_AclGroupDeleted(object sender, EntityChangesEventArgs e)
        {
            log.Info("Deleting AclGroup(s). Please wait...");

            AclGroupEntity item = (AclGroupEntity)e.NewEntity;

            // Remove item from list.
            Model.AclGroups.Items.Remove(item);

            // Delete item from database.
            AclGroupEntityCollection.DbDelete(new List<AclGroupEntity> { item });

            Model.LoadAll();

            log.Info("Deleting AclGroup(s). Done.");
        }

        /// <summary>
        /// Method called on AclGroup default change event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridAclGroups_DefaultChanged(object sender, EntityChangesEventArgs e)
        {
            log.Info("Setting default Section. Please wait...");

            AclGroupEntity newEntity = (AclGroupEntity)e.NewEntity;
            AclGroupEntityCollection.SetDefault(newEntity);
            Model.LoadAclGroups();

            log.Info("Setting default Section. Done.");
        }

        #endregion



        #region Methods : User

        /// <summary>
        /// Method called on User view cancel event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void UcDataGridUsers_UserCanceled(object sender, EntityChangesEventArgs e)
        {
            log.Info("Adding or editing User operation canceled. Please wait...");

            Model.LoadUsers();

            log.Info("Adding or editing AclGroup operation canceled. Done.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridUsers_UserAdded(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Adding or editing User informations. Please wait...");

            try
            {
                Model.AddUserNew((UserEntity)e.NewEntity);
                log.Info("Adding or editing User informations. Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
            finally
            {
                log.Warn("Adding or editing User informations. End.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void UcDataGridUsers_UserChanged(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Editing User informations. Please wait...");

            try
            {
                Model.SaveUserChanges((UserEntity)e.NewEntity);
                log.Info("Editing User informations. Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
            finally
            {
                log.Warn("Editing User informations. End.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on User view delete event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void UcDataGridUsers_UserDeleled(object sender, EntityChangesEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            log.Info("Deleting selected Users. Please wait...");

            try
            {
                Model.DeleleUser((UserEntity)e.NewEntity);
                log.Info(MessageBoxs.BusyContent = "Deleting selected Users. Done.");
            }
            catch (Exception ex)
            {
                MessageBoxs.BusyContent = "Deleting selected Users. failed !";
                log.Fatal(MessageBoxs.BusyContent, ex);
                MessageBoxs.Fatal(ex, (string)MessageBoxs.BusyContent);
            }
            finally
            {
                log.Warn(MessageBoxs.BusyContent = "Deleting selected Users. End.");
                MessageBoxs.IsBusy = false;
            }
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on page size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeSize();
            ArrangeSizeMiddleContents();
        }

        /// <summary>
        /// Method to arrange new page size.
        /// </summary>
        private void ArrangeSize()
        {
            this.Width = Math.Max(MainBlockContent.ActualWidth, 0);
            this.Height = Math.Max(MainBlockContent.ActualHeight, 0);

            TraceSize(MainBlockContent);
            TraceSize(this);
        }

        /// <summary>
        /// Method to arrange new page middle content size.
        /// </summary>
        private void ArrangeSizeMiddleContents()
        {
            BlockMiddleContentsName.Width = Math.Max(this.Width, 0);
            BlockMiddleContentsName.Height = Math.Max(this.Height, 0);

            TraceSize(BlockMiddleContentsName);

            (FindName("UcDataGridAclGroupsServerName") as FrameworkElement).Height = Math.Max(this.Height, 0);
            UcDataGridUsers.Height = Math.Max(this.Height, 0);

            TraceSize((FindName("UcDataGridAclGroupsServerName") as FrameworkElement));
            TraceSize(UcDataGridUsers);
        }

        #endregion
    }
}
