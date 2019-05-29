using Fotootof.Libraries.Enums;
using System.Windows.Controls;

namespace Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface Fotootof Interfaces AddInsContracts Extension.
    /// </summary>
    public interface IExtension
    {
        #region Properties

        /// <summary>
        /// Property to check and set the display mode of the application.
        /// </summary>
        DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Property to access to the extension module.
        /// </summary>
        IModule Module { get; set; }

        /// <summary>
        /// Property to access to the extension component.
        /// </summary>
        UserControl Component { get; set; }

        #endregion
    }
}
