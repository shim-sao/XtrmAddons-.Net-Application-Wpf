using System.Collections.Generic;
using System.IO;
using XtrmAddons.Net.SystemIO;

namespace Fotootof.BuilderV1
{
    /// <summary>
    /// 
    /// </summary>
    internal static class BuilderLocal
    {
        /// <summary>
        /// 
        /// </summary>
        internal static List<string> LocalFolders
            => new List<string>
        { "de", "en-GB", "es", "fr", "fr-FR", "Ro", "Ru", "Sv", "zh-Hans" };

        /// <summary>
        /// 
        /// </summary>
        internal static void Process()
        {
            string applicationPath = System.AppDomain.CurrentDomain.BaseDirectory;

            foreach (string folderSrc in LocalFolders)
            {
                if (Directory.Exists(folderSrc))
                {
                    string localPath = Path.Combine(applicationPath, Folders.Local);
                    string folderDest = Path.Combine(localPath, folderSrc);

                    if (!Directory.Exists(localPath))
                    {
                        Directory.CreateDirectory(folderDest);
                    }

                    SysDirectory.Move(folderSrc, folderDest);
                }
            }
        }
    }
}