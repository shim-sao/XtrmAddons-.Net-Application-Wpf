using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;

namespace Fotootof.Plugin.Api.PluginContracts
{
    /// <summary>
    /// Class Fotootof.Plugin.Test Plugin Contracts Module.
    /// </summary>
    [Export("ApiModule", typeof(IModule))]
    public class Module : ModuleBase
    {
        /// <summary>
        /// Property to access to the name of the module.
        /// </summary>
        public override string Name
        {
            get => "API";
            set => throw new System.NotImplementedException();
        }

        /// <summary>
        /// Property to access to the process <see cref="IProcess"/> of the module.
        /// </summary>
        [Import("ApiProcess")]
        public override IProcess Process { get; set; }

        /// <summary>
        /// Property to access to the process <see cref="IComponent"/> of the component.
        /// </summary>
        [Import("ApiComponent")]
        public override IComponent Component { get; set; }
    }
}