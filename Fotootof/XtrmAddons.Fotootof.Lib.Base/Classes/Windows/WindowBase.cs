using System.ComponentModel;
using System.Windows;
using System;
using XtrmAddons.Net.Application;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Windows Form.
    /// </summary>
    public abstract partial class WindowBase : Window
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

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        public dynamic DWords => Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        public dynamic DLogs => Culture.Translation.DLogs;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Windows Form Constructor.
        /// </summary>
        public WindowBase() : base()
        {
            // Merge application culture translation resources.
            Resources.MergedDictionaries.Add(Culture.Translation.Words);
            Resources.MergedDictionaries.Add(Culture.Translation.Logs);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to convert a Framework Element tag to an object type.
        /// </summary>
        /// <param name="fe">A framework element.</param>
        public static T Tag2Object<T>(object fe, bool defaut = false) where T : class
        {
            if (fe is null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(fe), "FrameworkElement element is null.");
                log.Error(e.Output(), e);
                throw e;
            }

            if (!fe.GetType().IsSubclassOf(typeof(FrameworkElement)))
            {
                TypeAccessException e = new TypeAccessException($"`{nameof(fe)}` Type of `{fe.GetType()}` : Invalid parameter type. FrameworkElement inheritance type is required.");
                log.Error(e.Output(), e);
                throw e;
            }

            if (defaut)
            {
                return (T)((FrameworkElement)fe).Tag ?? default(T);
            }
            else
            {
                return (T)((FrameworkElement)fe).Tag;
            }
        }

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Cancel event arguments.</param>
        protected abstract void Window_Closing(object sender, CancelEventArgs e);

        #endregion
    }
}
