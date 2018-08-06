using Fotootof.AddInsContracts.Catalog;
using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Layouts.Windows.About;
using Fotootof.Layouts.Windows.Forms.Album;
using Fotootof.Layouts.Windows.Forms.Client;
using Fotootof.Layouts.Windows.Forms.Section;
using Fotootof.Layouts.Windows.Forms.User;
using Fotootof.Layouts.Windows.Settings;
using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.Libraries.Systems;
using Fotootof.Theme;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;

namespace Fotootof.Layouts.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Layout.
    /// </summary>
    public partial class MainMenuHorizontalLayout : UserControl
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the application main window.
        /// </summary>
        public static object AppWindow => Application.Current.MainWindow;

        /// <summary>
        /// Property to access to the main application frame.
        /// </summary>
        public static Frame MainFrame
            => (AppWindow as Window).FindName("Frame_Content") as Frame;

        /// <summary>
        /// Property client added routed event handler.
        /// </summary>
        public static event RoutedEventHandler ClientAdded = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public MainMenuHorizontalModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Constructor.
        /// </summary>
        public MainMenuHorizontalLayout()
        {
            ThemeLoader.MergeThemeTo(Resources);

            // Initialize menu component
            InitializeComponent();

            // Add server handler.
            HttpServerBase.NotifyServerStartedHandlerOnce += (s, e) => { InitializeMenuItemsServer(); };
            HttpServerBase.NotifyServerStoppedHandlerOnce += (s, e) => { InitializeMenuItemsServer(); };

            // Initialize the model.
            Model = new MainMenuHorizontalModel(this);

            // Add plugin to menu.
            try
            {
                var extensions = CatalogBase.Extensions;

                if (extensions != null)
                {
                    foreach (IExtension ext in extensions)
                    {
                        var module = ext.Module;

                        log.Info($"Loading main menu Module : {ext.GetType()}");
                        var fe = FindName(module.ParentName);

                        if(!(fe is MenuItem menuItem) || menuItem == null)
                        {
                            continue;
                        }

                        menuItem.Items.Add(module.MenuItem);
                        module.MenuItem.Click += MenuControl_Click;
                    }
                }
            }
            catch(CompositionException e)
            {
                log.Error(e.Errors);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuControl_Click(object sender, RoutedEventArgs e)
        {
            IComponent ic = ((IModule)((MenuItem)sender).Tag).Component;

            if (ic != null && ic.Context != null)
            {
    //            MainNavigator.Server.Plugin(ic.Context);
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on client added routed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event.</param>
        private void RaiseClientAdded(object sender, RoutedEvent routedEvent)
        {
            ClientAdded?.Invoke(this, new RoutedEventArgs(routedEvent, sender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLogsWindow();
        }

        /// <summary>
        /// Method to initialize log window menu item.
        /// </summary>
        public void InitializeLogsWindow()
        {
            // Toggle check the right Theme MenuItem.
            ToggleMenuItemTheme();

            // Toggle the Logs Frame & check/uncheck the Logs MenuItem. 
            MenuItem mi = (MenuItem)FindName("MenuItemDisplayLogs");
            var a = 
            //mi.IsChecked = AppSettings.GetBool(mi, "IsChecked", mi.IsChecked);
            mi.IsChecked = Settings.Controls.Default.MainMenuMenuItemDisplayLogsIsChecked;
            if (mi.IsChecked)
            {
//                (Application.Current.MainWindow as MainWindow).ToggleLogs();
            }
        }

        /// <summary>
        /// Method to initialize server menu items.
        /// </summary>
        public void InitializeMenuItemsServer()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                ((MenuItem)FindName("MenuItemServerStartName")).IsEnabled = false;
                ((MenuItem)FindName("MenuItemServerStopName")).IsEnabled = true;
                ((MenuItem)FindName("MenuItemServerRestartName")).IsEnabled = true;
            }
            else
            {
                ((MenuItem)FindName("MenuItemServerStartName")).IsEnabled = true;
                ((MenuItem)FindName("MenuItemServerStopName")).IsEnabled = false;
                ((MenuItem)FindName("MenuItemServerRestartName")).IsEnabled = false;
            }
        }

        /// <summary>
        /// Method called on server start click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerStart_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Start();
        }

        /// <summary>
        /// Method called on server stop click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerStop_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Stop();
        }

        /// <summary>
        /// Method called on server restart click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerRestart_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.IsBusy = true;
            OnServerStop_Click(sender, e);
            OnServerStart_Click(sender, e);
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Method called on add server to firewall click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerAddToFirewall_Click(object sender, RoutedEventArgs e)
            => HttpServerBase.AddNetworkAcl();

        /// <summary>
        /// Method called on remove server from firwall click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnServerRemoveFromFirewall_Click(object sender, RoutedEventArgs e)
            => HttpServerBase.RemoveNetworkAcl();

        /// <summary>
        /// Method called on display logs window click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnDisplayLogsWindowClick(object sender, RoutedEventArgs e)
        {
 //           (Application.Current.MainWindow as MainWindow).ToggleLogs();
            MenuItem mi = (MenuItem)FindName("MenuItemDisplayLogs");
            //AppSettings.SaveBool(mi, "IsChecked", mi.IsChecked);
            Settings.Controls.Default.MainMenuMenuItemDisplayLogsIsChecked = mi.IsChecked;
            Settings.Controls.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            //FormAbout about = new FormAbout();
            //about.Show();

            WindowAboutLayout about = new WindowAboutLayout();
            about.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void LanguageChanged_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            string culture = (string)menu.Tag;

            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                menu.IsChecked = true;

            }
            catch
            {
                Thread.CurrentThread.CurrentUICulture = before;
            }

            ApplicationBase.Language = Thread.CurrentThread.CurrentCulture.ToString();
            ApplicationBase.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnDisplayLanguageFrClick(object sender, RoutedEventArgs e)
        {
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = before;
            }

            ApplicationBase.Language = Thread.CurrentThread.CurrentCulture.ToString();
            ApplicationBase.Save();
        }

        /// <summary>
        /// Method to navigate to users list view.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments</param>
        private void OnUsersListClick(object sender, RoutedEventArgs e)
        {
 //           ComponentNavigator.LoadPage(nameof(PageUsers), new PageUsers());
        }

        /// <summary>
        /// Method called on add new client event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments</param>
        public void OnServerAddClient_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((FrameworkElement)sender).Tag;
            Client client = new Client();

            if (tag != null && tag.GetType() == typeof(Client))
            { 
                client = tag as Client;
            }

            WindowFormClientLayout dlg = new WindowFormClientLayout(client);
            bool? result = dlg.ShowDialog();
            
            if (result == true)
            {
                MessageBase.IsBusy = true;

                ApplicationBase.Options.Remote.Clients.AddKeySingle(dlg.NewForm);
                ApplicationBase.SaveOptions();
                RaiseClientAdded(this, e.RoutedEvent);

                MessageBase.IsBusy = false;
            }
        }

        private void ServerSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Method to navigate to new user view.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">Routed event arguments</param>
        private void OnAddUser_Click(object sender, RoutedEventArgs e)
        {
            WindowFormUserLayout dlg = new WindowFormUserLayout();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                MessageBase.IsBusy = true;
                log.Warn("Adding or editing User informations. Please wait...");

                UserEntityCollection.DbInsert(new List<UserEntity> { dlg.NewForm });

  /*              if(MainFrame?.Content?.GetType() == typeof(Server.Components.Users.PageUsersLayout))
                {
//                    ((PageUsersLayout)MainFrame.Content).Model.Users.Items.Add(dlg.NewForm);
                }*/

                log.Warn("Adding or editing User informations. Done");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnAddSection_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormSectionLayout dlg = new WindowFormSectionLayout(new SectionEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                log.Info("Adding or editing Section informations. Please wait...");
                
                SectionEntityCollection.DbInsert(new List<SectionEntity> { dlg.NewForm });

                log.Info("Adding or editing Section informations. Done");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void OnAddAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(new AlbumEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {

                log.Info("Adding or editing Album informations. Please wait...");

                AlbumEntityCollection.DbInsert(new List<AlbumEntity> { dlg.NewForm });

                log.Info("Adding or editing Section informations. Done");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void EditionPreferences_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowSettingsLayout dlg = new WindowSettingsLayout();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
               
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ThemeChanged_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBase.YesNo("This action require to restart application. Do you want to continue ?", "Theme");
            if(result != MessageBoxResult.Yes)
            {
                return;
            }

            // Get the Theme name from the event sender.
            string theme = (string)((FrameworkElement)sender).Tag;

            // Add Theme to Application UI Parmeters.
            ApplicationBase.UI.AddParameter("ApplicationTheme", theme);
            ApplicationBase.SaveUi();

            // Check the right theme MenuItem.
            //ToggleMenuItemTheme

            // Restart the application.
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ToggleMenuItemTheme()
        {
            // Get the Theme for Application Parameters.
            string theme = ApplicationBase.UI.GetParameter("ApplicationTheme", "Light");

            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Theme => {theme}");

            // Check the right theme MenuItem.
            switch (theme)
            {
                case "Light":
                    (FindName("MenuItem_ThemeLight") as MenuItem).IsChecked = true;
                    (FindName("MenuItem_ThemeDark") as MenuItem).IsChecked = false;
                    break;

                case "Dark":
                    (FindName("MenuItem_ThemeLight") as MenuItem).IsChecked = false;
                    (FindName("MenuItem_ThemeDark") as MenuItem).IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// Method called on navigate to <see cref="PageCatalog"/> click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CatalogNavigateTo_Click(object sender, RoutedEventArgs e)
        {
//MainNavigator.Server.NavigateToPageCatalog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerRemote_Click(object sender, RoutedEventArgs e)
        {
//AppNavigator.NavigateToPageServer();
        }

        #endregion
    }
}