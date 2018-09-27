using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.Layouts.Dialogs;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Client.Section.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Control DataGrid Sections List.
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
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.TraceInformation($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// Method called on click event to add a new Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxs.NotImplemented($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// Method called on edit click to navigate to a Section edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void EditItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxs.NotImplemented($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// Method called on delete click to delete a Section.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxs.NotImplemented($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        #endregion



        #region Methods Resize

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
