using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Plugin.Test
{
    /// <summary>
    /// Logique d'interaction pour UcPluginTest.xaml
    /// </summary>
    [Export(typeof(UserControl))]
    public partial class UcPluginContext : UserControl
    {
        public UcPluginContext()
        {
            InitializeComponent();
        }
    }
}
