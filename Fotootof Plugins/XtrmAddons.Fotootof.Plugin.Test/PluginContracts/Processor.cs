using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    [Export(typeof(IProcess))]
    public class Processor : IProcess
    {
        public bool IsEnable { get => true; set => throw new System.NotImplementedException(); }

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
