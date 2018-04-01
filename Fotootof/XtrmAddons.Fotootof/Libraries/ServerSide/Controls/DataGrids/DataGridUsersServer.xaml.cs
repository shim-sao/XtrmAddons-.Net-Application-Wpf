using System;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Data Grid User List.
    /// </summary>
    public partial class DataGridUsersServer : DataGridUsers
    {
        #region Properties

        public override DataGrid ItemsDataGrid => ItemsLayout;

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control DataGrid AclGroups List Constructor.
        /// </summary>
        public DataGridUsersServer()
        {
            InitializeComponent();
        }


        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
