using Fotootof.Collections.Entities;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace Fotootof.Components.Server.Section.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Controls Albums List View.
    /// </summary>
    public partial class ListViewAlbumsLayout : ListViewAlbumsControl
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion



        #region Properties
/*
        /// <summary>
        /// 
        /// </summary>
        public override AlbumEntityCollection Items
        { 
            get => (AlbumEntityCollection)DataContext.GetPropertyValue("Items");
            set => DataContext.SetPropertyValue("Items",  value);
        }
*/
        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterQualityChanged
        {
            add { ((ComboBox)FindName("FiltersQualitySelector")).SelectionChanged += value; }
            remove { ((ComboBox)FindName("FiltersQualitySelector")).SelectionChanged -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedEventHandler FilterColorChanged
        {
            add { ((ComboBox)FindName("FiltersColorSelector")).SelectionChanged += value; }
            remove { ((ComboBox)FindName("FiltersColorSelector")).SelectionChanged -= value; }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewAlbumsLayout()
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
        public async override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected items list is not null. 
            if (SelectedItems == null)
            {
                NullReferenceException ex = Exceptions.GetReferenceNull(nameof(SelectedItems), SelectedItems);
                log.Warn(ex.Output(), ex);
                MessageBoxs.Warning(ex.Output());
                return;
            }

            // Alert user for acceptation.
            MessageBoxResult result = MessageBox.Show
            (
                String.Format(Fotootof.Layouts.Dialogs.Properties.Translations.MessageBox_Acceptation_DeleteGeneric, Local.Properties.Translations.Album, SelectedItems.Count),
                Local.Properties.Translations.ApplicationName,
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
                MessageBoxs.IsBusy = true;
                log.Warn("Deleting Album(s). Please wait...");

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
                NotifyDeleted(SelectedItems.ToArray());
            }

            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }

            // Stop to busy application.
            finally
            {
                log.Warn("Deleting Album(s). Done.");
                MessageBoxs.IsBusy = false;
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
                ComponentNavigator.NavigateToAlbum(SelectedItem.PrimaryKey);
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
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockRoot()
        {
            var blockRoot = FindName("GridBlockRootName") as FrameworkElement;
            blockRoot.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            DebugTraceSize(blockRoot);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockItems()
        {
            var blockHeader = FindName("StackPanelBlockHeaderName") as FrameworkElement;
            var blockItems = FindName("GridBlockItemsName") as FrameworkElement;

            double height = Math.Max(this.ActualHeight - blockHeader.RenderSize.Height, 0);
            double width = Math.Max(this.ActualWidth, 0);

            blockHeader.Width = width;

            blockItems.Width = width;
            blockItems.Height = height;

            ItemsCollection.Width = width;
            ItemsCollection.Height = height;

            DebugTraceSize(blockHeader);
            DebugTraceSize(blockItems);
            DebugTraceSize(ItemsCollection);
        }

        #endregion
    }
}
