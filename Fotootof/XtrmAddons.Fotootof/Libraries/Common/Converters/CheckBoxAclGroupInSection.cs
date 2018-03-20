using System;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Libraries.Common.Converters
{
    /// <summary>
    /// 
    /// </summary>
    [System.Obsolete("Use XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters.IsAclGroupInSection", true)]
    public class CheckBoxAclGroupInSection : IValueConverter
    {
        public static SectionEntity Section { get; set; }
        
        /// <summary>
        /// Method to convert string path of the picture into bitmap image. 
        /// </summary>
        /// <param name="value">The binding object path of the picture.</param>
        /// <param name="targetType">The target type for binding.</param>
        /// <param name="parameter">Parameter for convert.</param>
        /// <param name="culture">The culture to convert.</param>
        /// <returns>A bitmap image for image binding.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Section.AclGroupsPK.Contains((int)value);
        }

        /// <summary>
        /// Method to convert bitmap image into string path of the picture. 
        /// </summary>
        /// <param name="value">The binding object path of the picture.</param>
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