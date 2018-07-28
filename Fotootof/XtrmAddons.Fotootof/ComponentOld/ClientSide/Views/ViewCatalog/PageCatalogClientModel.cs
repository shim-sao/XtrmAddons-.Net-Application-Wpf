using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient.Responses;
using XtrmAddons.Fotootof.Common.Models.DataGrids;
using XtrmAddons.Net.Common.Extensions;
using System.Reflection;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;

namespace XtrmAddons.Fotootof.ComponentOld.ClientSide.Views.ViewCatalog
{
    public class PageCatalogClientModel : PageBaseModel<PageCatalogClient>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable observable collection of Sections.
        /// </summary>
        private DataGridSectionsModel<DataGridSections> sections;

        /// <summary>
        /// Variable observable collection of Albums.
        /// </summary>
        private ListViewAlbumsModel albums;

        /// <summary>
        /// Variable client http server.
        /// </summary>
        private ClientHttp server;

        /// <summary>
        /// Variable selected Section.
        /// </summary>
        private SectionEntity selectedSection;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the selected Section.
        /// </summary>
        public SectionEntity SelectedSection
        {
            get => selectedSection;
            set
            {
                if (selectedSection != value)
                {
                    selectedSection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Section.
        /// </summary>
        public DataGridSectionsModel<DataGridSections> Sections
        {
            get => sections;
            set
            {
                if (sections != value)
                {
                    sections = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Albums
        /// </summary>
        public ListViewAlbumsModel Albums
        {
            get => albums;
            set
            {
                if (albums != value)
                {
                    albums = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Albums
        /// </summary>
        public ClientHttp Server
        {
            get => server;
            set
            {
                if (server != value)
                {
                    server = value;
                    AddClientHttp();
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public PageCatalogClientModel(PageCatalogClient pageBase) : base(pageBase) { }

        #endregion



        #region Methods

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        private void AddClientHttp()
        {
            if (server == null)
            {
                log.Warn(new ArgumentNullException(nameof(server)));
            }
            else
            { 
                // Add authentication success event handler.
                server.OnAuthenticationSuccess -= Svr_OnAuthenticationSuccess;
                server.OnAuthenticationSuccess += new EventHandler(Svr_OnAuthenticationSuccess);

                // Add authentication failed event handler.
                server.OnAuthenticationFailed -= Svr_OnAuthenticationFailed;
                server.OnAuthenticationFailed += new EventHandler(Svr_OnAuthenticationFailed);

                // Add authentication Unauthorized event handler.
                server.OnAuthenticationUnauthorized -= Svr_OnAuthenticationUnauthorized;
                server.OnAuthenticationUnauthorized += new EventHandler(Svr_OnAuthenticationUnauthorized);

                // Add list sections success event handler.
                server.OnListSectionsSuccess -= Svr_OnListSectionsSuccess;
                server.OnListSectionsSuccess += new EventHandler(Svr_OnListSectionsSuccess);

                // Add list sections failed event handler.
                server.OnListSectionsFailed -= Svr_OnListSectionsFailed;
                server.OnListSectionsFailed += new EventHandler(Svr_OnListSectionsFailed);

                // Add single section success event handler.
                server.OnSingleSectionSuccess -= Svr_OnSingleSectionSuccess;
                server.OnSingleSectionSuccess += new EventHandler(Svr_OnSingleSectionSuccess);
            }
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private async void Svr_OnAuthenticationSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            await Server.ListSections();
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnAuthenticationFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            try
            {
                log.Debug(svrResp.Result.Error);
                System.Windows.MessageBox.Show($"Authentication to the server failed : {svrResp.Result.Error}", "Authentication error");
            }
            catch(Exception ex)
            {
                log.Error(ex);
                log.Error(svrResp);
                System.Windows.MessageBox.Show($"Authentication to the server failed : {ex.Output()}", "Authentication error");
            }
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnAuthenticationUnauthorized(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            try
            {
                log.Debug(svrResp.Result.Error);
                MessageBase.Error($"Authentication to the server failed : {svrResp.Result.Error}");
            }
            catch(Exception ex)
            {
                log.Error(ex.Output(), ex);
                log.Error(svrResp);
                MessageBase.Error($"Authentication to the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command list sections success.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnListSectionsSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSections> serverResponse) || serverResponse.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            List<SectionJson> list = serverResponse.Result.Response ?? new List<SectionJson>();
            Sections.Items = new SectionEntityCollection(list);
        }

        /// <summary>
        /// Method called on server command list sections failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnListSectionsFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSections> serverResponse) || serverResponse.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            try
            {
                log.Debug(serverResponse.Result.Error);
                MessageBase.Error($"List sections from the server failed : {serverResponse.Result.Error}");
            }
            catch (Exception ex)
            {
                log.Error(serverResponse);
                log.Error(ex.Output(), ex);
                MessageBase.Error($"List sections from the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command single section success.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnSingleSectionSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSection> svrResp) || svrResp.Result == null)
            {
                MessageBase.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            if(Albums == null)
            {
                Albums = new ListViewAlbumsModel();
            }

            try
            {

                SectionJson section = svrResp.Result.Response;

                var a = Albums.Items;

                if (section != null && section.Albums != null)
                {
                    Albums.Items = new AlbumEntityCollection(section.Albums);
                }
            }
            catch (Exception ex)
            {
                log.Error(svrResp);
                log.Error(ex.Output(), ex);
                MessageBase.Error($"Single section from the server failed : {ex.Output()}");
            }
        }

        #endregion
    }
}
