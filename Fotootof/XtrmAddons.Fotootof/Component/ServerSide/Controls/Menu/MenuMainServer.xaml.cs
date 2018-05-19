using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XtrmAddons.Fotootof.Lib.Base.Enums;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.Menu
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Client UI Control Main Menu Navigation.
    /// </summary>
    public partial class MenuMainServer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public DisplayMode DisplayMode { get; set; }
            = DisplayMode.Server;
        
        /// <summary>
        /// 
        /// </summary>
        public MenuMainServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageBrowser(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageBrowser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageServer(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageCatalog(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageCatalog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageUsers(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageUsers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (SolidColorBrush)FindResource("LightBlack");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (SolidColorBrush)FindResource("Transparent");
        }
    }
}