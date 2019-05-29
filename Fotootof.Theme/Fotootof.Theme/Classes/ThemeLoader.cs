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



        #region Methods

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
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(assemblyComponent), typeof(string));
                log.Error(e.Output(), e);
                throw e;
            }
            assemblyComponent = GetAssemblyComponent(assemblyComponent);

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
                throw;
            }

            if (rd.Count == 0)
            {
                log.Debug($"{typeof(ThemeLoader).Name}.{MethodBase.GetCurrentMethod().Name} : Loading empty Assembly Component resources dictionary.");
            }

            if (!Resources.MergedDictionaries.ToList().Contains(rd))
            {
                Resources.MergedDictionaries.Add(rd);
                log.Debug($"{typeof(ThemeLoader).Name}.{MethodBase.GetCurrentMethod().Name} : Resources theme dictionary merged.");
            }
            else
            {
                log.Debug($"{typeof(ThemeLoader).Name}.{MethodBase.GetCurrentMethod().Name} : Assembly Component resources dictionary already loaded.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyComponent"></param>
        /// <returns></returns>
        private static string GetAssemblyComponent(string assemblyComponent)
        {
            if (assemblyComponent == "")
                assemblyComponent = defaultAssemblyDictionary;

            else if (assemblyComponent == "Light")
                assemblyComponent = defaultAssemblyDictionary;

            else if (assemblyComponent == "Dark")
                assemblyComponent = altAssemblyDictionary;

            return assemblyComponent;
        }

        /// <summary>
        /// Method to merge the custon theme <see cref="ResourceDictionary"/> to another <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="resources">A <see cref="ResourceDictionary"/>.</param>
        private static void MergeTo(ResourceDictionary resources)
        {
            if(resources == null)
            {
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(resources), typeof(ResourceDictionary));
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
                ArgumentNullException e = Exceptions.GetArgumentNull(nameof(resources), typeof(ResourceDictionary));
                log.Error(e.Output(), e);
                throw e;
            }

            try
            {
                var res = (Application.Current.MainWindow)?.Resources;
                if (mergeApp == true && res != null && !resources.MergedDictionaries.ToList().Contains(res))
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

        #endregion
    }
}
