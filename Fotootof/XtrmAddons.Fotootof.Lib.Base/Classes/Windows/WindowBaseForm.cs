﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Classes Windows Form.
    /// </summary>
    public partial class WindowBaseForm : WindowBase
    {
        /// <summary>
        /// Method to check if form is ready to save.
        /// </summary>
        /// <returns>True if it is ready otherwise False.</returns>
        private bool isSaveEnabled = false;

        /// <summary>
        /// Method to check if form is ready to save.
        /// </summary>
        /// <returns>True if it is ready otherwise False.</returns>
        protected virtual bool IsSaveEnabled
        {
            get => isSaveEnabled;
            set => isSaveEnabled = value;
        }

        /// <summary>
        /// 
        /// </summary>
        protected System.Windows.Media.Brush BorderStyleError
            = (System.Windows.Media.Brush)Application.Current.Resources["Error"];

        /// <summary>
        /// 
        /// </summary>
        protected System.Windows.Media.Brush BorderStyleReady
            = (System.Windows.Media.Brush)Application.Current.Resources["Ready"];



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
            if(!ValidateForm())
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

        /// <summary>
        /// Method to set TextBlock text changes to an object property.
        /// </summary>
        /// <param name="tb">A TextBox to get value.</param>
        /// <param name="obj">An object to bind text box value.</param>
        /// <param name="propertyName">The name of the property of the object.</param>
        [Obsolete("Use Xaml validationError")]
        protected virtual void TextBox_TextChanged(TextBox tb, object obj, string propertyName = null, bool validate = true)
        {

            if (tb == null)
            {
                throw new ArgumentNullException(nameof(tb));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            propertyName = propertyName ?? (string)tb.Tag;

            if (!tb.Text.IsNullOrWhiteSpace())
            {
                obj.SetPropertyValue(propertyName, tb.Text);
            }
            else
            {
                obj.SetPropertyValue(propertyName, "");
            }

            if (validate)
            {
                IsSaveEnabled = ValidateForm();
            }
        }

        /// <summary>
        /// Method to validate the Form.
        /// </summary>
        protected bool ValidateForm()
        {
            return true;
        }

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected bool ValidateInput()
        {
            return true;
        }

        #endregion
    }
}
