using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.UserForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server UI Control Data Grid User List.
    /// </summary>
    public partial class UCUsersDataGrid : ControlBaseCollection
    {
        #region Properties

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        /// <summary>
        /// Property to access to the Users entities collection.
        /// </summary>
        public UserEntityCollection Users
        {
            get { return (UserEntityCollection)GetValue(UsersProperty); }
            set { SetValue(UsersProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for User Entities.
        /// </summary>
        public static readonly DependencyProperty UsersProperty =
            DependencyProperty.Register
            (
                    "Users",
                    typeof(UserEntityCollection) ,
                    typeof(UCUsersDataGrid) ,
                    new PropertyMetadata(new UserEntityCollection(false))
            );

        /// <summary>
        /// Delegate property event changes handler for an Section.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnDefaultChange = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public UserEntity SelectedItem => (UserEntity)ItemsCollection.SelectedItem;

        /// <summary>
        /// 
        /// </summary>
        public List<UserEntity> SelectedItems => new List<UserEntity>(ItemsCollection.SelectedItems.Cast<UserEntity>());

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server UI Control DataGrid AclGroups List Constructor.
        /// </summary>
        public UCUsersDataGrid()
        {
            InitializeComponent();
        }


        #endregion



        #region Methods

        /// <summary>
        /// Method called on add click event to add new User.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormUser dlg = new WindowFormUser(new UserEntity());
            bool? result = dlg.ShowDialog();
            
            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewEntity);
            }
            else
            {
                RaiseOnCancel(dlg.NewEntity);
            }
        }

        /// <summary>
        /// Method called on delete click to delete an User.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an User is founded. 
            if (SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.User, SelectedItem.Name),
                    Translation.DWords.ApplicationName,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model User collection.
                if (result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else
            {
                AppLogger.Warning("User not found !", true);

            }
        }

        /// <summary>
        /// Method called on edit click event to navigate to an User edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormUser dlg = new WindowFormUser(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewEntity, dlg.OldEntity);
                }
                else
                {
                    RaiseOnCancel(dlg.NewEntity);
                }
            }
            else
            {
                AppLogger.Warning("User not found !", true);
            }
        }

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SelectedItems.Count == 0)
            {
                Button_Edit.IsEnabled = false;
                Button_Delete.IsEnabled = false;
            }
            else
            {
                Button_Delete.IsEnabled = true;

                if (SelectedItems.Count > 1)
                {
                    Button_Edit.IsEnabled = false;
                }
                else
                {
                    Button_Edit.IsEnabled = true;
                }
            }
        }

        #endregion
    }
}
