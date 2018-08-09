using Fotootof.Layouts.Controls.DataGrids;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.Theme;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Components.Server.Users.Layouts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups.
    /// </summary>
    public partial class DataGridAclGroupsLayout : DataGridAclGroupsControl
    {
        #region Properties

        /// <summary>
        /// Property to access to the datagrid control.
        /// </summary>
        public override DataGrid ItemsDataGrid => FindName("ItemsLayout") as DataGrid;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups Constructor.
        /// </summary>
        public DataGridAclGroupsLayout()
        {
            ThemeLoader.MergeThemeTo(Resources);

            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxDefault_Checked<AclGroupEntity>(sender, e);
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
            Grid blockRoot = FindName("GridBlockRootName") as Grid;
            blockRoot.Arrange(new Rect(new Size(ActualWidth, ActualHeight)));
            DebugTraceSize(blockRoot);
        }

        /// <summary>
        /// Method to arrange or resize the items block.
        /// </summary>
        private void ArrangeBlockItems()
        {
            var blockHeader = FindName("StackPanelBlockHeaderName") as FrameworkElement;
            var blockItems = FindName("GridBlockItemsName") as FrameworkElement;
            var itemsLayout = FindName("ItemsLayout") as FrameworkElement;

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
