using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Window Base Model.
    /// </summary>
    public class WindowBaseModel<WindowBase> : ModelBase<WindowBase>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Window Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public WindowBaseModel(WindowBase owner) : base(owner) { }

        #endregion
    }
}
