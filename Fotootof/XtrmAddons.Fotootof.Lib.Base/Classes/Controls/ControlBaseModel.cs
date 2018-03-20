using XtrmAddons.Fotootof.Lib.Base.Classes.Models;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public abstract class ControlBaseModel<T> : ModelBase<T>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Window Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public ControlBaseModel() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Window Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The owner associated to the model.</param>
        public ControlBaseModel(T owner) : base(owner) { }

        #endregion
    }
}
