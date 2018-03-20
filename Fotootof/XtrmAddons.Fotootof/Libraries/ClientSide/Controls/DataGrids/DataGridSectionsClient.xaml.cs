using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Libraries.ClientSide.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Control DataGrid Sections List.
    /// </summary>
    public partial class DataGridSectionsClient : DataGridSections
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Client Control DataGrid Sections List Constructor.
        /// </summary>
        public DataGridSectionsClient()
        {
            InitializeComponent();
        }

        #endregion



        public override DataGrid ItemsDataGrid => ItemsLayout;

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;



        #region Methods

        #endregion
    }
}
