using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Common.Models.DataGrids;
using XtrmAddons.Fotootof.Common.Tools;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;

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
            AppOverwork.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageWaiting, "PageCatalogClient"));

            // Set page variables and properties.
            svr = server;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();
            
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageDone, "PageCatalogClient"));
            AppOverwork.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
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
            /*
            if (command)
            {
                command = Model.Server.ListSections().IsCompleted;

                log.Debug($"Sending command ListSections : {command}");
            }

    */
        }

        #endregion



        #region Methods Section

        /// <summary>
        /// Method to load the list of Section from server.
        /// </summary>
        //private void LoadSections(ClientHttpEventArgs<ServerResponseSections> serverResponse, bool reset = false)
        //{
        //    try
        //    {
        //        log.Info("Loading Sections list. Please wait...");

        //        List<SectionEntity> l = new List<SectionEntity>();
        //        foreach (SectionJson s in serverResponse.Response.Response)
        //        {
        //            l.Add(s.ToEntity());
        //        }

        //        if (reset || Model.Sections == null)
        //        {
        //            Model.Sections = new DataGridSectionsModel<DataGridSections>(UCDataGridSections);
        //        }
        //        Model.Sections.Items = new SectionEntityCollection(l);
        //        AppLogger.InfoAndClose("Loading Sections list. Done.");
        //    }
        //    catch (Exception e)
        //    {
        //        AppLogger.Fatal("Loading Sections list failed : " + e.Message, e);
        //    }
        //}

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



        #region Obsoletes

        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        [Obsolete("Will be remove. None sense...")]
        public override void Page_Loaded_Async(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}