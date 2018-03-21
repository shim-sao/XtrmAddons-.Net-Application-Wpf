using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Server Controls Albums List View.
    /// </summary>
    public partial class ListViewDirectoriesServer : ListViewDirectories
    {
        #region Properties
        public override Control AddControl => null;

        public override Control EditControl => null;

        public override Control DeleteControl => null;

        public override ListView ItemsCollection { get => ItemsCollectionDirectories; set => ItemsCollectionDirectories = value; }
        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewDirectoriesServer()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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

        /// <summary>
        /// Method called on items collection selection changed click event.
        /// </summary
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        /*private void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }*/

        #endregion
    }
}
