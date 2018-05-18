using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Base Interface Control Collection.</para>
    /// <para>This Interface inplement some properties and method usefull for managing collection in custom user control.</para>
    /// </summary>
    public interface IControlCollection
    {
        /// <summary>
        /// Method called on add click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        void OnAddNewItem_Click(object sender, System.Windows.RoutedEventArgs e);

        /// <summary>
        /// Method called on edit click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        void OnEditItem_Click(object sender, System.Windows.RoutedEventArgs e);

        /// <summary>
        /// Method called on delete click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        void OnDeleteItems_Click(object sender, System.Windows.RoutedEventArgs e);

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e);



        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        Control AddControl { get; }

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        Control EditControl { get; }

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        Control DeleteControl { get; }
    }
}