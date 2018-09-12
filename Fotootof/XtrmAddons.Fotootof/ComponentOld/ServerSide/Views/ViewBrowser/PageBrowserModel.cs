using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using ImgSize = XtrmAddons.Fotootof.Lib.Base.Classes.Images.ImageSize;

namespace XtrmAddons.Fotootof.ComponentOld.ServerSide.Views.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View Model.</para>
    /// </summary>
    internal class PageBrowserModel : PageBaseModel<PageBrowser>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the thumbnail image size to display. Vignette by default.
        /// </summary>
        public Size imageSize
            = new Size { Height = ImgSize.Vignette.ToDouble(), Width = ImgSize.Vignette.ToDouble() };

        /// <summary>
        /// Variable collection of directories and files informations.
        /// </summary>
        private CollectionStorage filesCollection;

        /// <summary>
        /// Variable to store the current <see cref="DriveInfo"/> or <see cref="DirectoryInfo"/> browsed.
        /// </summary>
        public object currentItem;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the stack of the preview <see cref="DriveInfo"/> or <see cref="DirectoryInfo"/> browsed.
        /// </summary>
        public Stack<object> PreviewStack { get; set; } = new Stack<object>();

        /// <summary>
        /// Property to access to the stack of the next <see cref="DriveInfo"/> or <see cref="DirectoryInfo"/> browsed.
        /// </summary>
        public Stack<object> NextStack { get; set; } = new Stack<object>();

        /// <summary>
        /// Property to access to the current <see cref="DriveInfo"/> or <see cref="DirectoryInfo"/> browsed.
        /// </summary>
        public object CurrentItem
        {
            get => currentItem;
            set
            {
                if (currentItem != value)
                {
                    currentItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the image size to display.
        /// </summary>
        public Size ImageSize
        {
            get => imageSize;
            set
            {
                if (imageSize != value)
                {
                    imageSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the collection of directories and files informations.
        /// </summary>
        public CollectionStorage FilesCollection
        {
            get => filesCollection;
            set
            {
                if (filesCollection != value)
                {
                    filesCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Browser Model Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public PageBrowserModel(PageBrowser pageBase) : base(pageBase) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to clear the <see cref="StorageCollection"/> of files displayed in the view.
        /// </summary>
        public void ClearFilesCollection()
        {
            // Clear model files collection.
            FilesCollection?.Clear();

            // Clear picture memory cache.
            PictureMemoryCache.Clear();

            // Fix increase memory.
            MemoryManager.fixMemoryLeak();
        }

        /// <summary>
        /// Method to get the current <see cref="DirectoryInfo"/> browsed.
        /// </summary>
        /// <returns>A <see cref="DirectoryInfo"/> or null if not found.</returns>
        public DirectoryInfo GetCurrentDirectoryInfo()
        {
            DirectoryInfo di = null;

            if (CurrentItem != null)
            {
                if (CurrentItem.GetType().Equals(typeof(DriveInfo)))
                {
                    di = new DirectoryInfo(((DriveInfo)CurrentItem).RootDirectory.FullName);
                }
                else
                {
                    di = CurrentItem as DirectoryInfo;
                }
            }

            return di;
        }

       
        public void ChangeCurrentItem(object item)
        {
            NextStack.Clear();
            PreviewStack.Push(CurrentItem);
            CurrentItem = item;
        }


        /// <summary>
        /// Method to load files informations.
        /// </summary>
        /// <param name="infoFiles">A list of files</param>
        public void LoadDirectoriesInfo(DirectoryInfo[] dirInfos)
        {
            if (dirInfos == null)
            {
                return;
            }

            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                var item = new StorageInfoModel(dirInfo) { ImageSize = ImageSize };
                FilesCollection.Add(item);
            }
        }

        /// <summary>
        /// Method to load files informations.
        /// </summary>
        /// <param name="infoFiles">A list of files</param>
        public void LoadFilesInfo(FileInfo[] infoFiles)
        {
            if (infoFiles == null)
            {
                return;
            }

            foreach (FileInfo fileInfo in infoFiles)
            {
                // Limit view to a selection of extensions.
                //if (ConverterPictureBase.Extensions.Contains(fileInfo.Extension.ToLower()))
                //{
                //    StorageInfoModel item = new StorageInfoModel(fileInfo) { ImageSize = ImageSize };
                //    FilesCollection.Add(item);
                //}

                StorageInfoModel item = new StorageInfoModel(fileInfo) { ImageSize = ImageSize };
                FilesCollection.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public void LoadInfos()
        {
            DirectoryInfo di = null;
            if (CurrentItem != null)
            {
                ClearFilesCollection();
                di = GetCurrentDirectoryInfo();

                LoadDirectoriesInfo(GetInfoDirectories(di));
                LoadFilesInfo(GetInfoFiles(di));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        public FileInfo[] GetInfoFiles(DirectoryInfo dirInfo, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            FileInfo[] info = null;

            if (dirInfo != null)
            {
                try
                {
                    info = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                }
                catch (Exception e)
                {
                    log.Error(e);
                    MessageBase.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        public DirectoryInfo[] GetInfoDirectories(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] info = null;

            if (dirInfo != null)
            {
                try
                {
                    info = dirInfo.GetDirectories();
                }
                catch (Exception e)
                {
                    log.Error(e);
                    MessageBase.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// Method to display header title of directory.
        /// </summary>
        /// <param name="dirInfo">A directory info.</param>
        public string GetText(object dirInfo)
        {
            if (dirInfo != null)
            {
                if (dirInfo.GetType().Equals(typeof(DirectoryInfo)))
                {
                    return ((DirectoryInfo)dirInfo).FullName;
                }

                if (dirInfo.GetType().Equals(typeof(DriveInfo)))
                {
                    return ((DriveInfo)dirInfo).Name;
                }
            }

            return "";
        }

        #endregion
    }
}
