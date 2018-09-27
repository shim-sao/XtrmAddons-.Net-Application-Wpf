using Fotootof.Collections.Entities;
using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityConverters.ValueConverters;
using Fotootof.SQLite.EntityManager.Data.Tables.Dependencies;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Forms.Album
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Window Form Album.</para>
    /// <para>The window provides the tools to create and edit an Album.</para>
    /// </summary>
    public partial class WindowFormAlbumLayout : WindowLayoutForm, IValidator
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
        internal new WindowFormAlbumModel Model
        {
            get => (WindowFormAlbumModel)model;
            private set => model = value;
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
        public Button ButtonSave => FindName("ButtonSaveName") as Button;

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => FindName("ButtonCancelName") as Button;

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AlbumId"></param>
        public WindowFormAlbumLayout(int AlbumId)
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.

            var op = new AlbumOptionsSelect
            {
                PrimaryKey = AlbumId,
                Dependencies = { EnumEntitiesDependencies.All }
            };

            var entity = Db.Albums.SingleOrDefault(op);

            InitializeModel(entity);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Window Form Album Constructor.
        /// </summary>
        /// <param name="entity">An Album entity to edit or new Album entity to add.</param>
        public WindowFormAlbumLayout(AlbumEntity entity = default(AlbumEntity)) : base()
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
            Model = new WindowFormAlbumModel(this);

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
            if (!IsValidInput(FindName("InputName") as TextBox))
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
        protected override bool IsValidForm()
        {
            try
            {
                IsValidFormNotNullOrWhiteSpace(NewForm, "Name");

                return true;
            }
            catch (ArgumentNullException e)
            {
                log.Error(e);
                throw new ArgumentNullException(e.Message);
            }
        }

        #endregion



        #region Methods Pictures

        /// <summary>
        /// Method to change thumbnail of an album.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void OnAlbumPicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = XtrmAddons.Net.Picture.PictureFileDialogBox.Show();

            if (dlg != null && dlg.FileName != "")
            {
                Model.UpdateAlbumPicturePropertyAsync((string)((Button)sender).Tag, dlg.FileName);
            }
        }

        #endregion



        #region Methods Section

        /// <summary>
        /// Method called on section check box checked event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxSection_Checked(object sender, RoutedEventArgs e)
        {
            NewForm.SectionsPKs.AddIfNotExists(GetTag<SectionEntity>(sender).PrimaryKey);
        }

        /// <summary>
        /// Method called on section check box unchecked event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxSection_UnChecked(object sender, RoutedEventArgs e)
        {
            NewForm.SectionsPKs.Remove(GetTag<SectionEntity>(sender).PrimaryKey);
        }

        #endregion



        #region Methods Filters

        /// <summary>
        /// Method called on filters selection changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The selection changed event arguments</param>
        private void Filters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.IsEditable = false;

            if(e.RemovedItems.Count > 0)
            {
                foreach (InfoEntity inf in e.RemovedItems) 
                {
                    NewForm.InfosPKs.Remove(inf.PrimaryKey);
                }
            }

            // Get color filter.
            InfoEntity color = (InfoEntity)(FindName("FiltersColorSelector") as ComboBox).SelectedItem;
            if(color != null && color.PrimaryKey != 0)
            {
                NewForm.InfosPKs.Add(color.PrimaryKey);
            }

            // Get quality filter.
            InfoEntity quality = (InfoEntity)(FindName("FiltersQualitySelector") as ComboBox).SelectedItem;
            if (quality != null && quality.PrimaryKey != 0)
            {
                NewForm.InfosPKs.Add(quality.PrimaryKey);
            }
        }

        /// <summary>
        /// Method called on color filters selector loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void FiltersColorSelector_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = FindName("FiltersColorSelector") as ComboBox;

            foreach (InfoEntity inf in Model.FiltersColor)
            {
                try
                {
                    Func<InfosInAlbums, bool> f = new Func<InfosInAlbums, bool>(x => x.InfoId == inf.InfoId);
                    if (Model.Album.InfosInAlbums.Exists(f))
                    {
                        cb.SelectedItem = inf;
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
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void FiltersQualitySelector_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = FindName("FiltersQualitySelector") as ComboBox;

            foreach (InfoEntity inf in cb.Items)
            {
                try
                {
                    Func<InfosInAlbums, bool> f = new Func<InfosInAlbums, bool>(x => x.InfoId == inf.InfoId);
                    if(Model.Album.InfosInAlbums.Exists(f))
                    {
                        cb.SelectedItem = inf;
                    }
                }
                catch(Exception ex)
                {
                    log.Error(ex.Output());
                }
            }
        }

        #endregion



        #region IDisposable
        
        /// <summary>
        /// Variable to track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;
        
        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Track whether Dispose has been called.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }
        
                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
        
        
                // Note disposing has been done.
                disposed = true;
        
            }
        }
        
        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called.
        /// It gives your base class the opportunity to finalize.
        /// Do not provide destructors in types derived from this class.
        /// </summary>
        ~WindowFormAlbumLayout()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
        
        #endregion
    }
}
