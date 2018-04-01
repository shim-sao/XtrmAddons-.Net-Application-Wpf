using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Windows Client Form.
    /// </summary>
    public partial class WindowClientForm : WindowBaseForm
    {
        #region Properties

        /// <summary>
        /// Accessors to page Client model.
        /// </summary>
        public WindowClientFormModel<WindowClientForm> Model { get; protected set; }

        /// <summary>
        /// Property to access to the old Client backuped informations.
        /// </summary>
        public Client OldForm { get; set; }

        /// <summary>
        /// Property to access to the new Client informations.
        /// </summary>
        public Client NewForm { get => Model.Client; set => Model.Client = value; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Windows Client Form Constructor.
        /// </summary>
        /// <param name="svr">An application Server configuration.</param>
        public WindowClientForm(Client svr = null)
        {
            InitializeComponent();
            OldForm = svr;
            Loaded += (s, e) => Window_Load();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window loaded event.
        /// </summary>
        private void Window_Load()
        { 
            // Assign item to the model.
            Model = new WindowClientFormModel<WindowClientForm>(this);

            // Assign closing event.
            Closing += Window_Closing;

            // Initialize Server first.
            NewForm = OldForm ?? new Client();
            NewForm.Key = NewForm.Key ?? string.Empty.GuidToBase64();

            // Assign model to data context for display.
            DataContext = Model;

            // Validate form for saving.
            ButtonSave.IsEnabled = ValidateForm();
        }

        /// <summary>
        /// Method to check and validate input name text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputText_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged((TextBox)sender, NewForm);
            ButtonSave.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method to check and validate input server host text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputHost_Changed(object sender, TextChangedEventArgs e)
        {
            string oldHost = NewForm.Host;

            TextBox_TextChanged((TextBox)sender, NewForm, "Host", false);

            if (NewForm.Name.IsNullOrWhiteSpace() || NewForm.Name == oldHost)
            {
                InputName.Text = NewForm.Name = NewForm.Host;
            }

            ButtonSave.IsEnabled = ValidateForm();
        }

        /// <summary>
        /// Method to check and validate server port text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputPort_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged((TextBox)sender, NewForm);
            ButtonSave.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method to check and validate input user name text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputUserName_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged((TextBox)sender, NewForm);
        }

        /// <summary>
        /// Method to check and validate input password text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputPassword_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged((TextBox)sender, NewForm);
        }

        /// <summary>
        /// Method to check if form is ready for saving.
        /// </summary>
        /// <returns>True if form is ready for saving otherwise, returns False.</returns>
        protected new bool ValidateForm()
        {
            bool save = true;

            if (NewForm != null)
            {
                if (NewForm.Name.IsNullOrWhiteSpace())
                {
                    save = false;
                    InputName.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Warning");
                }
                else
                {
                    InputName.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Ready");
                }

                if (NewForm.Host.IsNullOrWhiteSpace())
                {
                    save = false;
                    InputHost.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Warning");
                }
                else
                {
                    InputHost.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Ready");
                }

                if (NewForm.Port.IsNullOrWhiteSpace())
                {
                    save = false;
                    InputPort.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Warning");
                }
                else
                {
                    InputPort.BorderBrush = (Brush)Application.Current.MainWindow.FindResource("Ready");
                }
            }
            else
            {
                save = false;
            }

            return IsSaveEnabled = save;
        }

        #endregion
    }
}
