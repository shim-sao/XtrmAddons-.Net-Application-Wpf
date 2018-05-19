using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlUiElement;

namespace XtrmAddons.Fotootof.Component.ServerSide.Controls.ListViews
{
    public class ListViewStoragesServerModel : ListViewBaseModel<ListViewStoragesServer, StorageCollection>
    {
        #region Properties

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Controller.
        /// </summary>
        public UiElement ListViewStoragesServerImageSize
        {
            get => GetOptionsControl("ListViewStoragesServerImageSize");
            set
            {
                SetOptionsControl("ListViewStoragesServerImageSize", value);
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Constructor.
        /// </summary>
        /// <param name="control">The control associated to the model.</param>
        public ListViewStoragesServerModel(ListViewStoragesServer control) : base(control) { }

        #endregion



        #region Methods

        #endregion


    }
}
