using System;
using System.Windows.Controls;

namespace Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface Fotootof Interfaces AddInsContracts Module.
    /// </summary>
    public interface IPlugin : IDisposable
    {
        #region Properties

        /// <summary>
        /// <para>Property to define the name of the module.</para>
        /// <para>The name of the module will be displays as menu item title.</para>
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Property to define the name of the parent menu item.
        /// </summary>
        string ParentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        MenuItem MenuItem { get; set; }

        #endregion
    }
}
