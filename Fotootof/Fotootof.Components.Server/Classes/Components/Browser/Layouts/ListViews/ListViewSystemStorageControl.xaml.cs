using Fotootof.Collections;
using Fotootof.Collections.Entities;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Libraries.Controls.ListViews;
using Fotootof.Libraries.Models.Systems;
using Fotootof.Libraries.Systems;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace Fotootof.Components.Server.Browser.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Server Controls Albums List View.
    /// </summary>
    public partial class ListViewSystemStorageControl : ListViewStorages
    {
        #region Properties

        /// <summary>
        /// Property to access to the user control model.
        /// </summary>
        internal ListViewSystemStorageModel Model { get; private set; }

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection
            => FindName<ListView>("ItemsCollectionStorages");

        /// <summary>
        /// Property proxy to the combo box selection changed event handler.
        /// </summary>
        public event SelectionChangedEventHandler ImageSize_SelectionChanged
        {
            add => (FindName<ComboBox>("ComboBoxImageSizeName")).SelectionChanged += value;
            remove => (FindName<ComboBox>("ComboBoxImageSizeName")).SelectionChanged -= value;
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
        public TextBlock CounterTotalImages 
            => FindName<TextBlock>("TextBlockCounterTotalImagesName");

        /// <summary>
        /// 
        /// </summary>
        public TextBlock CounterTotalDirectories
            => FindName<TextBlock>("TextBlockCounterTotalDirectoriesName");

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Albums List View Constructor.
        /// </summary>
        public ListViewSystemStorageControl()
        {
            InitializeComponent();
            ItemsCollection.KeyDown += ItemsCollection.AddKeyDownSelectAllItems;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on user control loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/></param>
        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Model = new ListViewSystemStorageModel(this)
            {
                Items = ItemsCollection.ItemsSource as CollectionStorage
            };

            ComboBox selectorImgSize = (FindName<ComboBox>("ComboBoxImageSizeName"));
            selectorImgSize.SelectedIndex = AppSettings.GetInt(selectorImgSize, "SelectedIndex", selectorImgSize.SelectedIndex);
        }

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The selection changed arguments <see cref="SelectionChangedEventArgs"/>.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ItemsCollection_SelectionChanged(sender, e);

            if (SelectedItems.Count == 0)
            {
                (FindName("ButtonAddPicturesToAlbumName") as Button).IsEnabled = false;
            }
            else
            {
                bool find = false;
                foreach (var sim in SelectedItems)
                {
                    if (sim.IsPicture)
                    {
                        find = true;
                        break;
                    }
                }

                (FindName("ButtonAddPicturesToAlbumName") as Button).IsEnabled = find;
            }
        }

        /// <summary>
        /// Method called on add pictures to album click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Routed event arguments <see cref="RoutedEventArgs"/></param>
        private void OnAddPicturesToAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            DataGridAlbumsWindow dlg = new DataGridAlbumsWindow();
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
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();
        }

        /// <summary>
        /// Method to arrange the root block size on control resize.
        /// </summary>
        private void ArrangeBlockRoot()
        {
            Grid blockRoot = FindName("GridBlockRootName") as Grid;
            blockRoot.Arrange(new Rect(new Size(ActualWidth, ActualHeight)));
            DebugTraceSize(blockRoot);
        }

        /// <summary>
        /// Method to arrange the items block size on control resize.
        /// </summary>
        private void ArrangeBlockItems()
        {
            // Get the framework elements to resize.
            StackPanel blockHeader = FindName("StackPanelBlockHeaderName") as StackPanel;
            Grid blockItems = FindName("GridBlockItemsName") as Grid;

            // Get new sizes.
            double height = Math.Max(this.ActualHeight - blockHeader.RenderSize.Height, 0);

            // Resize required elements.
            blockItems.Height = height;
            ItemsCollection.Height = height;

            // Trace with DEBUG_SIZE
            DebugTraceSize(blockItems);
            DebugTraceSize(blockHeader);
            DebugTraceSize(ItemsCollection);
        }

        #endregion
    }
}