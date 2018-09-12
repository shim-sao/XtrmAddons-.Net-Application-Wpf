using Fotootof.AddInsContracts.Interfaces;
using Fotootof.Libraries.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.Storage;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.AddInsContracts.Catalog
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Lib Base Classes AppSystems Catalog Base.</para>
    /// </summary>
    public static class CatalogBase
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<string, DirectoryCatalog> Assemblies { get; }
            = new Dictionary<string, DirectoryCatalog>();

        /// <summary>
        /// 
        /// </summary>
        private static IEnumerable<IExtension> extensions;

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> AppRootDirectories { get; set; }
            = new string[] { ApplicationBase.Storage.Directories.FindKeyFirst("roaming.plugins").AbsolutePath };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> UserRootDirectories { get; set; }
            = new string[] { SpecialDirectoriesExtensions.RootDirectory(SpecialDirectoriesName.Plugin) };

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IExtension> Extensions
        {
            get
            {
                if (extensions == null)
                {
                    Initialize();
                }

                return extensions;
            }
            internal set
            {
                extensions = value;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            log.Info("Initializing extensions catalog. Please wait...");

            DirectoryInfo di = new DirectoryInfo("Plugins\\Extensions");
            log.Debug($"Directory assemblies : {di.FullName}");

            if (System.IO.Directory.Exists(di.FullName))
            {
                DirectoryCatalog dc = new DirectoryCatalog(di.FullName);
                InitializeModules(dc);
            }


            // Assemblies.Add("Plugins", new DirectoryCatalog(di.FullName));

            /*
            DirectoryInfo[] dis = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo dir in dis)
            {
                string path = dir;

                if (!Path.IsPathRooted(dir))
                {
                    string path = Environment.SpecialFolder.ApplicationData
                }
            }


            foreach (string dir in AppRootDirectories)
            {
                string path = dir;
                
                if (!Path.IsPathRooted(dir))
                {
                    string path = Environment.SpecialFolder.ApplicationData
                }
            }*/
            //Assemblies.Add("Plugins", new DirectoryCatalog("Plugins"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        private static void InitializeModules(DirectoryCatalog dc)
        {
            log.Info("Initializing extensions modules. Please wait...");

            if (dc == null)
            {
                log.Info("Catalog directory : empty.");
                return;
            }

            try
            {
                InterfaceBuilder builder = new InterfaceBuilder();
                CompositionContainer container = new CompositionContainer(dc);
                container.ComposeParts(builder);

                if (extensions == null)
                {
                    extensions = new List<IExtension>();
                }

                extensions = extensions.Concat(builder.Extensions);

                log.Debug($"Current Extensions count : {extensions.Count()}");
            }
            catch (ReflectionTypeLoadException ex)
            {
                log.Error(ex.Output(), ex);
                if (ex.LoaderExceptions != null)
                {
                    foreach (Exception le in ex.LoaderExceptions)
                    {
                        log.Error(le.Output(), le);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Output(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        [System.Obsolete("Use Extentions", true)]
        public static IEnumerable<IExtension> GetModules(string assemblyName)
        {
            // Check if modules are already loaded.
            if (extensions != null)
            {
                return extensions;
            }

            // Initialize assemblies.
            Initialize();

            // Check if the assembly name is a valid string.
            if (assemblyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ex = Exceptions.GetArgumentNull(nameof(assemblyName), assemblyName);
                log.Error(ex.Output(), ex);
                return null;
            }

            // Check if an assembly with that name has been loaded.
            if (!Assemblies.Keys.Contains(assemblyName))
            {
                NullReferenceException ex = new NullReferenceException(nameof(assemblyName));
                log.Error(ex.Output(), ex);
                return null;
            }

            try
            {
                InterfaceBuilder builder = new InterfaceBuilder();
                CompositionContainer container = new CompositionContainer(Assemblies[assemblyName]);
                container.ComposeParts(builder);

                return extensions = builder.Extensions;
            }
            catch (ReflectionTypeLoadException ex)
            {
                log.Error(ex.Output(), ex);
                if (ex.LoaderExceptions != null)
                {
                    foreach (Exception le in ex.LoaderExceptions)
                    {
                        log.Error(le.Output(), le);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Output(), ex);
            }

            return null;
        }

        #endregion
    }
}
