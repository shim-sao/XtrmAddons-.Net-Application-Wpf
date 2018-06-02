using System.Diagnostics;
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
            // Create path to configuration server directory. 
            Directory temp = new Directory
            {
                Key = "cache.filestypes",
                RelativePath = "Files Types",
                IsRelative = true,
                Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Cache)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.WriteLine("cache.filestypes = " + ApplicationBase.Storage.Directories.FindKeyFirst("cache.filestypes").AbsolutePath);

            // Create path to configuration server directory. 
            temp = new Directory
            {
                Key = "config.server",
                RelativePath = "Server",
                IsRelative = true,
                Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.WriteLine("config.server = " + ApplicationBase.Storage.Directories.FindKeyFirst("config.server").AbsolutePath);

            // Create path to configuration database directory
            temp = new Directory
            {
                Key = "config.database",
                RelativePath = "Database",
                IsRelative = true,
                Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.WriteLine("config.database = " + ApplicationBase.Storage.Directories.FindKeyFirst("config.database").AbsolutePath);

            // Create path  configuration database scheme directory
            temp = new Directory
            {
                Key = "config.database.scheme",
                RelativePath = "Database\\Scheme",
                IsRelative = true,
                Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
            };
            ApplicationBase.Storage.Directories.AddKeySingle(temp);
            temp.Create();
            Trace.WriteLine("config.database.scheme = " + ApplicationBase.Storage.Directories.FindKeyFirst("config.database.scheme").AbsolutePath);
        }
    }
}
