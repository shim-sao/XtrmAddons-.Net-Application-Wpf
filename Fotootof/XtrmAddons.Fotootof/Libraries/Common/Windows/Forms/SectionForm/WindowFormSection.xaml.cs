using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Converters;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.SectionForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Windows Form Section.
    /// </summary>
    public partial class WindowFormSection : WindowBaseForm, IWindowForm<SectionEntity>
    {
        #region Variables

        /// <summary>
        /// Variable Window AclGroup Form model.
        /// </summary>
        private WindowFormSectionModel<WindowFormSection> model;

        /// <summary>
        /// Variable old Section informations backup.
        /// </summary>
        public SectionEntity OldForm { get; set; }

        /// <summary>
        /// Variable Section informations.
        /// </summary>
        public SectionEntity NewForm
        {
            get => model.Section;
            set => model.Section = value;
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Windows Form Section Constructor.
        /// </summary>
        /// <param name="entity"></param>
        public WindowFormSection(SectionEntity entity = null) : base()
        {
            InitializeComponent();
            model = new WindowFormSectionModel<WindowFormSection>(this);
            Loaded += (s, e) => Window_Load(entity);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        protected void Window_Load(SectionEntity entity)
        {
            // Assign closing event.
            Closing += Window_Closing;

            // Initialize User first.
            entity = entity ?? new SectionEntity();

            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // Store data in new entity.
            OldForm = entity.Clone();

            // Assign Parent Page & entity to the model.
            NewForm = entity;

            // Set converter main entity.
            IsAclGroupInSection.Entity = model.Section;
            IsAlbumInSection.Entity = model.Section;

            // Assign list of AclGroup to the model.
            model.AclGroups = new AclGroupEntityCollection(true);

            // Assign list of Album to the model.
            model.Albums = new AlbumEntityCollection(true);

            DataContext = model;
        }

        /// <summary>
        /// Method called on input name text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string old = NewForm.Name;

            if (!InputName.Text.IsNullOrWhiteSpace())
            {
                NewForm.Name = InputName.Text;
            }

            if (!old.IsNullOrWhiteSpace())
            {
                //var a = NewForm.Alias.Sanitize().RemoveDiacritics().ToLower();
                //var b = old.Sanitize().RemoveDiacritics().ToLower();

                if (NewForm.Alias.IsNullOrWhiteSpace() || NewForm.Alias.Sanitize().RemoveDiacritics().ToLower() == old.Sanitize().RemoveDiacritics().ToLower())
                {
                    InputAlias.Text = InputName.Text;
                }
            }

            ButtonSave.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// Method called on input alias text changes event.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="routedEventArgs">Text changed event arguments.</param>
        private void InputAlias_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputAlias.Text != "" && InputAlias.Text != InputAlias.Text.Sanitize().RemoveDiacritics().ToLower())
            {
                InputAlias.Text = InputAlias.Text.Sanitize().RemoveDiacritics().ToLower();
            }

            NewForm.Alias = InputAlias.Text;

            ButtonSave.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCollectionAlbum_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCollectionAclGroup_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The text changed event arguments.</param>
        protected override bool IsSaveEnabled
        {
            get
            {
                bool save = true;

                if (NewForm != null)
                {
                    if (NewForm.Name == null || NewForm.Name.Length == 0)
                    {
                        save = false;
                    }
                }
                else
                {
                    save = false;
                }

                return save;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            model.Section.LinkAclGroup(entity.PrimaryKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            model.Section.UnLinkAclGroup(entity.PrimaryKey);
        }

        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)((CheckBox)sender).Tag;
            model.Section.LinkAlbum(entity.PrimaryKey);
        }

        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            AlbumEntity entity = (AlbumEntity)((CheckBox)sender).Tag;
            model.Section.UnLinkAlbum(entity.PrimaryKey);
        }
    }
}