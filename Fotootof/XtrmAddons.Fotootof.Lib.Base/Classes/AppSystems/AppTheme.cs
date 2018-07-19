using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppTheme
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static ResourceDictionary Resources { get; private set; }
            = new ResourceDictionary();

        /// <summary>
        /// 
        /// </summary>
        public static string Theme
            => ApplicationBase.UI.GetParameter("ApplicationTheme", "XtrmAddons.Fotootof.Template;component/Theme/Light.xaml");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        public static void Load(string assemblyComponent, UriKind uriKind = UriKind.Relative)
        {
            if (assemblyComponent == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(assemblyComponent), "The Assembly Component path must not be null !");
                log.Error(e.Output(), e);
                throw e;
            }

            ResourceDictionary rd = null;
            try
            {
                rd = new ResourceDictionary
                {
                    Source = new Uri(@assemblyComponent)
                };
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw e;
            }

            if(rd.Count == 0)
            {
                log.Debug($"{typeof(AppTheme).Name}.{MethodBase.GetCurrentMethod().Name} : Loading empty Assembly Component resources dictionary.");
            }
            
            if(!Resources.MergedDictionaries.ToList().Contains(rd))
            {
                Resources.MergedDictionaries.Add(rd);
            }
            else
            {
                log.Debug($"{typeof(AppTheme).Name}.{MethodBase.GetCurrentMethod().Name} : Assembly Component resources dictionary already loaded.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        public static void MergeTo(ResourceDictionary resources)
        {
            if(resources == null)
            {
                ArgumentNullException e = new ArgumentNullException(nameof(resources), "The resources must not be null !");
                log.Error(e.Output(), e);
                throw e;
            }

            try
            {
                resources.MergedDictionaries.Add(Resources);
            }
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw e;
            }
        }
    }
}
