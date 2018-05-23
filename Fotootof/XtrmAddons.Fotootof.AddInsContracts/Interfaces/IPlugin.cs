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
        /// 
        /// </summary>
        bool IsPluginCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        UserControl MenuControl { get; set; }

        #endregion


        #region Method

        /// <summary>
        /// 
        /// </summary>
        void CreatePlugin();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        UserControl GetPlugin();

        #endregion
    }
}
