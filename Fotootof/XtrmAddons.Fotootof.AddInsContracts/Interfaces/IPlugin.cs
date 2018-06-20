using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Interfaces.AddInsContracts
{
    /// <summary>
    /// Interface XtrmAddons Fotootof AddIns Contracts Plugin.
    /// </summary>
    public interface IPlugin
    {
        #region Properties

        /// <summary>
        /// Property check, or set, if the plugin is created.
        /// </summary>
        bool IsPluginCreated { get; set; }

        /// <summary>
        /// <para>Property to access to the custon user control.</para>
        /// </summary>
        UserControl MenuControl { get; set; }

        #endregion


        #region Method

        /// <summary>
        /// Method to initialize plugin.
        /// </summary>
        void InitializePlugin();

        /// <summary>
        /// Method to get user control plugin.
        /// </summary>
        /// <returns>A custon user control.</returns>
        UserControl GetPlugin();

        #endregion
    }
}
