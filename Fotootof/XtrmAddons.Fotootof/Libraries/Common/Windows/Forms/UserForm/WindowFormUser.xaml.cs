using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Converters;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.UserForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Windows User Form.
    /// </summary>
    public partial class WindowFormUser : WindowBaseForm
    {
        #region Variables

        /// <summary>
        /// Variable Window User Form model.
        /// </summary>
        //private WindowUserFormModel<WindowBaseForm<UserEntity>> model;

        #endregion



        #region Properties

        /// <summary>
        /// Variable old User informations backup.
        /// </summary>
        public UserEntity OldEntity { get; set;  }

        /// <summary>
        /// Variable old User informations backup.
        /// </summary>
        public UserEntity NewEntity => Model.User;

        /// <summary>
        /// 
        /// </summary>
        public UserEntity OldForm { get => OldEntity; set => OldEntity = value; }

        /// <summary>
        /// Variable old User informations backup.
        /// </summary>
        public UserEntity NewForm { get => Model?.User; set => Model.User = value; }

        /// <summary>
        /// Accessors to page user model.
        /// </summary>
        public WindowFormUserModel<WindowFormUser> Model { get; private set; }

        #endregion

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Windows User Form Constructor.
        /// </summary>
        /// <param name="PageBase">The parent Page Base of the window.</param>
        /// <param name="entity">A user entity.</param>
        public WindowFormUser(UserEntity entity = default(UserEntity), PageBase owner = null)
        {
            // Initialize window component.
            InitializeComponent();
            Model = new WindowFormUserModel<WindowFormUser>(this);
            Loaded += (s, e) => Window_Load(entity);
        }

        public void Window_Load(UserEntity entity = null)
        { 
            // Assign closing event.
            Closing += Window_Closing;

            // Initialize User first.
            entity = entity ?? new UserEntity();

            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // Store data in new entity.
            OldEntity.Bind(entity);

            // Assign Parent Page & entity to the model.
            Model.User = entity;
            CheckBoxUserInAclGroup.User = Model.User;

            // Assign AclGroup list to model.
            Model.AclGroups = new AclGroupEntityCollection(true);


            // Assign model to data context for display.
            DataContext = Model;

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputName_Changed(object sender, TextChangedEventArgs e)
        {
            if (!InputName.Text.IsNullOrWhiteSpace())
            {
                NewEntity.Name = InputName.Text;
            }

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputPassword_Changed(object sender, TextChangedEventArgs e)
        {
            if (!InputPassword.Text.IsNullOrWhiteSpace())
            {
                NewEntity.Password = InputPassword.Text;
            }

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputEmail_Changed(object sender, TextChangedEventArgs e)
        {
            if (!InputEmail.Text.IsNullOrWhiteSpace())
            {
                NewEntity.Email = InputEmail.Text;
            }

            ValidateForm();
        }

        /// <summary>
        /// Method to check if saving is enabled.
        /// </summary>
        protected override bool ValidateForm()
        {
            bool save = true;

            if (NewEntity != null)
            {
                if (NewEntity.Name.IsNullOrWhiteSpace())
                {
                    save = false;
                }

                if (NewEntity.Password.IsNullOrWhiteSpace())
                {
                    save = false;
                }

                if (NewEntity.Email.IsNullOrWhiteSpace() || !NewEntity.Email.IsValidEmail())
                {
                    save = false;
                }

                if (NewEntity.UsersInAclGroups == null || NewEntity.UsersInAclGroups.Count == 0)
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
            NewEntity.LinkAclGroup(entity.PrimaryKey);

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
            NewEntity.UnLinkAclGroup(entity.PrimaryKey);

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