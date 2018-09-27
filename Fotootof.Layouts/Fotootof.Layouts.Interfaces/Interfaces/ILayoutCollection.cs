using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Layouts.Interfaces
{
    /// <summary>
    /// <para>Interface Fotootof Layouts Layout Collection.</para>
    /// <para>This Interface inplement some properties and method usefull for managing collection in custom user control.</para>
    /// </summary>
    public interface ILayoutCollection
    {
        #region Properties

        /// <summary>
        /// Property to access to the main add <see cref="Control"/> of the layout.
        /// </summary>
        Control AddCtrl { get; }

        /// <summary>
        /// Property to access to the main edit <see cref="Control"/> of the layout.
        /// </summary>
        Control EditCtrl { get; }

        /// <summary>
        /// Property to access to the main delete <see cref="Control"/> of the layout.
        /// </summary>
        Control DeleteCtrl { get; }
        
        
        #endregion


        #region Methods

        /// <summary>
        /// Method called on add item click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        void AddItem_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on edit item click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        void EditItem_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on delete items click event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        void DeleteItems_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Selection changed arguments <see cref="SelectionChangedEventArgs"/>.</param>
        void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e);
        
        #endregion


    }
}