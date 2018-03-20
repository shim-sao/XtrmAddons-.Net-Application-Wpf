using System.ComponentModel;
using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Windows Form.
    /// </summary>
    public abstract partial class WindowBase : Window
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Windows Form Constructor.
        /// </summary>
        public WindowBase() : base()
        {
            // Merge application resources.
            Resources.MergedDictionaries.Add(Culture.Translation.Words);
            Resources.MergedDictionaries.Add(Culture.Translation.Logs);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Cancel event arguments.</param>
        protected abstract void Window_Closing(object sender, CancelEventArgs e);

        #endregion
    }
}
