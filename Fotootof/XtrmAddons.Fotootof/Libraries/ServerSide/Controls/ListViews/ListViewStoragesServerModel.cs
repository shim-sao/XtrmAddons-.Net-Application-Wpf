using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlUiElement;

namespace XtrmAddons.Fotootof.Libraries.ServerSide.Controls.ListViews
{
   public class ListViewStoragesServerModel : ListViewBaseModel<ListViewStoragesServer, StorageCollection>
    {
        #region Properties

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Controls Menu Main Controller.
        /// </summary>
        public UiElement ListViewStoragesServerImageSize
        {
            get { return ApplicationBase.UI.Controls.FindKey("ListViewStoragesServerImageSize"); }
            set
            {
                GetOptionsListViewStoragesServerImageSize(value);
                RaisePropertyChanged("ListViewStoragesServerImageSize");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private static void GetOptionsListViewStoragesServerImageSize(UiElement value)
        {
            if (ApplicationBase.UI.Controls.FindKey("ListViewStoragesServerImageSize") != null)
            {
                ApplicationBase.UI.Controls.ReplaceKeyUnique(value, "ListViewStoragesServerImageSize");
            }
            else
            {
                ApplicationBase.UI.Controls.Add(value);
            }
        }

        #endregion


    }
}
