using System;
using System.Collections.Generic;
using System.Windows;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient.Responses;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Component.ClientSide.ViewCatalog
{
    /// <summary>
    /// Logique d'interaction pour PageClient.xaml
    /// </summary>
    public partial class PageCatalogClient : PageBase
    {
        #region Variable

        /// <summary>
        /// Variable client http server.
        /// </summary>
        private ClientHttp svr;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageCatalogClientModel<PageCatalogClient> Model { get; protected set; }

        #endregion



        #region Constructor

        public PageCatalogClient(ClientHttp server)
        {
            svr = server;

            InitializeComponent();
            AfterInitializedComponent();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContentAsync()
        {
            // Paste page to the model & child elements.
            Model = new PageCatalogClientModel<PageCatalogClient>(this)
            {
                Server = svr,
                Sections = new DataGridSectionsModel<DataGridSections>(UcDataGridSections)
            };

            DataContext = Model;

            Model.Server.Authentication();
            Model.Server.ListSections();

            AppOverwork.IsBusy = false;
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

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        #endregion
    }
}