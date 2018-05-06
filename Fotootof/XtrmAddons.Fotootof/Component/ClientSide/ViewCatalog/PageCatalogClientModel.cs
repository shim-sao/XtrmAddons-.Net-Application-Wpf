using System;
using System.Collections.Generic;
using XtrmAddons.Fotootof.Lib.Api.Models.Json;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient;
using XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient.Responses;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ClientSide.ViewCatalog
{
    public class PageCatalogClientModel<PageCatalogClient> : PageBaseModel<PageCatalogClient>
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
            get { return sections; }
            set
            {
                sections = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Albums
        /// </summary>
        public ListViewAlbumsModel<ListViewAlbums> Albums
        {
            get { return albums; }
            set
            {
                albums = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the observable collection of Albums
        /// </summary>
        public ClientHttp Server
        {
            get { return server; }
            set
            {
                server = value;
                AddClientHttp();
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public PageCatalogClientModel(PageCatalogClient pageBase) : base(pageBase)
        {

        }

        #endregion



        #region Methods

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        private void AddClientHttp()
        {
            server.OnAuthenticationFailed += Svr_OnAuthenticationFailed;
            server.OnListSectionsSuccess += Svr_OnListSectionsSuccess;
            server.OnListSectionsFailed += Svr_OnListSectionsFailed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Svr_OnAuthenticationFailed(object sender, EventArgs e)
        {
            ClientHttpEventArgs<ServerResponse> serverResponse = e as ClientHttpEventArgs<ServerResponse>;
            
            System.Windows.MessageBox.Show("Authentication to the server failed : " + serverResponse.Response.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Svr_OnListSectionsSuccess(object sender, EventArgs e)
        {

            ClientHttpEventArgs<ServerResponseSections> serverResponse = e as ClientHttpEventArgs<ServerResponseSections>;
            
            List<SectionJson> list = serverResponse.Response.Response ?? new List<SectionJson>();
            Sections.Items = new SectionEntityCollection(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Svr_OnListSectionsFailed(object sender, EventArgs e)
        {
            ClientHttpEventArgs<ServerResponseSections> serverResponse = e as ClientHttpEventArgs<ServerResponseSections>;

            System.Windows.MessageBox.Show("List sections on the server failed : " + serverResponse.Response.Error);
        }

        #endregion
    }
}
