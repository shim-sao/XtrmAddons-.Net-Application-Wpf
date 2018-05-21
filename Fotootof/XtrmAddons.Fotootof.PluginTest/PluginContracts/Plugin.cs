using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;

namespace XtrmAddons.Fotootof.PluginTest.PluginContracts
{
    [Export(typeof(IPlugin))]
    public class Plugin : IPlugin
    {
        #region Properties

        public bool IsPluginCreated { get; set; }

        /// <summary>
        /// You do not need to create object, as it will import it automatically.
        /// this will be taken as we have export in GreetMessage Control
        /// </summary>
        [Import]
        public UserControl MenuControl { get; set; }

        #endregion


        #region Method

        public void CreatePlugin()
        {
            Trace.WriteLine("Creating Plugin Test");

            MenuControl = new UcPluginTest();

            IsPluginCreated = true;
        }

        public UserControl GetPlugin()
        {
            Trace.WriteLine("Getting Plugin Test");

            if (!IsPluginCreated)
            {
                CreatePlugin();
            }

            return MenuControl;
        }

        #endregion
    }
}
