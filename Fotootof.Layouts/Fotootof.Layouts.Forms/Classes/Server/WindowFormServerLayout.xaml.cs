using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Net.Common.Extensions;
using ServerInfo = XtrmAddons.Net.Application.Serializable.Elements.Remote.Server;

namespace Fotootof.Layouts.Forms.Server
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Server Window Form Layout.
    /// </summary>
    public partial class WindowFormServerLayout : WindowLayoutForm, IValidator
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model <see cref="WindowFormServerModel"/>.
        /// </summary>
        public new WindowFormServerModel Model
        {
            get => (WindowFormServerModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property to access to the old <see cref="ServerInfo"/> backuped informations.
        /// </summary>
        public ServerInfo OldFormData
            => Model.OldFormData;

        /// <summary>
        /// Property to access to the new <see cref="ServerInfo"/> informations.
        /// </summary>
        public ServerInfo NewFormData
            => Model.NewFormData;

        /// <summary>
        /// Property to access to the main form save <see cref="Button"/>.
        /// </summary>
        public Button ButtonSave
            => (Button)FindName("ButtonSaveName");

        /// <summary>
        /// Property to access to the main form cancel <see cref="Button"/>.
        /// </summary>
        public Button ButtonCancel
            => (Button)FindName("ButtonCancelName");

        #endregion



        #region Constructor

        /// <summary>
        ///Class XtrmAddons Fotootof Layouts Server Window Form Layout Constructor.
        /// </summary>
        /// <param name="svr">An application server configuration <see cref="ServerInfo"/>.</param>
        public WindowFormServerLayout(ServerInfo svr = default(ServerInfo))
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void Window_Load(object sender, RoutedEventArgs e)
        { 
            // Assign model to data context for display.
            DataContext = Model;
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="svr">An application server configuration <see cref="ServerInfo"/>.</param>
        protected void InitializeModel(ServerInfo svr = default(ServerInfo))
        {
            // 1 - Make sure the entity is not null.
            svr = svr ?? new ServerInfo();

            // 2 - Initialize the layout model.
            Model = new WindowFormServerModel(this)
            {
                OldFormData = svr.Clone(),
                NewFormData = svr
            };
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
            if (!IsValidInput((TextBox)FindName("InputName")))
            {
                return false;
            }

            // Check if the Host is not empty.
            Trace.WriteLine("Checking if the Host is not empty...");
            if (!IsValidInput((TextBox)FindName("InputHost")))
            {
                return false;
            }

            // Check if the Host is not empty.
            Trace.WriteLine("Checking if the Port is not empty...");
            if (!IsValidInput((TextBox)FindName("InputPort")))
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
                IsValidFormNotNullOrWhiteSpace(NewFormData, "Name");
                IsValidFormNotNullOrWhiteSpace(NewFormData, "Host");
                IsValidFormNotNullOrWhiteSpace(NewFormData, "Port");

                return true;
            }
            catch (ArgumentNullException e)
            {
                log.Error(e.Output());
                throw;
            }
        }

        #endregion



        #region Methods Events Host

        /// <summary>
        /// Method to check and validate input server host text changes.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Data transfer event arguments <see cref="DataTransferEventArgs"/>.</param>
        private void OnInputHost_SourceChanged(object sender, DataTransferEventArgs e)
        {
            TextBox tb = FindName("InputName") as TextBox;

            if (tb.Text.IsNullOrWhiteSpace())
            {
                NewFormData.Name = GetTag<TextBox>(sender).Text;
                OnInputStringRequired_SourceUpdated(tb, e);
            }

            OnInputStringRequired_SourceUpdated(sender, e);
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Track whether Dispose has been called.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    Model.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.


                // Note disposing has been done.
                disposed = true;
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~WindowFormServerLayout()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
