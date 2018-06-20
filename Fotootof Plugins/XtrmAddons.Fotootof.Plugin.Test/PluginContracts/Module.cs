using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;

namespace XtrmAddons.Fotootof.Plugin.Test.PluginContracts
{
    [Export(typeof(IModule))]
    public class Module : IModule
    {

        private void Initialize()
        {
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Initializing Module 'Plugin Test'");

            InterfaceControl = InterfaceControl ?? new MenuItem();
            InterfaceControl.Header = "Plugin Test";
            InterfaceControl.Click += new RoutedEventHandler(InterfaceControl_Click);
        }

        public MenuItem InterfaceControl { get; set; }

        /// <summary>
        /// We do not need to create object of it. As it is imported.
        /// </summary>
        [Import]
        public IProcess Process { get; set; }

        /// <summary>
        /// Plugin object will be created automatically.
        /// </summary>
        [Import]
        public IPlugin Plugin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Control Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MenuItem GetInterfaceObject()
        {
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Getting Object Interface.");

            Initialize();

            return InterfaceControl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InterfaceControl_Click(object sender, RoutedEventArgs e)
        {
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Starting Process...");

            Process.Run();
        }
    }
}