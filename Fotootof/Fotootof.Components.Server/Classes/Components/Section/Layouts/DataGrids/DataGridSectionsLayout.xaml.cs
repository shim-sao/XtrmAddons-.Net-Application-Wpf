using Fotootof.Layouts.Controls.DataGrids;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Server.Section.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Side Control Data Grid Sections List.
    /// </summary>
    public partial class DataGridSectionsLayout : DataGridSectionsControl
    {
        #region Properties

        /// <summary>
        /// Property to access to the datagrid control.
        /// </summary>
        public override DataGrid ItemsDataGrid => FindName("ItemsLayout") as DataGrid;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Client Control DataGrid Sections List Constructor.
        /// </summary>
        public DataGridSectionsLayout()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods Size Changed 

        /// <summary>
        /// Method called on user control size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();

        }

        /// <summary>
        /// Method to arrange or resize the root block.
        /// </summary>
        private void ArrangeBlockRoot()
        {
            var blockRoot = FindName<FrameworkElement>("GridBlockRootName");
            blockRoot.Arrange(new Rect(new Size(ActualWidth, ActualHeight)));
            DebugTraceSize(blockRoot);
        }

        /// <summary>
        /// Method to arrange or resize the items block.
        /// </summary>
        private void ArrangeBlockItems()
        {
            var blockHeader = FindName<FrameworkElement>("StackPanelBlockHeaderName");
            var blockItems = FindName<FrameworkElement>("GridBlockItemsName");
            var itemsLayout = FindName<FrameworkElement>("ItemsLayout");

            double height = Math.Max(ActualHeight - blockHeader.RenderSize.Height, 0);
            double width = Math.Max(ActualWidth, 0);

            blockHeader.Width = width;
            blockItems.Height = height;
            blockItems.Width = width;
            itemsLayout.Width = width;
            itemsLayout.Height = height;

            DebugTraceSize(blockItems);
            DebugTraceSize(blockHeader);
            DebugTraceSize(itemsLayout);
        }

        #endregion
    }
}
