using System.ComponentModel;
using System.Windows;
using System;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Windows Form.
    /// </summary>
    public abstract partial class WindowBase : Window
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                throw new ArgumentNullException(nameof(fe));
            }

            if (!fe.GetType().IsSubclassOf(typeof(FrameworkElement)))
            {
                throw new ArgumentException(fe.GetType() + " " + nameof(fe) + " : invalid parameter type. FrameworkElement inheritance type is required.");
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
