using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Net.Common.Extensions;
using RemoteClient = XtrmAddons.Net.Application.Serializable.Elements.Remote.Client;

namespace Fotootof.Layouts.Windows.Forms.Client
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Windows Client Form.
    /// </summary>
    public partial class WindowFormClientLayout : WindowLayoutForm, IValidator
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
        public new WindowFormClientModel Model
        {
            get => (WindowFormClientModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property to access to the old Client backuped informations.
        /// </summary>
        public RemoteClient OldForm { get; set; }

        /// <summary>
        /// Property to access to the new Client informations.
        /// </summary>
        public RemoteClient NewForm
        {
            get => Model.Client;
            set => Model.Client = value;
        }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => FindName("ButtonSaveName") as Button;

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => FindName("ButtonCancelName") as Button;

        /// <summary>
        /// Variable to store the secure Password.
        /// </summary>
        public bool IsNewForm { get; private set; } = true;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Windows Client Form Constructor.
        /// </summary>
        /// <param name="svr">An application Server configuration.</param>
        public WindowFormClientLayout(RemoteClient client = default(RemoteClient))
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel(client);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window loaded event.
        /// </summary>
        private void Window_Load(object sender, RoutedEventArgs e)
        { 
            // Assign model to data context for display.
            DataContext = Model;
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="entity">A client server preferences.</param>
        protected void InitializeModel(RemoteClient client = default(RemoteClient))
        {
            // 1 - Initialize view model.
            Model = new WindowFormClientModel(this);

            // 2 - Make sure entity is not null.
            client = client ?? new RemoteClient();

            // 3 - Store current entity data in a new entity.
            OldForm = client;

            // 4 - Assign entity to the model.
            NewForm = client;

            if (NewForm.Key.IsNotNullOrWhiteSpace())
            {
                IsNewForm = false;
            }
            else
            {
                NewForm.Key = string.Empty.GuidToBase64();
            }
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the form inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            // Check if the Name is not empty.
            Trace.WriteLine("Checking if the Name is not empty...");
            if (!IsValidInput(FindName("InputName") as TextBox))
            {
                return false;
            }

            // Check if the Host is not empty.
            Trace.WriteLine("Checking if the Host is not empty...");
            if (!IsValidInput(FindName("InputHost") as TextBox))
            {
                return false;
            }

            // Check if the Host is not empty.
            Trace.WriteLine("Checking if the Port is not empty...");
            if (!IsValidInput(FindName("InputPort") as TextBox))
            {
                return false;
            }

            Trace.WriteLine("All inputs have been verified !");
            return IsSaveEnabled = base.IsValidInputs();
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate a required password.
        /// </summary>
        /// <param name="pb">The password box to validate.</param>
        /// <returns>True if Password is valid otherwise false.</returns>
        protected bool IsValidPassword(PasswordBox pb)
        {
            // = pb.Password.Length > 0 ? pb.Password.MD5Hash() : "";
            SecurePassWord = pb.Password.Length > 0 ? pb.Password : "";

            Debug.WriteLine("pb.Password : " + pb.Password);
            Debug.WriteLine("pb.Password.Length : " + pb.Password.Length);
            Debug.WriteLine("SecurePassWord : " + SecurePassWord);

            // Check if it is new form.
            if (IsNewForm)
            {
                // We can set secure password to the form.
                NewForm.Password = SecurePassWord;
                Debug.WriteLine("SecurePassWord : " + SecurePassWord);

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
                Debug.WriteLine("Check if it is new form. : " + IsNewForm);
                Debug.WriteLine("SecurePassWord : " + SecurePassWord);

                if (pb.Password.IsNullOrWhiteSpace())
                {
                    if (OldForm.Password.IsNullOrWhiteSpace())
                    {
                        Debug.WriteLine("Old password is empty");
                        return false;
                    }

                    // Keep old password back.
                    NewForm.Password = OldForm.Password;
                    Debug.WriteLine("Keep old password back : " + OldForm.Password);
                    return true;
                }

                // We can set secure password to the form.
                NewForm.Password = SecurePassWord;
                Debug.WriteLine("Secure password sended to new form");

                // Password is not already valid.
                if (pb.Password.Length < 8)
                {
                    Debug.WriteLine("Secure password sended to new form");
                    return false;
                }
            }

            return true;
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the new Form Data.
        /// </summary>
        protected override bool IsValidForm()
        {
            try
            {
                IsValidFormNotNullOrWhiteSpace(NewForm, "Name");
                IsValidFormNotNullOrWhiteSpace(NewForm, "Host");
                IsValidFormNotNullOrWhiteSpace(NewForm, "Port");

                return true;
            }
            catch (ArgumentNullException e)
            {
                log.Error(e);
                throw new ArgumentNullException(e.Message);
            }
        }

        #endregion



        #region Methods Events Host

        /// <summary>
        /// Method to check and validate input server host text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments.</param>
        private void OnInputHost_SourceChanged(object sender, DataTransferEventArgs e)
        {
            if ((FindName("InputName") as TextBox).Text.IsNullOrWhiteSpace())
            {
                NewForm.Name = GetTag<TextBox>(sender).Text;
                OnInputStringRequired_SourceUpdated((FindName("InputName") as TextBox), e);
            }

            OnInputStringRequired_SourceUpdated(sender, e);
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
    }
}