using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewSlideshow
{
    /// <summary>
    /// 
    /// </summary>
    public class PageSlideshowModel : PageBaseModel<PageSlideshow>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private PictureEntityCollection pictures;

        /// <summary>
        /// 
        /// </summary>
        private PictureEntity currentPicture;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public PictureEntityCollection Pictures
        {
            get => pictures;
            set
            {
                pictures = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PictureEntity CurrentPicture
        {
            get => currentPicture;
            set
            {
                currentPicture = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartIndex { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int CurrentIndex { get; set; } = 0;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Page Slideshow Model Constructor.
        /// </summary>
        /// <param name="page"></param>
        public PageSlideshowModel(PageSlideshow page) : base(page: page) { }

        #endregion
    }
}