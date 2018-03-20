using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.DataGrids.AlbumsDataGrid
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Window Albums DataGrid Model.
    /// </summary>
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
                RaisePropertyChanged("Albums");
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