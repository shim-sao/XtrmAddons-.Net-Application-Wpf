using Fotootof.Collections;
using Fotootof.Collections.Entities;
using Fotootof.Libraries.Controls.ListViews;
using Fotootof.Libraries.Models.Systems;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System.Collections.Generic;

namespace Fotootof.Components.Server.Browser.Layouts
{
    internal class ListViewSystemStorageModel : ListViewBaseModel<ListViewSystemStorageControl, CollectionStorage>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public ListViewSystemStorageModel(ListViewSystemStorageControl control) : base(control) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected override void InitializeModel() { }

        #endregion
    }
}
