using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
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

        /// <summary>
        /// Property to define if the form can be saftly saved.
        /// </summary>
        protected override bool IsSaveEnabled
            => ValidateForm();

        #endregion


        #region Constructors

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
            //Loaded += Window_Load;
            //Closing += Window_Closing;
        }

        #endregion


        #region Methods

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
            /*if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }*/

            // 4 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 5 - Assign entity to the model.
            NewForm = entity;
            
            // 6 - Set model entity to dependencies converters.
            IsAclGroupInUser.Entity = NewForm;

            // 7.1 - Assign list of AclGroup to the model.
            model.AclGroups = new AclGroupEntityCollection(true);
        }

        /// <summary>
        /// Method to validate the Form Data.
        /// </summary>
        protected new bool ValidateForm()
        {
            if (NewForm == null)
            {
                return false;
            }

            if (NewForm.Name.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (NewForm.Password.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (NewForm.Email.IsNullOrWhiteSpace() || !NewForm.Email.IsValidEmail())
            {
                return false;
            }

            if (NewForm.UsersInAclGroups == null || NewForm.UsersInAclGroups.Count == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        private new bool ValidateInput()
        {
            if (NewForm == null)
            {
                return false;
            }

            if (InputName.Text.IsNullOrWhiteSpace())
            {
                return false;
            }
            
            if (!InputEmail.Text.IsValidEmail())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// <para>Method to send validation error to a TextBox.</para>
        /// <para>Disable Form Save Button to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Validation error event argumments.</param>
        private void Input_Error(object sender, ValidationErrorEventArgs e)
        {
            Button_Save.IsEnabled = false;
        }

        /// <summary>
        /// <para>Method called on input updated event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments</param>
        private void Input_Updated(object sender, DataTransferEventArgs e)
        {
            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// <para>Method called on input name text changed event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Button_Save.IsEnabled = tb.Text.IsNullOrWhiteSpace() ? false : IsSaveEnabled;
        }

        /// <summary>
        /// <para>Method called on input name text source changed event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputName_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Button_Save.IsEnabled = tb.Text.IsNullOrWhiteSpace() ? false : IsSaveEnabled;
        }

        /// <summary>
        /// <para>Method called on input Password text changed event.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.IsNullOrWhiteSpace())
            {
                NewForm.Password = OldForm.Password;
            }
        }

        /// <summary>
        /// <para>Method called on input Password text source updated event.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputPassword_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.IsNullOrWhiteSpace())
            {
                NewForm.Password = OldForm.Password;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputEmail_Changed(object sender, TextChangedEventArgs e)
        {
            NewForm.Email = InputEmail.Text;
            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            NewForm.LinkAclGroup(entity.PrimaryKey);

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
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

        #endregion
    }
}