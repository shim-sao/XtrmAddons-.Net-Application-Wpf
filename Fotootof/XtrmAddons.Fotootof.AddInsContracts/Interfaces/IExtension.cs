using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Enums;

namespace XtrmAddons.Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof Interfaces AddInsContracts Extension.
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
