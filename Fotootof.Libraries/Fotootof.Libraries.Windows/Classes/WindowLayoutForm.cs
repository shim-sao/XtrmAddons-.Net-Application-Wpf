using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Libraries.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Window Form Layout.
    /// </summary>
    public partial class WindowLayoutForm : WindowLayout, IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable data model of the Window Form.
        /// </summary>
        protected object model;

        #endregion



        #region Properties

        /// <summary>
        /// <para>Property to access to the data model of the Window Form.</para>
        /// <para>Child classes must implement their own accessibility based on the type of their model.</para>
        /// </summary>
        public virtual object Model
        {
            get => model;
            protected set => model = value;
        }

        /// <summary>
        /// Method to check if form is ready to save.
        /// </summary>
        /// <returns>True if the form is ready otherwise False.</returns>
        public virtual bool IsSaveEnabled
        {
            get => (bool)Model.GetPropertyValue("IsSaveEnabled");
            protected set => Model.SetPropertyValue("IsSaveEnabled", value);
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Window Form Layout Constructor.
        /// </summary>
        public WindowLayoutForm() : base() { }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on dialog save form click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        protected virtual void DialogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsValidForm())
                {
                    MessageBox.Show
                    (
                        "The form is not valid.",
                        Local.Properties.Translations.ApplicationName,
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation
                    );

                    DialogResult = false;
                }
                else
                {
                    DialogResult = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show
                (
                    "The form is not valid.\n" + ex.Message,
                    Local.Properties.Translations.ApplicationName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                DialogResult = false;
            }
        }

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Cancel event arguments <see cref="CancelEventArgs"/></param>
        protected override void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    "Are you sure to cancel operation ?\n All properties changes will be lost.",
                    Local.Properties.Translations.ApplicationName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                // If accepted, try to cancel operation.
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }

            else
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    "Are you sure to save changes ?",
                    Local.Properties.Translations.ApplicationName,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                // If user doesn't want to close, cancel closure
                if (result == MessageBoxResult.Cancel)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                    return;
                }

                // If accepted, try to save operation.
                if (result == MessageBoxResult.No)
                {
                    DialogResult = false;
                }
            }
        }

        #endregion



        #region Methods Validate Form
        
        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected virtual bool IsValidInputs()
        {
            log.Debug("Validate Inputs Result : True");
            return true;
        }

        /// <summary>
        /// Method to validate a simple required <see cref="TextBox"/> text not empty string.
        /// </summary>
        /// <param name="tb">A <see cref="TextBox"/> to verify text entry.</param>
        /// <returns>True if the text is not empty, null or whitespace, otherwise false.</returns>
        protected bool IsValidInput(TextBox tb)
        {
            log.Debug("Is valid Input " + tb.Name + " : " + tb.Text + " => " + tb.Text.IsNotNullOrWhiteSpace());
            return tb.Text.IsNotNullOrWhiteSpace();
        }

        /// <summary>
        /// Method to validate a required email.
        /// </summary>
        /// <param name="tb">A <see cref="TextBox"/> to verify text entry.</param>
        /// <returns>True if the entry is an email string, otherwise false.</returns>
        protected bool IsValidInputEmail(TextBox tb)
        {
            log.Debug("Is valid Email : " + tb.Text + " => " + tb.Text.IsValidEmail());
            return tb.Text.IsValidEmail();
        }

        /// <summary>
        /// Method to validate a required alias.
        /// </summary>
        /// <param name="tb">A <see cref="TextBox"/> to verify text entry.</param>
        /// <returns>True if the entry is an email string, otherwise false.</returns>
        protected bool IsValidInputAlias(TextBox tb)
        {
            log.Debug("Is valid Alias : " + tb.Text + " => " + tb.Text.Sanitize().RemoveDiacritics().ToLower().IsNotNullOrWhiteSpace());
            return tb.Text.Sanitize().RemoveDiacritics().ToLower().IsNotNullOrWhiteSpace();
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the form.
        /// </summary>
        protected virtual bool IsValidForm()
        {
            log.Debug("Validate Form Result : True");
            return true;
        }

        /// <summary>
        /// Method to validate a form data string.
        /// </summary>
        /// <param name="form">A <see cref="object"/> with an email property.</param>
        /// <param name="propertyName">The property name to check.</param>
        protected void IsValidFormNotNullOrWhiteSpace(object form, string propertyName)
        {
            // Validate the form field name.
            if (form.GetPropertyValue<string>(propertyName).IsNullOrWhiteSpace())
            {
                ArgumentNullException e = new ArgumentNullException(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        Local.Properties.Translations.FormFieldRequired,
                        propertyName
                        //Translation.Words[propertyName]
                    )
                );
                log.Debug(e.Output());
                log.Debug($"{propertyName} => {form.GetPropertyValue<string>(propertyName)}");
                throw e;
            }
        }

        /// <summary>
        /// Method to validate a form data email.
        /// </summary>
        /// <param name="form">A <see cref="object"/> with an email property.</param>
        /// <param name="propertyName">The property name for the email to check.</param>
        protected void IsValidFormEmail(object form, string propertyName)
        {
            // Validate the form field email.
            if (!form.GetPropertyValue<string>(propertyName).IsValidEmail())
            {
                ArgumentNullException e = new ArgumentNullException(
                    string.Format(
                        CultureInfo.CurrentUICulture, 
                        Local.Properties.Translations.FormFieldRequired,
                        propertyName
                        //Translation.Words[propertyName]
                    )
                );
                log.Debug(e.Output());
                log.Debug($"{propertyName} => {form.GetPropertyValue<string>(propertyName)}");
                throw e;
            }
        }

        #endregion



        #region Methods Events Input

        /// <summary>
        /// <para>Method to send validation error to a TextBox.</para>
        /// <para>Disable Form Save to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Validation error event argumments <see cref="ValidationErrorEventArgs"/>.</param>
        protected void OnInput_ValidationError(object sender, ValidationErrorEventArgs e)
        {
           IsSaveEnabled = false;
        }

        /// <summary>
        /// <para>Method called on input updated event.</para>
        /// <para>Disable Form Save to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Data transfer event arguments <see cref="DataTransferEventArgs"/>.</param>
        protected void OnInput_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            IsSaveEnabled = IsValidInputs();
        }

        #endregion



        #region Methods Events Input String Required

        /// <summary>
        /// <para>Method called on input name text changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Data transfer event arguments <see cref="TextChangedEventArgs"/>.</param>
        protected void OnInputStringRequired_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSaveEnabled = !IsValidInput(sender as TextBox) ? false : IsValidInputs();
        }

        /// <summary>
        /// <para>Method called on input name text source changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Data transfer event arguments <see cref="DataTransferEventArgs"/>.</param>
        protected void OnInputStringRequired_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            IsSaveEnabled = IsValidInput(sender as TextBox) ? false : IsValidInputs();
        }

        #endregion



        #region Methods Events Input Email

        /// <summary>
        /// <para>Method called on input name text changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Data transfer event arguments <see cref="TextChangedEventArgs"/>.</param>
        protected void OnInputEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSaveEnabled = !IsValidInputEmail(sender as TextBox) ? false : IsValidInputs();
        }

        /// <summary>
        /// <para>Method called on input email text source changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Data transfer event arguments <see cref="DataTransferEventArgs"/>.</param>
        protected void OnInputEmail_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            IsSaveEnabled = !IsValidInputEmail(sender as TextBox) ? false : IsValidInputs();
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

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
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.


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
        ~WindowLayoutForm()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
