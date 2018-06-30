using System.Windows;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Common Controls Pictures List View.
    /// </summary>
    public abstract class ListViewPictures : ListViewBase<PictureEntityCollection, PictureEntity>
    {
        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEditItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method called on items selection delete click event to delete a list of Pictures.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method called on items selection mouse double clear click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Mouse button event arguments.</param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method called on clear items selection click event.
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
