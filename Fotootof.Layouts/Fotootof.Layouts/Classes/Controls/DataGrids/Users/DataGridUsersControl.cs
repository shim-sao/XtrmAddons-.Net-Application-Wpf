using Fotootof.Collections.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Forms.User;
using Fotootof.Libraries.Controls.DataGrids;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls DataGrids Users.
    /// </summary>
    public abstract class DataGridUsersControl : DataGridBase<UserEntityCollection, UserEntity>
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
        /// Property Using a DependencyProperty as the backing store for Users Entities.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(UserEntityCollection),
                typeof(DataGridUsersControl),
                new PropertyMetadata(new UserEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new User.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            WindowFormUserLayout dlg = new WindowFormUserLayout();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                NotifyAdded(dlg.NewForm);
            }
            else
            {
                NotifyCanceled(dlg.NewForm);
            }
        }

        /// <summary>
        /// Method called on edit click to navigate to a User edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormUserLayout dlg = new WindowFormUserLayout(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    NotifyChanged(dlg.NewForm);
                }
                else
                {
                    NotifyCanceled(dlg.NewForm);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(UserEntity));
                log.Warn(message);
                MessageBoxs.Warning(message);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a User.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void DeleteItems_Click(object sender, RoutedEventArgs e)
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
                    NotifyDeleted(SelectedItem);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(UserEntity));
                log.Warn(message);
                MessageBoxs.Warning(message);
            }
        }

        #endregion
    }
}