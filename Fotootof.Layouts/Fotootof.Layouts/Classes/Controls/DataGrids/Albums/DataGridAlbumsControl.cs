using Fotootof.Collections.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Forms.Album;
using Fotootof.Libraries.Controls.DataGrids;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class Fotootof Layouts Controls Data Grids Albums.
    /// </summary>
    public abstract class DataGridAlbumsControl : DataGridBase<AlbumEntityCollection, AlbumEntity>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property Using a <see cref="DependencyProperty"/> as the backing store for Items.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(AlbumEntityCollection),
                typeof(DataGridAlbumsControl),
                new PropertyMetadata(new AlbumEntityCollection(false))
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on add item click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Adding Album informations. Please wait...");

                // Show open file dialog box 
                using (WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(new AlbumEntity()))
                {
                    bool? result = dlg.ShowDialog();

                    /*
                    if (!dlg.Activate())
                    {
                        log.Warn("Albums edit file dialog boxn is busy. Please try again.");
                        MessageBoxs.Warning("Albums edit file dialog boxn is busy. Please try again.");
                        dlg.Close();
                    }
                    */

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
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Updating Album informations. Done.");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on edit <see cref="AlbumEntity"/> click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Editing Album informations. Please wait...");

                // Check if an AclGroup is founded. 
                if (SelectedItem != null)
                {
                    // Show open file dialog box 
                    WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(SelectedItem);
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

                    log.Warn("Editing Album informations. Done.");
                }
                else
                {
                    string message = $"{typeof(AlbumEntity).Name} not found !";
                    log.Warn(message);
                    MessageBoxs.Warning(message);
                }

            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Deleting Album. Please wait...");

                // Check if an AclGroup is founded. 
                if (SelectedItem != null)
                {
                    // Alert user for acceptation.
                    MessageBoxResult result = MessageBox.Show
                    (
                        String.Format(
                            Dialogs.Properties.Translations.MessageBox_Acceptation_DeleteGeneric,
                            Local.Properties.Translations.Album,
                            SelectedItem.Name
                        ),
                        Local.Properties.Translations.ApplicationName,
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
                    string message = $"{typeof(AlbumEntity).Name} not found !";
                    log.Warn(message);
                    MessageBoxs.Warning(message);
                }

            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Deleting Album. Please wait...");
                MessageBoxs.IsBusy = false;
            }
        }

        /// <summary>
        /// Method called on section default check box changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxDefault_Checked<AlbumEntity>(sender, e);
        }

        /// <summary>
        /// <para>Method to select item by an extra check box field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked<AlbumEntity>(sender, e);
        }

        /// <summary>
        /// <para>Method to remove item by an extra check box field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox_UnChecked<AlbumEntity>(sender, e);
        }

        #endregion
    }
}
