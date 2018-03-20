using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewCatalog
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Server Side View Catalog Model.
    /// </summary>
    public class PageCatalogModel<PageCatalog> : PageBaseModel<PageCatalog>
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
        /// Variable observable collection of quality filters.
        /// </summary>
        private InfoEntityCollection qualityFilters;

        /// <summary>
        /// Variable observable collection of color filters.
        /// </summary>
        private InfoEntityCollection colorFilters;

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

        /// <summary>
        /// Property to access to the observable collection of quality filters.
        /// </summary>
        public InfoEntityCollection FiltersQuality
        {
            get { return qualityFilters; }
            set
            {
                qualityFilters = value;
                RaisePropertyChanged("FiltersQuality");
            }
        }

        /// <summary>
        /// Property to access to the observable collection of color filters.
        /// </summary>
        public InfoEntityCollection FiltersColor
        {
            get { return colorFilters; }
            set
            {
                colorFilters = value;
                RaisePropertyChanged("FiltersColor");
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public PageCatalogModel(PageCatalog pageBase) : base(pageBase) { }

        #endregion
    }
}
