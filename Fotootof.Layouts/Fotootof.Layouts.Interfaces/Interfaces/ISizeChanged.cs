using System.Windows;

namespace Fotootof.Layouts.Interfaces
{
    /// <summary>
    /// <para>Class Fotootof Layouts Controls Interface Size Changed.</para>
    /// <para>This Interface inplement some properties and method usefull for managing size changed in custom user control.</para>
    /// </summary>
    public interface ISizeChanged
    {
        /// <summary>
        /// Method called on layout control size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        void Layout_SizeChanged(object sender, SizeChangedEventArgs e);
    }
}