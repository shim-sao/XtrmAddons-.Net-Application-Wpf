using System.ComponentModel.Composition;
using System.Windows.Controls;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;
using XtrmAddons.Fotootof.Lib.Base.Enums;
using XtrmAddons.Net.Common.Objects;

namespace XtrmAddons.Fotootof.AddInsContracts.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof AddInsContracts Base Extension.
    /// </summary>
    [Export(typeof(IExtension))]
    public abstract class ExtensionBase : ObjectBaseNotifier, IExtension
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Properties

        /// <summary>
        /// Property to check and set the display mode of the application.
        /// </summary>
        public abstract DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Property to access to the extension module.
        /// </summary>
        [Import]
        public abstract IModule Module { get; set; }

        /// <summary>
        /// Property to access to the extension component.
        /// </summary>
        public UserControl Component
        {
            get => Module.Component.Context;
            set
            {
                if(Module.Component.Context != value)
                {
                    Module.Component.Context = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
