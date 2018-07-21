using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.AppSystems;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Manager.Base;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Layouts.Windows.Forms.AclGroupForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users AclGroup Window.
    /// </summary>
    public partial class WindowFormAclGroup : WindowBaseForm, IWindowForm<AclGroupEntity>
    {
        #region Variable

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
        public new WindowFormAclGroupModel Model
        {
            get => (WindowFormAclGroupModel)model;
            protected set { model = value; }
        }

        /// <summary>
        /// Property to access to the old AclGroup backuped informations.
        /// </summary>
        public AclGroupEntity OldForm { get; set; }

        /// <summary>
        /// Property to access to the new AclGroup informations.
        /// </summary>
        public AclGroupEntity NewForm { get => Model.AclGroup; set => Model.AclGroup = value; }

        /// <summary>
        /// Property to access to the main form save button.
        /// </summary>
        public Button ButtonSave => (Button)FindName("Button_Save") ?? new Button();

        /// <summary>
        /// Property to access to the main form cancel button.
        /// </summary>
        public Button ButtonCancel => (Button)FindName("Button_Cancel") ?? new Button();

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageBase"></param>
        /// <param name="group"></param>
        public WindowFormAclGroup(int AclGroupId)
        {
            // Initialize window component.
            InitializeComponent();

            // Load entity with all required dependencies.
            var entity = default(AclGroupEntity);
            if (AclGroupId > 0)
            {
                var op = new AclGroupOptionsSelect
                {
                    PrimaryKey = AclGroupId,
                    Dependencies = { EnumEntitiesDependencies.All }
                };

                entity = Db.AclGroups.SingleOrDefault(op);
            }

            // Initialize window data model.
            InitializeModel(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageBase"></param>
        /// <param name="group"></param>
        public WindowFormAclGroup(AclGroupEntity entity = default(AclGroupEntity)) : this(entity?.PrimaryKey ?? 0) { }

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
        /// Method to initialize the Model on Windows construct.
        /// </summary>
        /// <param name="entity">A AclGroup entity.</param>
        protected void InitializeModel(AclGroupEntity entity = default(AclGroupEntity))
        {
            // 1 - Initialize view model.
            Model = new WindowFormAclGroupModel(this);

            // 2 - Make sure entity is not null.
            entity = entity ?? new AclGroupEntity();

            // 3 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 4 - Assign entity to the model.
            NewForm = entity;

            // 5 - Set model entity to dependencies converters.
            IsUserInAclGroup.Entity = NewForm;

            var action = Model.AclAction_FindAction("section.add");
            CheckBoxSectionAdd.Tag = action;
            CheckBoxSectionAdd.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = Model.AclAction_FindAction("section.edit");
            CheckBoxSectionEdit.Tag = action;
            CheckBoxSectionEdit.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = Model.AclAction_FindAction("section.delete");
            CheckBoxSectionDelete.Tag = action;
            CheckBoxSectionDelete.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = Model.AclAction_FindAction("section.view");
            CheckBoxSectionView.Tag = action;
            CheckBoxSectionView.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);
        }

        #endregion



        #region Methods Validate Inputs

        /// <summary>
        /// Method to validate the Inputs.
        /// </summary>
        protected override bool IsValidInputs()
        {
            // Check if the name is not empty.
            log.Debug("Checking if the name is not empty...");
            if (!IsValidInput(InputName))
            {
                return false;
            }

            log.Debug("All inputs have been verified !");
            return IsSaveEnabled = base.IsValidInputs();
        }

        #endregion



        #region Methods Validate Form

        /// <summary>
        /// Method to validate the Form Data.
        /// </summary>
        protected override bool IsValidForm()
        {
            if (NewForm.Name.IsNullOrWhiteSpace())
            {
                return false;
            }

            return true;
        }

        #endregion



        #region Methods Collection Action

        /// <summary>
        /// Method called to uncheck AclAction on the AclActions list of the AclGroup.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAclAction_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                NewForm.AclActionsPKeys.Add(Tag2Object<AclActionEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAclAction_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                NewForm.AclActionsPKeys.Remove(Tag2Object<AclActionEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Error(ex);
            }
        }

        #endregion



        #region Methods Collection User

        /// <summary>
        /// Method called to uncheck AclAction on the AclActions list of the AclGroup.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxUser_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                NewForm.UsersPKeys.AddIfNotExists(Tag2Object<UserEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxUser_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                NewForm.UsersPKeys.Remove(Tag2Object<UserEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBase.Error(ex);
            }
        }

        #endregion





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputText_Changed(object sender, TextChangedEventArgs e)
        {
            OnInputChanged(sender, e);
        }

        /// <summary>
        /// Method called on input text changes.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The text changed event arguments.</param>
        private void OnInputChanged<T>(object sender, T e) where T : class
        {
            if (NewForm.Name != null && InputAlias.Text.Sanitize().RemoveDiacritics().ToLower() == NewForm.Name.Sanitize().RemoveDiacritics().ToLower())
            {
                InputAlias.Text = "";
            }

            if (InputName.Text != "")
            {
                NewForm.Name = InputName.Text;
            }

            if (InputAlias.Text != "")
            {
                NewForm.Alias = InputAlias.Text.Sanitize().RemoveDiacritics().ToLower();
            }
            else if (InputName.Text != "")
            {
                NewForm.Alias = InputName.Text.Sanitize().RemoveDiacritics().ToLower();
            }

            InputAlias.Text = NewForm.Alias;

            Button_Save.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        //protected override void DialogSave_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogResult = true;
        //}

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        //protected override void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    if(DialogResult != true)
        //    {
        //        // Alert user for acceptation.
        //        MessageBoxResult result = MessageBox.Show
        //        (
        //            "Are you sure to cancel operation ?\n All properties changes will be lost.",
        //            Translation.DWords.ApplicationName,
        //            MessageBoxButton.YesNo,
        //            MessageBoxImage.Warning
        //        );

        //        // If accepted, try to cancel operation.
        //        if (result == MessageBoxResult.No)
        //        {
        //            // If user doesn't want to close, cancel closure
        //            e.Cancel = true;
        //        }
        //    }

        //    else
        //    {
        //        // Alert user for acceptation.
        //        MessageBoxResult result = MessageBox.Show
        //        (
        //            "Are you sure to save changes ?",
        //            Translation.DWords.ApplicationName,
        //            MessageBoxButton.YesNoCancel,
        //            MessageBoxImage.Question
        //        );

        //        // If user doesn't want to close, cancel closure
        //        if (result == MessageBoxResult.Cancel)
        //        {
        //            // If user doesn't want to close, cancel closure
        //            e.Cancel = true;
        //            return;
        //        }

        //        // If accepted, try to save operation.
        //        if (result == MessageBoxResult.No)
        //        {
        //            DialogResult = false;
        //        }
        //    }
        //}
    }
}