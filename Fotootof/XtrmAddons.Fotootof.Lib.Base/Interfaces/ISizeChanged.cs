using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Base Interface Size Changed.</para>
    /// <para>This Interface inplement some properties and method usefull for managing size changed in custom user control.</para>
    /// </summary>
    public interface ISizeChanged
    {
        /// <summary>
        /// Method called on control size changed event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        void Control_SizeChanged(object sender, SizeChangedEventArgs e);
    }
}