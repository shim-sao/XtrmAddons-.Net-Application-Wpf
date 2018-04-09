using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewBrowser
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Side Component Browser View Controller.</para>
    /// </summary>
    public class PageBrowserController
    {
        #region Properties

        public PageBrowser Page { get; private set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Page Browser Controller Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public PageBrowserController(PageBrowser pageBase)
        {
            Page = pageBase;
        }

        #endregion


        #region Methods

        #endregion
    }
}
