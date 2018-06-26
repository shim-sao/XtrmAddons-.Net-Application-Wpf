using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewAlbum;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Layouts.Windows.Slideshow;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Win32;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class ListViewPicturesServer : ListViewPictures
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public PageAlbumModel Model
        {
            get
            {
                if(!(DataContext is PageAlbumModel))
                {
                    return null;
                }

                return (PageAlbumModel)DataContext;
            }
        }

        #endregion



        #region Constructor

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
            Microsoft.Win32.OpenFileDialog pfdb = PictureFileDialogBox.Show(true, Translation.DWords.DialogBoxTitle_PictureFileSelector);

            if(Model.Album == null)
            {
                NullReferenceException ex = new NullReferenceException(nameof(Model.Album));
                log.Error(ex.Output(), ex);
                return;
            }

            if(pfdb != null)
            {
                List<PictureEntity> pictures = new List<PictureEntity>();

                foreach (string s in pfdb.FileNames)
                {
                    PictureEntity item = (new StorageInfoModel(new FileInfo(s))).ToPicture();

                    // Check if storage information is and return a picture information.
                    if (item != null)
                    {
                        // Add Picture to the list for Pictures.
                        pictures.Add(item);
                        Model.Pictures.Add(item);
                    }
                }

                // Create a list of Albums to update.
                var albums = new AlbumEntity[] { Model.Album };

                // Insert Pictures into the database.
                var pictAdded = PictureEntityCollection.DbInsert(pictures, albums);

                bool updateAlbum = false;

                // Update Album informations.
                if (Model.Album.ThumbnailPictureId == 0)
                {
                    Model.Album.ThumbnailPictureId = pictAdded[0].PrimaryKey;
                    updateAlbum = true;
                }
                if (Model.Album.PreviewPictureId == 0)
                {
                    Model.Album.PreviewPictureId = pictAdded[0].PrimaryKey;
                    updateAlbum = true;
                }
                if (Model.Album.BackgroundPictureId == 0)
                {
                    Model.Album.BackgroundPictureId = pictAdded[0].PrimaryKey;
                    updateAlbum = true;
                }

                // Update the Album with new informations.
                if (updateAlbum)
                {
                    await AlbumEntityCollection.DbUpdateAsync(albums, null);
                }

                // Reload page.
                AppNavigator.NavigateToPageAlbumServer(Model.Album.PrimaryKey);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            List<PictureEntity> pictList = e.NewValue.GetPropertyValue("Pictures") as List<PictureEntity>;
            Items = new PictureEntityCollection(pictList);
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
            ((TextBlock)FindName("Counter_SelectedNumber")).Text = SelectedItems.Count.ToString();
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
