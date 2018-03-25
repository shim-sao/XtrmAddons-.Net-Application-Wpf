using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.Common.Models.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridSectionsModel<T> : DataGridBaseModel<T, SectionEntityCollection>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridSectionsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Models DataGrids Sections Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridSectionsModel(T owner) : base(owner) { }

        #endregion



        #region Properties

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected override void InitializeModel()
        {
            base.InitializeModel();

            Columns.Visibility.PrimaryKey = Visibility.Visible;
            Columns.Visibility.Name = Visibility.Visible;
        }

        #endregion
    }
}


