using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Interfaces.AddInsContracts
{
    /// <summary>
    /// Interface XtrmAddons Fotootof Interfaces AddInsContracts Module.
    /// </summary>
    public interface IModule
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        IProcess Process { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IPlugin Plugin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        MenuItem InterfaceControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Control Container { get; set; }

        #endregion



        #region Properties

        /// <summary>
        /// Method to get the menu item of the Module.
        /// </summary>
        /// <returns></returns>
        MenuItem GetInterfaceObject();

        #endregion
    }
}
