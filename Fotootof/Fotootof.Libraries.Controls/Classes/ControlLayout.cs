using Fotootof.Layouts.Interfaces;
using Fotootof.Theme;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Libraries.Controls
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Classes Controls Control Base.</para>
    /// <para>This Class is design to define some application custom properties and methods for user controls.</para>
    /// </summary>
    public abstract class ControlLayout : UserControl, ISizeChanged
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Control Base Constructor.
        /// </summary>
        public ControlLayout() : base()
        {
            // Merge application resources.
            Resources.MergedDictionaries.Add(XtrmAddons.Fotootof.Culture.Translation.Words);
            Resources.MergedDictionaries.Add(XtrmAddons.Fotootof.Culture.Translation.Logs);

            ThemeLoader.MergeThemeTo(Resources);
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindName<T>(string name) where T : class
        {
            return (T)FindName(name);
        }

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="fe">A <see cref="FrameworkElement"/>.</param>
        [Conditional("DEBUG")]
        protected void DebugTraceSize(FrameworkElement fe)
        {
            #if DEBUG_SIZE

            Trace.WriteLine(string.Format("----> Class({0}) : Object({1}) : Name({2})", GetType().Name, fe.GetType().Name, fe.Name));
            Trace.WriteLine("ActualSize = [" + fe.ActualWidth + "," + fe.ActualHeight + "]");
            Trace.WriteLine("Size = [" + fe.Width + "," + fe.Height + "]");
            Trace.WriteLine("RenderSize = [" + fe.RenderSize.Width + "," + fe.RenderSize.Height + "]");
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");

            #endif
        }

        /// <summary>
        /// Method called on layout control size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public abstract void Layout_SizeChanged(object sender, SizeChangedEventArgs e);

        #endregion
    }
}