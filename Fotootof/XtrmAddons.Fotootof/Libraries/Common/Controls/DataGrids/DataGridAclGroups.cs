using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AclGroupForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls DataGrid AclGroups.
    /// </summary>
    public abstract class DataGridAclGroups : DataGridBase<AclGroupEntityCollection, AclGroupEntity>
    {
        #region Properties
        
        /// <summary>
        /// Property Using a DependencyProperty as the backing store for AclGroup Entities.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
             DependencyProperty.Register
             (
                "Items",
                typeof(AclGroupEntityCollection),
                typeof(DataGridAclGroups),
                new PropertyMetadata(new AclGroupEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on add click event to add new AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAclGroup dlg = new WindowFormAclGroup();
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
        /// Method called on edit click event to navigate to an AclGroup edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEditItem_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormAclGroup dlg = new WindowFormAclGroup(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(SelectedItem);
                }
                else
                {
                    RaiseOnCancel(SelectedItem);
                }
            }
            else
            {
                log.Error("Group not found !");
                AppLogger.Warning("Group not found !");
            }
        }

        /// <summary>
        /// Method called on delete click to delete an AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null && SelectedItem.IsDefault == false)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(
                        (string)Translation.Words["MessageBox_Acceptation_DeleteGeneric"],
                        (string)Translation.Words["Group"],
                        SelectedItem.Name
                    ),
                    (string)Translation.Words["ApplicationName"],
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model AclGroup collection.
                if (result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else if (SelectedItem != null && SelectedItem.IsDefault == true)
            {
                log.Info("Default User Group cannot be delete !");
            }
            else
            {
                log.Error("User not found !");
                AppLogger.Warning("User not found !");
            }
        }

        /// <summary>
        /// Method called on AclGroup default check box changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        /*private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            RaiseDefaultChanged(entity);
        }*/

        #endregion
    }
}
