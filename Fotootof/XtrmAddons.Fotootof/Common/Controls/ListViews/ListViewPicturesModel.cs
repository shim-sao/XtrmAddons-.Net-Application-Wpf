using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews;

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
