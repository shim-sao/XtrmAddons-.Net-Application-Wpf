using System;
using System.Reflection;
using System.Windows.Controls;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Ui;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Lib Base Classes AppSystems Settings Base.</para>
    /// <para>This class provides easy accesses to store and retrive application settings.</para>
    /// </summary>
    public static class AppSettings
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable argument null exception message for empty Control.
        /// </summary>
        private static readonly string cNE = 
            "The argument object System.Windows.Control is required not null : ctrl";

        /// <summary>
        /// Variable argument null exception message for empty property name.
        /// </summary>
        private static readonly string pNNE = "The argument string Property Name is required not null : propertyName";

        #endregion



        #region Methods

        /// <summary>
        /// Method to get an UiElement application setting.
        /// </summary>
        /// <param name="ctrl">The Control to find settings.</param>
        /// <returns>The corresponding UiElement of Control or a new one if not find.</returns>
        /// <exception cref="ArgumentNullException">Occurs if Control argument is null.</exception>
        public static UiElement<object> GetUiElement(Control ctrl)
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            var setting = ApplicationBase.UI.Controls.FindControl(ctrl);

            if(setting == null)
            {
                setting = new UiElement<object>(ctrl);
                ApplicationBase.UI.Controls.Add(setting);
            }

            return setting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="ctrl"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Occurs if Control or Property Name argument are null.</exception>
        public static V GetValue<V>(Control ctrl, string propertyName, V defaultValue = default(V)) where V : class
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            return GetBindingProperty(ctrl, propertyName, defaultValue).Value as V;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Occurs if Control or Property Name argument are null.</exception>
        public static object GetValueObject(Control ctrl, string propertyName, object defaultValue = default(object))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            return GetBindingProperty(ctrl, propertyName, defaultValue).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(Control ctrl, string propertyName, bool defaultValue = default(bool))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            return (bool)GetBindingProperty(ctrl, propertyName, defaultValue).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(Control ctrl, string propertyName, int defaultValue = default(int))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            log.Debug($"{MethodBase.GetCurrentMethod().Name} : {ctrl.Uid}.{ctrl.Name}.{propertyName}");

            var val = GetBindingProperty(ctrl, propertyName, defaultValue).Value;

            log.Debug($"{MethodBase.GetCurrentMethod().Name} : {val.GetType()} => {val}");

            // Value is int so return it.
            if (val is int)
            {
                return (int)val;

            }

            // Value is long so return it.
            if (val is long)
            {
                return Convert.ToInt32(val);

            }

            // Value is int so parse it before return.
            if (val is string)
            {
                return Int32.Parse(val.ToString());

            }

            // Invalid object to convert as long.
            InvalidCastException e = new InvalidCastException($"Invalid cast conversion type : {val.GetType()} => int");
            log.Error(e.Output());
            MessageBase.DebugFatal(e);
            throw e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(Control ctrl, string propertyName, string defaultValue = default(string))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            return (string)GetBindingProperty(ctrl, propertyName, defaultValue).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetLong(Control ctrl, string propertyName, long defaultValue = default(long))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            log.Debug($"{MethodBase.GetCurrentMethod().Name} : {ctrl.Uid}.{ctrl.Name}.{propertyName}");

            var val = GetBindingProperty(ctrl, propertyName, defaultValue).Value;

            log.Debug($"{MethodBase.GetCurrentMethod().Name} : {val.GetType()} => {val}");
            
            // Value is long so return it.
            if(val.GetType() == typeof(long))
            {
                return (long)val;

            }

            // Value is int so convert it before return.
            if (val.GetType() == typeof(int))
            {
                return Convert.ToInt64(val);

            }

            // Value is int so parse it before return.
            if (val.GetType() == typeof(string))
            {
                return long.Parse(val.ToString());

            }
            
            // Invalid object to convert as long.
            InvalidCastException e = new InvalidCastException($"Invalid cast conversion type : {val.GetType()} => long");
            log.Error(e.Output());
            log.Error($"{MethodBase.GetCurrentMethod().Name} : {ctrl.Uid}.{ctrl.Name}.{propertyName} => {val.GetType()}");
            MessageBase.DebugFatal(e);
            throw e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl">The Control to find.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns></returns>
        public static BindingProperty<object> GetBindingProperty(Control ctrl, string propertyName, object defaultValue = null)
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            var setting = GetUiElement(ctrl);
            var binding = setting.FindBindingProperty(propertyName);
            if(binding == null)
            {
                binding = new BindingProperty<object>() { Name = propertyName, Value = defaultValue };
                setting.Context.Add(binding);
            }

            return binding;
        }

        /// <summary>
        /// Method to store a Control in settings.
        /// </summary>
        /// <param name="ctrl">The Control to add.</param>
        public static void SetUiElement(Control ctrl)
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            ApplicationBase.UI.Controls.AddKeySingle(new UiElement<object>(ctrl));
        }

        /// <summary>
        /// Method to store a Control property in settings.
        /// </summary>
        /// <param name="ctrl">The Control to add.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        public static void SetUiElement(Control ctrl, string propertyName, object propertyValue = null)
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            ApplicationBase.UI.Controls.AddKeySingle(new UiElement<object>(ctrl, propertyName, propertyValue));
        }

        /// <summary>
        /// Method to store an UiElement in settings.
        /// </summary>
        /// <param name="element">The UiElement to store.</param>
        public static void SetUiElement(UiElement<object> element)
        {
            if (element == null)
            {
                ArgumentNullException e = new ArgumentNullException("The argument object UiElement<object> is required not null : " + nameof(element));
                log.Error(e.Output());
                throw e;
            }

            ApplicationBase.UI.Controls.AddKeySingle(element);
        }

        /// <summary>
        /// Method to store an object Control property value.
        /// </summary>
        /// <typeparam name="V">The object Type to store.</typeparam>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static V SetValue<V>(Control ctrl, string propertyName, V propertyValue = default(V)) where V : class
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return bp.Value as V;
        }

        /// <summary>
        /// Method to save an object Control property value.
        /// </summary>
        /// <typeparam name="V">The object Type to store.</typeparam>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static V SaveValue<V>(Control ctrl, string propertyName, V propertyValue = default(V)) where V : class
        {
            V b = SetValue<V>(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return b as V;
        }

        /// <summary>
        /// Method to store a boolean Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static bool SetBool(Control ctrl, string propertyName, bool propertyValue = default(bool))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return (bool)bp.Value;
        }

        /// <summary>
        /// Method to store a boolean Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static bool? SetBool(Control ctrl, string propertyName, bool? propertyValue = default(bool?))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return (bool?)bp.Value;
        }

        /// <summary>
        /// Method to save a boolean Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static bool SaveBool(Control ctrl, string propertyName, bool propertyValue = default(bool))
        {
            bool val = SetBool(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return val;
        }

        /// <summary>
        /// Method to save a boolean Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static bool? SaveBool(Control ctrl, string propertyName, bool? propertyValue = default(bool))
        {
            bool? val = SetBool(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return val;
        }

        /// <summary>
        /// Method to store an int Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static int SetInt(Control ctrl, string propertyName, int propertyValue = default(int))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return (int)bp.Value;
        }

        /// <summary>
        /// Method to save an int Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static int SaveInt(Control ctrl, string propertyName, int propertyValue = default(int))
        {
            int val = SetInt(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return val;
        }

        /// <summary>
        /// Method to store a long Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static long SetLong(Control ctrl, string propertyName, long propertyValue = default(long))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return (int)bp.Value;
        }

        /// <summary>
        /// Method to save a long Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static long SaveLong(Control ctrl, string propertyName, long propertyValue = default(long))
        {
            long l = SetLong(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return l;
        }

        /// <summary>
        /// Method to store a string Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static string SetString(Control ctrl, string propertyName, string propertyValue = default(string))
        {
            if (ctrl == null)
            {
                ArgumentNullException ane = new ArgumentNullException(cNE);
                log.Error(ane.Output());
                throw ane;
            }

            if (propertyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ane = new ArgumentNullException(pNNE);
                log.Error(ane.Output());
                throw ane;
            }

            BindingProperty<object> bp = GetBindingProperty(ctrl, propertyName, propertyValue);
            bp.Value = propertyValue;

            return (string)bp.Value;
        }

        /// <summary>
        /// Method to save a string Control property value.
        /// </summary>
        /// <param name="ctrl">The Control.</param>
        /// <param name="propertyName">The property name to store in settings.</param>
        /// <param name="propertyValue">The property value to store in settings.</param>
        /// <returns>The property value stored in settings.</returns>
        public static string SaveString(Control ctrl, string propertyName, string propertyValue = default(string))
        {
            string s = SetString(ctrl, propertyName, propertyValue);
            ApplicationBase.SaveUi();

            return s;
        }

        #endregion
    }
}
