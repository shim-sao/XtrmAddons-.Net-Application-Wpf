using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof Lib Base Interfaces Window Form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWindowForm<T>
    {
        /// <summary>
        /// Property to access to the  old form informations.
        /// </summary>
        T OldForm { get; set; }

        /// <summary>
        /// Property to access to the  new form informations.
        /// </summary>
        T NewForm { get; set; }

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
