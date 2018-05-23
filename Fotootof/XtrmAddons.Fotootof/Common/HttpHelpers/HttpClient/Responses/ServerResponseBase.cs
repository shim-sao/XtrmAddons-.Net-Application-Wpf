namespace XtrmAddons.Fotootof.Common.HttpHelpers.HttpClient.Responses
{
    public class ServerResponseBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Authentication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual T Response { get; set; }
    }
}
