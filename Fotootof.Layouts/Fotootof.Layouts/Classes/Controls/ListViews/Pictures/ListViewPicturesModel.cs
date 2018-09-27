using Fotootof.Collections.Entities;
using Fotootof.Libraries.Controls.ListViews;

namespace Fotootof.Layouts.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
    /// </summary>
    public class ListViewPicturesModel<T> : ListViewBaseModel<T, PictureEntityCollection> where T : class
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
        /// </summary>
        public ListViewPicturesModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
        /// </summary>
        /// <param name="controlView">A Data Grid Base User Control.</param>
        public ListViewPicturesModel(T controlView) : base(controlView) { }

        #endregion
    }
}
