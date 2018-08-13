using Fotootof.Collections.Entities;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Dialogs.Win32;
using Fotootof.Layouts.Windows.Slideshow;
using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture;

namespace Fotootof.Components.Server.Album.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class ListViewPicturesLayout : ListViewPicturesControl, IAlbumEntity
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="ListViewPicturesModel"/> of the layout.
        /// </summary>
        internal ListViewPicturesModel Model { get; private set; }

        /// <summary>
        /// Property to access to the <see cref="AlbumEntity"/>.
        /// </summary>
        public AlbumEntity AlbumEntity
        {
            get => (AlbumEntity)GetValue(propertyAlbumEntity);
            set => SetValue(propertyAlbumEntity, value);
        }

        /// <summary>
        /// Property Using a <see cref="DependencyProperty"/> as the backing store for <see cref="AlbumEntity"/>.
        /// </summary>
        public static readonly DependencyProperty propertyAlbumEntity =
            DependencyProperty.Register
            (
                "AlbumEntity",
                typeof(AlbumEntity),
                typeof(ListViewPicturesLayout),
                new PropertyMetadata(null)
            );

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Pictures List View Constructor.
        /// </summary>
        static ListViewPicturesLayout()
        {
            PropertyItems.OverrideMetadata(typeof(ListViewPicturesLayout), new PropertyMetadata(new PictureEntityCollection()));
            Debug.Assert(!PropertyItems.OwnerType.Equals(typeof(ListViewPicturesLayout)));
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Pictures List View Constructor.
        /// </summary>
        public ListViewPicturesLayout()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Model = new ListViewPicturesModel(this);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog box for picture selection.
            Microsoft.Win32.OpenFileDialog pfdb = PictureFileDialogBox.Show(true, Translation.DWords.DialogBoxTitle_PictureFileSelector);

            // Check if refence to the album is not null.
            if (AlbumEntity == null)
            {
                NullReferenceException ex = Exceptions.GetReferenceNull(nameof(AlbumEntity), AlbumEntity);
                log.Error(ex.Output(), ex);
                return;
            }

            // Check the response of the dialog box.
            // If null, nothing to do.
            if (pfdb == null)
            {
                log.Warn("Adding Pictures. Process canceled.");
                return;
            }

            // Start to busy application.
            MessageBoxs.IsBusy = true;
            log.Warn($"Starting adding {pfdb.FileNames.Length} Pictures. Please wait...");

            var album = AlbumEntity;
            var pictAdded = PictureEntityCollection.FromFileNames(pfdb.FileNames, ref album);

            bool updateAlbum = false;

            // Update Album informations.
            if (
                AlbumEntity.ThumbnailPicture?.PrimaryKey is null ||
                AlbumEntity.ThumbnailPicture.PrimaryKey == 0 ||
                !File.Exists(AlbumEntity?.ThumbnailPicture?.PicturePath)
                )
            {
                AlbumEntity.ThumbnailPictureId = pictAdded[0]?.PrimaryKey ?? 0;
                updateAlbum = true;
            }

            if (
                AlbumEntity.PreviewPicture?.PrimaryKey is null ||
                AlbumEntity.PreviewPicture.PrimaryKey == 0 ||
                !File.Exists(AlbumEntity?.PreviewPicture?.PicturePath)
                )
            {
                AlbumEntity.PreviewPictureId = pictAdded[0]?.PrimaryKey ?? 0;
                updateAlbum = true;
            }

            if (
                AlbumEntity.BackgroundPicture?.PrimaryKey is null ||
                AlbumEntity.BackgroundPicture.PrimaryKey == 0 ||
                !File.Exists(AlbumEntity?.BackgroundPicture?.PicturePath)
                )
            {
                AlbumEntity.BackgroundPictureId = pictAdded[0]?.PrimaryKey ?? 0;
                updateAlbum = true;
            }

            // Update the Album with new informations.
            if (updateAlbum)
            {
                // Updating the Album will create a new DataContext.
                AlbumEntity = (await AlbumEntityCollection.DbUpdateAsync(AlbumEntity, AlbumEntity))[0];
            }
            else
            {
                // Need this to Populate new Pictures to update the DataContext.
                var a = AlbumEntity.Pictures;
            }

            // Raise the on delete event.
            log.Warn($"Raising adding event with {pictAdded?.ToArray()?.Length ?? 0} Picture(s)...");
            NotifyAdded(pictAdded.ToArray());

            // Stop to busy application.
            log.Warn($"Ending adding {pictAdded?.ToArray()?.Length ?? 0} Picture(s).");
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on delete click event to delete a list of Pictures.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public async override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected items list is not null. 
            if (SelectedItems == null)
            {
                NullReferenceException ex = Exceptions.GetReferenceNull(nameof(SelectedItems), SelectedItems);
                log.Warn(ex.Output(), ex);
                MessageBoxs.Warning(ex.Output());
                return;
            }

            // Alert user for acceptation.
            MessageBoxResult result = MessageBox.Show
            (
                String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.Picture, SelectedItems.Count),
                Translation.DWords.ApplicationName,
                MessageBoxButton.YesNoCancel
            );

            // If not accept, do nothing at this moment.
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            // If accepted, try to update page model collection.
            try
            {
                // Start to busy application.
                MessageBoxs.IsBusy = true;
                log.Warn("Starting deleting Picture(s). Please wait...");

                // Important : No need to defer for list view items refresh.
                log.Warn("Updating Picture(s) list view items...");
                
                if (log.IsDebugEnabled)
                {
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums = {AlbumEntity?.PicturesInAlbums?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepPKeys.Count = {AlbumEntity?.PicturesInAlbums?.DepPKeys?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepPKeysRemoved.Count = {AlbumEntity?.PicturesInAlbums?.DepPKeysRemoved?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepReferences.Count = {AlbumEntity?.PicturesInAlbums?.DepReferences?.Count ?? 0}");
                }

                // Removing Picture the dependency from the Album.
                foreach (PictureEntity item in SelectedItems)
                {
                    AlbumEntity.PicturesPKs.Remove(item.PrimaryKey);
                    AlbumEntity.Pictures.Remove(item);
                }

                if (log.IsDebugEnabled)
                {
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums = {AlbumEntity?.PicturesInAlbums?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepPKeys.Count = {AlbumEntity?.PicturesInAlbums?.DepPKeys?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepPKeysRemoved.Count = {AlbumEntity?.PicturesInAlbums?.DepPKeysRemoved?.Count ?? 0}");
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : AlbumEntity.PicturesInAlbums.DepReferences.Count = {AlbumEntity?.PicturesInAlbums?.DepReferences?.Count ?? 0}");
                }

                // Saving new album informations into the database.
                AlbumEntity = (await AlbumEntityCollection.DbUpdateAsync(AlbumEntity, AlbumEntity))[0];
                
                // Refresh of the list view items source.
                log.Warn($"Refreshing list view with {AlbumEntity?.Pictures} Pictures...");
                ItemsCollection.Items.Refresh();

                // Raise the on delete event.
                log.Warn($"Raising deleted event with {SelectedItems?.Count} Pictures...");
                NotifyDeleted(SelectedItems.ToArray());

                // Stop to busy application.
                log.Warn($"Ending of deleting  {SelectedItems?.Count} of Pictures.");
                MessageBoxs.IsBusy = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex.Output());
            }
        }

        /// <summary>
        /// Method called on collection item mouse double click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private async void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           if(ItemsCollection.SelectedItem is PictureEntity pict)
            {
                WindowSlideshowLayout ws = new WindowSlideshowLayout(Items, pict);
                ws.Show();
                ws.Activate();
                ws.Topmost = true;
                await Task.Delay(10);
                ws.Topmost = false;
                ws.Focus();
            }
            else
            {
                NullReferenceException ex = Exceptions.GetReferenceNull(nameof(ItemsCollection.SelectedItem), ItemsCollection.SelectedItem);
                log.Debug(ex);
                MessageBoxs.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private async void OnRefresh_Click(object sender, RoutedEventArgs e)
        {
            string fileName = DialogBase.FolderDialogBox() as string;

            if(fileName == null)
            {
                return;
            }

            MessageBoxs.IsBusy = true;
            log.Info("Refreshing pictures list. Please wait...");

            await Task.Delay(3000);
            
            List<PictureEntity> newItems = new List<PictureEntity>();
            int index = 0;

            foreach(PictureEntity pe in Items)
            {
                log.Info($"Processing on picture [{pe.PrimaryKey}]{pe.Name}. Please wait...");
                bool IsChanged = false;

                if (!pe.OriginalPath.IsNullOrWhiteSpace() && !File.Exists(pe.OriginalPath))
                {
                    log.Info($"OriginalPath : {pe.OriginalPath}");
                    IsChanged = Model.DirSearch(pe, "OriginalPath", fileName);
                }

                if (!pe.PicturePath.IsNullOrWhiteSpace() && !File.Exists(pe.PicturePath))
                {
                    log.Info($"PicturePath : {pe.PicturePath}");
                    IsChanged = Model.DirSearch(pe, "PicturePath", fileName);
                }

                if (!pe.ThumbnailPath.IsNullOrWhiteSpace() && !File.Exists(pe.ThumbnailPath))
                {
                    log.Info($"ThumbnailPath : {pe.ThumbnailPath}");
                    IsChanged = Model.DirSearch(pe, "ThumbnailPath", fileName);
                }

                if (pe.OriginalPath.IsNullOrWhiteSpace())
                {
                    pe.OriginalPath = pe.PicturePath;
                    IsChanged = true;
                }

                if (pe.PicturePath.IsNullOrWhiteSpace())
                {
                    pe.PicturePath = pe.OriginalPath;
                    IsChanged = true;
                }

                if (pe.ThumbnailPath.IsNullOrWhiteSpace())
                {
                    pe.ThumbnailPath = pe.PicturePath;
                    IsChanged = true;
                }

                if(IsChanged)
                {
                    newItems.Add(pe);
                }

                index++;
            }

            if(newItems.Count > 0)
            {
                PictureEntityCollection.DbUpdateAsync(newItems, null);
            }

            ItemsCollection.ItemsSource = new PictureEntityCollection(newItems);

            log.Info("Saving pictures list. Done...");
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnImageRefresh_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
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
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ItemsCollection_SelectionChanged(sender, e);
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockRoot()
        {
            var blockRoot = (FrameworkElement)FindName("GridBlockRootName");
            blockRoot.Arrange(new Rect(new Size(ActualWidth, ActualHeight)));
            DebugTraceSize(blockRoot);
        }

        /// <summary>
        /// Method to arrange or resize the items block.
        /// </summary>
        private void ArrangeBlockItems()
        {
            var blockHeader = (FrameworkElement)FindName("StackPanelBlockHeaderName");
            var blockItems = (FrameworkElement)FindName("GridBlockItemsName");

            double height = Math.Max(ActualHeight - blockHeader.RenderSize.Height, 0);

            blockItems.Height = height;
            ItemsCollection.Height = height;

            DebugTraceSize(blockItems);
            DebugTraceSize(blockHeader);
            DebugTraceSize(ItemsCollection);
        }

        #endregion
    }
}
