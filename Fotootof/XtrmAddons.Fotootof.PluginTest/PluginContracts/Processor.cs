using System.ComponentModel.Composition;
using System.Diagnostics;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;

namespace XtrmAddons.Fotootof.PluginTest.PluginContracts
{
    [Export(typeof(IProcess))]
    public class Processor : IProcess
    {


        public void Run()
        {
            Trace.WriteLine("Running process Plugin Test Module");

            // MessageBox.Show("Hello! This is a Greetings from Plugin");
        }
    }
}
