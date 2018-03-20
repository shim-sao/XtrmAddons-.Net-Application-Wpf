using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlStorage;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Settings
{
    public class WindowSettingsModel
    {
        /// <summary>
        /// Property to access to the application base directory.
        /// </summary>
        public static string BaseDirectory => ApplicationBase.BaseDirectory;

        /// <summary>
        /// Property to access to the application preferences directory file absolute path.
        /// </summary>
        public static string FilePreferencesXml => ApplicationBase.FilePreferencesXml;

        /// <summary>
        /// Property to access to the application options directory file absolute path.
        /// </summary>
        public static string FileOptionsXml => ApplicationBase.FileOptionsXml;

        /// <summary>
        /// Property to access to the application user interface directory file absolute path.
        /// </summary>
        public static string FileUiXml => ApplicationBase.FileUiXml;


        public string BinDirectory => ApplicationBase.BinDirectory;

        public string CacheDirectory => ApplicationBase.CacheDirectory;

        public string ConfigDirectory => ApplicationBase.ConfigDirectory;

        public string DataDirectory => ApplicationBase.ConfigDirectory;

        public string ThemeDirectory => ApplicationBase.ThemeDirectory;

        public StorageOptions Storage => ApplicationBase.Storage;

    }
}