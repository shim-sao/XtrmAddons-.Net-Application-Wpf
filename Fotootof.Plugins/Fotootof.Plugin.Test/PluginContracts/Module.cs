using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;

namespace Fotootof.Plugin.Test.PluginContracts
{
    /// <summary>
    /// Class Fotootof.Plugin.Test PluginContracts.
    /// </summary>
    [Export("TestModule", typeof(IModule))]
    public class Module : ModuleBase
    {
        #region Properties
        
        /// <summary>
        /// Property to access to the name of the module.
        /// </summary>
        public override string Name
        {
            get => "Plugin test";
            set => throw new System.NotImplementedException();
        }

        /// <summary>
        /// Property to access to the process <see cref="IProcess"/> of the module.
        /// </summary>
        [Import("TestProcessor")]
        public override IProcess Process { get; set; }

        /// <summary>
        /// Property nto access to the process <see cref="IComponent"/> of the component.
        /// </summary>
        [Import("TestComponent")]
        public override IComponent Component { get; set; }// = new Component();
        
        #endregion
    }
}