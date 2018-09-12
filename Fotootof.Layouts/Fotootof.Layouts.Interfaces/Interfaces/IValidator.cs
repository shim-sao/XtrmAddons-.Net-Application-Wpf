using System.Windows.Controls;

namespace Fotootof.Layouts.Interfaces
{
    /// <summary>
    /// Interface Fotootof Layouts Control Validator.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        Button ButtonSave { get; }

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        Button ButtonCancel { get; }
    }
}
