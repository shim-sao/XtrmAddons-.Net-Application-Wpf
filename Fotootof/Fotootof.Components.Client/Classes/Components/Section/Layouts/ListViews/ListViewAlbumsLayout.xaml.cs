using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Layouts.Dialogs;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace Fotootof.Components.Client.Section.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums List View.
    /// </summary>
    public partial class ListViewAlbumsLayout : ListViewAlbumsControl
    {
        #region Properties

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection 
            => FindName<ListView>("ListViewCollectionName");

        #endregion


        
        #region Properties Event Handler

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
        public ListViewAlbumsLayout()
        {
            InitializeComponent();

            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                //AppNavigator.NavigateToPageAlbumServer(SelectedItem.PrimaryKey);
                MessageBoxs.NotImplemented($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
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

        /// <summary>
        /// Method called on items collection selection changed click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Counter_SelectedNumber.Text = SelectedItems.Count.ToString();

            if (SelectedItems.Count > 0)
            {
                DeleteCtrl.IsEnabled = true;
                EditCtrl.IsEnabled = true;
            }
            else
            {
                DeleteCtrl.IsEnabled = false;
                EditCtrl.IsEnabled = false;
            }
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
            GridBlockRootName.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            DebugTraceSize(GridBlockRootName);
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

            DebugTraceSize(StackPanelBlockHeaderName);
            DebugTraceSize(GridBlockItemsName);
            DebugTraceSize(ItemsCollection);
        }

        #endregion
    }
}
