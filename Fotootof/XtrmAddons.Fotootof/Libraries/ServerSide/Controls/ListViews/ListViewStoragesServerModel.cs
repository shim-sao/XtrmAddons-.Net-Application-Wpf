using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
   public class ListViewStoragesServerModel : ListViewBaseModel<ListViewStoragesServer, StorageCollection>
    {



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public ListViewStoragesServerModel(ListViewStoragesServer control) : base(control) { }

        #endregion


    }
}
