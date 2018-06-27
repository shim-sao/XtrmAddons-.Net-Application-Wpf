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

        /// <summary>
        /// Method to get a formated object reference null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        public static NullReferenceException RefNull(string propertyName, object argument)
        {
            if(argument.GetType() == typeof(string))
            {
                return new NullReferenceException($"The reference Type 'string' is null : '{propertyName}'");
            }

            return new NullReferenceException($"The reference Type '{argument.GetType().Name}' is null : '{propertyName}'");
        }

        #endregion
    }
}
