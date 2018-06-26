using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

namespace XtrmAddons.Fotootof.Common.Controls.Menu
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main.
    /// </summary>
    public class MenuMainModel : ModelBase<MenuMain>
    {
        private Grid moduleContainer = new Grid();

        public Grid ModuleContainer
        {
            get
            {
                return moduleContainer;
            }
            set
            {
                if(moduleContainer != value)
                {
                    moduleContainer = value;
                }
                NotifyPropertyChanged();
            }
        }

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