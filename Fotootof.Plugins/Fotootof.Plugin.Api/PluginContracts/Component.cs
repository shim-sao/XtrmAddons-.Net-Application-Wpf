using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;

namespace Fotootof.Plugin.Api.PluginContracts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Plugin Test PluginContracts.
    /// </summary>
    [Export("ApiComponent", typeof(IComponent))]
    public class Component : ComponentBase
    {
        #region Method

        /// <summary>
        /// Method to initialize the component.
        /// </summary>
        public override void InitializeComponent()
        {
            if (!IsInitialized)
            {
                Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Initializing extention component. Please wait...");
                IsInitialized = true;
            }
        }

        #endregion
    }
}