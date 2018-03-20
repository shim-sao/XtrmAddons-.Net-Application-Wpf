using System.Dynamic;
using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Pages
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Page Base Model.
    /// </summary>
    public class PageBaseModel<PageBase> : ModelBase<PageBase>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Page Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public PageBaseModel(PageBase page) : base(page)
        {
        }

        #endregion
    }
}
