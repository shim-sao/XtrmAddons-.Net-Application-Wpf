using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Classes Controls Control Base.</para>
    /// <para>This Class is design to define some application custom properties and methods for user controls.</para>
    /// </summary>
    public abstract class ControlBase : UserControl, ISizeChanged
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        protected static bool sizeTrace = true;

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Control Base Constructor.
        /// </summary>
        public ControlBase() : base()
        {
            // Merge application resources.
            Resources.MergedDictionaries.Add(Culture.Translation.Words);
            Resources.MergedDictionaries.Add(Culture.Translation.Logs);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on window sized changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        protected void TraceSize(FrameworkElement fe)
        {
            if (!sizeTrace) return;

            Trace.WriteLine(string.Format("----> Class({0}) : Object({1}) : Name({2})", GetType().Name, fe.GetType().Name, fe.Name));
            Trace.WriteLine("ActualSize = [" + fe.ActualWidth + "," + fe.ActualHeight + "]");
            Trace.WriteLine("Size = [" + fe.Width + "," + fe.Height + "]");
            Trace.WriteLine("RenderSize = [" + fe.RenderSize.Width + "," + fe.RenderSize.Height + "]");
            Trace.WriteLine("-------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Method called on control size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public abstract void Control_SizeChanged(object sender, SizeChangedEventArgs e);

        #endregion
    }
}