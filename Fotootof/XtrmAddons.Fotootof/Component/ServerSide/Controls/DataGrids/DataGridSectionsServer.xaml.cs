using System;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Control DataGrid Sections List.
    /// </summary>
    public partial class DataGridSectionsServer : DataGridSections
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Client Control DataGrid Sections List Constructor.
        /// </summary>
        public DataGridSectionsServer()
        {
            InitializeComponent();
        }

        #endregion

        

        #region Properties

        public override DataGrid ItemsDataGrid => ItemsLayout;

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();

        }

        #endregion


        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockRoot()
        {
            Block_Root.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            TraceSize(Block_Root);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ArrangeBlockItems()
        {
            double height = this.ActualHeight - Block_Header.RenderSize.Height;

            Block_Header.Width = Math.Max(this.ActualWidth, 0);

            Block_Items.Width = Math.Max(this.ActualWidth, 0);
            Block_Items.Height = Math.Max(height, 0);

            ItemsLayout.Width = Math.Max(this.ActualWidth, 0);
            ItemsLayout.Height = Math.Max(height, 0);

            TraceSize(Block_Header);
            TraceSize(Block_Items);
            TraceSize(ItemsLayout);
        }

        #endregion
    }
}
