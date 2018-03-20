using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Entity;
using XtrmAddons.Fotootof.Lib.SQLite.Event;
using XtrmAddons.Fotootof.Server.Libraries.Base.Controls;
using XtrmAddons.Fotootof.Server.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Server.Libraries.Common.Windows.Forms;
using XtrmAddons.Fotootof.Server.Libraries.Base.Logs;
using System.Collections.Generic;

namespace XtrmAddons.Fotootof.Server.Libraries.Common.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Common Control DataGrid Sections List.
    /// </summary>
    public partial class UCSectionsDataGrid : UCControlBaseCollection
    {
        #region Properties

        public override Control AddControl => Button_Add;

        public override Control EditControl => Button_Edit;

        public override Control DeleteControl => Button_Delete;

        /// <summary>
        /// Property to access to the entities collection.
        /// </summary>
        public SectionEntityCollection Sections
        {
            get { return (SectionEntityCollection)GetValue(SectionsProperty); }
            set { SetValue(SectionsProperty, value); }
        }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty SectionsProperty =
            DependencyProperty.Register
            (
                "Sections",
                typeof(SectionEntityCollection) ,
                typeof(UCSectionsDataGrid) ,
                new PropertyMetadata(new SectionEntityCollection(false))
            );

        /// <summary>
        /// Delegate property event datagrid selection changed.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add => ItemsCollection.SelectionChanged += value;
            remove => ItemsCollection.SelectionChanged -= value;
        }

        /// <summary>
        /// Delegate property event changes handler for an Section.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnDefaultChange = delegate { };

        /// <summary>
        /// 
        /// </summary>
        public Section SelectedItem => (Section)ItemsCollection.SelectedItem;

        /// <summary>
        /// 
        /// </summary>
        public List<Section> SelectedItems => new List<Section>(ItemsCollection.SelectedItems.Cast<Section>());

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Common Control DataGrid Sections List Constructor.
        /// </summary>
        public UCSectionsDataGrid()
        {
            InitializeComponent();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to raise on default change event.
        /// </summary>
        protected void RaiseOnDefaultChange(Section entity)
        {
            OnDefaultChange?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method called on click event to add a new Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Show open file dialog box 
            WindowSectionForm dlg = new WindowSectionForm(new Section());
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
        /// Method called on edit click to navigate to a Section edit window.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if (SelectedItem != null)
            {
                // Show open file dialog box 
                WindowSectionForm dlg = new WindowSectionForm(SelectedItem);
                bool? result = dlg.ShowDialog();

                // Process open file dialog box results 
                if (result == true)
                {
                    RaiseOnChange(dlg.NewForm);
                }
                else
                {
                    RaiseOnCancel(dlg.NewForm);
                }
            }
            else
            {
                UILogs.Warning(string.Format("{0} not found !", nameof(Section)), true, true);
            }
        }

        /// <summary>
        /// Method called on delete click to delete a Section.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public override void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Check if an AclGroup is founded. 
            if(SelectedItem != null)
            {
                // Alert user for acceptation.
                MessageBoxResult result = MessageBox.Show
                (
                    String.Format(Properties.Resources.MessageBox_Acceptation_DeleteGeneric, Properties.Resources.Section, SelectedItem.Name),
                    Properties.Resources.ApplicationName ,
                    MessageBoxButton.YesNoCancel
                );

                // If accepted, try to update page model collection.
                if(result == MessageBoxResult.Yes)
                {
                    RaiseOnDelete(SelectedItem);
                }
            }
            else
            {
                UILogs.Warning(string.Format("{0} not found !", nameof(Section)), true, true);
            }
        }

        /// <summary>
        /// Method called on section default checkbox changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxDefault_Checked(object sender, RoutedEventArgs e)
        {
            Section entity = (Section)((CheckBox)sender).Tag;
            RaiseOnDefaultChange(entity);
        }

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        private void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedItems.Count == 0)
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
