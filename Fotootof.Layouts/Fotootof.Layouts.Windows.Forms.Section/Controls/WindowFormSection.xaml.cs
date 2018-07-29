using Fotootof.Layouts.Windows.Forms.Section.Models;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Collections.Entities;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Windows.Forms.Section.Controls
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form Section.</para>
    /// <para>The window provides the tools to create and edit a Section.</para>
    /// </summary>
    public partial class WindowFormSectionControl : WindowBaseForm, IWindowForm<SectionEntity>
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
        public new WindowFormSectionModel Model
        {
            get => (WindowFormSectionModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property old Section informations backup.
        /// </summary>
        public SectionEntity OldForm { get; set; }

        /// <summary>
        /// Property current or new Section informations.
        /// </summary>
        public SectionEntity NewForm
        {
            get => Model?.Section;
            set => Model.Section = value;
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
        /// <param name="SectionId">The Section primary key to edit.</param>
        public WindowFormSectionControl(int SectionId)
        {
            // Initialize window component.
            InitializeComponent();

            // Load entity with all required dependencies.
            var entity = default(SectionEntity);
            if (SectionId > 0)
            {
                var op = new SectionOptionsSelect
                {
                    PrimaryKey = SectionId,
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                entity = Db.Sections.SingleOrDefault(op);
            }

            // Initialize window data model.
            InitializeModel(entity);
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Windows Form Section Constructor.
        /// </summary>
        /// <param name="entity">A Section entity to edit.</param>
        public WindowFormSectionControl(SectionEntity entity = default(SectionEntity)) : this(entity?.PrimaryKey ?? 0) { }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add model to data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        protected void InitializeModel(SectionEntity entity = default(SectionEntity))
        {
            // 1 - Initialize view model.
            Model = new WindowFormSectionModel(this);

            // 2 - Make sure entity is not null.
            entity = entity ?? new SectionEntity();

            // 3 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 4 - Assign entity to the model.
            NewForm = entity;

            // 5 - Set model entity to dependencies converters.
            IsAclGroupInSection.Entity = Model.Section;
            IsAlbumInSection.Entity = Model.Section;

            // 6.1 - Assign list of AclGroup to the model for dependencies.
            // 6.2 - Assign list of Album to the model for dependencies.
            Model.AclGroups = new AclGroupEntityCollection(true);
            Model.Albums = new AlbumEntityCollection(true);
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            // Check if the name is not empty.
            if (!IsValidInput(InputName))
            {
                log.Warn($"The input name is invalid : {InputName}");
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
            
            //log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            //// Check if the name is not empty.
            //if (NewForm.Name.IsNullOrWhiteSpace())
            //{
            //    log.Warn($"The entity form name is invalid : {NewForm.Name}");
            //    return false;
            //}

            //log.Info("The entity form has been verified !");
            //return true;
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
                Model.Section.AlbumsPKeys.AddIfNotExists(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex);
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
                Model.Section.AlbumsPKeys.Remove(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex);
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
                Model.Section.AclGroupsPKeys.AddIfNotExists(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex);
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
                Model.Section.AclGroupsPKeys.Remove(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex.Output(), ex);
                MessageBase.Error(ex);
            }
        }

        #endregion
    }
}