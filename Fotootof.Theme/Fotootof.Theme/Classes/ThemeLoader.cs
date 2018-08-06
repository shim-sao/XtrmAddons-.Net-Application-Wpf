using Fotootof.Libraries.Logs;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Theme
{
    /// <summary>
    /// Class XtrmAddons Fotootof Theme Loader.
    /// </summary>
    public static class ThemeLoader
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Property to access to the custom default theme dictionary assembly path.
        /// </summary>
        public static readonly string defaultAssemblyDictionary
            = "Fotootof.Theme.Light;component/Dictionary.xaml";

        /// <summary>
        /// Property to access to the custom alt theme dictionary assembly path.
        /// </summary>
        public static readonly string altAssemblyDictionary
            = "Fotootof.Theme.Dark;component/Dictionary.xaml";

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the theme dictionary of resources <see cref="ResourceDictionary"/>
        /// </summary>
        public static ResourceDictionary Resources { get; private set; }
            = new ResourceDictionary();

        /// <summary>
        /// Property to access to the custom theme assembly.
        /// </summary>
        public static string Theme
            => ApplicationBase.UI.GetParameter("ApplicationTheme", defaultAssemblyDictionary);

        #endregion

        /// <summary>
        /// Method to load the custom theme <see cref="ResourceDictionary"/> from an <see cref="Assembly"/> 
        /// </summary>
        /// <param name="assemblyComponent">The path of the <see cref="Assembly"/> ; the path of the <see cref="ResourceDictionary"/> </param>
        /// <param name="uriKind">The kind of uri <see cref="UriKind"/></param>
        /// <example>
        ///     {Assembly};component/{ResourceDictionary}
        /// </example>
        public static void Load(string assemblyComponent, UriKind uriKind = UriKind.Relative)
        {
            if (assemblyComponent == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(assemblyComponent), "The Assembly Component path must not be null !");
                log.Error(e.Output(), e);
                throw e;
            }

            if (assemblyComponent == "Light")
                assemblyComponent = defaultAssemblyDictionary;

            if (assemblyComponent == "Dark")
                assemblyComponent = altAssemblyDictionary;

            ResourceDictionary rd = null;
            try
            {
                rd = new ResourceDictionary
                {
                    Source = new Uri(@assemblyComponent, uriKind)
                };
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw e;
            }

            if(rd.Count == 0)
            {
                log.Debug($"{typeof(ThemeLoader).Name}.{MethodBase.GetCurrentMethod().Name} : Loading empty Assembly Component resources dictionary.");
            }
            
            if(!Resources.MergedDictionaries.ToList().Contains(rd))
            {
                Resources.MergedDictionaries.Add(rd);
            }
            else
            {
                log.Debug($"{typeof(ThemeLoader).Name}.{MethodBase.GetCurrentMethod().Name} : Assembly Component resources dictionary already loaded.");
            }

        }

        /// <summary>
        /// Method to merge the custon theme <see cref="ResourceDictionary"/> to another <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="resources">A <see cref="ResourceDictionary"/>.</param>
        private static void MergeTo(ResourceDictionary resources)
        {
            if(resources == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(resources), resources);
                log.Error(e.Output(), e);
                throw e;
            }

            try
            {
                if (!resources.MergedDictionaries.ToList().Contains(Resources))
                {
                    resources.MergedDictionaries.Add(Resources);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw;
            }
        }

        /// <summary>
        /// Method to merge the custon theme <see cref="ResourceDictionary"/> to another <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="resources">A <see cref="ResourceDictionary"/>.</param>
        /// <param name="mergeApp">Should merge application resources.</param>
        public static void MergeThemeTo(ResourceDictionary resources, bool mergeApp = true)
        {
            if (resources == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(resources), resources);
                log.Error(e.Output(), e);
                throw e;
            }

            try
            {
                if (mergeApp == true && (Application.Current.MainWindow)?.Resources != null &&
                    !resources.MergedDictionaries.ToList().Contains((Application.Current.MainWindow).Resources))
                {
                    resources.MergedDictionaries.Add((Application.Current.MainWindow).Resources);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw;
            }

            Load(Theme);
            MergeTo(resources);
        }

        [System.Obsolete("MergeThemeTo", true)]
        public static void TempDeprecatedMergeDynamicResources(ResourceDictionary resources, bool mergeApp = true)
        {
            try
            {
                if (mergeApp)
                {
                    resources.MergedDictionaries.Add((Application.Current.MainWindow).Resources);
                }

                string theme = ApplicationBase.UI.GetParameter("ApplicationTheme", "Dark");
                ResourceDictionary rd = new ResourceDictionary
                {
                    Source = new Uri($"Fotootof.Template.{theme};component/Dictionary.xaml", UriKind.Relative)
                };
                resources.MergedDictionaries.Add(rd);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
            }
        }
    }
}
