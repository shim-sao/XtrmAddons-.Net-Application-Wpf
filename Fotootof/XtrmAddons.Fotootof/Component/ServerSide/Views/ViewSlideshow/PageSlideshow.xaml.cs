using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Fotootof.Common.Tools;
using System.Globalization;
using System;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;

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
        public PageSlideshowModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="collection">A pictures entities Collection.</param>
        public PageSlideshow(PictureEntityCollection collection, PictureEntity picture = null)
        {
            MessageBase.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageWaiting, "Slideshow"));

            pictures = collection;
            currentPicture = picture;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageDone, "Slideshow"));
            MessageBase.IsBusy = false;
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Component ServerSide View Slideshow Constructor.
        /// </summary>
        /// <param name="collection">An album primary key.</param>
        public PageSlideshow(int albumPk, PictureEntity picture = null)
        {
            MessageBase.IsBusy = true;
            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageWaiting, "Slideshow"));

            this.albumPk = albumPk;
            currentPicture = picture;

            // Constuct page component.
            InitializeComponent();
            AfterInitializedComponent();

            log.Info(string.Format(CultureInfo.CurrentCulture, DLogs.InitializingPageDone, "Slideshow"));
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
        public override async void InitializeModel()
        {

            Model = new PageSlideshowModel(this)
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
        }

        #endregion



        #region Methods Size Changed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e) { }

        #endregion
    }
}
