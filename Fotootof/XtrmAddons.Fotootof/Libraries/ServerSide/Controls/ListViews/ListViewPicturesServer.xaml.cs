using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Slideshow;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Common Controls Pictures List View.
    /// </summary>
    public partial class ListViewPicturesServer : ListViewPictures
    {
        #region Properties

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => Button_Add;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => Button_Edit;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => Button_Delete;

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public override ListView ItemsCollection
        {
            get => PicturesCollection;
            set => PicturesCollection = value;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            List<PictureEntity> pictList = e.NewValue.GetPropertyValue("Pictures") as List<PictureEntity>;
            Items = new PictureEntityCollection(pictList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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
        /// <param name="multiselect"></param>
        /// <returns></returns>
        protected CommonOpenFileDialog FolderDialogBox(bool multiselect = false, Environment.SpecialFolder folder = Environment.SpecialFolder.MyDocuments)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = folder.ToString();

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = multiselect;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dlg;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnRefresh_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = FolderDialogBox();

            if(dlg == null)
            {
                return;
            }
            
            AppLogger.Info("Refreshing pictures list. Please wait...", true);
            await Task.Delay(3000);
            
            List<PictureEntity> newItems = new List<PictureEntity>();
            int index = 0;

            foreach(PictureEntity pe in Items)
            {
                AppLogger.Info(string.Format("Processing on picture [{0}]{1}. Please wait...", pe.PrimaryKey, pe.Name), true);
                bool IsChanged = false;

                if (!pe.OriginalPath.IsNullOrWhiteSpace() && !File.Exists(pe.OriginalPath))
                {
                    AppLogger.Info(string.Format("OriginalPath : {0}", pe.OriginalPath));
                    IsChanged = DirSearch(pe, "OriginalPath", dlg.FileName);
                }

                if (!pe.PicturePath.IsNullOrWhiteSpace() && !File.Exists(pe.PicturePath))
                {
                    AppLogger.Info(string.Format("PicturePath : {0}", pe.PicturePath));
                    IsChanged = DirSearch(pe, "PicturePath", dlg.FileName);
                }

                if (!pe.ThumbnailPath.IsNullOrWhiteSpace() && !File.Exists(pe.ThumbnailPath))
                {
                    AppLogger.Info(string.Format("ThumbnailPath : {0}", pe.ThumbnailPath));
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

            AppLogger.InfoAndClose("Saving pictures list. Done...", true);
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
                Trace.TraceInformation(filename);
                AppLogger.Info(string.Format("Searching Picture : {0}", filename));

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
                AppLogger.Info(uae.Message);
            }
            catch (Exception e)
            {
                AppLogger.Error(string.Format("Searching Picture {0} failed !\n {1}", propertyPathName, e.Message), true);
                log.Error(e);
            }

            return IsChanged;
        }

        //private bool FileSearch(PictureEntity pe, string propertyPathName)
        //{
        //    try
        //    {
        //        foreach(DriveInfo di in DriveInfo.GetDrives())
        //        {
        //            string filename = Path.GetFileName(pe.GetPropertyValue(propertyPathName).ToString());
        //            string[] files = Directory.GetFiles(di.Name, filename, SearchOption.AllDirectories);
                    
        //            foreach (string f in files)
        //            {
        //                FileInfo fi = new FileInfo(f);

        //                long? l = (long)pe.GetPropertyValue(propertyPathName.Replace("Path", "Length"));

        //                if (l == null || l == 0 || fi.Length == l)
        //                {
        //                    pe.SetPropertyValue(propertyPathName, fi.FullName);
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        AppLogger.Error(string.Format("Serching Picture {0} failed !", propertyPathName), true);
        //        log.Error(e);
        //    }

        //    return false;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageRefresh_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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
            Counter_SelectedNumber.Text = SelectedItems.Count.ToString();
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
            double height = Math.Max(this.ActualHeight - Block_Header.RenderSize.Height, 0);

            Block_Items.Height = height;
            ItemsCollection.Height = height;

            TraceSize(Block_Items);
            TraceSize(Block_Header);
            TraceSize(ItemsCollection);
        }

        #endregion
    }
}
