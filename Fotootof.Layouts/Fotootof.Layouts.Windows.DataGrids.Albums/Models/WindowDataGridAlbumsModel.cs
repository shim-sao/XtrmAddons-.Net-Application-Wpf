using Fotootof.Layouts.Windows.DataGrids.Albums.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;

namespace Fotootof.Layouts.Windows.DataGrids.Albums.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Window Albums DataGrid Model.
    /// </summary>
    public class WindowDataGridAlbumsModel : WindowBaseDataGridModel<WindowDataGridAlbumsControl>
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
        /// <param name="owner"></param>
        public WindowDataGridAlbumsModel(WindowDataGridAlbumsControl owner) : base(owner) { }

        #endregion
    }
}