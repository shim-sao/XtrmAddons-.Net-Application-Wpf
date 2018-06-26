using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;
using XtrmAddons.Fotootof.Lib.Base.Enums;
using XtrmAddons.Net.Common.Objects;

namespace XtrmAddons.Fotootof.AddInsContracts.Base
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IModule))]
    public abstract class ModuleBase : ObjectBaseNotifier, IModule
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private MenuItem menuItem;

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string ParentName { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [Import]
        public abstract Interfaces.IComponent Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public abstract IProcess Process { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Variable Instantiate a SafeHandle instance.
        /// </summary>
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

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
                }

                // Dispose not managed objects & big size variables = null.
                MenuItem = null;
                Component = null;

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
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
