using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Layouts.Windows.Slideshow;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Win32;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Interfaces;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class ListViewPicturesServer : ListViewPictures, IAlbum
    {
        #region Properties

        /// <summary>
        /// Property to access to the main items data grid.
        /// </summary>
        public AlbumEntity AlbumEntity
        {
            get => (AlbumEntity)GetValue(propertyAlbumEntity);
            set => SetValue(propertyAlbumEntity, value);
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty propertyAlbumEntity =
            DependencyProperty.Register
            (
                "AlbumEntity",
                typeof(AlbumEntity),
                typeof(ListViewPicturesServer),
                new PropertyMetadata(null)
            );

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Common Controls Pictures List View Constructor.
        /// </summary>
        static ListViewPicturesServer()
        {
            PropertyItems.OverrideMetadata(typeof(ListViewPicturesServer), new PropertyMetadata(new PictureEntityCollection()));
            Debug.Assert(!PropertyItems.OwnerType.Equals(typeof(ListViewPicturesServer)));
        }

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
        /// Method called on click event to add a new Album.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override async void OnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog box for picture selection.
            Microsoft.Win32.OpenFileDialog pfdb = PictureFileDialogBox.Show(true, Translation.DWords.DialogBoxTitle_PictureFileSelector);

            // Check if refence to the album is not null.
            if (AlbumEntity == null)
            {
                NullReferenceException ex = ExceptionBase.RefNull(nameof(AlbumEntity), AlbumEntity);
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
            MessageBase.IsBusy = true;
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
                //AlbumEntity = (await AlbumEntityCollection.DbUpdateAsync(AlbumEntity, AlbumEntity))[0];
                await AlbumEntityCollection.DbUpdateAsync(AlbumEntity, AlbumEntity);
            }

            //await AlbumEntity.Save();

            // Raise the on delete event.
            log.Warn($"Raising adding event with {pictAdded?.ToArray()?.Length ?? 0} Picture(s)...");
            RaiseOnAdd(pictAdded.ToArray());

            // Stop to busy application.
            log.Warn($"Ending adding {pictAdded?.ToArray()?.Length ?? 0} Picture(s).");
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on delete click event to delete a list of Pictures.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public async override void OnDeleteItems_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected items list is not null. 
            if (SelectedItems == null)
            {
                NullReferenceException ex = ExceptionBase.RefNull(nameof(SelectedItems), SelectedItems);
                log.Warn(ex.Output(), ex);
                MessageBase.Warning(ex.Output());
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
                MessageBase.IsBusy = true;
                log.Warn("Starting deleting Picture(s). Please wait...");

                // Delete item from database.
                //var pictDeleted = await PictureEntityCollection.DbDeleteAsync(SelectedItems);

                // Important : No need to defer for list view items refresh.
                log.Warn("Updating Picture(s) list view items...");

                /*foreach (PictureEntity item in SelectedItems)
                {
                     AlbumEntity.UnLinkPicture(item.PrimaryKey);
                }*/

                foreach (PictureEntity item in SelectedItems)
                {
                    AlbumEntity.Pictures.Remove(item);
                }


                AlbumEntity = (await AlbumEntityCollection.DbUpdateAsync(AlbumEntity, AlbumEntity))[0];
                
                // Refresh of the list view items source.
                log.Warn($"Refreshing list view with {AlbumEntity?.Pictures} Pictures...");
                ItemsCollection.Items.Refresh();

                // Raise the on delete event.
                log.Warn($"Raising deleted event with {SelectedItems?.Count} Pictures...");
                RaiseOnDelete(SelectedItems.ToArray());

                // Stop to busy application.
                log.Warn($"Ending of deleting  {SelectedItems?.Count} of Pictures.");
                MessageBase.IsBusy = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex.Output());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private async void ItemsCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PictureEntity pict = ItemsCollection.SelectedItem as PictureEntity;

            WindowSlideshow ws = new WindowSlideshow(Items, pict);
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private async void OnRefresh_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = DialogBase.FolderDialogBox();

            if(dlg == null)
            {
                return;
            }

            MessageBase.IsBusy = true;
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
                    IsChanged = DirSearch(pe, "OriginalPath", dlg.FileName);
                }

                if (!pe.PicturePath.IsNullOrWhiteSpace() && !File.Exists(pe.PicturePath))
                {
                    log.Info($"PicturePath : {pe.PicturePath}");
                    IsChanged = DirSearch(pe, "PicturePath", dlg.FileName);
                }

                if (!pe.ThumbnailPath.IsNullOrWhiteSpace() && !File.Exists(pe.ThumbnailPath))
                {
                    log.Info($"ThumbnailPath : {pe.ThumbnailPath}");
                    IsChanged = DirSearch(pe, "ThumbnailPath", dlg.FileName);
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
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="propertyPathName"></param>
        /// <param name="SelectedPath"></param>
        /// <returns></returns>
        private bool DirSearch(PictureEntity pe, string propertyPathName, string SelectedPath)
        {
            if(FileSearch(pe, propertyPathName, SelectedPath))
            {
                return true;
            }

            /*
             * Search on all drives : to be optimize something like that.
             * 
            string root = Path.GetPathRoot((string)pe.GetPropertyValue(propertyPathName));
            Trace.TraceInformation(root);

            if (FileSearch(pe, propertyPathName, root))
            {
                return true;
            }

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if(di.Name == root)
                {
                    continue;
                }

                Trace.TraceInformation(di.Name);
                return FileSearch(pe, propertyPathName, di.Name);
            }
            */

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="propertyPathName"></param>
        /// <param name="sDir"></param>
        /// <returns></returns>
        private bool FileSearch(PictureEntity pe, string propertyPathName, string sDir)
        {
            bool IsChanged = false;

            try
            {
                string filename = Path.Combine(sDir, Path.GetFileName(pe.GetPropertyValue(propertyPathName).ToString()));

                log.Info(string.Format("Searching Picture : {0}", filename));
                Trace.TraceInformation(filename);

                if (File.Exists(filename))
                {
                    FileInfo fi = new FileInfo(filename);
                    string lProp = propertyPathName.Replace("Path", "Length");
                    long? l = (long)pe.GetPropertyValue(lProp);

                    if (l == null || l == 0 || fi.Length == l)
                    {
                        pe.SetPropertyValue(propertyPathName, fi.FullName);
                        pe.SetPropertyValue(lProp, fi.Length);
                        return true;
                    }
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    IsChanged = FileSearch(pe, propertyPathName, d);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                log.Info(uae.Message);
            }
            catch (Exception e)
            {
                log.Error(e);
                MessageBase.Error(string.Format("Searching Picture {0} failed !\n\r {1}", propertyPathName, e.Message));
            }

            return IsChanged;
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
        /// </summary
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed event arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ItemsCollection_SelectionChanged(sender, e);

            //((TextBlock)FindName("Counter_SelectedNumber")).Text = SelectedItems.Count.ToString();
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
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
            double height = Math.Max(ActualHeight - Block_Header.RenderSize.Height, 0);

            Block_Items.Height = height;
            ItemsCollection.Height = height;

            TraceSize(Block_Items);
            TraceSize(Block_Header);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}
