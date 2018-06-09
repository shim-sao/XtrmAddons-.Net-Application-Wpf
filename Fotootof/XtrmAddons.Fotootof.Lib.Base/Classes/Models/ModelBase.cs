using System;
using System.Windows.Controls;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Ui;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Models
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public class ModelBase<T> : ObjectBaseNotifier // INotifyPropertyChanged
    {
        #region Variable

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
        /// Property to access to the owner object associated to the model.
        /// </summary>
        public T OwnerBase { get; protected set; }

        /// <summary>
        /// Property alias to access to the translation words.
        /// </summary>
        public dynamic Words => Culture.Translation.Words;

        /// <summary>
        /// Property alias to access to the dynamic translation words.
        /// </summary>
        public dynamic DWords => Culture.Translation.DWords;

        /// <summary>
        /// Property alias to access to the translation logs.
        /// </summary>
        public dynamic Logs => Culture.Translation.Logs;

        /// <summary>
        /// Property alias to access to the dynamic translation logs.
        /// </summary>
        public dynamic DLogs => Culture.Translation.DLogs;

        #endregion


/*
        #region Events Handlers
        
        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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
*/


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

        #endregion



        #region Obsolete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[Obsolete("Use SettingsBase", true)]
        //public UiElement<object> GetSettings(Control ctrl)
        //{
        //    if(ctrl == null)
        //    {
        //        ArgumentNullException ane = new ArgumentNullException($"The argument object System.Windows.Control is required not null : {nameof(ctrl)}");
        //        log.Error(ane.Output());
        //        throw ane;
        //    }

        //    return ApplicationBase.UI.Controls.FindControl(ctrl) ?? new UiElement<object>(ctrl);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[Obsolete("Use SettingsBase")]
        //public V GetSettingsValue<V>(Control ctrl, string propertyName, V defaultValue = null) where V : class
        //{
        //    if (ctrl == null)
        //    {
        //        ArgumentNullException ane = new ArgumentNullException("The argument object System.Windows.Control is required not null : " + nameof(ctrl));
        //        log.Error(ane.Output());
        //        throw ane;
        //    }

        //    if (propertyName == null)
        //    {
        //        ArgumentNullException ane = new ArgumentNullException("The argument string Property Name is required not null : " + nameof(propertyName));
        //        log.Error(ane.Output());
        //        throw ane;
        //    }

        //    return GetBindingProperty(ctrl, propertyName, defaultValue) as V;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[Obsolete("Use SettingsBase")]
        //public BindingProperty<object> GetBindingProperty(Control ctrl, string propertyName, object defaultValue = null)
        //{
        //    if (ctrl == null)
        //    {
        //        ArgumentNullException ane = new ArgumentNullException("The argument object System.Windows.Control is required not null : " + nameof(ctrl));
        //        log.Error(ane.Output());
        //        throw ane;
        //    }

        //    if (propertyName == null)
        //    {
        //        ArgumentNullException ane = new ArgumentNullException("The argument string Property Name is required not null : " + nameof(propertyName));
        //        log.Error(ane.Output());
        //        throw ane;
        //    }

        //    return GetSettings(ctrl).FindBindingProperty(propertyName) ?? new BindingProperty<object>() { Name = propertyName, Value = defaultValue };
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        //[Obsolete("Use SettingsBase")]
        //public void SetControlSettings(UiElement<object> element)
        //{
        //    ApplicationBase.BeginInvokeIfRequired(new Action(() =>
        //    {
        //        ApplicationBase.UI.Controls.AddKeySingle(element);
        //    }));
        //}

        #endregion
    }
}
