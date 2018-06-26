using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Controls.DataGrids;
using XtrmAddons.Fotootof.Common.Controls.ListViews;
using XtrmAddons.Fotootof.Common.Models.DataGrids;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewPlugin
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component Server Side View Plugin Model.
    /// </summary>
    public class PagePluginModel : PageBaseModel<PagePlugin>
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Server Side View Plugin Model Constructor.
        /// </summary>
        /// <param name="page">The page associated to the model.</param>
        public PagePluginModel(PagePlugin page) : base(page: page) { }

        #endregion
    }
}
