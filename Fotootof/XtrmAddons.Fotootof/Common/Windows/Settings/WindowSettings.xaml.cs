using System.Windows;

namespace XtrmAddons.Fotootof.Common.Windows.Settings
{
    /// <summary>
    /// Logique d'interaction pour WindowSettings.xaml
    /// </summary>
    public partial class WindowSettings : Window
    {
        public WindowSettings()
        {
            InitializeComponent();

            DataContext = new WindowSettingsModel();
        }

        private void DialogSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
