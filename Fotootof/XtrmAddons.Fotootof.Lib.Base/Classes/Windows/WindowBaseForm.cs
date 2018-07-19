using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Classes Windows Form.
    /// </summary>
    public partial class WindowBaseForm : WindowBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
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
            protected set { model = value; }
        }

        /// <summary>
        /// Method to check if form is ready to save.
        /// </summary>
        /// <returns>True if it is ready otherwise False.</returns>
        public virtual bool IsSaveEnabled
        {
            get => (bool)Model.GetPropertyValue("IsSaveEnabled");
            protected set => Model.SetPropertyValue("IsSaveEnabled", value);
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Windows Form Constructor.
        /// </summary>
        public WindowBaseForm() : base() { }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on dialog save form click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected virtual void DialogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsValidForm())
                {
                    MessageBox.Show
                    (
                        "The form is not valid.",
                        Culture.Translation.DWords.ApplicationName,
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
                    Culture.Translation.DWords.ApplicationName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                DialogResult = false;
            }
        }

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Cancel event arguments.</param>
        protected override void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    "Are you sure to cancel operation ?\n All properties changes will be lost.",
                    Culture.Translation.DWords.ApplicationName,
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
                    Culture.Translation.DWords.ApplicationName,
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
        /// Method to validate a simple required not empty string.
        /// </summary>
        protected bool IsValidInput(TextBox tb)
        {
            log.Debug("Is valid Input " + tb.Name + " : " + tb.Text + " => " + tb.Text.IsNotNullOrWhiteSpace());
            return tb.Text.IsNotNullOrWhiteSpace();
        }

        /// <summary>
        /// Method to validate a required email.
        /// </summary>
        /// <param name="tb">A text box to check text entry.</param>
        /// <returns>True if the entry is an email string otherwise false.</returns>
        protected bool IsValidInputEmail(TextBox tb)
        {
            log.Debug("Is valid Email : " + tb.Text + " => " + tb.Text.IsValidEmail());
            return tb.Text.IsValidEmail();
        }

        /// <summary>
        /// Method to validate a required alias.
        /// </summary>
        /// <param name="tb">A text box to check text entry.</param>
        /// <returns>True if the entry is an email string otherwise false.</returns>
        protected bool IsValidInputAlias(TextBox tb)
        {
            log.Debug("Is valid Alias : " + tb.Text + " => " + tb.Text.Sanitize().RemoveDiacritics().ToLower().IsNotNullOrWhiteSpace());
            return tb.Text.Sanitize().RemoveDiacritics().ToLower().IsNotNullOrWhiteSpace();
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the Form.
        /// </summary>
        protected virtual bool IsValidForm()
        {
            log.Debug("Validate Form Result : True");
            return true;
        }

        /// <summary>
        /// Method to validate the Form Data Name.
        /// </summary>
        protected void IsValidFormNotNullOrWhiteSpace(object form, string propertyName)
        {
            // Validate the form field name.
            if (form.GetPropertyValue<string>(propertyName).IsNullOrWhiteSpace())
            {
                ArgumentNullException e = new ArgumentNullException(string.Format(CultureInfo.CurrentUICulture, Translation.DWords.FormFieldRequired, Translation.Words[propertyName]));
                log.Debug(e.Output());
                log.Debug($"{propertyName} => {form.GetPropertyValue<string>(propertyName)}");
                throw e;
            }
        }

        /// <summary>
        /// Method to validate the Form Data Email.
        /// </summary>
        protected void IsValidFormEmail(object form, string propertyName)
        {
            // Validate the form field email.
            if (!form.GetPropertyValue<string>(propertyName).IsValidEmail())
            {
                ArgumentNullException e = new ArgumentNullException(string.Format(CultureInfo.CurrentUICulture, Translation.DWords.FormFieldRequired, Translation.Words[propertyName]));
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Validation error event argumments.</param>
        protected void OnInput_ValidationError(object sender, ValidationErrorEventArgs e)
        {
           IsSaveEnabled = false;
        }

        /// <summary>
        /// <para>Method called on input updated event.</para>
        /// <para>Disable Form Save to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments</param>
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        protected void OnInputStringRequired_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSaveEnabled = !IsValidInput(sender as TextBox) ? false : IsValidInputs();
        }

        /// <summary>
        /// <para>Method called on input name text source changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        protected void OnInputEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSaveEnabled = !IsValidInputEmail(sender as TextBox) ? false : IsValidInputs();
        }

        /// <summary>
        /// <para>Method called on input email text source changed event.</para>
        /// <para>Send Form validation to Model to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments.</param>
        protected void OnInputEmail_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            IsSaveEnabled = !IsValidInputEmail(sender as TextBox) ? false : IsValidInputs();
        }

        #endregion
    }
}
