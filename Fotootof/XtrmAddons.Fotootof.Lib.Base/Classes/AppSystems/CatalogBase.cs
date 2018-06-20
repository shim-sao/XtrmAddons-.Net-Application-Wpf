using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using XtrmAddons.Fotootof.Builders.AddInsContracts;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems
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
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<string, DirectoryCatalog> Assemblies { get; }
            = new Dictionary<string, DirectoryCatalog>();

        /// <summary>
        /// 
        /// </summary>
        private static IEnumerable<IModule> modules;

        #endregion



        #region Properties

        public static IEnumerable<IModule> GetModules(string assemblyName)
        {
            // Check if modules are already loaded.
            if(modules != null)
            {
                return modules;
            }

            // Initialize assemblies.
            Initialize();

            // Check if the assembly name is a valid string.
            if (assemblyName.IsNullOrWhiteSpace())
            {
                ArgumentNullException ex = ExceptionBase.ArgNull(nameof(assemblyName), assemblyName);
                log.Error(ex.Output(), ex);
                return null;
            }

            // Check if an assembly with that name has been loaded.
            if(!Assemblies.Keys.Contains(assemblyName))
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

                return modules = builder.Modules;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Output(), ex);
            }

            return null;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            Assemblies.Add("Plugins", new DirectoryCatalog("Plugins"));
        }

        #endregion
    }
}
