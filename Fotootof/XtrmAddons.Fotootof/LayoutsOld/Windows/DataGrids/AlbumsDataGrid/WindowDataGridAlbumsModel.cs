using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;

namespace XtrmAddons.Fotootof.LayoutsOld.Windows.DataGrids.AlbumsDataGrid
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Window Albums DataGrid Model.
    /// </summary>
    [System.Obsolete("use Fotootof.Layouts.Windows.DataGrids.Albums.dll")]
    public class WindowDataGridAlbumsModel<WindowDataGridAlbums> : WindowBaseDataGridModel<WindowDataGridAlbums>
    {
        #region Variables

        /// <summary>
        /// Variable Album entities.
        /// </summary>
        private AlbumEntityCollection albums;

        #endregion Variables



        #region Properties

        /// <summary>
        /// Property to access to the Album entities collection.
        /// </summary>
        public AlbumEntityCollection Albums
        {
            get { return albums; }
            set
            {
                albums = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowBase"></param>
        public WindowDataGridAlbumsModel(WindowDataGridAlbums windowBase) : base(windowBase) { }

        #endregion
    }
}