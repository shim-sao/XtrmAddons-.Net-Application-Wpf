using Fotootof.Collections.Entities;
using Fotootof.Collections.Models;
using Fotootof.Components.Client.Section.Layouts;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.HttpHelpers.HttpClient;
using Fotootof.Libraries.HttpHelpers.HttpClient.Responses;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Json.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;

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

                // Add single section failed event handler.
                server.OnSingleSectionFailed -= Svr_OnSingleSectionFailed;
                server.OnSingleSectionFailed += new EventHandler(Svr_OnSingleSectionFailed);

                // Add single section success event handler.
                server.OnSingleSectionSuccess -= Svr_OnSingleSectionSuccess;
                server.OnSingleSectionSuccess += new EventHandler(Svr_OnSingleSectionSuccess);

                // Add single album failed event handler.
                server.OnSingleAlbumFailed -= Svr_OnSingleAlbumFailed;
                server.OnSingleAlbumFailed += new EventHandler(Svr_OnSingleAlbumFailed);

                // Add single album success event handler.
                server.OnSingleAlbumSuccess -= Svr_OnSingleAlbumSuccess;
                server.OnSingleAlbumSuccess += new EventHandler(Svr_OnSingleAlbumSuccess);
            }
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private async void Svr_OnAuthenticationSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponse result = GetResult<ClientHttpEventArgs<ServerResponse>, ServerResponse>(sender, e);
            if (result == null) return;

            await Server.ListSections();
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnAuthenticationFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponse result = GetResult<ClientHttpEventArgs<ServerResponse>, ServerResponse>(sender, e);
            if (result == null) return;

            try
            {
                log.Debug(result.Error);
                MessageBoxs.Error(String.Format(Properties.Translations.AuthenticationServerFailed, result.Error), Properties.Translations.AuthenticationError);
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                log.Error(e);
                MessageBoxs.Error(String.Format(Properties.Translations.AuthenticationServerFailed, ex.Output()), Properties.Translations.AuthenticationError);
            }
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnAuthenticationUnauthorized(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponse result = GetResult<ClientHttpEventArgs<ServerResponse>, ServerResponse>(sender, e);
            if (result == null) return;

            try
            {
                log.Debug(result.Error);
                MessageBoxs.Error(String.Format(Properties.Translations.AuthenticationServerFailed, result.Error), Properties.Translations.AuthenticationError);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                log.Error(e);
                MessageBoxs.Error(String.Format(Properties.Translations.AuthenticationServerFailed, ex.Output()), Properties.Translations.AuthenticationError);
            }
        }

        /// <summary>
        /// Method called on server command list sections success.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnListSectionsSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponseSections result = GetResult<ClientHttpEventArgs<ServerResponseSections>, ServerResponseSections>(sender, e);
            if (result == null) return;

            List<SectionJsonModel> list = result.Response ?? new List<SectionJsonModel>();
            Sections.Items = new SectionEntityCollection(list);
        }

        /// <summary>
        /// Method called on server command list sections failed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnListSectionsFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponseSections result = GetResult<ClientHttpEventArgs<ServerResponseSections>, ServerResponseSections>(sender, e);
            if (result == null) return;

            try
            {
                log.Debug(result.Error);
                MessageBoxs.Error($"Loading list of sections from the server failed : {result.Error}");
            }
            catch (Exception ex)
            {
                log.Error(e);
                log.Error(ex.Output(), ex);
                MessageBoxs.Error($"Loading list of sections from the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command single Section fail.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnSingleSectionFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            MessageBoxs.NotImplemented();
        }

        /// <summary>
        /// Method called on server command single section success.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnSingleSectionSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user.
            ServerResponseSection result = GetResult<ClientHttpEventArgs<ServerResponseSection>, ServerResponseSection>(sender, e);
            if (result == null) return;

            if (Albums == null)
            {
                Albums = new ListViewAlbumsModel();
            }

            try
            {
                SectionJsonModel section = result.Response;
                if (section?.AlbumsPKeys != null)
                {
                    Albums.Items = new AlbumModelCollection(section.Albums);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error($"Loading single section from the server failed : {ex.Output()}");
            }
        }

        /// <summary>
        /// Method called on server command single Album fail.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnSingleAlbumFailed(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            MessageBoxs.NotImplemented();
        }

        /// <summary>
        /// Method called on server command single Album success.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private void Svr_OnSingleAlbumSuccess(object sender, EventArgs e)
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            MessageBoxs.NotImplemented();
        }

        /// <summary>
        /// Method to get result of an HTTP request.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The event arguments <see cref="EventArgs"/>.</param>
        private U GetResult<T, U>(object sender, EventArgs e) where U : class where T : class
        {
            log.Debug($"{GetType().Name} event handler : {MethodBase.GetCurrentMethod().Name}");

            // Get and check the server response to verify if the server is connected.
            // Prevent bad response for the user
            T svrResp = e as T;
            U result = null;

            if (svrResp != null)
            {
                result = svrResp.GetPropertyValue<U>("Result");
            }

            if (result == null)
            {
                log.Debug(Properties.Translations.ServerNotResponding);
                MessageBoxs.Error(Properties.Translations.ServerNotResponding);
            }

            return result;
        }

        #endregion
    }
}