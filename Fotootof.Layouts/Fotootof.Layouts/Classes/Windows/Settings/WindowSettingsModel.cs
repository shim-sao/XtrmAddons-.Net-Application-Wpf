using Fotootof.Libraries.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Helpers;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;

namespace Fotootof.Layouts.Windows.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class WindowSettingsModel : WindowLayoutFormModel<WindowSettingsLayout>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window AclGroup Form Model Constructor.
        /// </summary>
        /// <param name="window">The page associated to the model.</param>
        public WindowSettingsModel(WindowSettingsLayout window) : base(window) { }

        #endregion



        #region Properties Files

        /// <summary>
        /// Property to access to the application preferences directory file absolute path.
        /// </summary>
        public string FilePreferencesXml
        {
            get => ((SerializerHelper)ApplicationBase.SerializerHelper).FileName_Preferences;
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
        
        /// <summary>
        /// 
        /// </summary>
        public XtrmAddons.Net.Application.Serializable.Elements.Data.Database Database
            => ApplicationBase.Options.Data.Databases.FindDefaultFirst();

        /// <summary>
        /// 
        /// </summary>
        public StorageOptions Storage => ApplicationBase.Storage;

        #endregion

    }
}