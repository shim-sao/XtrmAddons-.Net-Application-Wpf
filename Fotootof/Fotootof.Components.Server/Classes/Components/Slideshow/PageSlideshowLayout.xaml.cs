using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Components;
using Fotootof.Libraries.Systems;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System.Globalization;
using System.Windows;

namespace Fotootof.Components.Server.Slideshow
{
    /// <summary>
    /// Class Fotootof Component Slideshow.
    /// </summary>
    public partial class PageSlideshowLayout : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store constructor album id argument.
        /// </summary>
        private int albumPk;

        /// <summary>
        /// Variable to store constructor album id argument.
        /// </summary>
        private PictureEntity currentPicture;

        /// <summary>
        /// Variable to store the constructor collection argument.
        /// </summary>
        private PictureEntityCollection pictures;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageSlideshowModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="collection">A pictures entities Collection.</param>
        /// <param name="picture"></param>
        public PageSlideshowLayout(PictureEntityCollection collection, PictureEntity picture = null)
        {
            MessageBase.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Properties.Logs.InitializingPageWaiting, "Slideshow"));

            pictures = collection;
            currentPicture = picture;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Properties.Logs.InitializingPageDone, "Slideshow"));
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="albumPk"></param>
        /// <param name="picture"></param>
        public PageSlideshowLayout(int albumPk, PictureEntity picture = null)
        {
            MessageBase.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, Properties.Logs.InitializingPageWaiting, "Slideshow"));

            this.albumPk = albumPk;
            currentPicture = picture;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, Properties.Logs.InitializingPageDone, "Slideshow"));
            MessageBase.IsBusy = false;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize page content asynchronously.
        /// </summary>
        public override void InitializeModel()
        {

            Model = new PageSlideshowModel(this)
            {
                Pictures = pictures,
                AlbumPK = albumPk,
                CurrentPicture = currentPicture
            };
            Model.InitializeModelAsync();
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e) { }

        #endregion
    }
}
