using Fotootof.Libraries.Models;

namespace Fotootof.Libraries.Controls
{
    /// <summary>
    /// Class Fotootof Libraries Base Model.
    /// </summary>
    /// <typeparam name="T">The class Type of the model owner.</typeparam>
    public abstract class ControlBaseModel<T> : ModelBase<T>
    {
        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Base Model Constructor.
        /// </summary>
        public ControlBaseModel() : base() { }

        /// <summary>
        /// Class Fotootof Libraries Base Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> view associated to the model.</param>
        public ControlBaseModel(T controlView) : base(controlView) { }

        #endregion
    }
}
