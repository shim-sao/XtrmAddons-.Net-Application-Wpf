using Fotootof.Collections.Entities;
using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Components;
using System;
using System.Linq;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Components.Server.Section
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Server Side View Catalog Model.
    /// </summary>
    internal class PageSectionModel : ComponentModel<PageSectionLayout>
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
        private DataGridSectionsModel<DataGridSectionsControl> sections;

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
        /// <param name="controlView">The page associated to the model.</param>
        public PageSectionModel(PageSectionLayout controlView) : base(controlView) { }

        #endregion



        #region Methods Sections

        /// <summary>
        /// Method to load the <see cref="SectionEntityCollection"/> from the database.
        /// </summary>
        public void LoadSections()
        {
            MessageBoxs.IsBusy = true;
            log.Warn("Loading Sections list : Start. Please wait...");

            try
            {
                var collec = new SectionEntityCollection(true);
                Sections.Items = new SectionEntityCollection(collec.OrderBy(x => x.Name));
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Fatal(ex, "Loading Sections list failed !");
            }
            finally
            {
                log.Info("Loading Sections list : End.");
                MessageBoxs.IsBusy = false;
            }
        }

        #endregion
    }
}
