using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using Fotootof.HttpClient.WebAuth;
using Fotootof.HttpClient.WebClient;

namespace Fotootof.HttpClient
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpWebClientApplication : IDisposable
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public HttpWebClient Client { get; private set; }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverHostname"></param>
        /// <param name="serverPort"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public HttpWebClientApplication( string serverHostname, string serverPort, string email, string password, string username = null)
        {
            Client = new HttpWebClient(serverHostname, serverPort, new HttpWebAuth(email, password, username));
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
                    Client?.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
                Client = null;

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
        //~HttpWebClientApplication()
        //{
        //    // Do not re-create Dispose clean-up code here.
        //    // Calling Dispose(false) is optimal in terms of
        //    // readability and maintainability.
        //    Dispose(false);
        //}
        
        #endregion
    }
}
