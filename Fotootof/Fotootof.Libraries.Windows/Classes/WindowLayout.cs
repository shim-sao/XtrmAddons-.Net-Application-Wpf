﻿using Fotootof.Libraries.Logs;
using Fotootof.Theme;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using Fotootof.SQLite.Services;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Libraries.Windows
{
    /// <summary>
    /// Class Fotootof Libraries Window Layout.
    /// </summary>
    public abstract partial class WindowLayout : Window
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property alias to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Windows Form Constructor.
        /// </summary>
        public WindowLayout() : base()
        {
            // Merge application culture translation resources.
            Resources.MergedDictionaries.Add(XtrmAddons.Fotootof.Culture.Translation.Words);
            Resources.MergedDictionaries.Add(XtrmAddons.Fotootof.Culture.Translation.Logs);

            ThemeLoader.MergeThemeTo(Resources);
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindName<T>(string name) where T : class
        {
            return (T)FindName(name);
        }

        /// <summary>
        /// Method to convert a <see cref="FrameworkElement"/> Tag to an <see cref="object"/> of <see cref="Type"/> T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fe">An <see cref="object"/> inherited from <see cref="FrameworkElement"/>.</param>
        /// <param name="defaut"></param>
        /// <returns></returns>
        public static T GetTag<T>(object fe, T defaut = null) where T : class
        {
            if (fe is null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(fe), fe);
                log.Error(e.Output(), e);
                throw e;
            }

            if (!fe.GetType().IsSubclassOf(typeof(FrameworkElement)))
            {

                TypeAccessException e = new TypeAccessException
                (
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Fotootof.Libraries.Logs.Properties.Translations.TypeAccessExceptionFrameworkElement,
                        nameof(fe),
                        fe.GetType()
                    )
                );
                log.Error(e.Output(), e);
                throw e;
            }

            if (defaut != null)
            {
                return (T)(fe as FrameworkElement).Tag ?? defaut;
            }
            else
            {
                return (T)(fe as FrameworkElement).Tag;
            }
        }

        /// <summary>
        /// Method called on <see cref="Window"/> closing.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The cancel event arguments <see cref="CancelEventArgs"/>.</param>
        protected abstract void Window_Closing(object sender, CancelEventArgs e);

        #endregion
    }
}