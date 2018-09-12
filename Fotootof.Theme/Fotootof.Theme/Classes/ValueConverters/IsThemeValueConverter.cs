using System;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Net.Application;

namespace Fotootof.Theme.ValueConverters
{
    /// <summary>
    /// <para>Method to check if an argument string is the custom theme preferences.</para>
    /// </summary>
    public class IsThemeValueConverter : IValueConverter
    {
        private static string theme;

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
            theme = ApplicationBase.UI.GetParameter("ApplicationTheme", ThemeLoader.defaultAssemblyDictionary);

            if (theme == "")
                theme = ThemeLoader.defaultAssemblyDictionary;

            else if (theme == "Light")
                theme = ThemeLoader.defaultAssemblyDictionary;

            else if (theme == "Dark")
                theme = ThemeLoader.altAssemblyDictionary;

            string str = (string)value;

            if (str == "")
                str = ThemeLoader.defaultAssemblyDictionary;

            else if (str == "Light")
                str = ThemeLoader.defaultAssemblyDictionary;

            else if (str == "Dark")
                str = ThemeLoader.altAssemblyDictionary;

            return str == theme;
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
            return theme;
        }
    }
}