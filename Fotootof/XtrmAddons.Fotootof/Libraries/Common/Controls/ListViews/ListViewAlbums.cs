using System;
using System.Windows;
using System.Windows.Input;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums List View.
    /// </summary>
    public abstract class ListViewAlbums : ListViewBase<AlbumEntityCollection, AlbumEntity>
    {
        #region Properties

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(AlbumEntityCollection),
                typeof(ListViewAlbums),
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
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                AppLogger.Warning(message);
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
                    String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.Album, SelectedItem.Name),
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
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                AppLogger.Warning(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppNavigator.NavigateToPageAlbumServer(SelectedItem.PrimaryKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnAlbum_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// Method called clear items selection click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ClearItemsSelection_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectedItems.Clear();
        }

        /// <summary>
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void SelectAllItems_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectAll();
        }

        #endregion
    }
}
