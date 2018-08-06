using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Controls.DataGrids;
using System.Windows;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridUsersModel<T> : DataGridBaseModel<T, UserEntityCollection>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridUsersModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids Users Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridUsersModel(T owner) : base(owner) { }

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


