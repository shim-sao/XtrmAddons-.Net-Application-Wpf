using Fotootof.AddInsContracts.Base;
using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Libraries.Enums;
using System;
using System.ComponentModel.Composition;

namespace Fotootof.Plugin.Api.PluginContracts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Plugin Api Plugin Contracts Extension.
    /// </summary>
    [Export(typeof(IExtension))]
    public class Extension : ExtensionBase
    {
        /// <summary>
        /// Property to access to the display mode of the extension.
        /// </summary>
        public override DisplayMode DisplayMode
        {
            get => DisplayMode.Server;
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Property to access to the module of the extension.
        /// </summary>
        [Import("ApiModule")]
        public override IModule Module { get; set; }
    }
}