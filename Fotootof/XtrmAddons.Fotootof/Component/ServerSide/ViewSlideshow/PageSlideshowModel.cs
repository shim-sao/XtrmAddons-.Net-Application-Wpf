using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Libraries.Common.Collections;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewSlideshow
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
                RaisePropertyChanged("Pictures");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Start { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int Current { get; set; } = 0;

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Album Model Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public PageSlideshowModel(PageSlideshow pageBase)
            : base(pageBase) { }

        #endregion
    }
}
