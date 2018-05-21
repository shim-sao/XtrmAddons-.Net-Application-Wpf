using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Component.ClientSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums List View.
    /// </summary>
    public partial class ListViewAlbumsClient : ListViewAlbums
    {
        #region Properties

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        public override ListView ItemsCollection { get => LvItemsCollection; set => LvItemsCollection = value; }

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
        public ListViewAlbumsClient()
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

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
