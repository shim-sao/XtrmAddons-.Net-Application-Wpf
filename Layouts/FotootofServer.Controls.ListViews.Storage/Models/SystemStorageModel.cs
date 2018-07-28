using XtrmAddons.Fotootof.Lib.Base.Classes.Collections;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;

namespace FotootofServer.Controls.ListViews.Storage
{
    internal class SystemStorageModel : ListViewBaseModel<SystemStorageControl, CollectionStorage>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public SystemStorageModel(SystemStorageControl control) : base(control) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method to initialize model.
        /// </summary>
        protected override void InitializeModel() { }

        #endregion

    }
}
