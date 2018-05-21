using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Component.ClientSide.Controls.DataGrids
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

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }



        #region Methods

        #endregion
    }
}
