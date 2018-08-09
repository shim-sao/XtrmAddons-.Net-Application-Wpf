using Fotootof.Components.Server.Album;
using Fotootof.Components.Server.Browser;
using Fotootof.Components.Server.Plugin;
using Fotootof.Components.Server.Remote;
using Fotootof.Components.Server.Section;
using Fotootof.Components.Server.Slideshow;
using Fotootof.Components.Server.Users;
using Fotootof.Navigator;
using System.Windows.Controls;

namespace Fotootof.Components.Server
{
    /// <summary>
    /// <para>Class Fotootof Server Page Navigator.</para>
    /// <para>Provide some page navigation management.</para>
    /// </summary>
    public class ComponentNavigator : AppNavigatorBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Methods

        /// <summary>
        /// Method to navigate to a <see cref="PageAlbumLayout"/>.
        /// </summary>
        public static void NavigateToAlbum(int albumId)
            => Navigate(new PageAlbumLayout(albumId));

        /// <summary>
        /// Method to navigate to the <see cref="PageBrowserLayout"/>.
        /// </summary>
        public static void NavigateToBrowser()
            => Navigate(new PageBrowserLayout());

        /// <summary>
        /// Method to navigate to a <see cref="PageSectionLayout"/>.
        /// </summary>
        public static void NavigateToSection()
            => Navigate(new PageSectionLayout());

        /// <summary>
        /// Method to navigate to a <see cref="PageUsersLayout"/>.
        /// </summary>
        public static void NavigateToUsers()
            => Navigate(new PageUsersLayout());

        /// <summary>
        /// Method to navigate to a <see cref="PageRemoteLayout"/>.
        /// </summary>
        public static void NavigateToRemote()
            => Navigate(new PageRemoteLayout());

        /// <summary>
        /// Method to navigate to a <see cref="PagePluginLayout"/>.
        /// </summary>
        public static void NavigateToPlugin(UserControl uc)
            => Navigate(new PagePluginLayout(uc));

        /// <summary>
        /// Method to navigate to a <see cref="PageSlideshowLayout"/>.
        /// </summary>
        public static void NavigateToSlideshow(int albumPk)
            => Navigate(new PageSlideshowLayout(albumPk));

        #endregion Methods
    }
}
