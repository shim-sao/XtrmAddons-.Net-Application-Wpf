using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Systems;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums DataGrid.
    /// </summary>
    public partial class DataGridAlbumsLayout : DataGridAlbumsControl
    {
        #region Properties

        /// <summary>
        /// Property to access to the entities collection.
        /// </summary>
        public AlbumEntityCollection Albums
        {
            get => (AlbumEntityCollection)GetValue(AlbumsProperty);
            set => SetValue(AlbumsProperty, value);
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty AlbumsProperty =
            DependencyProperty.Register
            (
                "Albums",
                typeof(AlbumEntityCollection),
                typeof(DataGridAlbumsLayout),
                new PropertyMetadata(new AlbumEntityCollection(false))
            );

        /// <summary>
        /// Property to access to the list of selected albums.
        /// </summary>
        public ObservableCollection<AlbumEntity> SelectedAlbums { get; } = new ObservableCollection<AlbumEntity>();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums DataGrid Constructor.
        /// </summary>
        public DataGridAlbumsLayout()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        /*public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(new AlbumEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                NotifyAdded(dlg.NewForm);
            }
            else
            {
                NotifyCanceled(dlg.NewForm);
            }
        }*/

        /// <summary>
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
       /* public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)(FindName("DataGridCollectionName") as DataGrid).SelectedItem;

            // Check if an AclGroup is founded. 
            if (entity != null)
            {
                // Show open file dialog box 
                WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(entity);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    NotifyChanged(dlg.NewForm);
                }
                else
                {
                    NotifyCanceled(dlg.NewForm);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                MessageBase.Warning(message);
            }
        }*/

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        /*public override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)(FindName("DataGridCollectionName") as DataGrid).SelectedItem;

            // Check if an AclGroup is founded. 
            if (entity != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.Album, entity.Name),
                    Translation.DWords.ApplicationName,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model collection.
                if (result == MessageBoxResult.Yes)
                {
                    NotifyDeleted(entity);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                MessageBase.Warning(message);
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            AlbumEntity a = cb.Tag as AlbumEntity;

            if(!SelectedAlbums.Contains(a))
            {
                SelectedAlbums.Add(a);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            AlbumEntity a = cb.Tag as AlbumEntity;

            if (SelectedAlbums.Contains(a))
            {
                SelectedAlbums.Remove(a);
            }
        }

        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        #endregion
    }
}
