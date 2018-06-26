using System;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof Interfaces AddInsContracts Module.
    /// </summary>
    public interface IModule : IDisposable
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

        /// <summary>
        /// 
        /// </summary>
        IComponent Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IProcess Process { get; set; }

        #endregion
    }
}
