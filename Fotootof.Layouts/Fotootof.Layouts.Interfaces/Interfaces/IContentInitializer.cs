using System.Windows;

namespace Fotootof.Layouts.Interfaces
{
    /// <summary>
    /// <para>Class Fotootof Layouts Interface Content Initializer.</para>
    /// <para>This Interface inplement some properties and method usefull for managing custom user control initialization.</para>
    /// </summary>
    public interface IContentInitializer
    {
        /// <summary>
        /// Method to initialize page data model.
        /// </summary>
        void InitializeModel();

        /// <summary>
        /// Method called on page loaded to initialize and display data context.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        void Control_Loaded(object sender, RoutedEventArgs e);
    }
}