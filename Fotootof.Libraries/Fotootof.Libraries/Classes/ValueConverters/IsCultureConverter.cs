using System;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Net.Application;

namespace Fotootof.Libraries.ValueConverters
{
    /// <summary>
    /// <para>Method to check dependencies on the set entity.</para>
    /// </summary>
    public class IsCultureConverter : IValueConverter
    {
        /// <summary>
        /// Method to convert check item dependencies. 
        /// </summary>
        /// <param name="value">The binding object.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns>True if entity dependency is found otherwise false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == ApplicationBase.Language;
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