using Fotootof.AddInsContracts.Interfaces;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Objects;

namespace Fotootof.AddInsContracts.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof AddInsContracts Base Component
    /// </summary>
    // [Export(typeof(IComponent))]
    public abstract class ComponentBase : ObjectBaseNotifier, IComponent
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store if the Component is already initialized.
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// Variable to store the parent framework element.
        /// </summary>
        private FrameworkElement parent;

        /// <summary>
        /// Variable to store the custon user control.
        /// </summary>
        private UserControl context;

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual void InitializeComponent()
        {
            if (IsInitialized == false)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
                log.Info("Child class must implement their own component intializer.");

                IsInitialized = true;
            }
        }

        /// <summary>
        /// Property check, or set, if the Component is already initialized.
        /// </summary>
        public bool IsInitialized
        {
            get => isInitialized;
            set
            {
                if (isInitialized != value)
                {
                    isInitialized = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the parent framework element.</para>
        /// </summary>
        public FrameworkElement Parent
        {
            get => parent;
            set
            {
                if (parent != value)
                {
                    parent = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// <para>Property to access to the custon user control.</para>
        /// </summary>
        public UserControl Context
        {
            get
            {
                if (context == null)
                {
                    InitializeComponent();
                }
                return context;
            }
            set
            {
                if (context != value)
                {
                    context = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region IDisposable Support

        /// <summary>
        /// Variable is disposed ?
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Method to dispose the object.
        /// </summary>
        /// <param name="disposing">Dispose managed objects ?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects.
                    Parent = null;
                    Context = null;
                    IsInitialized = false;
                }

                // Dispose not managed objects & big size variables = null.

                // Flag disposed value.
                disposed = true;
            }
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~ComponentBase()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
