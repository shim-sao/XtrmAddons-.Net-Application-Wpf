using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using XtrmAddons.Fotootof.Lib.HttpClient.WebAuth;
using XtrmAddons.Fotootof.Lib.HttpClient.WebClient;

namespace XtrmAddons.Fotootof.Lib.HttpClient
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



        #region IDisposable Support

        private bool disposedValue = false; // Pour détecter les appels redondants
                                            // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    Client.Dispose();
                    handle.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;

                Client = null;
            }

            Client.Dispose();
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~HttpWebClientApplication()
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
