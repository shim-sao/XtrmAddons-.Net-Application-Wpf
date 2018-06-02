using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

namespace XtrmAddons.Fotootof.Common.Controls.Menu
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main.
    /// </summary>
    public class MenuMainModel<MenuMain> : ModelBase<MenuMain>
    {
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