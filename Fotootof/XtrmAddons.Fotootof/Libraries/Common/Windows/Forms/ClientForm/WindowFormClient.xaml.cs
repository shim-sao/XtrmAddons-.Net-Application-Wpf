using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Windows Client Form.
    /// </summary>
    public partial class WindowClientForm : WindowBaseForm, IWindowForm<Client>
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
        public new WindowFormClientModel<WindowClientForm> Model
        {
            get => (WindowFormClientModel<WindowClientForm>)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property to access to the old Client backuped informations.
        /// </summary>
        public Client OldForm { get; set; }

        /// <summary>
        /// Property to access to the new Client informations.
        /// </summary>
        public Client NewForm
        {
            get => Model.Client;
            set => Model.Client = value;
        }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => Button_Save;

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => Button_Cancel;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Windows Client Form Constructor.
        /// </summary>
        /// <param name="svr">An application Server configuration.</param>
        public WindowClientForm(Client svr = default(Client))
        {
            // Initialize window component.
            InitializeComponent();
            Name = "UCWindowClientForm";

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
        /// <param name="entity">A Section entity.</param>
        protected void InitializeModel(Client svr = default(Client))
        {
            // 1 - Initialize view model.
            Model = new WindowFormClientModel<WindowClientForm>(this);

            // 2 - Make sure entity is not null.
            svr = svr ?? new Client();

            // 3 - Store current entity data in a new entity.
            OldForm = svr;

            // 4 - Assign entity to the model.
            NewForm = svr;
            NewForm.Key = NewForm.Key ?? string.Empty.GuidToBase64();
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            // Check if the Name is not empty.
            Trace.WriteLine("Checking if the Name is not empty...");
            if (!IsValidInput(InputName))
            {
                return false;
            }

            // Check if the Host is not empty.
            Trace.WriteLine("Checking if the Host is not empty...");
            if (!IsValidInput(InputHost))
            {
                return false;
            }

            Trace.WriteLine("All inputs have been verified !");
            IsSaveEnabled = true;
            return base.IsValidInputs();
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to check and validate input server host text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments.</param>
        private void OnInputHost_SourceChanged(object sender, DataTransferEventArgs e)
        {
            if (InputName.Text.IsNullOrWhiteSpace())
            {
                InputName.Text = ((TextBox)sender).Text;
            }

            OnInputStringRequired_SourceUpdated(sender, e);
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
    }
}
