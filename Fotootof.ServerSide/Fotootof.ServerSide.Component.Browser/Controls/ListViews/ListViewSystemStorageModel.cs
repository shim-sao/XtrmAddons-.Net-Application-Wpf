using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;

namespace Fotootof.ServerSide.Component.Browser.Controls
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
