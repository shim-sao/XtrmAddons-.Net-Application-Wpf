using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using XtrmAddons.Fotootof.Builders.AddInsContracts;

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
        private static readonly DirectoryCatalog catalog = new DirectoryCatalog("Plugins");

        /// <summary>
        /// 
        /// </summary>
        private static readonly CompositionContainer container = new CompositionContainer(catalog);

        /// <summary>
        /// 
        /// </summary>
        private static InterfaceBuilder builder;

        #endregion



        #region Properties

        public static InterfaceBuilder Builder
        {
            get
            {
                if(builder == null)
                {
                    Initialize();
                }

                return builder;
            }
            private set => builder = value;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            builder = new InterfaceBuilder();
            container.ComposeParts(builder);

        }

        #endregion
    }
}
