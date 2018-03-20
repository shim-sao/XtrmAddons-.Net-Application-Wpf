using System;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Libraries.Common.Converters
{
    /// <summary>
    /// </summary>
    public class CheckBoxUserInAclGroup : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        public static UserEntity User { get; set; }

        public static AclGroupEntity AclGroup { get; set; }

        /// <summary>
        /// Method to convert. 
        /// </summary>
        /// <param name="value">The binding object.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (User != null)
                return User.AclGroupsPK.Contains((int)value);

            if (AclGroup != null)
                return AclGroup.UsersPK.Contains((int)value);

            return false;

        }

        /// <summary>
        /// Method to convert. 
        /// </summary>
        /// <param name="value">The binding object.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns>throw Not Implemented Exception.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}