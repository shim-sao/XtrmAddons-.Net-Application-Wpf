using System;

namespace Fotootof.Libraries.Models.Interfaces
{
    /// <summary>
    /// Interface Fotootof Libraries Model Form Base
    /// </summary>
    public interface IFormData<T>
    {
        /// <summary>
        /// Variable to store the <see cref="Type"/> form data.
        /// </summary>
        T NewFormData { get; set; }

        /// <summary>
        /// Variable to store the old <see cref="Type"/> form data.
        /// </summary>
        T OldFormData { get; set; }

    }
}
