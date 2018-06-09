using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Systems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Dependencies;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Common.Collections;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.AlbumForm
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Window Form Album.</para>
    /// <para>The window provides the tools to create and edit an Album.</para>
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
        public new WindowFormAlbumModel<WindowFormAlbum> Model
        {
            get => (WindowFormAlbumModel<WindowFormAlbum>)model;
            protected set { model = value; }
        }

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
        /// <param name="entity">An Album entity to edit or new Album entity to add.</param>
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

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the window form inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            // Check if the name is not empty.
            Trace.WriteLine("Checking if the name is not empty...");
            if (!IsValidInput(InputName))
            {
                return false;
            }

            Trace.WriteLine("All inputs have been verified !");
            return IsSaveEnabled = base.IsValidInputs();
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the Form Data.
        /// </summary>
        private new bool IsValidForm()
        {
            if (NewForm.Name.IsNullOrWhiteSpace())
            {
                return false;
            }

            return true;
        }

        #endregion



        #region Methods Pictures

        /// <summary>
        /// Method to change thumbnail of an album.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void OnAlbumPicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = Net.Picture.PictureFileDialogBox.Show();

            if (dlg != null && dlg.FileName != "")
            {
                Model.UpdateAlbumPictureProperty((string)((Button)sender).Tag, dlg.FileName);
            }
        }

        #endregion



        #region Methods Section

        /// <summary>
        /// Method called on section check box checked event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void CheckBoxSection_Checked(object sender, RoutedEventArgs e)
        {
            NewForm.LinkSection(Tag2Object<SectionEntity>(sender).PrimaryKey);
        }

        /// <summary>
        /// Method called on section check box unchecked event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Routed event arguments.</param>
        private void CheckBoxSection_UnChecked(object sender, RoutedEventArgs e)
        {
            NewForm.UnlinkSection(Tag2Object<SectionEntity>(sender).PrimaryKey);
        }

        #endregion



        #region Methods Filters

        /// <summary>
        /// Method called on filters selection changed event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The selection changed event arguments</param>
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
            InfoEntity color = (InfoEntity)FiltersColorSelector.SelectedItem;
            if(color != null && color.PrimaryKey != 0)
            {
                NewForm.LinkInfo(color.PrimaryKey);
            }

            // Get quality filter.
            InfoEntity quality = (InfoEntity)FiltersQualitySelector.SelectedItem;
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
                    Func<InfosInAlbums, bool> f = new Func<InfosInAlbums, bool>(x => x.InfoId == inf.InfoId);
                    if (Model.Album.InfosInAlbums.Exists(f))
                    {
                        FiltersColorSelector.SelectedItem = inf;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Output());
                }
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
                    Func<InfosInAlbums, bool> f = new Func<InfosInAlbums, bool>(x => x.InfoId == inf.InfoId);
                    if(Model.Album.InfosInAlbums.Exists(f))
                    {
                        FiltersQualitySelector.SelectedItem = inf;
                    }
                }
                catch(Exception ex)
                {
                    log.Error(ex.Output());
                }
            }
        }

        #endregion
    }
}
