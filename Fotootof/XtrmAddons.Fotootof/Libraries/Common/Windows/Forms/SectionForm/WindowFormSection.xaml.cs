using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.SectionForm
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form Section.</para>
    /// <para>Provides form to add or edit a Section.</para>
    /// </summary>
    public partial class WindowFormSection : WindowBaseForm, IWindowForm<SectionEntity>
    {
        #region Variables

        /// <summary>
        /// Variable Window AclGroup Form model of the view.
        /// </summary>
        private WindowFormSectionModel<WindowFormSection> model;

        /// <summary>
        /// Variable preview TextBox Name text for Alias autofill compared.
        /// </summary>
        private string previewName = "";

        /// <summary>
        /// Variable current TextBox Name text for Alias autofill compared.
        /// </summary>
        private bool updateNameAlias = false;

        #endregion



        #region Properties

        /// <summary>
        /// Property old Section informations backup.
        /// </summary>
        public SectionEntity OldForm { get; set; }

        /// <summary>
        /// Property current or new Section informations.
        /// </summary>
        public SectionEntity NewForm
        {
            get => model?.Section;
            set => model.Section = value;
        }

        /// <summary>
        /// Property to define if the form can be saftly saved.
        /// </summary>
        protected override bool IsSaveEnabled
            => ValidateForm();

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Windows Form Section Constructor.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        public WindowFormSection(SectionEntity entity = default(SectionEntity)) : base()
        {
            InitializeComponent();
            InitializeModel(entity);
            //Loaded += Window_Load;
            //Closing += Window_Closing;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        protected void InitializeModel(SectionEntity entity = default(SectionEntity))
        {
            // 1 - Initialize view model.
            model = new WindowFormSectionModel<WindowFormSection>(this);

            // 2 - Make sure entity is not null.
            entity = entity ?? new SectionEntity();

            // 3 - Initialize new entity if required.
            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // 4 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 5 - Assign entity to the model.
            NewForm = entity;

            // 6 - Set model entity to dependencies converters.
            IsAclGroupInSection.Entity = model.Section;
            IsAlbumInSection.Entity = model.Section;

            // 7.1 - Assign list of AclGroup to the model.
            model.AclGroups = new AclGroupEntityCollection(true);

            // 7.2 - Assign list of Album to the model.
            model.Albums = new AlbumEntityCollection(true);
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            DataContext = model;
        }

        /// <summary>
        /// Method to validate the Form Data.
        /// </summary>
        private new bool ValidateForm()
        {
            if (NewForm == null)
            {
                return false;
            }

            if (NewForm.Name.IsNullOrWhiteSpace())
            {
                return false;
            }

            //NewForm.Alias = NewForm.Alias.Sanitize().RemoveDiacritics().ToLower();
            if (NewForm.Alias.IsNullOrWhiteSpace())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        private new bool ValidateInput()
        {
            if (NewForm == null)
            {
                return false;
            }

            if (InputName.Text.IsNullOrWhiteSpace())
            {
                return false;
            }

            string alias = InputAlias.Text.Sanitize().RemoveDiacritics().ToLower();
            if (alias.IsNullOrWhiteSpace())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// <para>Method to send validation error to a TextBox.</para>
        /// <para>Disable Form Save Button to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Validation error event argumments.</param>
        private void Input_Error(object sender, ValidationErrorEventArgs e)
        {
            Button_Save.IsEnabled = false;
        }

        /// <summary>
        /// <para>Method called on input updated event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments</param>
        private void Input_Updated(object sender, DataTransferEventArgs e)
        {
            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (InputAlias.Text.IsNullOrWhiteSpace() || InputAlias.Text == previewName)
            {
                updateNameAlias = true;
            }

            previewName = tb.Text.Sanitize().RemoveDiacritics().ToLower();
            Button_Save.IsEnabled = tb.Text.IsNullOrWhiteSpace() ? false : IsSaveEnabled;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputName_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.IsNotNullOrWhiteSpace() && InputAlias.Text.IsNullOrWhiteSpace() || updateNameAlias)
            {
                InputAlias.Text = tb.Text.Sanitize().RemoveDiacritics().ToLower();

                if (InputAlias.Text.IsNotNullOrWhiteSpace())
                {
                    InputAlias.ValidationClear();
                }

                if(!InputAlias.Focus())
                {
                    NewForm.Alias = InputAlias.Text;
                }
            }

            updateNameAlias = false;
            Button_Save.IsEnabled = tb.Text.IsNullOrWhiteSpace() ? false : IsSaveEnabled;
        }


        /// <summary>
        /// <para>Method called on input Password text changed event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Text changed event arguments.</param>
        private void InputAlias_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            //tb.Text = tb.Text.Sanitize().RemoveDiacritics().ToLower();
            updateNameAlias = false;
            Button_Save.IsEnabled = tb.Text.IsNullOrWhiteSpace() ? false : IsSaveEnabled;
        }

        /// <summary>
        /// <para>Method called on input updated event.</para>
        /// <para>Send Form validation to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Data transfer event arguments</param>
        private void InputAlias_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Sanitize().RemoveDiacritics().ToLower();
            updateNameAlias = false;
            Button_Save.IsEnabled = IsSaveEnabled;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void DataGridCollectionAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void DataGridCollectionAclGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.LinkAclGroup(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.UnLinkAclGroup(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.LinkAlbum(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.UnlinkAlbum(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        #endregion
    }
}