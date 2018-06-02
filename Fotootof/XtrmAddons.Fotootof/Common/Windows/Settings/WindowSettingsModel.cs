using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;
using XtrmAddons.Net.Application.Helpers;

namespace XtrmAddons.Fotootof.Common.Windows.Settings
{
    public class WindowSettingsModel
    {
        /// <summary>
        /// Property to access to the application base directory.
        /// </summary>
        public static string BaseDirectory => ApplicationBase.Directories.Base;

        /// <summary>
        /// Property to access to the application preferences directory file absolute path.
        /// </summary>
        public static string FilePreferencesXml => ((Net.Application.Helpers.SerializerHelper)ApplicationBase.SerializerHelper).FileName_Preferences;

        /// <summary>
        /// Property to access to the application options directory file absolute path.
        /// </summary>
        public static string FileOptionsXml => ((SerializerHelper)ApplicationBase.SerializerHelper).FileName_Options;

        /// <summary>
        /// Property to access to the application user interface directory file absolute path.
        /// </summary>
        public static string FileUiXml => ((SerializerHelper)ApplicationBase.SerializerHelper).FileName_Ui;


        public string BinDirectory => ApplicationBase.Directories.Bin;

        public string CacheDirectory => ApplicationBase.Directories.Cache;

        public string ConfigDirectory => ApplicationBase.Directories.Config;

        public string DataDirectory => ApplicationBase.Directories.Config;

        public string ThemeDirectory => ApplicationBase.Directories.Theme;

        public StorageOptions Storage => ApplicationBase.Storage;

    }
}