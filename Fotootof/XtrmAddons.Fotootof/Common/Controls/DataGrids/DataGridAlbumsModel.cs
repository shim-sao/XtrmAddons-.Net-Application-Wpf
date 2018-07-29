using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Common.Models.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Albums.
    /// </summary>
    /// <typeparam name="DataGridAlbums">The type data grid control view of albums associated to the model.</typeparam>
    public class DataGridAlbumsModel<T> : DataGridBaseModel<T, AlbumEntityCollection>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Albums Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public DataGridAlbumsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Albums Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public DataGridAlbumsModel(T owner) : base(owner) { }

        #endregion



        #region Properties

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected new void InitializeModel()
        {
            base.InitializeModel();

            Columns.Visibility.PrimaryKey = Visibility.Visible;
            Columns.Visibility.Name = Visibility.Visible;
        }

        #endregion
    }
}


