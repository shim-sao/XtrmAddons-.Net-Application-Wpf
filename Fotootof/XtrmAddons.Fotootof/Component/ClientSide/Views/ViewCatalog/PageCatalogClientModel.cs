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

namespace XtrmAddons.Fotootof.Component.ClientSide.Views.ViewCatalog
{
    public class PageCatalogClientModel : PageBaseModel<PageCatalogClient>
    {
        #region Variables

        /// <summary>
        /// Variable observable collection of Sections.
        /// </summary>
        private DataGridSectionsModel<DataGridSections> sections;

        /// <summary>
        /// Variable observable collection of Albums.
        /// </summary>
        private ListViewAlbumsModel<ListViewAlbums> albums;

        /// <summary>
        /// Variable client http server.
        /// </summary>
        private ClientHttp server;

        #endregion



        #region Properties

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
        public ListViewAlbumsModel<ListViewAlbums> Albums
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
                // Add authentication failed event handler.
                server.OnAuthenticationFailed -= Svr_OnAuthenticationFailed;
                server.OnAuthenticationFailed += Svr_OnAuthenticationFailed;

                // Add list sections success event handler.
                server.OnListSectionsSuccess -= Svr_OnListSectionsSuccess;
                server.OnListSectionsSuccess += Svr_OnListSectionsSuccess;

                // Add list sections failed event handler.
                server.OnListSectionsFailed -= Svr_OnListSectionsFailed;
                server.OnListSectionsFailed += Svr_OnListSectionsFailed;
            }
        }

        /// <summary>
        /// Method called on server command authentication failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnAuthenticationFailed(object sender, EventArgs e)
        {
            ClientHttpEventArgs<ServerResponse> serverResponse = e as ClientHttpEventArgs<ServerResponse>;
            
            try
            {
                log.Debug(serverResponse.Response.Error);
                System.Windows.MessageBox.Show("Authentication to the server failed : " + serverResponse.Response.Error, "Authentication error");
            }
            catch(Exception ex)
            {
                log.Error(ex);
                log.Error(serverResponse);
                System.Windows.MessageBox.Show("Authentication to the server failed : " + ex.Output(), "Authentication error");
            }
        }

        /// <summary>
        /// Method called on server command list sections success.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnListSectionsSuccess(object sender, EventArgs e)
        {

            ClientHttpEventArgs<ServerResponseSections> serverResponse = e as ClientHttpEventArgs<ServerResponseSections>;
            
            List<SectionJson> list = serverResponse.Response.Response ?? new List<SectionJson>();
            Sections.Items = new SectionEntityCollection(list);
        }

        /// <summary>
        /// Method called on server command list sections failed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The event arguments.param>
        private void Svr_OnListSectionsFailed(object sender, EventArgs e)
        {
            ClientHttpEventArgs<ServerResponseSections> serverResponse = e as ClientHttpEventArgs<ServerResponseSections>;

            try
            {
                log.Debug(serverResponse.Response.Error);
                System.Windows.MessageBox.Show("List sections from the server failed : " + serverResponse.Response.Error, "Sections list");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Error(serverResponse);
                System.Windows.MessageBox.Show("List sections from the server failed : " + ex.Output(), "Sections list");
            }
        }

        #endregion
    }
}
