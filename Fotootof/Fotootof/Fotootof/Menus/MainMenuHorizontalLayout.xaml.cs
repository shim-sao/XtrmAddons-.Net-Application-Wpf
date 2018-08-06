using Fotootof.AddInsContracts.Catalog;
using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Components.Server;
using Fotootof.Components.Server.Users;
using Fotootof.Layouts.Interfaces;
using Fotootof.Layouts.Windows.About;
using Fotootof.Layouts.Windows.Forms.Album;
using Fotootof.Layouts.Windows.Forms.Client;
using Fotootof.Layouts.Windows.Forms.Section;
using Fotootof.Layouts.Windows.Forms.User;
using Fotootof.Layouts.Windows.Settings;
using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.Libraries.Logs;
using Fotootof.Libraries.Systems;
using Fotootof.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.HttpServer;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Layout.
    /// </summary>
    public partial class MainMenuHorizontalLayout : UserControl, IMenuMain
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
        public static MainWindow AppWindow => Application.Current.MainWindow as MainWindow;

        /// <summary>
        /// Property to access to the main application frame.
        /// </summary>
        public static Frame MainFrame
            => AppWindow.FindName("Frame_Content") as Frame;

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
        /// Class Fotootof Main Menu Horizontal Layout Constructor.
        /// </summary>
        public MainMenuHorizontalLayout()
        {
            // Add custom theme to resources.
            ThemeLoader.MergeThemeTo(Resources);

            // Initialize the model.
            Model = new MainMenuHorizontalModel(this);

            // Initialize menu component
            InitializeComponent();

            // Add server handler.
            HttpServerBase.NotifyServerStartedHandlerOnce += (s, e) => { InitializeMenuItemsServer(); };
            HttpServerBase.NotifyServerStoppedHandlerOnce += (s, e) => { InitializeMenuItemsServer(); };

            // Add extensions plugin to menu.
            InitializeExtensions();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuControl_Click(object sender, RoutedEventArgs e)
        {
            IComponent ic = ((IModule)(sender as MenuItem).Tag).Component;

            if (ic != null && ic.Context != null)
            {
                ComponentNavigator.Plugin(ic.Context);
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fe"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindName<T>(string name) where T : class
        {
            return (T)FindName(name);
        }

        /// <summary>
        /// Method to convert a <see cref="FrameworkElement"/> Tag to an <see cref="object"/> of <see cref="Type"/> T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fe">An <see cref="object"/> inherited from <see cref="FrameworkElement"/>.</param>
        /// <param name="defaut"></param>
        /// <returns></returns>
        public static T GetTag<T>(object fe, T defaut = null) where T : class
        {
            if (fe is null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(fe), fe);
                log.Error(e.Output(), e);
                throw e;
            }

            if (!fe.GetType().IsSubclassOf(typeof(FrameworkElement)))
            {
                TypeAccessException e = new TypeAccessException
                (
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Libraries.Logs.Properties.Translations.TypeAccessExceptionFrameworkElement,
                        nameof(fe),
                        fe.GetType()
                    )
                );

                log.Error(e.Output(), e);
                throw e;
            }

            FrameworkElement frameworkElement = (fe as FrameworkElement);
            if (defaut != null)
            {
                return (T)frameworkElement.Tag ?? defaut;
            }
            else
            {
                return (T)frameworkElement.Tag;
            }
        }

        /// <summary>
        /// Method to add extensions plugin to menu.
        /// </summary>
        public void InitializeExtensions()
        {
            // 
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

                        if (!(fe is MenuItem menuItem) || menuItem == null)
                        {
                            continue;
                        }

                        menuItem.Items.Add(module.MenuItem);
                        module.MenuItem.Click += MenuControl_Click;
                    }
                }
            }
            catch (CompositionException e)
            {
                log.Error(e.Errors);
            }

        }

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
        }

        /// <summary>
        /// Method to initialize server menu items.
        /// </summary>
        public void InitializeMenuItemsServer()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                FindName<MenuItem>("MenuItemServerStartName").IsEnabled = false;
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnServerStart_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Start();
        }

        /// <summary>
        /// Method called on server stop click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnServerStop_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Stop();
        }

        /// <summary>
        /// Method called on server restart click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnServerAddToFirewall_Click(object sender, RoutedEventArgs e)
            => HttpServerBase.AddNetworkAcl();

        /// <summary>
        /// Method called on remove server from firwall click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnServerRemoveFromFirewall_Click(object sender, RoutedEventArgs e)
            => HttpServerBase.RemoveNetworkAcl();

        /// <summary>
        /// Method called on display logs frame click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ShowLogsFrame_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the log frame from main window.
            AppWindow.ToggleLogs();

            // Save application controls settings.
            Settings.Controls.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            WindowAboutLayout about = new WindowAboutLayout();
            about.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void NavigateToUsers_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToUsers();
        }

        /// <summary>
        /// Method called on add new client event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement).Tag;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented();
        }

        /// <summary>
        /// Method to navigate to new user view.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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

                if(MainFrame?.Content?.GetType() == typeof(PageUsersLayout))
                {
                      ((PageUsersLayout)MainFrame.Content).Model.Users.Items.Add(dlg.NewForm);
                }

                log.Warn("Adding or editing User informations. Done");
                MessageBase.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
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
            string theme = (string)(sender as FrameworkElement).Tag;

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
            string theme = ApplicationBase.UI.GetParameter("ApplicationTheme", ThemeLoader.defaultAssemblyDictionary);

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
        /// Method called on the navigate to <see cref="Components.Server.Section.PageSectionLayout"/> click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void NavigateToSection_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToSection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerRemote_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToBrowser();
        }

        #endregion
    }
}