using System;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;

namespace XtrmAddons.Fotootof.Settings
{
    /// <summary>
    /// Class XtrmAddons Fotootof Settings Preferences.
    /// </summary>
    public static class SettingsPreferences
    {
        /// <summary>
        /// Method to initialize storage preferences.
        /// </summary>
        public static void InitializeStorage()
        {
            #region application data
            // Create path to data server directory. 
            Directory temp = new Directory
            {
                Key = "data.server",
                RelativePath = "Data\\Server",
                IsRelative = true,
                Root = Environment.CurrentDirectory
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.TraceInformation($"data.server => {ApplicationBase.Storage.Directories.FindKeyFirst("data.server").AbsolutePath}");

            // Create path to configuration database directory
            temp = new Directory
            {
                Key = "data.database",
                RelativePath = "Data\\Database",
                IsRelative = true,
                Root = Environment.CurrentDirectory
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.TraceInformation($"data.database => {ApplicationBase.Storage.Directories.FindKeyFirst("data.database").AbsolutePath}");
            #endregion


            #region application roaming
            // Create path to configuration server public html directory
            temp = new Directory
            {
                Key = "roaming.public_html",
                RelativePath = "Public_html",
                IsRelative = true,
                Root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.TraceInformation($"roaming.public_html => {ApplicationBase.Storage.Directories.FindKeyFirst("roaming.public_html").AbsolutePath}");
            #endregion

            
            #region user cache
            // Create path to cache filestypes directory. 
            temp = new Directory
            {
                Key = "cache.filestypes",
                RelativePath = "Files Types",
                IsRelative = true,
                Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Cache)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.TraceInformation($"cache.filestypes => {ApplicationBase.Storage.Directories.FindKeyFirst("cache.filestypes").AbsolutePath}");
            #endregion
        }
    }
}
