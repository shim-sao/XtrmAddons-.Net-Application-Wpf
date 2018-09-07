using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;
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
                    Parent = null;
                    Context = null;
                    IsInitialized = false;
                }

                // Dispose not managed objects & big size variables = null.

                // Flag disposed value.
                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~HttpWebClient()
        //{
        //    // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //    Dispose(false);
        //}

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
