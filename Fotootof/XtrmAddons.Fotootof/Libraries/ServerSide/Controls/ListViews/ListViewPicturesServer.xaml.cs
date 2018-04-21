using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Slideshow;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class ListViewPicturesServer : ListViewPictures
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
        public override ListView ItemsCollection { get => PicturesCollection; set => PicturesCollection = value; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Pictures List View Constructor.
        /// </summary>
        public ListViewPicturesServer()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PictureEntity pict = ItemsCollection.SelectedItem as PictureEntity;
            List<PictureEntity> pictList = ItemsCollection.Items.SourceCollection as List<PictureEntity>;

            //AppNavigator.NavigateToPageSlideshowServer(new PictureEntityCollection(pictList));

            WindowSlideshow ws = new WindowSlideshow(new PictureEntityCollection(pictList), pict);
            ws.Show();
            ws.Activate();
            ws.Topmost = true;
            await Task.Delay(10);
            ws.Topmost = false;
            ws.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageRefresh_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImage_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.SelectAll();
        }

        /// <summary>
        /// Method called on unselect all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void UnselectAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.UnselectAll();
        }

        /// <summary>
        /// Method called on items collection selection changed click event.
        /// </summary
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Counter_SelectedNumber.Text = SelectedItems.Count.ToString();
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            Block_Items.Height = height;
            ItemsCollection.Height = height;

            TraceSize(Block_Items);
            TraceSize(Block_Header);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}
