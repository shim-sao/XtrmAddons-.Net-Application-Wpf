using System;
using System.Globalization;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace XtrmAddons.Fotootof.Libraries.Common.Converters
{
    /// <summary>
    /// <para>Method to check dependencies of the set entity.</para>
    /// <para>Perform check on the first none null entity Album|Section.</para>
    /// </summary>
    [System.Obsolete("Use => XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters.IsAlbumInSection", true)]
    public class CheckBoxAlbumInSection : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Property Album entity.
        /// </summary>
        public static AlbumEntity Album { get; set; }

        /// <summary>
        /// Property Section entity.
        /// </summary>
        public static SectionEntity Section { get; set; }

        #endregion

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
            if (Album != null)
            {
                return Album.SectionsPK.Contains((int)value);
            }

            if (Section != null)
            {
                return Section.AlbumsPK.Contains((int)value);
            }

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