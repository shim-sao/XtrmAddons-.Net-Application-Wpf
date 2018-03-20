using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Enums;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Menu
{
    /// <summary>
    /// Class XtrmAddons PhotoAlbum Client UI Control Main Menu Navigation.
    /// </summary>
    public partial class MenuServerMain : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Server;
        
        /// <summary>
        /// 
        /// </summary>
        public MenuServerMain()
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
            Navigator.NavigateToPageBrowser();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageServer(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateToPageServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageCatalog(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateToPageCatalog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToPageUsers(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateToPageUsers();
        }
    }
}