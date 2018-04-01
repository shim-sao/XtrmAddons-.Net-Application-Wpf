using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Converters;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.UserForm
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form User.</para>
    /// <para>Provides form to add or edit a User.</para>
    /// </summary>
    public partial class WindowFormUser : WindowBaseForm, IWindowForm<UserEntity>
    {
        #region Variables

        /// <summary>
        /// Variable Window User Form model of the view.
        /// </summary>
        private WindowFormUserModel<WindowFormUser> model;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public UserEntity OldForm { get; set; }

        /// <summary>
        /// Variable old User informations backup.
        /// </summary>
        public UserEntity NewForm
        {
            get => model?.User;
            set => model.User = value;
        }

        #endregion


        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Windows User Form Constructor.
        /// </summary>
        /// <param name="PageBase">The parent Page Base of the window.</param>
        /// <param name="entity">A user entity.</param>
        public WindowFormUser(UserEntity entity = default(UserEntity))
        {
            // Initialize window component.
            InitializeComponent();
            InitializeModel(entity);
            Loaded += Window_Load;
            Closing += Window_Closing;
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            DataContext = model;
        }

        protected void InitializeModel(UserEntity entity = default(UserEntity))
        {
            // 1 - Initialize view model.
            model = new WindowFormUserModel<WindowFormUser>(this);

            // 2 - Make sure entity is not null.
            entity = entity ?? new UserEntity();

            // 3 - Initialize new entity if required.
            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // 4 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 5 - Assign entity to the model.
            NewForm = entity;

            // 6 - Set model entity to dependencies converters.
            CheckBoxUserInAclGroup.User = model.User;

            // 7.1 - Assign list of AclGroup to the model.
            model.AclGroups = new AclGroupEntityCollection(true);

            // 8 - Validate form on first entry
            //ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputName_Changed(object sender, TextChangedEventArgs e)
        {
            NewForm.Name = InputName.Text;
            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputPassword_Changed(object sender, TextChangedEventArgs e)
        {
            NewForm.Password = InputPassword.Text;
            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputEmail_Changed(object sender, TextChangedEventArgs e)
        {
            NewForm.Email = InputEmail.Text;
            ValidateForm();
        }

        /// <summary>
        /// Method to check if saving is enabled.
        /// </summary>
        protected new bool ValidateForm()
        {
            bool save = true;

            if (NewForm != null)
            {
                if (NewForm.Name.IsNullOrWhiteSpace())
                {
                    save = false;
                }

                if (NewForm.Password.IsNullOrWhiteSpace())
                {
                    save = false;
                }

                if (NewForm.Email.IsNullOrWhiteSpace() || !NewForm.Email.IsValidEmail())
                {
                    save = false;
                }

                if (NewForm.UsersInAclGroups == null || NewForm.UsersInAclGroups.Count == 0)
                {
                    save = false;
                }
            }
            else
            {
                save = false;
            }

            ButtonSave.IsEnabled = IsSaveEnabled = save;

            return save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            //CheckBox_Checked<AclGroupEntity>(sender, e);
            NewForm.LinkAclGroup(entity.PrimaryKey);

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            NewForm.UnLinkAclGroup(entity.PrimaryKey);

            ValidateForm();
        }

        /// <summary>
        /// Method called on data grid selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        private void DataGridCollectionItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}