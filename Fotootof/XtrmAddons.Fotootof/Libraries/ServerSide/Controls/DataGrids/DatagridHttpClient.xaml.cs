﻿using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.DataGrids
{
    /// <summary>
    /// Logique d'interaction pour DatagridHttpClient.xaml
    /// </summary>
    public partial class DatagridHttpClient : ControlBase
    {
        #region Properties

        /// <summary>
        /// Accessors to Window AclGroup Form model.
        /// </summary>
        public DataGridHttpClientModel<DatagridHttpClient> Model { get; private set; }

        /// <summary>
        /// Property to access to the application main window.
        /// </summary>
        public static MainWindow AppWindow => (MainWindow)Application.Current.MainWindow;

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public DatagridHttpClient()
        {
            InitializeComponent();

            Model = new DataGridHttpClientModel<DatagridHttpClient>(this);
            DataContext = Model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if(dg.SelectedItems.Count > 0)
            {
                Button btn = new Button() { Tag = dg.SelectedItem };
                Edit_Click(btn, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AppWindow.UCMainMenuServer.OnServerAddClient_Click(sender, e);
        }

        private void Explore_Click(object sender, RoutedEventArgs e)
        {
            Client client = ((FrameworkElement)sender).Tag as Client;
            ClientHttp clientHttp = new ClientHttp(client);

            Navigator.NavigateToPageCatalogClient(clientHttp);
        }
    }
}