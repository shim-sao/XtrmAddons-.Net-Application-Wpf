using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window Form Model Album.
    /// </summary>
    public class WindowFormAlbumModel<WindowAlbumForm> : WindowBaseFormModel<WindowAlbumForm>
    {
        #region Variables

        /// <summary>
        /// Variable Album entity.
        /// </summary>
        private AlbumEntity album;

        /// <summary>
        /// Variable Section entities collection.
        /// </summary>
        private SectionEntityCollection sections;

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
        /// Property to access to the Album.
        /// </summary>
        public AlbumEntity Album
        {
            get { return album; }
            set
            {
                album = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Property to access to the Section entities collection.
        /// </summary>
        public SectionEntityCollection Sections
        {
            get { return sections; }
            set
            {
                sections = value;
                NotifyPropertyChanged();
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
        /// Class XtrmAddons Fotootof Libraries Common Window Form Model Album Constructor.
        /// </summary>
        /// <param name="window">The page associated to the model.</param>
        public WindowFormAlbumModel(WindowAlbumForm window) : base(window)
        {
            FiltersQuality = InfoEntityCollection.TypesQuality();
            FiltersColor = InfoEntityCollection.TypesColor();
        }

        #endregion
    }
}