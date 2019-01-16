using Fotootof.Collections;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.Enums;
using Fotootof.Libraries.Models.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using XtrmAddons.Net.Memory;
using XtrmAddons.Net.Picture;
using ImgSize = Fotootof.Libraries.Enums.ImageSize;

namespace Fotootof.Components.Server.Browser
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Components Browser Model.
    /// </summary>
    internal class PageBrowserModel : ComponentModel<PageBrowserLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the thumbnail image <see cref="Size"/> to display. Vignette by default.
        /// </summary>
        public Size imageSize
            = new Size { Height = ImgSize.Vignette.ToDouble(), Width = ImgSize.Vignette.ToDouble() };

        /// <summary>
        /// Variable <see cref="CollectionStorage"/> collection of directories and files informations.
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
        /// Property to access to the image <see cref="Size"/> to display.
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
        /// Property to access to the <see cref="CollectionStorage"/> collection of directories and files informations.
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
        /// Class XtrmAddons Fotootof Server Components Browser Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> associated to the model.</param>
        public PageBrowserModel(PageBrowserLayout controlView) : base(controlView) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to clear the <see cref="CollectionStorage"/> of files displayed in the view.
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
        /// Method to load directories informations <see cref="DirectoryInfo"/> into the files collection.
        /// </summary>
        /// <param name="dirInfos">An array of directories informations <see cref="DirectoryInfo"/>.</param>
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
        /// Method to load files informations <see cref="FileInfo"/> into the files collection.
        /// </summary>
        /// <param name="infoFiles">An array of files informations <see cref="FileInfo"/>.</param>
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
        /// Method to load informations of a directory <see cref="DirectoryInfo"/>.
        /// </summary>
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
        /// Method to get an array of sub <see cref="FileInfo"/> in a <see langword="abstract"/><see cref="DirectoryInfo"/>.
        /// </summary>
        /// <param name="dirInfo">A <see cref="DirectoryInfo"/>.</param>
        /// <param name="option">The directory search options <see cref="SearchOption"/>.</param>
        /// <param name="pattern">The files pattern to search.</param>
        /// <returns></returns>
        public FileInfo[] GetInfoFiles(DirectoryInfo dirInfo, SearchOption option = SearchOption.TopDirectoryOnly, string pattern = "*.*")
        {
            FileInfo[] info = null;

            if (dirInfo != null)
            {
                try
                {
                    info = dirInfo.GetFiles(pattern, SearchOption.TopDirectoryOnly);

                }
                catch (Exception e)
                {
                    log.Error(e);
                    MessageBoxs.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// Method to get an array of sub <see cref="DirectoryInfo"/> in a <see langword="abstract"/><see cref="DirectoryInfo"/>.
        /// </summary>
        /// <param name="dirInfo">A <see cref="DirectoryInfo"/>.</param>
        /// <returns>An array of <see cref="DirectoryInfo"/> or null.</returns>
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
                    MessageBoxs.Error(e);
                }
            }

            return info;
        }

        /// <summary>
        /// Method to display the header title of a directory.
        /// </summary>
        /// <param name="dirInfo">A <see cref="DirectoryInfo"/>.</param>
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
