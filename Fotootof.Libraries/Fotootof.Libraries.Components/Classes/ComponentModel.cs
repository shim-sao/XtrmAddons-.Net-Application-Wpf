using Fotootof.Libraries.Controls;

namespace Fotootof.Libraries.Components
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Component View Model.
    /// </summary>
    public class ComponentModel<T> : ControlLayoutModel<T> where T : class
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Component View Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> associated to the model.</param>
        public ComponentModel(T controlView) : base(controlView) { }

        #endregion
    }
}
