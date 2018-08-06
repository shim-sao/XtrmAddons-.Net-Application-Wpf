using Fotootof.Libraries.Models;

namespace Fotootof.Libraries.Components
{
    /// <summary>
    /// Class Fotootof Libraries Component Page Base Model.
    /// </summary>
    public class PageBaseModel<T> : ModelBase<T>
    {
        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Component Page Base Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> associated to the model.</param>
        public PageBaseModel(T controlView) : base(controlView) { }

        #endregion
    }
}
