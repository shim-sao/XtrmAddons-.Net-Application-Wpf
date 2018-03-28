using System.Diagnostics;
using System.Threading.Tasks;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlStorage;

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
        public static async Task InitializeStorage()
        {
            await Task.Run(() =>
            {
                // Create path to configuration server directory. 
                Directory temp = new Directory
                {
                    Key = "cache.filestypes",
                    RelativePath = "Files Types",
                    IsRelative = true,
                    Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Cache)
                };
                ApplicationBase.Storage.Directories.ReplaceKey(temp);
                temp.Create();
                Trace.WriteLine("cache.filestypes = " + ApplicationBase.Storage.Directories.FindKey("cache.filestypes").AbsolutePath);

                // Create path to configuration server directory. 
                temp = new Directory
                {
                    Key = "config.server",
                    RelativePath = "Server",
                    IsRelative = true,
                    Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
                };
                ApplicationBase.Storage.Directories.ReplaceKey(temp);
                temp.Create();
                Trace.WriteLine("config.server = " + ApplicationBase.Storage.Directories.FindKey("config.server").AbsolutePath);

                // Create path to configuration database directory
                temp = new Directory
                {
                    Key = "config.database",
                    RelativePath = "Database",
                    IsRelative = true,
                    Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
                };
                ApplicationBase.Storage.Directories.ReplaceKey(temp);
                temp.Create();
                Trace.WriteLine("config.database = " + ApplicationBase.Storage.Directories.FindKey("config.database").AbsolutePath);

                // Create path  configuration database scheme directory
                temp = new Directory
                {
                    Key = "config.database.scheme",
                    RelativePath = "Database\\Scheme",
                    IsRelative = true,
                    Root = SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Config)
                };
                ApplicationBase.Storage.Directories.ReplaceKey(temp);
                temp.Create();
                Trace.WriteLine("config.database.scheme = " + ApplicationBase.Storage.Directories.FindKey("config.database.scheme").AbsolutePath);
            });
        }
    }
}
