using Fotootof.Libraries.Logs;
using System;

namespace Fotootof.Libraries.Systems
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Classes AppSystems Message Base.
    /// </summary>
    [System.Obsolete("Fotootof.Libraries.Logs.Exceptions")]
    public class ExceptionBase
    {
        #region Methods

        /// <summary>
        /// Method to get a formated object argument null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        public static ArgumentNullException ArgNull(string propertyName, object argument)
        {
            return Exceptions.GetArgumentNull(propertyName, argument);
        }

        /// <summary>
        /// Method to get a formated object reference null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        public static NullReferenceException RefNull(string propertyName, object argument)
        {
            return Exceptions.GetReferenceNull(propertyName, argument);
        }

        #endregion
    }
}
