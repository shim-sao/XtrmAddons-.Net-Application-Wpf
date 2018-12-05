using Fotootof.Libraries.Enums;
using Fotootof.Theme;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Fotootof.Components.Server.Menus
{
    /// <summary>
    /// Class XtrmAddons Fotootof Components Server Menu.
    /// </summary>
    public partial class SubMenuToolbar : UserControl
    {
        #region Properties

        /// <summary>
        /// Property to access to the <see cref="DisplayMode"/> of the menu.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }
            = DisplayMode.Server;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Components Server Menu Constructor.
        /// </summary>
        public SubMenuToolbar()
        {
            ThemeLoader.MergeThemeTo(Resources);
            InitializeComponent();
        }

        #endregion



        #region Methods
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        private void SwitchSelected(Button selected)
        {
            foreach (Button b in StackPanelSubMenuName.Children.OfType<Button>())
            {
                b.Background = (SolidColorBrush)FindResource("TransparentBrush");
            }

            selected.Background = (SolidColorBrush)FindResource("CustomHighlightBrush");
        }

        /// <summary>
        /// Method to navigate to the browser page.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void NavigateToBrowser_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToBrowser();
            SwitchSelected(((Button)sender));
        }

        /// <summary>
        /// Method to navigate to the Browser component view.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void NavigateToRemote_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToRemote();
            SwitchSelected(((Button)sender));
        }

        /// <summary>
        /// Method to navigate to the Section component view.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void NavigateToSection_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToSection();
            SwitchSelected(((Button)sender));
        }

        /// <summary>
        /// Method to navigate to the Users component view.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void NavigateToUsers_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToUsers();
            SwitchSelected(((Button)sender));
        }

        /// <summary>
        /// Method called on button mouse enter.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The mouse event arguments <see cref="MouseEventArgs"/></param>
        private void ItemButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((Button)sender).Background != (SolidColorBrush)FindResource("CustomHighlightBrush"))
            {
                ((Button)sender).Background = (SolidColorBrush)FindResource("CustomOverBrush");
            }
        }

        /// <summary>
        /// Method called on button mouse leave.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void ItemButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(((Button)sender).Background != (SolidColorBrush)FindResource("CustomHighlightBrush"))
            {
                ((Button)sender).Background = (SolidColorBrush)FindResource("TransparentBrush");
            }
        }

        #endregion
    }
}