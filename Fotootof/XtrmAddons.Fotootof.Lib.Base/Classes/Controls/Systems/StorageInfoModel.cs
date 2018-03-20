using System;
using System.IO;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems
{
    public class StorageInfoModel
    {
        /// <summary>
        /// Property full name of the item information.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Property full name or path to the image associated to the item.
        /// </summary>
        public string ImageFullName { get; private set; }

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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        public StorageInfoModel(FileInfo info)
        {
            FileInfo = info;
            FullName = FileInfo.FullName;
            ImageFullName = FileInfo.FullName;
            DateCreated = FileInfo.CreationTime;
            DateModified = FileInfo.LastWriteTime;
            IsFile = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        public StorageInfoModel(DirectoryInfo info)
        {
            DirectoryInfo = info;
            FullName = DirectoryInfo.FullName;
            ImageFullName = DirectoryInfo.FullName;
            DateCreated = DirectoryInfo.CreationTime;
            DateModified = DirectoryInfo.LastWriteTime;
        }
    }
}
