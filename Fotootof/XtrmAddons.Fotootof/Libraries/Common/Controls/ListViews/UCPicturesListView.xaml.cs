using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class UCPicturesListView : ControlBaseCollection
    {
        #region Properties

        public override Control AddControl => ButtonAdd;

        public override Control EditControl => ButtonEdit;

        public override Control DeleteControl => ButtonDelete;

        /// <summary>
        /// Property to access to the entities collection.
        /// </summary>
        public PictureEntityCollection Pictures
        {
            get { return (PictureEntityCollection)GetValue(PicturesProperty); }
            set { SetValue(PicturesProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty PicturesProperty =
            DependencyProperty.Register
            (
                "Pictures",
                typeof(PictureEntityCollection),
                typeof(UCPicturesListView),
                new PropertyMetadata(new PictureEntityCollection())
            );

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Pictures List View Constructor.
        /// </summary>
        public UCPicturesListView()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Picture.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            /*// Show open file dialog box 
            WindowAlbumForm dlg = new WindowAlbumForm(PageBase);
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewEntity);
            }
            else
            {
                RaiseOnCancel(dlg.NewEntity);
            }*/
        }

        /// <summary>
        /// Method called on edit click to navigate to a Picture edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            /*Album entity = (Album)AlbumsCollection.SelectedItem;

            // Check if an AclGroup is founded. 
            if (entity != null)
            {
                // Show open file dialog box 
                WindowAlbumForm dlg = new WindowAlbumForm(PageBase, entity);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewEntity);
                }
                else
                {
                    RaiseOnCancel(dlg.NewEntity);
                }
            }
            else
            {
                UILogs.Warning(string.Format("{0} not found !", typeof(Album).Name), true, true);
            }*/
        }

        /// <summary>
        /// Method called on delete click to delete a Picture.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            /*Album entity = (Album)AlbumsCollection.SelectedItem;

            // Check if an AclGroup is founded. 
            if(entity != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Properties.Resources.MessageBox_Acceptation_DeleteGeneric, Properties.Resources.Album, entity.Name),
                    Properties.Resources.ApplicationName ,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model collection.
                if(result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(entity);
                }
            }
            else
            {
                UILogs.Warning(string.Format("{0} not found !", typeof(Album).Name), true, true);
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*ListView lv = (ListView)sender;
            Album entity = (Album)lv.SelectedItem;
            UINavigation.NavigateToPageAlbum(entity.PrimaryKey);*/

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAlbumnRefresh_Click(object sender, RoutedEventArgs e)
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

        #endregion

        private void SelectAllItems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearItemsSelection_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
