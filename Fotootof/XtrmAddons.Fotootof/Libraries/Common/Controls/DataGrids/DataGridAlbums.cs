using System;
using System.Windows;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    public abstract class DataGridAlbums : DataGridBase<AlbumEntityCollection, AlbumEntity>
    {
        #region Properties

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Items.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(AlbumEntityCollection),
                typeof(DataGridAlbums),
                new PropertyMetadata(new AlbumEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAlbum dlg = new WindowFormAlbum(new AlbumEntity());
            bool? result = dlg.ShowDialog();

            if (!dlg.Activate())
            {
                AppLogger.Warning("Can't open Albums edit file dialog box. Please try again.");
                dlg.Close();
            }

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
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormAlbum dlg = new WindowFormAlbum(SelectedItem);
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
                AppLogger.Warning(string.Format("{0} not found !", typeof(AlbumEntity).Name), true, true);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(
                        (string)Translation.Words["MessageBox_Acceptation_DeleteGeneric"],
                        (string)Translation.Words["Album"],
                        SelectedItem.Name
                    ),
                    (string)Translation.Words["ApplicationName"],
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
                AppLogger.Warning(string.Format("{0} not found !", typeof(AlbumEntity).Name), true, true);
            }
        }

        /// <summary>
        /// Method called on section default check box changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxDefault_Checked<AlbumEntity>(sender, e);
        }

        /// <summary>
        /// <para>Method to select item by an extra check box field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked<AlbumEntity>(sender, e);
        }

        /// <summary>
        /// <para>Method to remove item by an extra check box field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox_UnChecked<AlbumEntity>(sender, e);
        }

        #endregion
    }
}
