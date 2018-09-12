using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.ComponentOld.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Controls Albums List View.
    /// </summary>
    public partial class ListViewAlbumsServer : ListViewAlbums
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override AlbumEntityCollection Items
        { 
            get => (AlbumEntityCollection) DataContext.GetPropertyValue("Items");
            set => DataContext.SetPropertyValue("Items",  value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterQualityChanged
        {
            add { FiltersQualitySelector.SelectionChanged += value; }
            remove { FiltersQualitySelector.SelectionChanged -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterColorChanged
        {
            add { FiltersColorSelector.SelectionChanged += value; }
            remove { FiltersColorSelector.SelectionChanged -= value; }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewAlbumsServer()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
            //ItemsCollection.Loaded += (s,e) => ControlHeaderTotal.Text = Albums.Count.ToString();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on delete click event to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public async override void OnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected items list is not null. 
            if (SelectedItems == null)
            {
                NullReferenceException ex = ExceptionBase.RefNull(nameof(SelectedItems), SelectedItems);
                log.Warn(ex.Output(), ex);
                MessageBase.Warning(ex.Output());
                return;
            }

            // Alert user for acceptation.
            MessageBoxResult result = MessageBox.Show
            (
                String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.Album, SelectedItems.Count),
                Translation.DWords.ApplicationName,
                MessageBoxButton.YesNoCancel
            );

            // If not accept, do nothing at this moment.
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            // If accepted, try to update page model collection.
            try
            {
                // Start to busy application.
                MessageBase.IsBusy = true;
                log.Warn("Starting deleting Album(s). Please wait...");

                // Delete item from database.
                await AlbumEntityCollection.DbDeleteAsync(SelectedItems);
                
                // Important : No need to defer for list view items refresh.
                log.Warn("Updating Album(s) list view items...");
                foreach (AlbumEntity item in SelectedItems)
                {
                    Items.Remove(item);
                }

                // Refresh of the list view items source.
                log.Warn("Refreshing Album(s) list view...");
                ItemsCollection.Items.Refresh();

                // Raise the on delete event.
                log.Warn("Refreshing Album(s) list view...");
                RaiseOnDelete(SelectedItems.ToArray());

                // Stop to busy application.
                log.Warn("Ending deleting Album(s).");
                MessageBase.IsBusy = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex.Output());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                AppNavigator.NavigateToPageAlbumServer(SelectedItem.PrimaryKey);
            }
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
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void OnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectAll();
        }

        /// <summary>
        /// Method called on unselect all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void OnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.UnselectAll();
        }

        #endregion

        

        #region Methods Size Changed

        /// <summary>
        /// Method called on user control size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockRoot()
        {
            GridBlockRootName.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            TraceSize(GridBlockRootName);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockItems()
        {
            double height = Math.Max(this.ActualHeight - StackPanelBlockHeaderName.RenderSize.Height, 0);
            double width = Math.Max(this.ActualWidth, 0);

            StackPanelBlockHeaderName.Width = width;

            GridBlockItemsName.Width = width;
            GridBlockItemsName.Height = height;

            ItemsCollection.Width = width;
            ItemsCollection.Height = height;

            TraceSize(StackPanelBlockHeaderName);
            TraceSize(GridBlockItemsName);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}
