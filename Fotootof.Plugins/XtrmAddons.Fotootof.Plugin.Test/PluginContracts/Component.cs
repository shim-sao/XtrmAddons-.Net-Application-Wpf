using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    [Export("TestComponent", typeof(IComponent))]
    public class Component : ComponentBase
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        #region Method

        /// <summary>
        /// 
        /// </summary>
        public override void InitializeComponent()
        {
            if (!IsInitialized)
            {
                Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Initializing extention component. Please wait...");

                Context = new UcPluginContext();
                IsInitialized = true;
            }
        }

        #endregion
    }
}
