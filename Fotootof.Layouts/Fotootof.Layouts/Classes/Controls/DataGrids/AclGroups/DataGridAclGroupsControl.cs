using Fotootof.Collections.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Windows.Forms.AclGroup;
using Fotootof.Libraries.Controls.DataGrids;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts DataGrid AclGroups.
    /// </summary>
    public abstract class DataGridAclGroupsControl : DataGridBase<AclGroupEntityCollection, AclGroupEntity>
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
        /// Property Using a DependencyProperty as the backing store for AclGroup Entities.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
             DependencyProperty.Register
             (
                "Items",
                typeof(AclGroupEntityCollection),
                typeof(DataGridAclGroupsControl),
                new PropertyMetadata(new AclGroupEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on add click event to add new AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAclGroupLayout dlg = new WindowFormAclGroupLayout();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                NotifyAdded(dlg.NewFormData);
            }
            else
            {
                NotifyCanceled(dlg.NewFormData);
            }
        }

        /// <summary>
        /// Method called on edit click event to navigate to an AclGroup edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormAclGroupLayout dlg = new WindowFormAclGroupLayout(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    NotifyChanged(dlg.NewFormData);
                }
                else
                {
                    NotifyCanceled(dlg.NewFormData);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(AclGroupEntity));
                log.Warn(message);
                MessageBoxs.Warning(message);
            }
        }

        /// <summary>
        /// Method called on delete click to delete an AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void DeleteItems_Click(object sender, RoutedEventArgs e)
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
                    NotifyDeleted(SelectedItem);
                }
            }
            else if (SelectedItem != null && SelectedItem.IsDefault == true)
            {
                log.Info("Default User Group cannot be delete !");
            }
            else
            {
                log.Error("User not found !");
                MessageBoxs.Warning("User not found !");
            }
        }
/*
        /// <summary>
        /// Method called on AclGroup default check box changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            RaiseDefaultChanged(entity);
        }
*/
        #endregion
    }
}
