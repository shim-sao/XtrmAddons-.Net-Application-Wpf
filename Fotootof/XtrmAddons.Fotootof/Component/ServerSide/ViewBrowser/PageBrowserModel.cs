using System.Collections.ObjectModel;
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
        private ObservableCollection<ListViewFilesItemModel> filesCollection;

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<ListViewFilesItemModel> directoriesCollection;

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ListViewFilesItemModel> FilesCollection
        {
            get => filesCollection;
            set
            {
                filesCollection = value;
                RaisePropertyChanged("FilesCollection");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ListViewFilesItemModel> DirectoriesCollection
        {
            get => directoriesCollection;
            set
            {
                directoriesCollection = value;
                RaisePropertyChanged("DirectoriesCollection");
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
