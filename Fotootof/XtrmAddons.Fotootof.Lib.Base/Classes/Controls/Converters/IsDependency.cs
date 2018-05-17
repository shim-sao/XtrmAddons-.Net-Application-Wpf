using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters
{
    /// <summary>
    /// <para>Method to check dependencies on the set entity.</para>
    /// </summary>
    public abstract class IsDependency<T> : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Property to access to the entity informations.
        /// </summary>
        public static T Entity { get; set; }

        /// <summary>
        /// Property to access to the entity dependencies primary key name..
        /// </summary>
        public abstract string DependenciesPKName { get; }

        #endregion

        /// <summary>
        /// Method to convert check item dependencies. 
        /// </summary>
        /// <param name="value">The binding object.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns>True if entity dependency is found otherwise false<returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Entity != null)
            {
                return ((IList<int>)(Entity).GetPropertyValue(DependenciesPKName, false))?.Contains((int)value);
            }

            return false;
        }

        /// <summary>
        /// Method to rollback convert check item dependencies.  
        /// </summary>
        /// <param name="value">The binding object.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns>throw Not Implemented Exception.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}