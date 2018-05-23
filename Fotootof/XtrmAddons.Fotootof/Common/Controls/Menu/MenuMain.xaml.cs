using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Builders.AddInsContracts;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Common.Windows.Forms;
using XtrmAddons.Fotootof.Common.Windows.Forms.SectionForm;
using XtrmAddons.Fotootof.Common.Windows.Forms.UserForm;
using XtrmAddons.Fotootof.Common.Windows.Settings;
using XtrmAddons.Fotootof.Component.ServerSide.Views.ViewUsers;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;
using XtrmAddons.Fotootof.Layouts.Windows.About;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Application.Serializable.Elements.XmlUiElement;

namespace XtrmAddons.Fotootof.Common.Controls.Menu
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main.
    /// </summary>
    public partial class MenuMain : UserControl
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the application main window.
        /// </summary>
        public static MainWindow AppWindow => (MainWindow)Application.Current.MainWindow;

        /// <summary>
        /// Property to access to the main application frame.
        /// </summary>
        public static Frame MainFrame => AppWindow.Frame_Content;

        /// <summary>
        /// Property client added routed event handler.
        /// </summary>
        public static event RoutedEventHandler ClientAdded = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public MenuMainModel<MenuMain> Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Constructor.
        /// </summary>
        public MenuMain()
        {
            InitializeComponent();
            HttpServerBase.AddStartHandlerOnce((s, e) => { InitializeMenuItemsServer(); });
            HttpServerBase.AddStopHandlerOnce((s, e) => { InitializeMenuItemsServer(); });
            Model = new MenuMainModel<MenuMain>(this);

            try
            {
                DirectoryCatalog catalog = new DirectoryCatalog("Plugins");

                InterfaceBuilder builder = new InterfaceBuilder();
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(builder);

                foreach (IModule attacher in builder.Attachers)
                {
                    MenuItem_Plugins.Items.Add(attacher.GetInterfaceObject());
                    //attacher.Container = MenuItem_Plugins;
                }
            }
            catch(CompositionException e)
            {
                log.Error(e.Errors);
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
        /// Method to initialize server menu items.
        /// </summary>
        public void InitializeLogsWindow()
        {
            var ctrl = Model.ShowLogsWindow;

            if (ctrl == null)
            {
                Model.ShowLogsWindow = new UiElement(MenuItem_ShowLogsWindow);
                ApplicationBase.Save();
            }
            else
            {
                if(ctrl.JsonContext != null)
                {
                    MenuItem_ShowLogsWindow = ctrl.ToControl(MenuItem_ShowLogsWindow);
                }

                if (MenuItem_ShowLogsWindow.IsChecked)
                {
                    AppNavigator.MainWindow.LogsToggle();
                }
            }
        }

        /// <summary>
        /// Method to initialize server menu items.
        /// </summary>
        public void InitializeMenuItemsServer()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                MenuItem_Server_Start.IsEnabled = false;
                MenuItem_Server_Stop.IsEnabled = true;
                MenuItem_ServerRestart.IsEnabled = true;
            }
            else
            {
                MenuItem_Server_Start.IsEnabled = true;
                MenuItem_Server_Stop.IsEnabled = false;
                MenuItem_ServerRestart.IsEnabled = false;
            }
        }

        /// <summary>
        /// Method called on file exit click.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OnFileExit_Click(object sender, RoutedEventArgs e)
        {
            AppNavigator.MainWindow.Close();
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
            AppOverwork.IsBusy = true;
            OnServerStop_Click(sender, e);
            OnServerStart_Click(sender, e);
            AppOverwork.IsBusy = false;
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
            AppNavigator.MainWindow.LogsToggle();
            Model.ShowLogsWindow = new UiElement(MenuItem_ShowLogsWindow);
            ApplicationBase.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnDisplayHelpAboutClick(object sender, RoutedEventArgs e)
        {
            //FormAbout about = new FormAbout();
            //about.Show();

            WindowAbout about = new WindowAbout();
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
            AppNavigator.LoadPage(nameof(PageUsers), new PageUsers());
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
            
            WindowClientForm dlg = new WindowClientForm(client);
            bool? result = dlg.ShowDialog();
            
            if (result == true)
            {
                AppOverwork.IsBusy = true;

                ApplicationBase.Options.Remote.Clients.ReplaceKeyUnique(dlg.NewForm);
                ApplicationBase.Save();
                RaiseClientAdded(this, e.RoutedEvent);

                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// Method to navigate to new user view.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">Routed event arguments</param>
        private void OnAddUser_Click(object sender, RoutedEventArgs e)
        {
            WindowFormUser dlg = new WindowFormUser();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                log.Info("Adding or editing User informations. Please wait...");

                UserEntityCollection.DbInsert(new List<UserEntity> { dlg.NewForm });

                if(MainFrame.Content.GetType() == typeof(PageUsers))
                {
                    ((PageUsers)MainFrame.Content).Model.Users.Items.Add(dlg.NewForm);
                }

                log.Info("Adding or editing User informations. Done");
                AppOverwork.IsBusy = false;
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
            WindowFormSection dlg = new WindowFormSection(new SectionEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                log.Info("Adding or editing Section informations. Please wait...");
                
                SectionEntityCollection.DbInsert(new List<SectionEntity> { dlg.NewForm });

                log.Info("Adding or editing Section informations. Done");
                AppOverwork.IsBusy = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void OnEditionPreferences_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowSettings dlg = new WindowSettings();
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
               
            }

        }

        #endregion
    }
}