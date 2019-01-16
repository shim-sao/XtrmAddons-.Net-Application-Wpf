using Fotootof.Layouts.Dialogs;
using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Forms.Section
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form Section.</para>
    /// <para>The window provides the tools to create and edit a Section.</para>
    /// </summary>
    public partial class WindowFormSectionLayout : WindowLayoutForm, IValidator
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the Window model.
        /// </summary>
        public new WindowFormSectionModel Model
        {
            get => (WindowFormSectionModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property old Section informations backup.
        /// </summary>
        public SectionEntity OldForm
        {
            get => Model?.NewFormData;
            set => Model.NewFormData = value;
        }

        /// <summary>
        /// Property current or new Section informations.
        /// </summary>
        public SectionEntity NewForm
        {
            get => Model?.NewFormData;
            set => Model.NewFormData = value;
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
        /// Class XtrmAddons Fotootof Layouts Windows Form Section Constructor.
        /// </summary>
        /// <param name="sectionId">The Section primary key to edit.</param>
        public WindowFormSectionLayout(int sectionId)
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel(sectionId);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Windows Form Section Constructor.
        /// </summary>
        /// <param name="entity">A Section entity to edit.</param>
        public WindowFormSectionLayout(SectionEntity entity = default(SectionEntity)) : this(entity?.PrimaryKey ?? 0) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="Window"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Routed event atguments <see cref="RoutedEventArgs"/>.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add model to data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize the data model <see cref="WindowFormSectionModel"/> of the Window Form User.
        /// </summary>u
        /// <param name="entityId">A <see cref="SectionEntity"/> primary key or id to edit or a default entity is created if no argument is specified.</param>
        protected void InitializeModel(int entityId = 0)
        {
            Model = new WindowFormSectionModel(this, entityId);
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            TextBox name = FindName("TextBoxInputNameName") as TextBox;

            // Check if the name is not empty.
            if (!IsValidInput(name))
            {
                log.Debug($"The input Name is invalid : {name.Text}");
                return false;
            }

            log.Info("All inputs have been verified !");
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
            catch (Exception e)
            {
                log.Error(e.Output(), e);
                throw;
            }
        }

        #endregion



        #region Methods Collection Albums

        /// <summary>
        /// Method called on Album <see cref="CheckBox"/> check click event.
        /// Its add Album dependency to the Section.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.NewFormData.AlbumsPKeys.AddIfNotExists(GetTag<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        /// <summary>
        /// Method called on Album <see cref="CheckBox"/> uncheck click event.
        /// Its remove Album dependency from the Section.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.NewFormData.AlbumsPKeys.Remove(GetTag<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        #endregion



        #region Methods Collection AclGroups

        /// <summary>
        /// Method called on AclGroup <see cref="CheckBox"/> check click event.
        /// Its add AclGroup dependency to the Section.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.NewFormData.AclGroupsPKeys.AddIfNotExists(GetTag<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        /// <summary>
        /// Method called on AclGroup <see cref="CheckBox"/> uncheck click event.
        /// Its remove AclGroup dependency from the Section.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/>.</param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.NewFormData.AclGroupsPKeys.Remove(GetTag<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBoxs.Error(ex);
            }
        }

        #endregion
    }
}