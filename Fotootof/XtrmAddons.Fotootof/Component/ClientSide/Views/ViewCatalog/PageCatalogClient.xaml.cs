using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Common.Models.DataGrids;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Component.ClientSide.Views.ViewCatalog
{
    /// <summary>
    /// Logique d'interaction pour PageClient.xaml
    /// </summary>
    public partial class PageCatalogClient : PageBase
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable client http server.
        /// </summary>
        private ClientHttp svr;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageCatalogClientModel Model { get; protected set; }



        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        public PageCatalogClient(ClientHttp server)
        {
            MessageBase.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageWaiting, "Catalog Client"));

            // Set page variables and properties.
            svr = server;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();
            
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageDone, "Catalog Client"));
            MessageBase.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

            UcDataGridSections.ItemsDataGrid.SelectionChanged += Sections_SelectionChangedAsync;
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override async void InitializeModel()
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            Model = new PageCatalogClientModel(this)
            {
                Server = svr,
                Sections = new DataGridSectionsModel<DataGridSections>(UcDataGridSections)
            };
            
            bool command = await Model.Server.Authentication();

            log.Debug($"Sending command Authentication : {command}");
        }

        #endregion



        #region Methods Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Sections_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            await Model.Server.SingleSection(((SectionEntity)UcDataGridSections.ItemsDataGrid.SelectedItem).PrimaryKey);
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            this.Width = fe.ActualWidth;
            this.Height = fe.ActualHeight;

            Block_MiddleContents.Width = this.Width;
            Block_MiddleContents.Height = this.Height - Block_TopControls.RenderSize.Height;

            UcDataGridSections.Height = this.Height - Block_TopControls.RenderSize.Height;
            //UcListViewAlbums.Height = this.Height - Block_TopControls.RenderSize.Height;
        }

        #endregion
    }
}