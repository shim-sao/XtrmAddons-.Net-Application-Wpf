using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Common.Models.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models DataGrids AclGroups.
    /// </summary>
    /// <typeparam name="T">The type of data grid control associated to the model.</typeparam>
    public class DataGridAclGroupsModel<T> : DataGridBaseModel<T, AclGroupEntityCollection>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids AclGroups Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridAclGroupsModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models DataGrids AclGroups Constructor.
        /// </summary>
        /// <param name="control"></param>
        public DataGridAclGroupsModel(T owner) : base(owner) { }

        #endregion



        #region Properties

        /// <summary>
        /// Method to initialize the model.
        /// </summary>
        protected new void InitializeModel()
        {
            base.InitializeModel();

            Columns.Visibility.PrimaryKey = Visibility.Visible;
            Columns.Visibility.Name = Visibility.Visible;
        }

        #endregion
    }
}