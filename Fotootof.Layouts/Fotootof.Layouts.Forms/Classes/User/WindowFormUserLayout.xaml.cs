using Fotootof.Collections.Entities;
using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityConverters.ValueConverters;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Forms.User
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form User.</para>
    /// <para>Provides form to add or edit a User.</para>
    /// </summary>
    public partial class WindowFormUserLayout : WindowLayoutForm, IValidator
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the secure Password.
        /// </summary>
        private string SecurePassWord = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model.
        /// </summary>
        public new WindowFormUserModel Model
        {
            get => model as WindowFormUserModel;
            protected set { model = value; }
        }

        /// <summary>
        /// Variable old User informations backup.
        /// </summary>
        public UserEntity OldForm
        {
            get => Model?.OldUser;
            set => Model.OldUser = value;
        }

        /// <summary>
        /// Property to access  old User informations backup.
        /// </summary>
        public UserEntity NewForm
        {
            get => Model?.User;
            set => Model.User = value;
        }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => FindName("ButtonSaveName") as Button;

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => FindName("ButtonCancelName") as Button;

        #endregion



        #region 

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Component Windows Form User Constructor.
        /// </summary>
        /// <param name="UserId">A user entity id or primary key.</param>
        public WindowFormUserLayout(int UserId)
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel(UserId);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Component Windows Form User Constructor.
        /// </summary>
        /// <param name="entity">A user entity to edit or a default entity is created if no argument is specified.</param>
        public WindowFormUserLayout(UserEntity entity = default(UserEntity)) : this(entity?.PrimaryKey ?? 0) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add model to data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize the data model of the Window Form User.
        /// </summary>
        /// <param name="entity">A user entity to edit or a default entity is created if no argument is specified.</param>
        [System.Obsolete("use : InitializeModel(int UserId = 0)")]
        protected void InitializeModel(UserEntity entity = default(UserEntity))
        {
            // 1 - Make sure entity is not null.
            entity = entity ?? new UserEntity();

            // 2 - Initialize view model.
            Model = new WindowFormUserModel(this);

            // 4 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 5 - Assign entity to the model.
            NewForm = entity;
            
            // 6 - Set model entity to dependencies converters.
            IsAclGroupInUser.Entity = NewForm;

            // 7.1 - Assign list of AclGroup to the model.
            Model.AclGroups = new AclGroupEntityCollection(true);
        }

        /// <summary>
        /// Method to initialize the data model <see cref="WindowFormUserModel"/> of the Window Form User.
        /// </summary>u
        /// <param name="entityId">A <see cref="UserEntity"/> primary key or id to edit or a default entity is created if no argument is specified.</param>
        protected void InitializeModel(int entityId = 0)
        {
            Model = new WindowFormUserModel(this, entityId);
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the new Form Data.
        /// </summary>
        protected new bool IsValidForm()
        {
            try
            {
                IsValidFormNotNullOrWhiteSpace(NewForm, "Name");
                IsValidFormEmail(NewForm, "Email");
                IsValidFormPassword();
                IsValidFormUserGroupsDependencies();

                return true;
            }
            catch (ArgumentNullException e)
            {
                log.Error(e);
                throw new ArgumentNullException(e.Message);
            }
        }

        /// <summary>
        /// Method to validate the Form Data Password.
        /// </summary>
        private void IsValidFormPassword()
        {
            // Validate the form field Password
            if (NewForm.Password.IsNullOrWhiteSpace())
            {
                // New password is blank, so check for old form value. 
                if (OldForm.Password.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(string.Format(CultureInfo.CurrentUICulture, Local.Properties.Translations.FormFieldRequired, Local.Properties.Translations.Password));
                }

                // Keep old password back if new password is blank.
                NewForm.Password = OldForm.Password;
            }
        }

        /// <summary>
        /// Method to validate the Form Data User Groups Dependencies.
        /// </summary>
        private void IsValidFormUserGroupsDependencies()
        {
            // Validate the groups association fields.
            if (NewForm.AclGroupsPKeys == null || NewForm.AclGroupsPKeys.Count == 0)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentUICulture, Local.Properties.Translations.FormFieldRequired, Local.Properties.Translations.UsersGroups));
            }
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            // Check if the name is not empty.
            Trace.WriteLine("Checking if the name is not empty...");
            if (!IsValidInput(FindName("InputName") as TextBox))
            {
                return false;
            }

            // Check if the password is valid.
            Trace.WriteLine("Checking if the password is valid...");
            if (!IsValidPassword(FindName("InputPassword") as PasswordBox))
            {
                return false;
            }

            // Check if the email is valid.
            Trace.WriteLine("Checking if the email is valid...");
            if (!IsValidInputEmail(FindName("InputEmail") as TextBox))
            {
                return false;
            }

            // Check for user group association.
            // We need at least 1 association.
            Trace.WriteLine("Checking if the user group association is valid...");
            Trace.WriteLine("NewForm.UsersInAclGroups = " + NewForm.UsersInAclGroups);
            Trace.WriteLine("NewForm.UsersInAclGroups.Count = " + NewForm.UsersInAclGroups.Count);
            if (NewForm.AclGroupsPKeys == null || NewForm.AclGroupsPKeys.Count == 0)
            {
                return false;
            }

            Trace.WriteLine("All inputs have been verified !");
            return base.IsValidInputs();
        }

        /// <summary>
        /// Method to validate a required password.
        /// </summary>
        /// <param name="pb">The password box to validate.</param>
        /// <returns>True if Password is valid otherwise false.</returns>
        protected bool IsValidPassword(PasswordBox pb)
        {
            SecurePassWord = pb.Password.Length > 0 ? pb.Password.MD5Hash() : "";

            Trace.WriteLine("pb.Password : " + pb.Password);
            Trace.WriteLine("pb.Password.Length : " + pb.Password.Length);
            Trace.WriteLine("SecurePassWord : " + SecurePassWord);

            // Check if it is new form.
            if (NewForm.PrimaryKey == 0)
            {
                // We can set secure password to the form.
                NewForm.Password = SecurePassWord;
                Trace.WriteLine("SecurePassWord : " + SecurePassWord);

                // Check if new secure password is valid.
                if (pb.Password.IsNullOrWhiteSpace() || pb.Password.Length < 8)
                {
                    return false;
                }

                return true;
            }

            // For edit we must check if password is changed.
            // Empty imput doen't change old paswword.
            else
            {
                Trace.WriteLine("Check if it is new form. : " + NewForm.PrimaryKey);
                Trace.WriteLine("SecurePassWord : " + SecurePassWord);

                if (pb.Password.IsNullOrWhiteSpace())
                {
                    if (OldForm.Password.IsNullOrWhiteSpace())
                    {
                        Trace.WriteLine("Old password is empty");
                        return false;
                    }

                    // Keep old password back.
                    NewForm.Password = OldForm.Password;
                    Trace.WriteLine("Keep old password back : " + OldForm.Password);
                    return true;
                }

                // We can set secure password to the form.
                NewForm.Password = SecurePassWord;
                Trace.WriteLine("Secure password sended to new form");

                // Password is not already valid.
                if (pb.Password.Length < 8)
                {
                    Trace.WriteLine("Secure password sended to new form");
                    return false;
                }
            }

            return true;
        }

        #endregion



        #region Methods Events InputPassword

        /// <summary>
        /// Method called on Password text input changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void OnInputPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IsSaveEnabled = !IsValidPassword(sender as PasswordBox) ? false : IsValidInputs();
        }

        #endregion



        #region Methods Events CheckBox AclGroup

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            NewForm.AclGroupsPKeys.AddIfNotExists(GetTag<AclGroupEntity>(sender).PrimaryKey);
            IsSaveEnabled = IsValidInputs();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            NewForm.AclGroupsPKeys.Remove(GetTag<AclGroupEntity>(sender).PrimaryKey);
            IsSaveEnabled = NewForm.AclGroupsPKeys.Count == 0 ? false : IsValidInputs();
        }

        #endregion
    }
}