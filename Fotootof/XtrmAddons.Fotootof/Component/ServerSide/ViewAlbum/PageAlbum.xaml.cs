using System.Windows;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Fotootof.Libraries.Common.Tools;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewAlbum
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Pages Album.
    /// </summary>
    public partial class PageAlbum : PageBase
    {
        #region Variable

        /// <summary>
        /// Variable page album view model.
        /// </summary>
        private PageAlbumModel<PageAlbum> model;

        /// <summary>
        /// Variable album id.
        /// </summary>
        private int itemId;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the page album model.
        /// </summary>
        public PageAlbumModel<PageAlbum> Model
        {
            get
            {
                return model;
            }
        }

        #endregion



        #region constructor

        /// <summary>
        /// 
        /// </summary>
        public PageAlbum(int albumId = 0)
        {
            itemId = albumId;

            InitializeComponent();
            AfterInitializedComponent();
        }

        /// <summary>
        /// Method to initialize page content.
        /// </summary>
        public override void InitializeContent()
        {
            InitializeContentAsync();
        }

        /// <summary>
        /// Method to initialize page content asynchronously.
        /// </summary>
        public async override void InitializeContentAsync()
        {

            // Initialize associated view model of the page.
            model = new PageAlbumModel<PageAlbum>(this);

            // Set busy indicator
            AppOverwork.IsBusy = true;
            AppOverwork.BusyContent = "Initializing page content. Please wait...";

            AlbumEntity album = new AlbumEntity();

            if (itemId > 0)
            {
                AlbumOptionsSelect options = new AlbumOptionsSelect
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    PrimaryKey = itemId
                };
                album = await MainWindow.Database.Albums.SingleOrNullAsync(options);

                if(album == null)
                {
                    album = new AlbumEntity();
                }
            }
            else
            {
                album  = new AlbumEntity();
            }
            var g = album.Pictures;
            model.Album = album;
            var a = model.Pictures;
            DataContext = model;

            PicturesListView.TextBlockAlbumName.Text = model.Album.Name;

            // End of busy indicator.
            AppOverwork.BusyContent = "Initializing page content. Done.";
            AppOverwork.IsBusy = false;
        }

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
