using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
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
            get => GetOptionsControl("ShowLogsWindow");
            set
            {
                SetOptionsControl("ShowLogsWindow", value);
                NotifyPropertyChanged();
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
    }
}
