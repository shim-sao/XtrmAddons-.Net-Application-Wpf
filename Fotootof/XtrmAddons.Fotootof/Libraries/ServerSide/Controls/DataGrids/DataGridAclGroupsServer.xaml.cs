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
        /// 
        /// </summary>
        public override DataGrid ItemsDataGrid => ItemsLayout;

        /// <summary>
        /// 
        /// </summary>
        public override Control AddControl => Button_Add;

        /// <summary>
        /// 
        /// </summary>
        public override Control EditControl => Button_Edit;

        /// <summary>
        /// 
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxDefault_Checked<AclGroupEntity>(sender, e);
        }

        #endregion
    }
}
