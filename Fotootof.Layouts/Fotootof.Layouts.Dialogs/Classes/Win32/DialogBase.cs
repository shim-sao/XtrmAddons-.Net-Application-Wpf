using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Dialogs.Win32
{
    /// <summary>
    /// Class Fotootof Layouts Dialogs Win32 Dialog Base.
    /// </summary>
    public static class DialogBase
    {
        /// <summary>
        /// Method to open a simple folder dialog box <see cref="CommonOpenFileDialog"/>.
        /// </summary>
        /// <param name="multiselect">Allow or not multiple selection.</param>
        /// <param name="folder">The initial folder to open for the selection.</param>
        /// <returns>A result <see cref="string"/> or <see cref="IEnumerable{String}"/> for multiple selection. Return null if operation is canceled.</returns>
        public static object FolderDialogBox(bool multiselect = false, string folder = "")
        {
            if(folder.IsNullOrWhiteSpace())
            {
                folder = Environment.SpecialFolder.MyDocuments.ToString();
            }

            CommonOpenFileDialog dlg = new CommonOpenFileDialog
            {
                Title = Properties.Translations.DialogBoxTitle_PictureFileSelector,
                IsFolderPicker = true,
                InitialDirectory = folder,

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
                if(multiselect)
                    return dlg.FileNames;
                else
                    return dlg.FileName;
            }

            return null;
        }
    }
}
