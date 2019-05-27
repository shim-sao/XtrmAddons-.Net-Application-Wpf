using Fotootof.Libraries.Controls;
using System.Reflection;

namespace Fotootof.Layouts.Windows.Logs
{
    /// <summary>
    /// Class Fotootof Layouts Window Log Model.
    /// </summary>
    public class WindowLogModel : ControlLayoutModel<WindowLogLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Constructor

        /// <summary>
        /// Class Fotootof Layouts Window About Model Constructor.
        /// </summary>
        /// <param name="controlView">The associated window form base.</param>
        public WindowLogModel(WindowLogLayout controlView) : base(controlView) { }

        #endregion

    }
}
