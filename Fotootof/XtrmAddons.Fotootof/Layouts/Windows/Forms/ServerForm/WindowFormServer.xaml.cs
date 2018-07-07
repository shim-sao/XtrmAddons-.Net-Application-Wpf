using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.ServerForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Windows Client Form.
    /// </summary>
    public partial class WindowServerForm : WindowBaseForm, IWindowForm<Server>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model.
        /// </summary>
        public new WindowFormServerModel Model
        {
            get => (WindowFormServerModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property to access to the old Client backuped informations.
        /// </summary>
        public Server OldForm { get; set; }

        /// <summary>
        /// Property to access to the new Client informations.
        /// </summary>
        public Server NewForm
        {
            get => Model.Server;
            set => Model.Server = value;
        }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => (Button)FindName("Button_Save");

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => (Button)FindName("Button_Cancel");

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
        public WindowServerForm(Server svr = default(Server))
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel(svr);
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
        protected void InitializeModel(Server svr = default(Server))
        {
            // 1 - Initialize view model.
            Model = new WindowFormServerModel(this);

            // 2 - Make sure entity is not null.
            svr = svr ?? new Server();

            // 3 - Store current entity data in a new entity.
            OldForm = svr;

            // 4 - Assign entity to the model.
            NewForm = svr;

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
            TextBox tb = FindName("InputName") as TextBox;

            if (tb.Text.IsNullOrWhiteSpace())
            {
                NewForm.Name = Tag2Object<TextBox>(sender).Text;
                OnInputStringRequired_SourceUpdated(tb, e);
            }

            OnInputStringRequired_SourceUpdated(sender, e);
        }

        #endregion
    }
}
