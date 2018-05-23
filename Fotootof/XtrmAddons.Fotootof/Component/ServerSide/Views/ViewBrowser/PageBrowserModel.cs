using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Images;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Common.Collections;
using ImgSize = XtrmAddons.Fotootof.Lib.Base.Classes.Images.ImageSize;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View Model.</para>
    /// </summary>
    public class PageBrowserModel : PageBaseModel<PageBrowser>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static new readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable image size to display.
        /// </summary>
        public Size imageSize
            = new Size { Height = ImgSize.Vignette.ToDouble(), Width = ImgSize.Vignette.ToDouble() };

        /// <summary>
        /// Variable collection of directories and files informations.
        /// </summary>
        private StorageCollection filesCollection;

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
                if (imageSize != value)
                {
                    imageSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the collection of directories and files informations.
        /// </summary>
        public StorageCollection FilesCollection
        {
            get => filesCollection;
            set
            {
                if (filesCollection != value)
                {
                    filesCollection = value;
                    NotifyPropertyChanged();
                }
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
