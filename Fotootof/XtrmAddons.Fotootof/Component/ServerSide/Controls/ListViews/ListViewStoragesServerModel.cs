using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Ui;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    public class ListViewStoragesServerModel : ListViewBaseModel<ListViewStoragesServer, StorageCollection>
    {
        #region Properties

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Controller.
        /// </summary>
        //public UiElement ListViewStoragesServerImageSize
        //{
        //    get => GetOptionsControl("ListViewStoragesServerImageSize");
        //    set
        //    {
        //        SetOptionsControl("ListViewStoragesServerImageSize", value);
        //        NotifyPropertyChanged();
        //    }
        //}

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public ListViewStoragesServerModel(ListViewStoragesServer control) : base(control) { }

        #endregion

    }
}
