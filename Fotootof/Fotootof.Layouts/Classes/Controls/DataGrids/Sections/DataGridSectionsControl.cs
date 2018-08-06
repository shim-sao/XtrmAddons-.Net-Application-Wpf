using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Controls.DataGrids;
using Fotootof.Libraries.Systems;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DataGridSectionsControl : DataGridBase<SectionEntityCollection, SectionEntity>
    {
        #region Properties

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Items.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(SectionEntityCollection),
                typeof(DataGridSectionsControl),
                new PropertyMetadata(new SectionEntityCollection())
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Show open file dialog box 
            WindowFormSectionLayout dlg = new WindowFormSectionLayout(new SectionEntity());
            bool? result = dlg.ShowDialog();

            if(!dlg.Activate())
            {
                string message = "Sections edit file dialog box is busy. Please try again.";
                log.Warn(message);
                MessageBase.Warning(message);
                dlg.Close();
            }

            // Process open file dialog box results 
            if (result == true)
            {
                NotifyAdded(dlg.NewForm);
            }
            else
            {
                NotifyCanceled(dlg.NewForm);
            }
            */
        }

        /// <summary>
        /// Method called on edit click to navigate to a Section edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                /*
                // Show open file dialog box 
                WindowFormSectionLayout dlg = new WindowFormSectionLayout(SelectedItem);
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
                */
            }
            else
            {
                string message = string.Format("{0} not found !", nameof(SectionEntity));
                log.Warn(message);
                MessageBase.Warning(message);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Section.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (

                    String.Format(
                        (string)Translation.Words["MessageBox_Acceptation_DeleteGeneric"],
                        (string)Translation.Words["Section"],
                        SelectedItem.Name
                    ),
                    (string)Translation.Words["ApplicationName"],
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
                string message = string.Format("{0} not found !", nameof(SectionEntity));
                log.Warn(message);
                MessageBase.Warning(message);
            }
        }

        /// <summary>
        /// Method called on section default check box changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxDefault_Checked<SectionEntity>(sender, e);
        }

        #endregion
    }
}
