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
        public static ArgumentNullException ObjArgNull(Type type, string propertyName)
        {
            return new ArgumentNullException($"The argument Type {type.Name} must be not null : {propertyName}");
        }

        #endregion
    }
}
