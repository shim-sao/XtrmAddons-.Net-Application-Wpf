using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Component.ServerSide.ViewUsers;
using XtrmAddons.Fotootof.Forms.About;
using XtrmAddons.Fotootof.Lib.HttpServer;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpServer;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.SectionForm;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.UserForm;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Settings;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.Menu
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
        /// Method to initialize server menu items.
        /// </summary>
        public void InitializeMenuItemsServer()
        {
            if (HttpWebServerApplication.IsStarted)
            {
                MenuItem_ServerStart.IsEnabled = false;
                MenuItem_ServerStop.IsEnabled = true;
                MenuItem_ServerRestart.IsEnabled = true;
            }
            else
            {
                MenuItem_ServerStart.IsEnabled = true;
                MenuItem_ServerStop.IsEnabled = false;
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
            // Set the row grid splitter Height.
            AppNavigator.MainWindow.RowGridSplitter.Height =
                AppNavigator.MainWindow.RowGridSplitter.Height == new GridLength(0)
                ? new GridLength(5) : new GridLength(0);

            // Set the grid row logs height.
            AppNavigator.MainWindow.RowGridLogs.Height =
                AppNavigator.MainWindow.RowGridLogs.Height == new GridLength(0)
                ? new GridLength(160) : new GridLength(0);

            /*            
            UINavigation.AppWindow.RowGridMain.Height =
                UINavigation.AppWindow.RowGridLogs.Height == new GridLength(0)
                ? new GridLength(UINavigation.AppWindow.ActualHeight)
                : new GridLength(UINavigation.AppWindow.ActualHeight - 220);

            UINavigation.AppWindow.Frame_Content.Height = UINavigation.AppWindow.RowGridMain.Height.Value;
            */
            AppNavigator.MainWindow.UpdateLayout();

            if (MenuItemDisplayLogsWindow.IsChecked == true)
            {
                MenuItemDisplayLogsWindow.IsChecked = false;
            }
            else
            {
                MenuItemDisplayLogsWindow.IsChecked = true;
            }

            AppNavigator.MainWindow.UpdateLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisplayHelpAboutClick(object sender, RoutedEventArgs e)
        {
            FormAbout about = new FormAbout();
            about.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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
                AppLogger.Info("Adding or editing User informations. Please wait...");

                UserEntityCollection.DbInsert(new List<UserEntity> { dlg.NewForm });

                if(MainFrame.Content.GetType() == typeof(PageUsers))
                {
                    ((PageUsers)MainFrame.Content).Model.Users.Items.Add(dlg.NewForm);
                }

                AppLogger.Info("Adding or editing User informations. Done");
                AppLogger.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddSection_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormSection dlg = new WindowFormSection(new SectionEntity());
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                AppLogger.Info("Adding or editing Section informations. Please wait...");
                
                SectionEntityCollection.DbInsert(new List<SectionEntity> { dlg.NewForm });

                AppLogger.Info("Adding or editing Section informations. Done");
                AppLogger.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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

        #endregion Methods
    }
}