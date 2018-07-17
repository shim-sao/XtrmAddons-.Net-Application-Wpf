using System;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof Interfaces AddInsContracts Module.
    /// </summary>
    public interface IModule : IPlugin
    {
        #region Properties

        /// <summary>
        /// Property to access to the component of the module.
        /// </summary>
        IComponent Component { get; set; }

        /// <summary>
        /// Property to access to the process of the module.
        /// </summary>
        IProcess Process { get; set; }

        #endregion
    }
}
