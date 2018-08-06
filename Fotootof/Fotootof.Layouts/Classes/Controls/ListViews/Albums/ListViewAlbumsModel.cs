using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Controls.ListViews;

namespace Fotootof.Layouts.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListViewAlbumsModel : ListViewBaseModel<ListViewAlbumsControl, AlbumEntityCollection>
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
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewAlbumsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
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


