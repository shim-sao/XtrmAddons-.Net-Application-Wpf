using Fotootof.Libraries.Models;
using System;
using System.Reflection;

namespace Fotootof.Libraries.Controls
{
    /// <summary>
    /// Class Fotootof Libraries Base Model.
    /// </summary>
    /// <typeparam name="T">The class Type of the model owner.</typeparam>
    public abstract class ControlLayoutModel<T> : ModelBase, IDisposable where T : class
    {
        #region Variable

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        

        #region Properties

        /// <summary>
        /// Property to access to the owner object associated to the model.
        /// </summary>
        public T ControlView { get; protected set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Libraries Base Model Constructor.
        /// </summary>
        public ControlLayoutModel() : base() { }

        /// <summary>
        /// Class Fotootof Libraries Base Model Constructor.
        /// </summary>
        /// <param name="controlView">The <see cref="object"/> view associated to the model.</param>
        public ControlLayoutModel(T controlView) : base()
        {
            ControlView = controlView;
        }

        #endregion



        #region IDisposable

        /// <summary>
        /// Variable to track whether Dispose has been called.
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
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Track whether Dispose has been called.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ControlView = null;
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.


                // Note disposing has been done.
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
        ~ControlLayoutModel()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
