using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Enums;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.UI.Control.Menu
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Client UI Control Main Menu Navigation.
    /// </summary>
    public partial class UCMainNavigation : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Server;
        
        /// <summary>
        /// 
        /// </summary>
        public UCMainNavigation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageBrowser(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageBrowser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageServer(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageCatalog(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageCatalog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageUsers(object sender, RoutedEventArgs e)
        {
            AppNavigator.NavigateToPageUsers();
        }
    }
}