using System;
using System.ComponentModel.Composition;
using XtrmAddons.Fotootof.AddInsContracts.Base;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;
using XtrmAddons.Fotootof.Lib.Base.Enums;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    [Export(typeof(IExtension))]
    public class Extension : ExtensionBase
    {
        public override DisplayMode DisplayMode { get => DisplayMode.Server; set => throw new NotImplementedException(); }

        [Import]
        public override IModule Module { get; set; }// = new Module();
    }
}