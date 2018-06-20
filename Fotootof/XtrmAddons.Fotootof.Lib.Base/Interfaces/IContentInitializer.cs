using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Base Interface Content Initializer.</para>
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
        void Control_Loaded(object sender, RoutedEventArgs e);
    }
}