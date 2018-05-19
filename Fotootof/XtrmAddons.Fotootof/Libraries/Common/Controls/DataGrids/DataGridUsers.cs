using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.UserForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls DataGrids Users.
    /// </summary>
    public abstract class DataGridUsers : DataGridBase<UserEntityCollection, UserEntity>
    {
        #region Properties

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Users Entities.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(UserEntityCollection),
                typeof(DataGridUsers),
                new PropertyMetadata(new UserEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new User.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            WindowFormUser dlg = new WindowFormUser();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewForm);
            }
            else
            {
                RaiseOnCancel(dlg.NewForm);
            }
        }

        /// <summary>
        /// Method called on edit click to navigate to a User edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormUser dlg = new WindowFormUser(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewForm);
                }
                else
                {
                    RaiseOnCancel(dlg.NewForm);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(UserEntity));
                log.Warn(message);
                AppLogger.Warning(message);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a User.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (

                    String.Format(
                        Translation.DWords.MessageBox_Acceptation_DeleteGeneric,
                        Translation.DWords.User,
                        SelectedItem.Name
                    ),
                    Translation.DWords.ApplicationName,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model collection.
                if (result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(UserEntity));
                log.Warn(message);
                AppLogger.Warning(message);
            }
        }

        #endregion
    }
}