using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Drawing.Imaging;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Win32
{
    public static class DialogBase
    {
        /// <summary>
        /// Method to display picture file dialog box selector.
        /// </summary>
        /// <param name="multiselect">Multiple selection enabled ?. False by default. Optional.</param>
        /// <returns>A picture file dialog box selector, null if canceled.</returns>
        [System.Obsolete("XtrmAddons.Net.Picture", true)]
        public static OpenFileDialog PictureFileDialogBox(bool multiselect = false)
        {
            // Configure open file dialog box 
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = ""
            };

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                dlg.Filter = string.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            dlg.Filter = string.Format("{0}{1}{2} ({3})|{3}", dlg.Filter, sep, "All Files", "*.*");
            dlg.DefaultExt = ".JPG"; // Default file extension 
            dlg.FilterIndex = 2;
            dlg.Multiselect = multiselect;
            dlg.Title = Culture.Translation.DWords.DialogBoxTitle_PictureFileSelector;

            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                return dlg;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiselect"></param>
        /// <returns></returns>
        public static CommonOpenFileDialog FolderDialogBox(bool multiselect = false, Environment.SpecialFolder folder = Environment.SpecialFolder.MyDocuments)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog
            {
                Title = Culture.Translation.DWords.DialogBoxTitle_PictureFileSelector,
                IsFolderPicker = true,
                InitialDirectory = folder.ToString(),

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = Environment.SpecialFolder.DesktopDirectory.ToString(),
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = multiselect,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dlg;
            }

            return null;
        }

    }
}
