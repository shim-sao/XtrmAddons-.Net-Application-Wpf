using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Libraries.Enums;
using System;
using System.ComponentModel.Composition;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IExtension))]
    public class Extension : ExtensionBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override DisplayMode DisplayMode { get => DisplayMode.Server; set => throw new NotImplementedException(); }

        /// <summary>
        /// 
        /// </summary>
        [Import("TestModule")]
        public override IModule Module { get; set; }// = new Module();
    }
}