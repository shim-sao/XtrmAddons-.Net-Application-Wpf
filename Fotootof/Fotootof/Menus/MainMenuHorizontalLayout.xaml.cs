using Fotootof.AddInsContracts.Catalog;
using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Collections.Entities;
using Fotootof.Components.Server;
using Fotootof.Components.Server.Users;
using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Forms.Section;
using Fotootof.Layouts.Forms.User;
using Fotootof.Layouts.Interfaces;
using Fotootof.Layouts.Windows.About;
using Fotootof.Layouts.Forms.Album;
using Fotootof.Layouts.Forms.Client;
using Fotootof.Layouts.Settings;
using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.Libraries.Logs;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Remote;
using XtrmAddons.Net.Common.Extensions;
using Fotootof.Layouts.Forms.Server;
using Fotootof.HttpServer;
using System.Diagnostics;

namespace Fotootof.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Layout.
    /// </summary>
    public partial class MainMenuHorizontalLayout : UserControl, IMenuMain
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="Application"/> <see cref="MainWindow"/>.
        /// </summary>
        public static MainWindow AppWindow
            => Application.Current.MainWindow as MainWindow;

        /// <summary>
        /// Property to access to the main <see cref="Application"/> <see cref="Frame"/>.
        /// </summary>
        public static Frame MainFrame
            => AppWindow.FindName("Frame_Content") as Frame;

        /// <summary>
        /// Event handler for added client <see cref="RoutedEventHandler"/>.
        /// </summary>
        public static event RoutedEventHandler ClientAdded = delegate { };

        /// <summary>
        /// Property to access to the layout model <see cref="MainMenuHorizontalModel"/>.
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

            // Add extensions plugin to menu.
            InitializeExtensions();

        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on plugin control menu click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void MenuControl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IComponent ic = ((IModule)((MenuItem)sender).Tag).Component;
                if (ic != null && ic.Context != null)
                {
                    ComponentNavigator.NavigateToPlugin(ic.Context);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Output());
                MessageBoxs.Error(ex);
            }
        }

        /// <summary>
        /// Method to find an child element by its name.
        /// </summary>
        /// <typeparam name="T">The Type of element to get.</typeparam>
        /// <param name="name">The name of the element to searh.</param>
        /// <returns>The lement to be found or null.</returns>
        public T FindName<T>(string name) where T : class
        {
            return (T)FindName(name);
        }

        /// <summary>
        /// Method to convert a <see cref="FrameworkElement"/> Tag to an <see cref="object"/> as value of <see cref="Type"/> T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fe">An <see cref="object"/> inherited from <see cref="FrameworkElement"/>.</param>
        /// <param name="defaut">A default value.</param>
        /// <returns>The tag value of the <see cref="FrameworkElement"/>.</returns>
        /// <exception cref="ArgumentNullException">Occurs if the <see cref="FrameworkElement"/> fe is null.</exception>
        /// <exception cref="TypeAccessException">Occurs if fe is not a <see cref="FrameworkElement"/>.</exception>
        public static T GetTagAs<T>(object fe, T defaut = null) where T : class
        {
            if (fe is null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(fe), typeof(object));
                log.Error(e.Output(), e);
                throw e;
            }
            
            if(fe is FrameworkElement frameworkElement)
            {
                if (defaut != null)
                {
                    return (T)frameworkElement.Tag ?? defaut;
                }
                else
                {
                    return (T)frameworkElement.Tag;
                }
            }
            else
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void RaiseClientAdded(object sender, RoutedEvent e)
        {
            ClientAdded?.Invoke(this, new RoutedEventArgs(e, sender));
        }

        /// <summary>
        /// Method called on <see cref="FrameworkElement"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
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
        /// Method called on add new client event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        public void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((FrameworkElement)sender).Tag;
            Client client = new Client();

            if (tag != null && tag.GetType() == typeof(Client))
            {
                client = tag as Client;
            }

            using (WindowFormClientLayout dlg = new WindowFormClientLayout(client))
            {
                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    MessageBoxs.IsBusy = true;

                    ApplicationBase.Options.Remote.Clients.AddKeySingle(dlg.NewForm);
                    ApplicationBase.SaveOptions();
                    RaiseClientAdded(this, e.RoutedEvent);

                    MessageBoxs.IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Method called on add user click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            using (WindowFormUserLayout dlg = new WindowFormUserLayout())
            {
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    MessageBoxs.IsBusy = true;
                    log.Warn("Adding or editing User informations. Please wait...");

                    UserEntityCollection.DbInsert(new List<UserEntity> { dlg.NewForm });

                    if (MainFrame?.Content?.GetType() == typeof(PageUsersLayout))
                    {
                        ((PageUsersLayout)MainFrame.Content).Model.Users.Items.Add(dlg.NewForm);
                    }

                    log.Warn("Adding or editing User informations. Done");
                    MessageBoxs.IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Method to save the application settings.
        /// </summary>
        private void SaveSettings()
        {
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
            Trace.TraceInformation(Local.Properties.Logs.WaitingApplicationSave);
            Settings.Controls.Default.Save();
            Settings.Sections.Default.Save();
            ApplicationBase.Save();
        }

        /// <summary>
        /// Method called on open help about dialog window click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            using (WindowAboutLayout about = new WindowAboutLayout())
            {
                about.Show();
            }
        }

        /// <summary>
        /// Method called on language changed click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void LanguageChanged_Click(object sender, RoutedEventArgs e)
        {
            // Ask for user confirmation.
            var result = MessageBoxs.YesNo(Layouts.Dialogs.Properties.Translations.ApplicationRestartRequired, Local.Properties.Translations.Language);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            // Get culture parameters.
            MenuItem menu = (MenuItem)sender;
            string culture = (string)menu.Tag;
            CultureInfo before = Thread.CurrentThread.CurrentCulture;

            // Change application culture info.
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            }
            catch
            {
                Thread.CurrentThread.CurrentUICulture = before;
            }

            // Save language to the application preferences.
            ApplicationBase.Language = Thread.CurrentThread.CurrentCulture.ToString();
            ApplicationBase.Save();

            // Restart the application.
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Method called on theme changed event click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ThemeChanged_Click(object sender, RoutedEventArgs e)
        {
            // Ask for user confirmation.
            var result = MessageBoxs.YesNo(Layouts.Dialogs.Properties.Translations.ApplicationRestartRequired, Local.Properties.Translations.Theme);
            if(result != MessageBoxResult.Yes)
            {
                return;
            }

            // Get the Theme name from the event sender.
            string theme = (string)((FrameworkElement)sender).Tag;

            // Add Theme to Application UI Parmeters.
            ApplicationBase.UI.AddParameter("ApplicationTheme", theme);
            ApplicationBase.SaveUi();

            // Restart the application.
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        #endregion



        #region Methods : Catalog

        /// <summary>
        /// Method called on add album click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CatalogAddAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            using (WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(new AlbumEntity()))
            {
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {

                    log.Info("Adding or editing Album informations. Please wait...");

                    AlbumEntityCollection.DbInsert(new List<AlbumEntity> { dlg.NewForm });

                    log.Info("Adding or editing Section informations. Done");
                    MessageBoxs.IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Method called on add section click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CatalogAddSection_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            using (WindowFormSectionLayout dlg = new WindowFormSectionLayout(new SectionEntity()))
            {
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    log.Info("Adding or editing Section informations. Please wait...");

                    SectionEntityCollection.DbInsert(new List<SectionEntity> { dlg.NewForm });

                    log.Info("Adding or editing Section informations. Done");
                    MessageBoxs.IsBusy = false;
                }
            }
        }

        #endregion



        #region Methods : Edition

        /// <summary>
        /// Method called on edition preferences click event <see cref="WindowSettingsLayout"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void EditionPreferences_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            using (WindowSettingsLayout dlg = new WindowSettingsLayout())
            {
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    App.SaveSettings();
                }
            }
        }

        #endregion



        #region Methods : File

        #endregion



        #region Methods : Navigate To


        /// <summary>
        /// Method called on navigate to <see cref="Components.Server.Remote.PageRemoteLayout"/> click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void NavigateToRemote_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToRemote();
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
        /// Method to navigate to users list view.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void NavigateToUsers_Click(object sender, RoutedEventArgs e)
        {
            ComponentNavigator.NavigateToUsers();
        }

        #endregion



        #region Methods : Server

        /// <summary>
        /// Method called on server start click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerStart_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Start();
        }

        /// <summary>
        /// Method called on server stop click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerStop_Click(object sender, RoutedEventArgs e)
        {
            HttpServerBase.Stop();
        }

        /// <summary>
        /// Method called on server restart click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerRestart_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxs.IsBusy = true;
            ServerStop_Click(sender, e);
            ServerStart_Click(sender, e);
            MessageBoxs.IsBusy = false;
        }

        /// <summary>
        /// Method called on sever edit settings event click.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void ServerSettings_Click(object sender, RoutedEventArgs e)
        {
            using (WindowFormServerLayout dlg = new WindowFormServerLayout(ApplicationBase.Options.Remote.Servers.FindDefaultFirst()))
            {
                try
                {
                    bool? result = dlg.ShowDialog();

                    if (result == true)
                    {
                        dlg.NewFormData.IsDefault = true;

                        var def = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();
                        if (def == null)
                        {
                            ApplicationBase.Options.Remote.Servers.Add(dlg.NewFormData);
                        }
                        else
                        {
                            ApplicationBase.Options.Remote.Servers.ReplaceDefault(dlg.NewFormData);
                        }


                        var eee = ApplicationBase.Options.Remote.Servers;

                        ApplicationBase.SaveOptions();
                        if (HttpWebServerApplication.IsStarted)
                        {
                            ServerRestart_Click(sender, e);
                        }
                    }
                    else
                    {
                        var def = ApplicationBase.Options.Remote.Servers.FindDefaultFirst();
                        def.Bind(dlg.OldFormData);
                    }

                }
                catch (Exception ex)
                {
                    log.Error(ex.Output(), ex);
                    MessageBoxs.Error(ex);
                }
            }
        }

        #endregion
    }
}