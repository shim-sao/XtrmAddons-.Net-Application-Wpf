using System;
using System.Collections.Generic;
using XtrmAddons.Net.Common.Extensions;
using System.Reflection;
using Fotootof.Libraries.Components;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Libraries.HttpHelpers.HttpClient;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.Layouts.Dialogs;
using Fotootof.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using Fotootof.Libraries.HttpHelpers.HttpClient.Responses;

namespace Fotootof.Components.Client.Section
{
    /// <summary>
    /// Class XtrmAddons Fotootof Client Component Server Side View Catalog Model.
    /// </summary>
    public class PageSectionModel : ComponentModel<PageSectionLayout>
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
        private DataGridSectionsModel<DataGridSectionsControl> sections;

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
        public DataGridSectionsModel<DataGridSectionsControl> Sections
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
        /// Class XtrmAddons Fotootof Client Component Models List Sections Constructor.
        /// </summary>
        /// <param name="controlView">The page associated to the model.</param>
        public PageSectionModel(PageSectionLayout controlView) : base(controlView) { }

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
        /// <param name="e">The event arguments</param>
        private async void Svr_OnAuthenticationSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            await Server.ListSections();
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments</param>
        private void Svr_OnAuthenticationFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
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
        /// <param name="e">The event arguments</param>
        private void Svr_OnAuthenticationUnauthorized(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponse> svrResp) || svrResp.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            try
            {
                log.Debug(svrResp.Result.Error);
                MessageBoxs.Error($"Authentication to the server failed : {svrResp.Result.Error}");
            }
            catch(Exception ex)
            {
                log.Error(ex.Output(), ex);
                log.Error(svrResp);
                MessageBoxs.Error($"Authentication to the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command list sections success.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments</param>
        private void Svr_OnListSectionsSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSections> serverResponse) || serverResponse.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            List<SectionJson> list = serverResponse.Result.Response ?? new List<SectionJson>();
            Sections.Items = new SectionEntityCollection(list);
        }

        /// <summary>
        /// Method called on server command list sections failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments</param>
        private void Svr_OnListSectionsFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSections> serverResponse) || serverResponse.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
                return;
            }

            try
            {
                log.Debug(serverResponse.Result.Error);
                MessageBoxs.Error($"List sections from the server failed : {serverResponse.Result.Error}");
            }
            catch (Exception ex)
            {
                log.Error(serverResponse);
                log.Error(ex.Output(), ex);
                MessageBoxs.Error($"List sections from the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command single section success.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments</param>
        private void Svr_OnSingleSectionSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            if (!(e is ClientHttpEventArgs<ServerResponseSection> svrResp) || svrResp.Result == null)
            {
                MessageBoxs.Error("Authentication to the server failed : The server does not respond !");
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
                MessageBoxs.Error($"Single section from the server failed : {ex.Output()}");
            }
        }

        #endregion
    }
}
