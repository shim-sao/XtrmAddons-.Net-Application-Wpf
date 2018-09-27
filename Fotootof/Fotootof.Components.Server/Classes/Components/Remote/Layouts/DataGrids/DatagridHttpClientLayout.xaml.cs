using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Controls;
using Fotootof.Libraries.HttpHelpers.HttpClient;
using System.Windows;
using System.Windows.Controls;
using RemoteClient = XtrmAddons.Net.Application.Serializable.Elements.Remote.Client;

namespace Fotootof.Components.Server.Remote.Layouts
{
    /// <summary>
    /// Logique d'interaction pour DatagridHttpClient.xaml
    /// </summary>
    public partial class DatagridHttpClientLayout : ControlLayout
    {
        #region Properties

        /// <summary>
        /// Accessors to Window AclGroup Form model.
        /// </summary>
        public DataGridHttpClientModel<DatagridHttpClientLayout> Model { get; private set; }

        /// <summary>
        /// Property to access to the application main window.
        /// </summary>
        public static Window AppWindow => Application.Current.MainWindow;

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public DatagridHttpClientLayout()
        {
            InitializeComponent();

            Model = new DataGridHttpClientModel<DatagridHttpClientLayout>(this);
            DataContext = Model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
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
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            (AppWindow.FindName("AppMainMenu") as IMenuMain).AddClient_Click(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Explore_Click(object sender, RoutedEventArgs e)
        {
            RemoteClient client = ((FrameworkElement)sender).Tag as RemoteClient;
            ClientHttp clientHttp = new ClientHttp(client);

           ComponentNavigator.NavigateToSectionClient(clientHttp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
