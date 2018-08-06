using Fotootof.Libraries.Models;

namespace Fotootof.Libraries.Windows
{
    /// <summary>
    /// Class Fotootof Libraries Window Model Base.
    /// </summary>
    public class WindowLayoutModel<T> : ModelBase<T>
    {
        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Window Model Base Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> associated to the model.</param>
        public WindowLayoutModel(T controlView) : base(controlView) { }

        #endregion
    }
}