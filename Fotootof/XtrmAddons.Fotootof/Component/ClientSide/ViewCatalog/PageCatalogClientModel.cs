using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
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
                RaisePropertyChanged("Sections");
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
                RaisePropertyChanged("Albums");
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
    }
}
