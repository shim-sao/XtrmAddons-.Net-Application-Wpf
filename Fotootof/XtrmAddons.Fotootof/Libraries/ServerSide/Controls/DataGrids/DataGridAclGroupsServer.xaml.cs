using System;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups.
    /// </summary>
    public partial class DataGridAclGroupsServer : DataGridAclGroups
    {
        #region Properties

        /// <summary>
        /// Property to access to the datagrid control.
        /// </summary>
        public override DataGrid ItemsDataGrid => ItemsLayout;

        /// <summary>
        /// Property to access to the main add button control.
        /// </summary>
        public override Control AddControl => Button_Add;

        /// <summary>
        /// Property to access to the main edit button control.
        /// </summary>
        public override Control EditControl => Button_Edit;

        /// <summary>
        /// Property to access to the main delete button control.
        /// </summary>
        public override Control DeleteControl => Button_Delete;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups Constructor.
        /// </summary>
        public DataGridAclGroupsServer()
        {
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
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ArrangeBlockRoot();
            ArrangeBlockItems();
        }

        /// <summary>
        /// Method to arrange or resize the root block.
        /// </summary>
        private void ArrangeBlockRoot()
        {
            Block_Root.Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
            TraceSize(Block_Root);
        }

        /// <summary>
        /// Method to arrange or resize the items block.
        /// </summary>
        private void ArrangeBlockItems()
        {
            double height = Math.Max(this.ActualHeight - Block_Header.RenderSize.Height, 0);
            double width = Math.Max(this.ActualWidth, Block_Root.Width);

            Block_Header.Width = width;

            Block_Items.Width = width;
            Block_Items.Height = height;

            ItemsLayout.Width = width;
            ItemsLayout.Height = height;

            TraceSize(Block_Header);
            TraceSize(Block_Items);
            TraceSize(ItemsLayout);
        }

        #endregion
    }
}
