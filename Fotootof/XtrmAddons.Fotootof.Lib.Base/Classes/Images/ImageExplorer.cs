using System.IO;
using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Images
{
    public class ImageExplorer
    {
        /// <summary>
        /// Method to get an file information icon.
        /// </summary>
        /// <param name="ext">The extension of the file.</param>
        /// <returns>The icon of the file extension type.</returns>
        [System.Obsolete("Never been used... Try to find existing Windows APi for that...", true)]
        public static string DirectoryInfoIcon(DirectoryInfo di)
        {
            string icon = null;

            if (Directory.GetFiles(di.FullName).Length > 0)
            {
                icon = (string)Application.Current.Resources["IconFolderEmptyClosed"];
            }
            else
            {
                icon = (string)Application.Current.Resources["IconFolderClosed"];
            }

            return icon;
        }

    }
}
