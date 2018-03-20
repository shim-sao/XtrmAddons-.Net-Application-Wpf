using System;

namespace XtrmAddons.Fotootof.Libraries.Common.HttpHelpers.HttpClient
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
        /// <param name="args">Event Arguments.</param>
        public ClientHttpEventArgs(object server, T response)
        {
            Server = server;
            Response = response;
        }

        /// <summary>
        /// Property to access to the  Server arguments.
        /// </summary>
        public object Server { get; set; }

        /// <summary>
        /// Property to access to the  Response arguments.
        /// </summary>
        public T Response { get; set; }
    }
}
