using System;

namespace XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Base Client Http Event Arguments.
    /// </summary>
    //[System.Obsolete("use => XtrmAddons.Fotootof.Lib.Base.Classes.HttpClient.ClientHttpEventArgs")]
    public class ClientHttpEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Base Client Http Event Arguments Constructor.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="result"></param>
        public ClientHttpEventArgs(object server, T result)
        {
            Server = server;
            Result = result;
        }

        /// <summary>
        /// Property to access to the  Server arguments.
        /// </summary>
        public object Server { get; set; }

        /// <summary>
        /// Property to access to the  Response arguments.
        /// </summary>
        public T Result { get; set; }
    }
}
