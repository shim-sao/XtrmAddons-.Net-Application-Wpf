using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListViewAlbumsModel<T> : ListViewBaseModel<T, AlbumEntityCollection>
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
        /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewAlbumsModel() : base()
        {
            FiltersQuality = InfoEntityCollection.TypesQuality();
            FiltersColor = InfoEntityCollection.TypesColor();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Albums Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewAlbumsModel(T owner) : base(owner)
        {
            FiltersQuality = InfoEntityCollection.TypesQuality();
            FiltersColor = InfoEntityCollection.TypesColor();
        }

        #endregion
    }
}


