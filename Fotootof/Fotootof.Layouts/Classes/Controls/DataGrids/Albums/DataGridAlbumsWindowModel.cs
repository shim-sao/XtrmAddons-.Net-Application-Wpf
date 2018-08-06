using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Windows;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Window Albums DataGrid Model.
    /// </summary>
    public class DataGridAlbumsWindowModel : WindowLayoutModel<DataGridAlbumsWindow>
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
        public DataGridAlbumsWindowModel(DataGridAlbumsWindow owner) : base(owner) { }

        #endregion
    }
}