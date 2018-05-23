using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Common.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
    /// </summary>
    public class ListViewPicturesModel<T> : ListViewBaseModel<T, PictureEntityCollection>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewPicturesModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Models List View Pictures Constructor.
        /// </summary>
        /// <param name="control">A Data Grid Base User Control.</param>
        public ListViewPicturesModel(T owner) : base(owner) { }

        #endregion
    }
}
