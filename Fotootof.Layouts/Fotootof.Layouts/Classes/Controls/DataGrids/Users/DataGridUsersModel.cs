using Fotootof.Collections.Entities;
using Fotootof.Libraries.Controls.DataGrids;
using System.Windows;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridUsersModel<T> : DataGridBaseModel<T, UserEntityCollection> where T : class
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users Constructor.
        /// </summary>
        public DataGridUsersModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public DataGridUsersModel(T controlView) : base(controlView) { }

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


