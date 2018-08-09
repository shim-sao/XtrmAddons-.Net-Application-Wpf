using Fotootof.Libraries.Models;

namespace Fotootof.Libraries.Components
{
    /// <summary>
    /// Class Fotootof Libraries Component component View Model.
    /// </summary>
    public class ComponentModel<T> : ModelBase<T>
    {
        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Component component View Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> associated to the model.</param>
        public ComponentModel(T controlView) : base(controlView) { }

        #endregion
    }
}
