using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.SQLiteService;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.LayoutsOld.Windows.Forms.UserForm
{
    /// <summary>
    /// <para>Class XtrmAddons Net Windows Validation Rule String Required.</para>
    /// <para>Check if a formated string as email is valid and not null, empty or whitespace.</para>
    /// </summary>
    internal class StringUniqueEmail : ValidationRule
    {

        public Wrapper Wrapper { get; set; }

        /// <summary>
        /// Method to validate rule to apply to the object.
        /// </summary>
        /// <param name="value">A string.</param>
        /// <param name="cultureInfo">The culture informations.</param>
        /// <returns>True if conditions are validated otherwise false.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            if (!str.IsValidEmail())
            {
                return new ValidationResult(false, "This email is not not valid.");
            }

            if (!IsUniqueEmail(Wrapper.PrimaryKey, str))
            {
                return new ValidationResult(false, "This email is already registered.");
            }

            //return ValidationResult.ValidResult;
            return new ValidationResult(true, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        public bool IsUniqueEmail(int pk, string email)
        {
            var op = new UserOptionsSelect { Email = email };
            var u = ((SQLiteSvc)ApplicationSession.Properties.Database).Users.SingleOrNull(op);

            if (u == null)
            {
                return true;
            }
            else if(u.PrimaryKey == pk)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// <para>Class XtrmAddons Net Windows Validation Rule String Required.</para>
    /// <para>Check if a formated string as email is valid and not null, empty or whitespace.</para>
    /// </summary>
    internal class StringUniqueName : ValidationRule
    {

        public Wrapper Wrapper { get; set; }

        /// <summary>
        /// Method to validate rule to apply to the object.
        /// </summary>
        /// <param name="value">A string.</param>
        /// <param name="cultureInfo">The culture informations.</param>
        /// <returns>True if conditions are validated otherwise false.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            if (str.IsNullOrWhiteSpace() || str.Length < 3)
            {
                return new ValidationResult(false, "The name must not be null, empty or whitespace.");
            }

            if (!IsUniqueName(Wrapper.PrimaryKey, str))
            {
                return new ValidationResult(false, "This name is already registered.");
            }

            //return ValidationResult.ValidResult;
            return new ValidationResult(true, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        public bool IsUniqueName(int pk, string name)
        {
            var op = new UserOptionsSelect { Name = name };
            var u = ((SQLiteSvc)ApplicationSession.Properties.Database).Users.SingleOrNull(op);

            if (u == null)
            {
                return true;
            }
            else if (u.PrimaryKey == pk)
            {
                return true;
            }

            return false;
        }
    }

    internal class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty PrimaryKeyProperty =
             DependencyProperty.Register("PrimaryKey", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty NameProperty =
             DependencyProperty.Register("Name", typeof(string),
             typeof(Wrapper), new FrameworkPropertyMetadata(""));


        public int PrimaryKey
        {
            get { return (int)GetValue(PrimaryKeyProperty); }
            set { SetValue(PrimaryKeyProperty, value); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    internal class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}