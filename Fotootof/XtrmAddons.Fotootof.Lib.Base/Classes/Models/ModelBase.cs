using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Ui;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public class ModelBase<T> : INotifyPropertyChanged
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property alias to access to the main database connector.
        /// </summary>
        public static SQLiteSvc Db
            => (SQLiteSvc)ApplicationSession.Properties.Database;

        /// <summary>
        /// Property to access to the owner object associated to the model.
        /// </summary>
        public T OwnerBase { get; protected set; }

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        public dynamic DWords => Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        public dynamic DLogs => Culture.Translation.DLogs;

        #endregion



        #region Events Handlers

        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Model Constructor.
        /// </summary>
        public ModelBase() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        public ModelBase(T owner)
        {
            OwnerBase = owner;
        }

        #endregion



        #region Methods

        /// <summary>
        /// <para>This method is called by the Set accessor of each property.</para>
        /// <para>The CallerMemberName attribute that is applied to the optional propertyName</para>
        /// </summary>
        /// <param name="propertyName">Parameter causes the property name of the caller to be substituted as an argument.</param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UiElement<object> GetControlSettings(Control ctrl)
        {
            return ApplicationBase.UI.Controls.FindControl(ctrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void SetControlSettings(UiElement<object> element)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ApplicationBase.UI.Controls.AddKeySingle(element);
            }));
        }

        #endregion
    }
}
