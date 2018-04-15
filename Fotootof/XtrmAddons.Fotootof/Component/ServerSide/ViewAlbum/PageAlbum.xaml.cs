using System;
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
        #region Properties

        /// <summary>
        /// Property to access to the page album model.
        /// </summary>
        public PageAlbumModel<PageAlbum> Model { get; private set; }

        /// <summary>
        /// Property to access to the album id.
        /// </summary>
        private int ItemId { get; }

        #endregion



        #region constructor

        /// <summary>
        /// 
        /// </summary>
        public PageAlbum(int albumId = 0)
        {
            InitializeComponent();
            ItemId = albumId;
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
            Model = new PageAlbumModel<PageAlbum>(this);

            // Set busy indicator
            AppOverwork.IsBusy = true;
            AppOverwork.BusyContent = "Initializing page content. Please wait...";

            AlbumEntity album = new AlbumEntity();

            if (ItemId > 0)
            {
                AlbumOptionsSelect options = new AlbumOptionsSelect
                {
                    Dependencies = { EnumEntitiesDependencies.All },
                    PrimaryKey = ItemId
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

            Model.Album = album;
            DataContext = Model;

            PicturesCollection.Title_Text.Text = Model.Album.Name;

            // End of busy indicator.
            AppOverwork.BusyContent = "Initializing page content. Done.";
            AppOverwork.IsBusy = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Size changed event arguments.</param>
        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement fe = ((MainWindow)AppWindow).Block_Content as FrameworkElement;

            this.Width = Math.Max(fe.ActualWidth, 0);
            this.Height = Math.Max(fe.ActualHeight, 0);

            Block_MiddleContents.Width = Math.Max(this.Width, 0);
            Block_MiddleContents.Height = Math.Max(this.Height, 0);

            PicturesCollection.Height = Math.Max(this.Height, 0);

            TraceSize(fe);
            TraceSize(this);
            TraceSize(Block_MiddleContents);
            TraceSize(PicturesCollection);
        }

        #endregion
    }
}
