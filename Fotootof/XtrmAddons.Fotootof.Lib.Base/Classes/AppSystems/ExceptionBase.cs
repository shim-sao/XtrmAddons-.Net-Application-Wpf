using System;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib Base Classes AppSystems Message Base.
    /// </summary>
    public class ExceptionBase
    {
        #region Methods

        /// <summary>
        /// Method to get a formated object argument null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        public static ArgumentNullException ArgNull(string propertyName, object argument)
        {
            if(argument.GetType() == typeof(string))
            {
                return new ArgumentNullException($"The argument Type 'string' must be not null, empty or whitespace : '{propertyName}'");
            }

            return new ArgumentNullException($"The argument Type '{argument.GetType().Name}' must be not null : '{propertyName}'");
        }

        #endregion
    }
}
