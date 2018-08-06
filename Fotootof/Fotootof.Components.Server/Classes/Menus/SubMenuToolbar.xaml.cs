using Fotootof.Components.Server;
using Fotootof.Libraries.Enums;
using Fotootof.Theme;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Fotootof.Components.Server.Menus
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Client UI Control Main Menu Navigation.
    /// </summary>
    public partial class SubMenuToolbar : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public DisplayMode DisplayMode { get; set; }
            = DisplayMode.Server;
        
        /// <summary>
        /// 
        /// </summary>
        public SubMenuToolbar()
        {
            ThemeLoader.MergeThemeTo(Resources);

            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageBrowser(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToBrowser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageServer(object sender, RoutedEventArgs e)
        {
            //AppNavigator.NavigateToPageServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageCatalog(object sender, RoutedEventArgs e)
        {
            //AppNavigator.NavigateToPageCatalog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void NavigateToPageUsers(object sender, RoutedEventArgs e)
        {
            //AppNavigator.NavigateToPageUsers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (SolidColorBrush)FindResource("CustomHighlightBrush");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (SolidColorBrush)FindResource("TransparentBrush");
        }
    }
}