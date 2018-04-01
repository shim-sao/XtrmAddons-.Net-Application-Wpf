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
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="margin"></param>
        public void StretchHeight(FrameworkElement source, FrameworkElement destination, double margin = 0)
        {
            if ((source.ActualHeight - margin) > 0)
            {
                // destination.MinHeight = source.ActualHeight - margin;
                // destination.MaxHeight = source.ActualHeight - margin;
                destination.Height = source.ActualHeight - margin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="margin"></param>
        public void StretchWidth(FrameworkElement source, FrameworkElement destination, double margin = 0)
        {
            if((source.ActualWidth - margin) > 0)
            {
                //destination.MinWidth = source.ActualWidth - margin;
                //destination.MaxWidth = source.ActualWidth - margin;
                destination.Width = source.ActualWidth - margin;
            }
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