namespace XtrmAddons.Fotootof.Lib.HttpClient.WebAuth
{
    /// <summary>
    /// Class XtrmAddons Fotootof Client Http Web Auth.
    /// </summary>
    public class HttpWebAuth
    {
        /// <summary>
        /// Variable server user email.
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Variable server user login.
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// Variable server user password.
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// XtrmAddons PhotoAlbum Client Http Web Auth constructor.
        /// </summary>
        public HttpWebAuth() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Client Http Web Auth Constructor.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="login">The password of the user.</param>
        public HttpWebAuth(string email, string password, string username = null)
        {
            Email = email;
            UserName = username;
            Password = password;
        }
    }
}
