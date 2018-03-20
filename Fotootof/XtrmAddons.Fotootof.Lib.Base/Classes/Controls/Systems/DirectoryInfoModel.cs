using System;
using System.IO;
using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems
{
    public class DirectoryInfoModel : StorageInfoModel<DirectoryInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        public DirectoryInfoModel(DirectoryInfo info)
        {
            ItemInfo = info;
            FullName = info.FullName;
            ImageFullName = info.FullName;
            DateCreated = info.CreationTime;
            DateModified = info.LastWriteTime;
        }
    }
}
