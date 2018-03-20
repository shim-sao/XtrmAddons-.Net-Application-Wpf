using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Converters;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AclGroupForm
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Users AclGroup Window.
    /// </summary>
    public partial class WindowFormAclGroup : WindowBaseForm, IWindowForm<AclGroupEntity>
    {
        #region Properties

        /// <summary>
        /// Accessors to Window AclGroup Form model.
        /// </summary>
        public WindowFormAclGroupModel<WindowFormAclGroup> Model { get; private set; }

        /// <summary>
        /// Property to access to the old AclGroup backuped informations.
        /// </summary>
        public AclGroupEntity OldForm { get; set; }

        /// <summary>
        /// Property to access to the new AclGroup informations.
        /// </summary>
        public AclGroupEntity NewForm { get => Model.AclGroup; set => Model.AclGroup = value; }

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageBase"></param>
        /// <param name="group"></param>
        public WindowFormAclGroup(AclGroupEntity entity, PageBase pageBase = null)
        {
            // Initialize window component.
            InitializeComponent();
            Model = new WindowFormAclGroupModel<WindowFormAclGroup>(this);
            Loaded += (s, e) => Window_LoadAsync(entity);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        private async void Window_LoadAsync(AclGroupEntity entity)
        {
            // Assign closing event.
            Closing += Window_Closing;

            entity = entity ?? new AclGroupEntity();

            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // Store data in new entity.
            OldForm = entity.Clone();

            // Assign Parent Page & entity to the model.
            NewForm = entity;

            // Assign model to data context for display.
            DataContext = Model;

            Model.AclActions = await MainWindow.Database.AclActions.ListAsync();

            var action = await Model.AclAction_FindActionAsync("section.add");
            CheckBoxSectionAdd.Tag = action;
            CheckBoxSectionAdd.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = await Model.AclAction_FindActionAsync("section.edit");
            CheckBoxSectionEdit.Tag = action;
            CheckBoxSectionEdit.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = await Model.AclAction_FindActionAsync("section.delete");
            CheckBoxSectionDelete.Tag = action;
            CheckBoxSectionDelete.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            action = await Model.AclAction_FindActionAsync("section.view");
            CheckBoxSectionView.Tag = action;
            CheckBoxSectionView.IsChecked = Model.AclGroup_CanDoAclAction(action.AclActionId);

            CheckBoxUserInAclGroup.User = null;
            CheckBoxUserInAclGroup.AclGroup = Model.AclGroup;
            Model.Users = new UserEntityCollection(true);

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
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

            ButtonSave.IsEnabled = IsSaveEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The text changed event arguments.</param>
        protected override bool ValidateForm()
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

            ButtonSave.IsEnabled = IsSaveEnabled = save;

            return save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DialogSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Method called on windows closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Window_Closing(object sender, CancelEventArgs e)
        {
            if(DialogResult != true)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    "Are you sure to cancel operation ?\n All properties changes will be lost.",
                    Translation.DWords.ApplicationName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                // If accepted, try to cancel operation.
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }

            else
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    "Are you sure to save changes ?",
                    Translation.DWords.ApplicationName,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                // If user doesn't want to close, cancel closure
                if (result == MessageBoxResult.Cancel)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                    return;
                }

                // If accepted, try to save operation.
                if (result == MessageBoxResult.No)
                {
                    DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Method called on Section check box click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxSection_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            AclActionEntity action = (AclActionEntity)cb.Tag;

            if (cb.IsChecked == true)
            {
                NewForm.LinkAclAction(action.PrimaryKey);
            }
            else
            {
                NewForm.UnLinkAclAction(action.PrimaryKey);
            }

            ValidateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxUser_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            UserEntity entity = (UserEntity)cb.Tag;

            if (cb.IsChecked == true)
            {
                NewForm.LinkUser(entity.PrimaryKey);
            }
            else
            {
                NewForm.UnLinkUser(entity.PrimaryKey);
            }

            ValidateForm();
        }
    }
}