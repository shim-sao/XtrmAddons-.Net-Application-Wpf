using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Layouts.Windows.DataGrids.AlbumsDataGrid;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Server Controls Albums List View.
    /// </summary>
    public partial class ListViewStoragesServer : ListViewStorages
    {
        #region Properties

        /// <summary>
        /// Property to access to the user control model.
        /// </summary>
        public ListViewStoragesServerModel Model { get; private set; }

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => (Control)FindName("Button_Add");

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => (Control)FindName("Button_Edit");

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => (Control)FindName("Button_Delete");

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection => FindName<ListView>("ItemsCollectionStorages");

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event SelectionChangedEventHandler ImageSize_SelectionChanged
        {
            add => ((ComboBox)FindName("ComboBoxImageSizeName")).SelectionChanged += value;
            remove => ((ComboBox)FindName("ComboBoxImageSizeName")).SelectionChanged -= value;
        }

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event MouseButtonEventHandler ItemsCollection_MouseDoubleClick
        {
            add => ItemsCollection.MouseDoubleClick += value;
            remove => ItemsCollection.MouseDoubleClick -= value;
        }

        /// <summary>
        /// 
        /// </summary>
        public TextBlock CounterTotalImages => Counter_TotalImages;

        /// <summary>
        /// 
        /// </summary>
        public TextBlock CounterTotalDirectories => Counter_TotalDirectories;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewStoragesServer()
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
        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Model = new ListViewStoragesServerModel(this)
            {
                Items = ItemsCollection.ItemsSource as StorageCollection
            };

            ComboBox selectorImgSize = ((ComboBox)FindName("ComboBoxImageSizeName"));
            selectorImgSize.SelectedIndex = AppSettings.GetInt(selectorImgSize, "SelectedIndex", selectorImgSize.SelectedIndex);
        }

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
                bool find = false;
                foreach(var sim in SelectedItems)
                {
                    if(sim.IsPicture)
                    {
                        find = true;
                        break;
                    }
                }

                Button_AddPicturesToAlbum.IsEnabled = find;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
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
                        // Add Picture to the list for Pictures.
                        pictures.Add(picture);
                    }
                }

                // Insert Pictures into the database.
                IEnumerable<AlbumEntity> albums = dlg.SelectedAlbums.ToList();
                PictureEntityCollection.DbInsert(pictures, ref albums);
            }
            
            // Clear user selection.
            ItemsCollection.SelectedItems.Clear();
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
        /// Method called on select all click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void OnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.UnselectAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed changed event arguments.</param>
        private void OnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ItemsCollection.Visibility = Visibility.Hidden;
            ItemsCollection.Items.Refresh();
            ItemsCollection.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        private void ComboBoxImageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox selectorImgSize = ((ComboBox)FindName("ComboBoxImageSizeName"));

            if (selectorImgSize != null)
            {
                AppSettings.SaveInt(selectorImgSize, "SelectedIndex", selectorImgSize.SelectedIndex);
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
        /// Method to arrange the root block size on control resize.
        /// </summary>
        private void ArrangeBlockRoot()
        {
            // Get the framework element to resize.
            Grid blockRoot = FindName("GridBlockRootName") as Grid;

            // Resize required element.
            blockRoot.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            TraceSize(GridBlockRootName);
        }

        /// <summary>
        /// Method to arrange the items block size on control resize.
        /// </summary>
        private void ArrangeBlockItems()
        {
            // Get the framework elements to resize.
            StackPanel blockHeader = FindName("StackPanelBlockHeaderName") as StackPanel;

            // Get new sizes.
            double height = Math.Max(this.ActualHeight - blockHeader.RenderSize.Height, 0);

            // Resize required elements.
            GridBlockItemsName.Height = height;
            ItemsCollection.Height = height;
            
            // Trace with DEBUG_SIZE
            TraceSize(GridBlockItemsName);
            TraceSize(blockHeader);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}