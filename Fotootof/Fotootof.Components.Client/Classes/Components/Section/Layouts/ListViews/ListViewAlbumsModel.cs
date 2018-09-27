using Fotootof.Collections.Entities;
using Fotootof.Collections.Models;
using Fotootof.Layouts.Controls.ListViews;
using Fotootof.Libraries.Controls.ListViews;

namespace Fotootof.Components.Client.Section.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
    /// </summary>
    public class ListViewAlbumsModel : ListViewBaseModel<ListViewAlbumsControl, AlbumModelCollection>
    {
        #region Variables

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
        /// Property to access to the observable collection of quality filters.
        /// </summary>
        public InfoEntityCollection FiltersQuality
        {
            get { return qualityFilters; }
            set
            {
                qualityFilters = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
        /// </summary>
        public ListViewAlbumsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
        /// </summary>
        /// <param name="controlView">A Data Grid Base User Control.</param>
        public ListViewAlbumsModel(ListViewAlbumsControl controlView) : base(controlView) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected override void InitializeModel()
        {
            FiltersQuality = InfoEntityCollection.TypesQuality();
            FiltersColor = InfoEntityCollection.TypesColor();
        }

        #endregion
    }
}


