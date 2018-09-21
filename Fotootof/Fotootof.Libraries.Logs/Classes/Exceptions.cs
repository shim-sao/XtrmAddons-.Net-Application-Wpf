﻿using System;
using System.Globalization;

namespace Fotootof.Libraries.Logs
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Logs Exceptions.
    /// </summary>
    public class Exceptions
    {
        #region Methods

        /// <summary>
        /// Method to get a formated object argument null exception.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="argument">The property.</param>
        /// <returns>A formated argument null exception.</returns>
        public static ArgumentNullException GetArgumentNull(string propertyName, object argument)
        {
            if(argument.GetType() == typeof(string))
            {
                return new ArgumentNullException(propertyName, Properties.Translations.ExceptionsArgumentNullString);
            }

            return new ArgumentNullException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    Properties.Translations.ExceptionsArgumentNull,
                    argument.GetType().Name,
                    propertyName
                )
            );
        }

        /// <summary>
        /// Method to get a formated object reference null exception.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="argument">The property.</param>
        /// <returns>A formated null reference exception.</returns>
        public static NullReferenceException GetReferenceNull(string propertyName, object argument)
        {
            if(argument?.GetType() == typeof(string))
            {
                return new NullReferenceException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Properties.Translations.ExceptionsReferenceNullString,
                        propertyName
                    )
                );
            }

            return new NullReferenceException(
                string.Format(
                        CultureInfo.CurrentCulture,
                        Properties.Translations.ExceptionsReferenceNull,
                        argument.GetType().Name,
                        propertyName
                    )
                );
        }

        #endregion
    }
}