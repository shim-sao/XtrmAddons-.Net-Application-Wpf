using System.Windows;

namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Base Interface Page Base.</para>
    /// <para>This Interface inplement some properties and method usefull for managing custom user control.</para>
    /// </summary>
    public interface IContentInit
    {
        /// <summary>
        /// Method called on page loaded to initialize and display data context.
        /// </summary>
        void Page_Loaded(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on page loaded to initialize and display data context asynchronously.
        /// </summary>
        void Page_Loaded_Async(object sender, RoutedEventArgs e);
    }
}