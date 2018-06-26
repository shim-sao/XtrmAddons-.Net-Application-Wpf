using System;
using System.IO;
using System.Linq;
using System.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Picture.Extensions;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Lib Base Classes Controls Systems Storage Informations.</para>
    /// <para>Storage informations model for custom control displays.</para>
    /// </summary>
    public class StorageInfoModel
    {
        #region Properties

        /// <summary>
        /// Property full name of the item information.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Property single name of the item information.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Property full name or path to the image associated to the item.
        /// </summary>
        public string ImageFullName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Size ImageSize { get; set; } = new Size { Height = 32, Width = 32 };


        /// <summary>
        /// Property displays height of the image associated to the item.
        /// </summary>
        public int ImageHeight { get; set; } = 32;

        /// <summary>
        /// Property displays width of the image associated to the item.
        /// </summary>
        public int ImageWidth { get; set; } = 32;


        /// <summary>
        /// Property creation date of the item.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Property modification date of the item.
        /// </summary>
        public DateTime DateModified { get; private set; }

        /// <summary>
        /// Property to access to the system file informations if the item is a file.
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        /// <summary>Property to access to the system directory informations if the item is a file.
        /// 
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; private set; }

        /// <summary>
        /// Property to define if this is file or directory informations.
        /// </summary>
        public bool IsFile { get; private set; } = false;

        /// <summary>
        /// Property to define if the item is a valid picture.
        /// </summary>
        public bool IsPicture { get; private set; } = false;

        /// <summary>
        /// Property to define if the item is locked for editing.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Property to define if the item displays is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Property to define if the file is enabled for editing.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Property to define the displays margin.
        /// </summary>
        public int Margin { get; set; } = 0;

        /// <summary>
        /// Property to define the displays padding.
        /// </summary>
        public int Padding { get; set; } = 0;

        /// <summary>
        /// Property to define the displays backgroung.
        /// </summary>
        public string Background { get; private set; } = "#FFFFFF";

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Base Classes Controls Systems Storage Informations Constructor.
        /// </summary>
        /// <param name="fi">A file system informations.</param>
        public StorageInfoModel(FileInfo info)
        {
            FileInfo = info;
            Name = FileInfo.Name;
            FullName = FileInfo.FullName;
            ImageFullName = FileInfo.FullName;
            DateCreated = FileInfo.CreationTime;
            DateModified = FileInfo.LastWriteTime;
            IsFile = true;
            CheckIfIsPicture();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib Base Classes Controls Systems Storage Informations Constructor.
        /// </summary>
        /// <param name="fi">A directory system informations.</param>
        public StorageInfoModel(DirectoryInfo info)
        {
            DirectoryInfo = info;
            Name = DirectoryInfo.Name;
            FullName = DirectoryInfo.FullName;
            ImageFullName = DirectoryInfo.FullName;
            DateCreated = DirectoryInfo.CreationTime;
            DateModified = DirectoryInfo.LastWriteTime;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to convert object in new Picture entity.
        /// </summary>
        /// <returns>A Picture entity with Image informations or null if it is directory or non image file.</returns>
        public PictureEntity ToPicture()
        {
            if (!IsFile)
            {
                return null;
            }

            string ext = Path.GetExtension(FullName);
            string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".tiff" };
            PictureEntity p = null;

            if (extensions.Contains(ext.ToLower()))
            {
                dynamic metadata = FullName.PictureMetadata();
                DateTime date = ((string)metadata.DateTaken).IsNotNullOrWhiteSpace() ? DateTime.Parse(metadata.DateTaken) : DateTime.Now;
                string Title = metadata.Title ?? "";
                string Comment = metadata.Comment ?? "";

                p = new PictureEntity()
                {
                    PrimaryKey = 0,

                    Name = Title,
                    Comment = Comment,

                    Captured = date,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,

                    OriginalPath = FullName,
                    OriginalWidth = (int)metadata.Width,
                    OriginalHeight = (int)metadata.Height,
                    OriginalLength = (long)metadata.Length,

                    PicturePath = FullName,
                    PictureWidth = (int)metadata.Width,
                    PictureHeight = (int)metadata.Height,
                    PictureLength = (long)metadata.Length,

                    ThumbnailPath = FullName,
                    ThumbnailWidth = (int)metadata.Width,
                    ThumbnailHeight = (int)metadata.Height,
                    ThumbnailLength = (long)metadata.Length
                };
            }

            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckIfIsPicture()
        {
            if (!IsFile)
            {
                IsPicture = false;
            }

            string ext = Path.GetExtension(FullName);
            string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".tiff" };

            if (extensions.Contains(ext.ToLower()))
            {
                IsPicture = true;
            }
        }

        #endregion
    }
}