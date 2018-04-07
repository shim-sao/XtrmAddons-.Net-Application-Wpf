using System.Collections.ObjectModel;
using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Controls.ListViews;
using ImgSize = XtrmAddons.Fotootof.Lib.Base.Classes.Images.ImageSize;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View Model.</para>
    /// </summary>
    public class PageBrowserModel<PageBrowser> : PageBaseModel<PageBrowser>
    {
        #region Variables

        /// <summary>
        /// Variable image size to display.
        /// </summary>
        public Size imageSize = new Size { Height = ImgSize.Thumbnail.ToDouble(), Width = ImgSize.Thumbnail.ToDouble() };

        /// <summary>
        /// Variable collection of directories and files informations.
        /// </summary>
        private ObservableCollection<StorageInfoModel> filesCollection;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the image size to display.
        /// </summary>
        public Size ImageSize
        {
            get => imageSize;
            set
            {
                imageSize = value;
                RaisePropertyChanged("ImageSize");
            }
        }

        /// <summary>
        /// Property to access to the collection of directories and files informations.
        /// </summary>
        public ObservableCollection<StorageInfoModel> FilesCollection
        {
            get => filesCollection;
            set
            {
                filesCollection = value;
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
