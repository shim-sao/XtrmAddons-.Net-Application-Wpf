using System;
using System.Windows;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof AddIns Contracts Component.
    /// </summary>
    public interface IComponent : IDisposable
    {
        #region Properties

        /// <summary>
        /// Property check, or set, if the Component is already initialized.
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// <para>Property to access to the parent framework element.</para>
        /// </summary>
        FrameworkElement Parent { get; set; }

        /// <summary>
        /// <para>Property to access to the custon user control.</para>
        /// </summary>
        UserControl Context { get; set; }

        #endregion
    }
}
