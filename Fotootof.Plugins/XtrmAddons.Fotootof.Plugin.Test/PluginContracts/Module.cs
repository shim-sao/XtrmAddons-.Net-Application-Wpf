using System.ComponentModel.Composition;
using XtrmAddons.Fotootof.AddInsContracts.Base;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name { get => "Plugin test"; set => throw new System.NotImplementedException(); }

        /// <summary>
        /// 
        /// </summary>
        public override string ParentName { get; set; } = "MenuMain_MenuItem_Plugins";

        /// <summary>
        /// We do not need to create object of it. As it is imported.
        /// </summary>
        [Import]
        public override IProcess Process { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public override IComponent Component { get; set; }// = new Component();
    }
}