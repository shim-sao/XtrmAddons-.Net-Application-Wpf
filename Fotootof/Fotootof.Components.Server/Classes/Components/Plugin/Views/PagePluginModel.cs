
using Fotootof.Libraries.Components;

namespace Fotootof.Components.Server.Plugin
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Plugin View Model.
    /// </summary>
    public class PagePluginModel : ComponentModel<PagePluginLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Plugin View  Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="PagePluginLayout"/> view or layout associated to the model.</param>
        public PagePluginModel(PagePluginLayout controlView) : base(controlView) { }

        #endregion
    }
}