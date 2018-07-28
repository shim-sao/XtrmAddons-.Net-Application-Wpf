using System.Windows;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls List Views Storage.
    /// </summary>
    [System.Obsolete("use XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews.ListViewStorages")]
    public abstract class ListViewStorages : ListViewBase<StorageCollection, StorageInfoModel>
    {
        #region Properties

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for storage directories and files informations.
        /// </summary>
        public new static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(StorageCollection),
                typeof(ListViewStorages),
                new PropertyMetadata(new StorageCollection())
            );

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAddNewItem_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEditItem_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDeleteItems_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        protected void Item_MouseEnter(object sender, MouseEventArgs e) { }

        /// <summary>
        /// Method called clear items selection click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void ClearItemsSelection_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectedItems.Clear();
        }

        /// <summary>
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void SelectAllItems_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectAll();
        }

        #endregion
    }
}
