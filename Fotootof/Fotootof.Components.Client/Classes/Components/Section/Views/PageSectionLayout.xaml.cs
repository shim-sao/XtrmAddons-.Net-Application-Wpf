using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.HttpHelpers.HttpClient;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Client.Section
{
    /// <summary>
    /// Class Fotootof Components Client Section Layout.
    /// </summary>
    public partial class PageSectionLayout : ComponentView
    {
        #region Variable

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable <see cref="ClientHttp"/> server.
        /// </summary>
        private ClientHttp svr;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the <see cref="PageSectionModel"/>.
        /// </summary>
        public PageSectionModel Model { get; protected set; }

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        public PageSectionLayout(ClientHttp server)
        {
            MessageBoxs.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageWaiting, "Section Client"));

            // Set page variables and properties.
            svr = server;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Local.Properties.Logs.InitializingPageDone, "Section Client"));
            MessageBoxs.IsBusy = false;
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

            Model = new PageSectionModel(this)
            {
                Server = svr,
                Sections = new DataGridSectionsModel<DataGridSectionsControl>(UcDataGridSections)
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
        /// Method called on <see cref="FrameworkElement"/> size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var bmc = (Grid)FindName("BlockMiddleContentsName");
                
                double height = Math.Max(MainBlockContent.ActualHeight - Block_TopControls.RenderSize.Height, 0);
                double width = Math.Max(MainBlockContent.ActualWidth, 0);

                bmc.Width = width;
                bmc.Height = height;
            }
            catch (Exception ex)
            {
                log.Debug(ex.Output());
            }
        }

        #endregion
    }
}