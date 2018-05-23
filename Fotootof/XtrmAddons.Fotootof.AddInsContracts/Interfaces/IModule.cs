using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Interfaces.AddInsContracts
{
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
        /// 
        /// </summary>
        /// <returns></returns>
        MenuItem GetInterfaceObject();

        #endregion
    }
}
