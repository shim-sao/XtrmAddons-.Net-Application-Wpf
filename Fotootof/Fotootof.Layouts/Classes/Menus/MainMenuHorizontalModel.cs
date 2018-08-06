using Fotootof.Libraries.Models;
using System.Windows.Controls;

namespace Fotootof.Layouts.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Model.
    /// </summary>
    public class MainMenuHorizontalModel : ModelBase<MainMenuHorizontalLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Grid moduleContainer = new Grid();

        /// <summary>
        /// 
        /// </summary>
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
        
        #endregion


        #region Constructor

        /// <summary>
        /// Class Fotootof Main Menu Horizontal Model Constructor.
        /// </summary>
        public MainMenuHorizontalModel(MainMenuHorizontalLayout controlView) : base(controlView) { }

        #endregion
    }
}