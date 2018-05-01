using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Albums DataGrid.
    /// </summary>
    public partial class UCAlbumsDataGrid : ControlBaseCollection
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override Control AddControl => ButtonAdd;

        /// <summary>
        /// 
        /// </summary>
        public override Control EditControl => ButtonEdit;

        /// <summary>
        /// 
        /// </summary>
        public override Control DeleteControl => ButtonDelete;

        /// <summary>
        /// Property to access to the entities collection.
        /// </summary>
        public AlbumEntityCollection Albums
        {
            get { return (AlbumEntityCollection)GetValue(AlbumsProperty); }
            set { SetValue(AlbumsProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty AlbumsProperty =
            DependencyProperty.Register
            (
                "Albums",
                typeof(AlbumEntityCollection),
                typeof(UCAlbumsDataGrid),
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
        public UCAlbumsDataGrid()
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
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAlbum dlg = new WindowFormAlbum(new AlbumEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewForm);
            }
            else
            {
                RaiseOnCancel(dlg.NewForm);
            }
        }

        /// <summary>
        /// Method called on edit click to navigate to a Album edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)AlbumsCollection.SelectedItem;

            // Check if an AclGroup is founded. 
            if (entity != null)
            {
                // Show open file dialog box 
                WindowFormAlbum dlg = new WindowFormAlbum(entity);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewForm);
                }
                else
                {
                    RaiseOnCancel(dlg.NewForm);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                AppLogger.Warning(message);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Album.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)AlbumsCollection.SelectedItem;

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
                    RaiseOnDelete(entity);
                }
            }
            else
            {
                string message = string.Format("{0} not found !", typeof(AlbumEntity).Name);
                log.Warn(message);
                AppLogger.Warning(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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
        /// <param name="sender"></param>
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

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
