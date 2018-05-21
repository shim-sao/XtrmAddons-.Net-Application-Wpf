using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Common.Collections;

namespace XtrmAddons.Fotootof.Component.ServerSide.Views.ViewSlideshow
{
    /// <summary>
    /// Class XtrmAddons Fotootof Component ServerSide View Slideshow.
    /// </summary>
    public partial class PageSlideshow : PageBase
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable to store the constructor collection argument.
        /// </summary>
        private PictureEntityCollection pictures;

        /// <summary>
        /// Variable to store the constructor current picture argument.
        /// </summary>
        private PictureEntity currentPicture;

        /// <summary>
        /// Variable to store constructor album id argument.
        /// </summary>
        private int albumPk;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page model.
        /// </summary>
        public PageSlideshowModel<PageSlideshow> Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="collection">A pictures entities Collection.</param>
        public PageSlideshow(PictureEntityCollection collection, PictureEntity picture = null)
        {
            InitializeComponent();
            pictures = collection;
            currentPicture = picture;
            AfterInitializedComponent();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="collection">An album primary key.</param>
        public PageSlideshow(int albumPk, PictureEntity picture = null)
        {
            InitializeComponent();
            this.albumPk = albumPk;
            currentPicture = picture;
            AfterInitializedComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Page_Loaded_Async(sender, e);
        }

        /// <summary>
        /// Method to initialize page content asynchronously.
        /// </summary>
        public override async void Page_Loaded_Async(object sender, RoutedEventArgs e)
        {

            Model = new PageSlideshowModel<PageSlideshow>(this)
            {
                Pictures = pictures
            };

            if (albumPk > 0)
            {
                AlbumEntity album = new AlbumEntity();

                AlbumOptionsSelect options = new AlbumOptionsSelect
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    PrimaryKey = albumPk
                };
                album = await MainWindow.Database.Albums.SingleOrNullAsync(options);

                if (album != null)
                {
                    Model.Pictures = new PictureEntityCollection(album.Pictures);
                }
            }

            if(currentPicture != null)
            {
                Model.CurrentPicture = currentPicture;
            }
            else if(Model.Pictures.Count > 0)
            {
                Model.CurrentPicture = Model.Pictures[0];
            }

            DataContext = Model;
        }

        /// <summary>
        /// Method called on user control size changed event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        #endregion
    }
}
