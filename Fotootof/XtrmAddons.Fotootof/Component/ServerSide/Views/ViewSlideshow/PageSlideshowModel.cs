using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewSlideshow
{
    /// <summary>
    /// 
    /// </summary>
    public class PageSlideshowModel<PageSlideshow> : PageBaseModel<PageSlideshow>
    {
        #region Variables

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
        /// <param name="pageBase"></param>
        public PageSlideshowModel(PageSlideshow pageBase)
            : base(pageBase) { }

        #endregion
    }
}