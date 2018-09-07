using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Objects;

namespace Fotootof.AddInsContracts.Base
{
    /// <summary>
    /// Class Fotootof AddInsContracts Module Base.
    /// </summary>
    // [Export(typeof(IModule))]
    public abstract class ModuleBase : ObjectBaseNotifier, IModule
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the module <see cref="System.Windows.Controls.MenuItem"/>.
        /// </summary>
        private MenuItem menuItem;

        #endregion


        #region Properties

        /// <summary>
        /// Property to access to the name of the module.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Property to access to the parent <see cref="FrameworkElement"/> name.
        /// </summary>
        public virtual string ParentName { get; set; } = "MenuItem_Plugins_Name";

        /// <summary>
        /// Property to acces to the <see cref="System.Windows.Controls.MenuItem"/> of the module.
        /// </summary>
        public MenuItem MenuItem
        {
            get
            {
                if (menuItem == null)
                {
                    InitializeModule();
                }
                return menuItem;
            }
            set
            {
                if (menuItem != value)
                {
                    menuItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Property to access to the <see cref="IComponent"/> associated to the model.
        /// </summary>
        // [Import]
        public abstract IComponent Component { get; set; }

        /// <summary>
        /// Property to access to the <see cref="IProcess"/> associated to the model.
        /// </summary>
        // [Import]
        public abstract IProcess Process { get; set; }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the module.
        /// </summary>
        private void InitializeModule()
        {
            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            menuItem = menuItem ?? new MenuItem()
            {
                Header = Name,
                Tag = this

            };
            menuItem.Click += new RoutedEventHandler(InterfaceControl_Click);
        }

        /// <summary>
        /// Method 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void InterfaceControl_Click(object sender, RoutedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {Process}");

            if (Process != null && Process.IsEnable)
            {
                log.Info($"Starting module '{Name}' process. Please wait...");
                Process.Run();
            }
        }

        #endregion



        #region IDisposable Support

        /// <summary>
        /// Variable is disposed ?
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Method to dispose the object.
        /// </summary>
        /// <param name="disposing">Dispose managed objects ?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed objects.
                    MenuItem = null;
                    Component = null;
                }

                // Dispose not managed objects & big size variables = null.

                // Flag disposed value.
                disposedValue = true;
            }
        }

        // TODO: replace a finalizer only if the Dispose (bool disposing) feature above has code to release unmanaged resources.
        // ~HttpWebClient()
        //{
        //    // Do not modify this code. Place the cleaning code in Dispose (bool disposing) above.
        //    Dispose(false);
        //}

        /// <summary>
        /// This code is added to properly implement the deleteable template.
        /// </summary>
        public void Dispose()
        {
            // Do not modify this code. Place the cleaning code in Dispose (bool disposing) above.
            Dispose(true);

            // TODO: remove comment marks for the next line if the finalizer is replaced above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
