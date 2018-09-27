using Fotootof.Collections.Entities;
using Fotootof.Libraries.Controls.DataGrids;
using System.Windows;

namespace Fotootof.Layouts.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridSectionsModel<T> : DataGridBaseModel<T, SectionEntityCollection> where T : class
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        public DataGridSectionsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="controlView"></param>
        public DataGridSectionsModel(T controlView) : base(controlView) { }

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


