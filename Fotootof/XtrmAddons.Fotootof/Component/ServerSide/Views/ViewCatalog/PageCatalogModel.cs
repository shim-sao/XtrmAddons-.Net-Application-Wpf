using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewCatalog
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Server Side View Catalog Model.
    /// </summary>
    public class PageCatalogModel : PageBaseModel<PageCatalog>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable observable collection of Sections.
        /// </summary>
        private DataGridSectionsModel<DataGridSections> sections;

        /// <summary>
        /// Variable observable collection of Albums.
        /// </summary>
        private ListViewAlbumsModel albums;

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
        /// Property to access to the observable collection of quality filters.
        /// </summary>
        public InfoEntityCollection FiltersQuality
        {
            get => qualityFilters;
            set
            {
                if (qualityFilters != value)
                {
                    qualityFilters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the observable collection of color filters.
        /// </summary>
        public InfoEntityCollection FiltersColor
        {
            get => colorFilters;
            set
            {
                if (colorFilters != value)
                {
                    colorFilters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Models List Sections Constructor.
        /// </summary>
        /// <param name="page">The page associated to the model.</param>
        public PageCatalogModel(PageCatalog page) : base(page: page) { }

        #endregion
    }
}
