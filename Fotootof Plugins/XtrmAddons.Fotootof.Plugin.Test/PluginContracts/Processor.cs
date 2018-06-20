using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    [Export(typeof(IProcess))]
    public class Processor : IProcess
    {
        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            Trace.WriteLine("Running process Module Plugin Test.");
            MessageBox.Show("Running process Module Plugins Test.");
        }
    }
}
