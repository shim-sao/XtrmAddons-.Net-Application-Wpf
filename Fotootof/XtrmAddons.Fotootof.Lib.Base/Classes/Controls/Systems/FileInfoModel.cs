using System;
using System.IO;
using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems
{
    public class FileInfoModel : StorageInfoModel<FileInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        public FileInfoModel(FileInfo info)
        {
            ItemInfo = info;
            FullName = ItemInfo.FullName;
            ImageFullName = ItemInfo.FullName;
            DateCreated = ItemInfo.CreationTime;
            DateModified = ItemInfo.LastWriteTime;
        }
    }
}
