using System.Collections.ObjectModel;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Browser Model.
    /// </summary>
    public class PageBrowserModel<PageBrowser> : PageBaseModel<PageBrowser>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<StorageInfoModel> storagesCollection;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<StorageInfoModel> StoragesCollection
        {
            get => storagesCollection;
            set
            {
                storagesCollection = value;
                RaisePropertyChanged("FilesCollection");
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Browser Model Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public PageBrowserModel(PageBrowser pageBase) : base(pageBase) { }

        #endregion

    }
}
