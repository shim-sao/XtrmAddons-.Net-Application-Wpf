using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Windows.DataGrids.AlbumsDataGrid;
using XtrmAddons.Net.Windows.Controls.Extensions;
using System.Linq;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Server Controls Albums List View.
    /// </summary>
    public partial class ListViewStoragesServer : ListViewStorages
    {
        #region Properties

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => null;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => null;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => null;

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection
        {
            get => ItemsCollectionStorages;
            set => ItemsCollectionStorages = value;
        }

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event SelectionChangedEventHandler ImageSize_SelectionChanged
        {
            add => ComboBox_ImageSize.SelectionChanged += value;
            remove => ComboBox_ImageSize.SelectionChanged -= value;
        }

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event MouseButtonEventHandler ItemsCollection_MouseDoubleClick
        {
            add => ItemsCollection.MouseDoubleClick += value;
            remove => ItemsCollection.MouseDoubleClick -= value;
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewStoragesServer()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
            //SelectionChanged += ItemsCollection_SelectionChanged;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ItemsCollection_SelectionChanged(sender, e);

            if (SelectedItems.Count == 0)
            {
                Button_AddPicturesToAlbum.IsEnabled = false;
            }
            else
            {
                Button_AddPicturesToAlbum.IsEnabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddPicturesToAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowDataGridAlbums dlg = new WindowDataGridAlbums();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Do nothing, no files are selected.
                if (dlg.SelectedAlbums.Count == 0)
                {
                    return;
                }

                // Process adding for each selected storage informations.
                List<PictureEntity> pictures = new List<PictureEntity>();
                foreach (StorageInfoModel item in ItemsCollection.SelectedItems)
                {
                    // Create Picture entity.
                    PictureEntity picture = item.ToPicture();

                    // Check if storage information is and return a picture information.
                    if (picture != null)
                    {
                        /*
                        // Process adding for each albums.
                        foreach (AlbumEntity a in dlg.SelectedAlbums)
                        {
                            // Try to find Picture and Album dependency
                            PicturesInAlbums dependency = (new List<PicturesInAlbums>(picture.PicturesInAlbums)).Find(p => p.AlbumId == a.AlbumId);

                            // Associate Picture to the Album if not already set.
                            if (dependency == null)
                            {
                                picture.PicturesInAlbums.Add(
                                    new PicturesInAlbums
                                    {
                                        AlbumId = a.AlbumId,
                                        Ordering = picture.PicturesInAlbums.Count + 1
                                    }
                                );
                            }
                        }
                        */

                        // Add Picture to the list for Pictures final saving.
                        pictures.Add(picture);
                    }
                }

                // Insert Pictures into the database.
                PictureEntityCollection.DbInsert(pictures, dlg.SelectedAlbums.ToList());
            }
            
            // Clear user selection.
            ItemsCollection.SelectedItems.Clear();
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
            double height = this.ActualHeight - Block_Header.RenderSize.Height;

            Block_Items.Height = height;
            ItemsCollection.Height = height;
            
            TraceSize(Block_Items);
            TraceSize(Block_Header);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}