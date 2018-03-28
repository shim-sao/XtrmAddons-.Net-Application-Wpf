using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Culture;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.AclGroupForm;

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups.
    /// </summary>
    public partial class UCAclGroupDataGrid : ControlBaseCollection
    {
        #region Properties

        /// <summary>
        /// Property to access to the AclGroup entities collection.
        /// </summary>
        public AclGroupEntityCollection AclGroups
        {
            get { return (AclGroupEntityCollection)GetValue(AclGroupsProperty); }
            set { SetValue(AclGroupsProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for AclGroup Entities.
        /// </summary>
        public static readonly DependencyProperty AclGroupsProperty =
            DependencyProperty.Register
            (
                "AclGroups",
                typeof(AclGroupEntityCollection),
                typeof(UCAclGroupDataGrid),
                new PropertyMetadata(new AclGroupEntityCollection(false))
            );

        /// <summary>
        /// Delegate property event data grid selection changed.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add => ItemsCollection.SelectionChanged += value;
            remove => ItemsCollection.SelectionChanged -= value;
        }

        /// <summary>
        /// Delegate property event changes handler for an Section.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> DefaultChanged = delegate { };

        /// <summary>
        /// Property to access to the data grid selected item.
        /// </summary>
        public AclGroupEntity SelectedItem => (AclGroupEntity)ItemsCollection.SelectedItem;

        /// <summary>
        /// Property to access to the data grid selected items.
        /// </summary>
        public AclGroupEntityCollection SelectedItems => new AclGroupEntityCollection(ItemsCollection.SelectedItems.Cast<AclGroupEntity>());

        /// <summary>
        /// 
        /// </summary>
        public override Control AddControl => Button_Add;

        /// <summary>
        /// 
        /// </summary>
        public override Control EditControl => Button_Edit;

        /// <summary>
        /// 
        /// </summary>
        public override Control DeleteControl => Button_Delete;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Controls DataGrid AclGroups Constructor.
        /// </summary>
        public UCAclGroupDataGrid()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to raise on default change event.
        /// </summary>
        protected void RaiseDefaultChanged(AclGroupEntity entity)
        {
            DefaultChanged?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method called on add click event to add new AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowFormAclGroup dlg = new WindowFormAclGroup(new AclGroupEntity(), (PageBase)Tag);
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewForm);
            }
            else
            {
                RaiseOnCancel(dlg.NewForm);
            }
        }

        /// <summary>
        /// Method called on edit click event to navigate to an AclGroup edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowFormAclGroup dlg = new WindowFormAclGroup(SelectedItem, (PageBase)Tag);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(SelectedItem);
                }
                else
                {
                    RaiseOnCancel(SelectedItem);
                }
            }
            else
            {
                AppLogger.Warning("Group not found !", false);
            }
        }

        /// <summary>
        /// Method called on delete click to delete an AclGroup.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if(SelectedItem != null && SelectedItem.IsDefault == false )
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Translation.DWords.MessageBox_Acceptation_DeleteGeneric, Translation.DWords.Group, SelectedItem.Name),
                    Translation.DWords.ApplicationName ,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model AclGroup collection.
                if(result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else if(SelectedItem != null && SelectedItem.IsDefault == true)
            {
                AppLogger.Info("Default User Group cannot be delete !", true);
            }
            else 
            {
                AppLogger.Warning("User Group not found !", true);
            }
        }

        /// <summary>
        /// Method called on AclGroup default check box changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            AclGroupEntity entity = (AclGroupEntity)((CheckBox)sender).Tag;
            RaiseDefaultChanged(entity);
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

        public override void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
