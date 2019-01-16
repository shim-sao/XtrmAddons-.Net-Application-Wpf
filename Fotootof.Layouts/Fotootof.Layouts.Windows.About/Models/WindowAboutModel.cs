using Fotootof.Libraries.Controls;
using System.Reflection;
using System.Windows.Media.Imaging;
using XtrmAddons.Net.Picture.Extensions;

namespace Fotootof.Layouts.Windows.About
{
    /// <summary>
    /// Class Fotootof Layouts Window About Model.
    /// </summary>
    public class WindowAboutModel : ControlLayoutModel<WindowAboutLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Accesseurs d'attribut de l'assembly

        /// <summary>
        /// Property to access to the main application assembly title.
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Property to access to the main application assembly version.
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
#if DEBUG
                return Assembly.GetEntryAssembly().GetName().Version.ToString() + " - development)";
#else
                return Assembly.GetEntryAssembly().GetName().Version.ToString() + " - release";
#endif
            }
        }

        /// <summary>
        /// Property to access to the main application assembly description.
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Property to access to the main application assembly product.
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Property to access to the main application assembly copyright.
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Property to access to the main application assembly company.
        /// </summary>
        public BitmapImage ImageCompany => Properties.Resource.x_logo_64x64.ToBitmapImage();

        /// <summary>
        /// Property to access to the main application assembly company.
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Layouts Window About Model Constructor.
        /// </summary>
        /// <param name="controlView">The associated window form base.</param>
        public WindowAboutModel(WindowAboutLayout controlView) : base(controlView) { }

        #endregion

    }
}
