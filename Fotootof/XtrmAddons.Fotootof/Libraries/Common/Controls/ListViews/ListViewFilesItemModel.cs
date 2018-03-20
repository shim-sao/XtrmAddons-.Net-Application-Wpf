using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Picture;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Browser Controls ListView Files Item Model.
    /// </summary>
    public class ListViewFilesItemModel : PictureInfos
    {
        #region Properties

        /// <summary>
        /// Property to access to the thumbnail image.
        /// </summary>
        public override BitmapImage Thumbnail
        {
            get { return bmpThumbnail; }
            set { bmpThumbnail = value; }
        }

        /// <summary>
        /// Property to access to the image.
        /// </summary>
        public override BitmapImage Original
        {
            get { return bmpOriginal; }
            set { bmpOriginal = value; }
        }

        /// <summary>
        /// Property to access to the resized image.
        /// </summary>
        public override BitmapImage Preview
        {
            get { return bmpPreview; }
            set { bmpPreview = value; }
        }

        /// <summary>
        /// Property to define if the file is locked for editing.
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Margin { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool IsContextMenuEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int sizeLimit = (int)Application.Current.Resources["IconLarge"];

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Browser Controls ListView Files Item Model Constructor.
        /// </summary>
        public ListViewFilesItemModel() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Browser Controls ListView Files Item Model Constructor.
        /// </summary>
        public ListViewFilesItemModel(FileInfo fileInfo, string filesType = null)
        {
            int sizeLimit = (int)Application.Current.Resources["IconLarge"];
            string ext = Path.GetExtension(fileInfo.FullName);
            string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".tiff" };

            if (extensions.Contains(ext))
            {
                ThumbnailWidth = sizeLimit;
                ThumbnailHeight = sizeLimit;
                OriginalWidth = sizeLimit;
                OriginalHeight = sizeLimit;

                IsContextMenuEnabled = true;
            }
            else
            {
                sizeLimit = 256;
                
                ThumbnailWidth = sizeLimit;
                ThumbnailHeight = sizeLimit;
                OriginalWidth = sizeLimit;
                OriginalHeight = sizeLimit;

                Margin = 35;
                Locked = true;
            }
            
            Name = fileInfo.Name;
            Description = fileInfo.FullName;
            ThumbnailPath = fileInfo.FullName;
            OriginalPath = fileInfo.Name;

            DateCreated = fileInfo.CreationTime;
            DateModified = fileInfo.LastWriteTime;
        }

        #endregion



        #region Methods

        public PictureEntity ToPicture()
        {
            string ext = Path.GetExtension(OriginalPath);
            string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".tiff" };
            PictureEntity p = null;

            dynamic metadata = OriginalPath.PictureMetadata().Data;

            if (extensions.Contains(ext.ToLower()))
            {
                DateTime date = metadata.DateTaken != null ? DateTime.Parse(metadata.DateTaken) : DateTime.Now;
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

                    ThumbnailPath = ThumbnailPath,
                    ThumbnailWidth = (int)metadata.Width,
                    ThumbnailHeight = (int)metadata.Height,

                    PicturePath = OriginalPath,
                    PictureWidth = (int)metadata.Width,
                    PictureHeight = (int)metadata.Height
                };
            }

            return p;
        }

        #endregion
    }
}
