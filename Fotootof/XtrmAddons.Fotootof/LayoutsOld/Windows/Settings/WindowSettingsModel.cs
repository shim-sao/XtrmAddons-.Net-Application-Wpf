using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;
using XtrmAddons.Net.Application.Helpers;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;

namespace XtrmAddons.Fotootof.LayoutsOld.Windows.Settings
{
    [System.Obsolete("use Fotootof.Layouts.Windows.Settings.dll")]
    public class WindowSettingsModel : WindowBaseFormModel<WindowSettings>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window AclGroup Form Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowSettingsModel(WindowSettings window) : base(window) { }

        #endregion



        #region Properties Files

        /// <summary>
        /// Property to access to the application preferences directory file absolute path.
        /// </summary>
        public string FilePreferencesXml
        {
            get => ((Net.Application.Helpers.SerializerHelper)ApplicationBase.SerializerHelper).FileName_Preferences;
        }

        /// <summary>
        /// Property to access to the application options directory file absolute path.
        /// </summary>
        public string FileOptionsXml
        {
            get => ((SerializerHelper)ApplicationBase.SerializerHelper).FileName_Options;
        }

        /// <summary>
        /// Property to access to the application user interface directory file absolute path.
        /// </summary>
        public string FileUiXml
        {
            get => ((SerializerHelper)ApplicationBase.SerializerHelper).FileName_Ui;
        }

        #endregion


        #region Properties Directories

        /// <summary>
        /// Property to access to the application base directory.
        /// </summary>
        public DirectoryHelper Directories
        {
            get => ApplicationBase.Directories;
        }
        
        public Net.Application.Serializable.Elements.Data.Database Database
            => ApplicationBase.Options.Data.Databases.FindDefaultFirst();

        #endregion


        public StorageOptions Storage => ApplicationBase.Storage;

    }
}