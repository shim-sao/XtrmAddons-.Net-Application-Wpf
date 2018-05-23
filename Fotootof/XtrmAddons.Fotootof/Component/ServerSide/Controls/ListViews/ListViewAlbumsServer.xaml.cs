using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Server Side Controls Albums List View.
    /// </summary>
    public partial class ListViewAlbumsServer : ListViewAlbums
    {
        #region Properties

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => Button_Add;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => Button_Edit;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => Button_Delete;

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection
        {
            get => AlbumssCollection;
            set => AlbumssCollection = value;
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

        /// <summary>
        /// Method called on items collection selection changed click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Counter_SelectedNumber.Text = SelectedItems.Count.ToString();

            if(SelectedItems.Count > 0)
            {
                Button_Delete.IsEnabled = true;
                Button_Edit.IsEnabled = true;
            }
            else
            {
                Button_Delete.IsEnabled = false;
                Button_Edit.IsEnabled = false;
            }
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
            Block_Root.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            TraceSize(Block_Root);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockItems()
        {
            double height = Math.Max(this.ActualHeight - Block_Header.RenderSize.Height, 0);
            double width = Math.Max(this.ActualWidth, 0);

            Block_Header.Width = width;

            Block_Items.Width = width;
            Block_Items.Height = height;

            ItemsCollection.Width = width;
            ItemsCollection.Height = height;

            TraceSize(Block_Header);
            TraceSize(Block_Items);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}
