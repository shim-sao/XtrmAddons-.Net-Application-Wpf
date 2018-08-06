using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;

namespace XtrmAddons.Fotootof.ComponentOld.ServerSide.Controls.ListViews
{
    [System.Obsolete("use Fotootof.ServerSide.Component.Browser.dll")]
    public class ListViewStoragesServerModel : ListViewBaseModel<ListViewStoragesServer, CollectionStorage>
    {
        #region Properties
        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public ListViewStoragesServerModel(ListViewStoragesServer control) : base(control) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected override void InitializeModel() { }

        #endregion

    }
}
