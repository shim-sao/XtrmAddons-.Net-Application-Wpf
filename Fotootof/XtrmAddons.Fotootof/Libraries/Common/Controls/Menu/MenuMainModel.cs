using XtrmAddons.Fotootof.Lib.Base.Classes.Models;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlUiElement;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.Menu
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main.
    /// </summary>
    public class MenuMainModel<MenuMain> : ModelBase<MenuMain>
    {
        #region Properties

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Controller.
        /// </summary>
        public UiElement ShowLogsWindow
        {
            get { return ApplicationBase.UI.Controls.FindKey("ShowLogsWindow"); }
            set
            {
                GetOptionsShowLogsWindow(value);
                RaisePropertyChanged("ShowLogsWindow");
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Controller.
        /// </summary>
        public MenuMainModel(MenuMain menuMain)
        {
            OwnerBase = menuMain;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private static void GetOptionsShowLogsWindow(UiElement value)
        {
            if (ApplicationBase.UI.Controls.FindKey("ShowLogsWindow") != null)
            {
                ApplicationBase.UI.Controls.ReplaceKeyUnique(value, "ShowLogsWindow");
            }
            else
            {
                ApplicationBase.UI.Controls.Add(value);
            }
        }

        #endregion
    }
}
