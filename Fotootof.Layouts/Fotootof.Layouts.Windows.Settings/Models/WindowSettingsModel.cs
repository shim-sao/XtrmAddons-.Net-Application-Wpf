using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;
using XtrmAddons.Net.Application.Helpers;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using Fotootof.Layouts.Windows.Settings.Controls;

namespace Fotootof.Layouts.Windows.Settings.Models
{
    public class WindowSettingsModel : WindowBaseFormModel<WindowSettingsControl>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Window AclGroup Form Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowSettingsModel(WindowSettingsControl window) : base(window) { }

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
        
        public XtrmAddons.Net.Application.Serializable.Elements.Data.Database Database
            => ApplicationBase.Options.Data.Databases.FindDefaultFirst();

        #endregion


        public StorageOptions Storage => ApplicationBase.Storage;

    }
}