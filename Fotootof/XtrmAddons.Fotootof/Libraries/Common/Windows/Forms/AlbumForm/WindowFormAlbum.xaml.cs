using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AlbumForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Common Window Form Album.
    /// </summary>
    public partial class WindowFormAlbum : WindowBaseForm
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model.
        /// </summary>
        public new WindowFormAlbumModel<WindowFormAlbum> Model { get; private set; }

        /// <summary>
        /// Variable old Album informations backup.
        /// </summary>
        public AlbumEntity OldForm { get; set; }

        /// <summary>
        /// Variable old Album informations backup.
        /// </summary>
        public AlbumEntity NewForm
        {
            get => Model.Album;
            set => Model.Album = value;
        }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => Button_Save;

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => Button_Cancel;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Form Album Constructor.
        /// </summary>
        /// <param name="entity"></param>
        public WindowFormAlbum(AlbumEntity entity = default(AlbumEntity)) : base()
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel(entity);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

            InputName.TextChanged += InputName_TextChanged;
            InputAlias.TextChanged += InputAlias_TextChanged;
            InputDescription.TextChanged += InputDescription_TextChanged;
            InputComment.TextChanged += InputComment_TextChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void InitializeModel(AlbumEntity entity)
        {
            Model = new WindowFormAlbumModel<WindowFormAlbum>(this);

            // Initialize User first.
            entity = entity ?? new AlbumEntity();

            // Store data in new entity.
            OldForm = entity.Clone();

            // Assign entity to the model.
            IsSectionInAlbum.Entity = NewForm = entity;

            // Assign list of AclGroup to the model.
            Model.Sections = new SectionEntityCollection(true);
        }

        /// <summary>
        /// Method called on input description text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewForm.Description = InputDescription.Text;

            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method called on input comment text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewForm.Comment = InputComment.Text;

            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method called on input name text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string oldName = NewForm.Name;
            NewForm.Name = InputName.Text;

            if (!oldName.IsNullOrWhiteSpace())
            {
                if (NewForm.Alias.IsNullOrWhiteSpace() || NewForm.Alias.Sanitize().RemoveDiacritics().ToLower() == oldName.Sanitize().RemoveDiacritics().ToLower())
                {
                    InputAlias.Text = InputName.Text;
                }
            }

            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method called on input alias text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputAlias_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(InputAlias.Text != "" && InputAlias.Text != InputAlias.Text.Sanitize().RemoveDiacritics().ToLower())
            {
                InputAlias.Text = InputAlias.Text.Sanitize().RemoveDiacritics().ToLower();
            }

            NewForm.Alias = InputAlias.Text;

            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method to check if saving is enabled.
        /// </summary>
        //[Obsolete()]
        //public override bool IsSaveEnabled
        //{
        //    get
        //    {
        //        bool save = true;

        //        if (NewForm != null)
        //        {
        //            if (NewForm.Name.IsNullOrWhiteSpace())
        //            {
        //                save = false;
        //                InputName.BorderBrush = BorderStyleError;
        //            }
        //            else
        //            {
        //                InputName.BorderBrush = BorderStyleReady;
        //            }

        //            if (NewForm.Alias.IsNullOrWhiteSpace())
        //            {
        //                save = false;
        //                InputAlias.BorderBrush = BorderStyleError;
        //            }
        //            else
        //            {
        //                InputAlias.BorderBrush = BorderStyleReady;
        //            }
        //        }
        //        else
        //        {
        //            save = false;
        //        }

        //        return save;
        //    }
        //}

        /// <summary>
        /// Method called on section check box checked event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void CheckBoxSection_Checked(object sender, RoutedEventArgs e)
        {
            SectionEntity entity = (SectionEntity)((CheckBox)sender).Tag;
            NewForm.LinkSection(entity.PrimaryKey);
        }

        /// <summary>
        /// Method called on section check box unchecked event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void CheckBoxSection_UnChecked(object sender, RoutedEventArgs e)
        {
            SectionEntity entity = (SectionEntity)((CheckBox)sender).Tag;
            NewForm.UnLinkSection(entity.PrimaryKey);
        }

        /// <summary>
        /// Method to change thumbnail of an album.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void OnThumbnail_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = Net.Picture.PictureFileDialogBox.Show();

            if (dlg != null && dlg.FileName != "")
            {
               /* NewForm.ThumbnailPath = dlg.FileName;

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new System.Uri(NewForm.ThumbnailPath);
                bmp.EndInit();

                ImageThumbnail.Source = bmp;*/
            }
        }

        /// <summary>
        /// Method to change picture of an album.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void OnPicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = Net.Picture.PictureFileDialogBox.Show();

            if (dlg != null && dlg.FileName != "")
            {
               /* NewForm.PicturePath = dlg.FileName;

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new System.Uri(NewForm.PicturePath);
                bmp.EndInit();

                ImagePicture.Source = bmp;*/
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Filters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.IsEditable = false;

            if(e.RemovedItems.Count > 0)
            {
                foreach (InfoEntity inf in e.RemovedItems) 
                {
                    NewForm.UnLinkInfo(inf.PrimaryKey);
                }
            }

            // Get color filter.
            InfoEntity color = FiltersColorSelector.SelectedItem as InfoEntity;
            if(color != null && color.PrimaryKey != 0)
            {
                NewForm.LinkInfo(color.PrimaryKey);
            }

            // Get quality filter.
            InfoEntity quality = FiltersQualitySelector.SelectedItem as InfoEntity;
            if (quality != null && quality.PrimaryKey != 0)
            {
                NewForm.LinkInfo(quality.PrimaryKey);
            }
        }

        /// <summary>
        /// Method called on color filters selector loaded event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void FiltersColorSelector_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (InfoEntity inf in Model.FiltersColor)
            {
                try
                {
                    InfosInAlbums info = Model.Album.InfosInAlbums.Single(x => x.InfoId == inf.InfoId);
                    FiltersColorSelector.SelectedItem = inf;
                }
                catch { }
            }
        }

        /// <summary>
        /// Method called on quality filters selector loaded event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void FiltersQualitySelector_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (InfoEntity inf in FiltersQualitySelector.Items)
            {
                try
                {
                    InfosInAlbums info = Model.Album.InfosInAlbums.Single(x => x.InfoId == inf.InfoId);
                    FiltersQualitySelector.SelectedItem = inf;
                }
                catch { }
            }
        }

        #endregion
    }
}
