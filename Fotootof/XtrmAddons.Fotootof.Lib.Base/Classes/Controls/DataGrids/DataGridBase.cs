using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Event;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.DataGrids
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Controls DataGrids.
    /// </summary>
    /// <typeparam name="T">The collection items type.</typeparam>
    /// <typeparam name="U">The item type.</typeparam>
    public abstract class DataGridBase<T, U> : ControlBaseCollection, IListItems<T>
    {
        #region Events

        /// <summary>
        /// Delegate property event changes handler for an Section.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> DefaultChanged = delegate { };

        /// <summary>
        /// Delegate property event datagrid selection changed.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add => ItemsDataGrid.SelectionChanged += value;
            remove => ItemsDataGrid.SelectionChanged -= value;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public T Items { get => (T)GetValue(PropertyItems); set => SetValue(PropertyItems, value); }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(T),
                typeof(DataGridBase<T, U>),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// Property to access to the main items data grid.
        /// </summary>
        public virtual DataGrid ItemsDataGrid { get; set; }

        /// <summary>
        /// Property to access to the datagrid selected item.
        /// </summary>
        public U SelectedItem => (U)ItemsDataGrid.SelectedItem;

        /// <summary>
        /// Property to access to the datagrid selected items.
        /// </summary>
        public List<U> SelectedItems => new List<U>(ItemsDataGrid.SelectedItems.Cast<U>());

        #endregion
        


        #region Methods

        /// <summary>
        /// Method to raise on default change event.
        /// </summary>
        protected void RaiseDefaultChanged<V>(V item) where V : U
        {
            DefaultChanged?.Invoke(this, new EntityChangesEventArgs(item));
        }

        /// <summary>
        /// Method called on click event to add a new Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        /*public void OnAdd_Click<W>(object sender, RoutedEventArgs e) where W : class
        {
            // Show open file dialog box 
            W dlg = (W)Activator.CreateInstance(typeof(W));
            bool? result = ((WindowBaseForm<U>)dlg).ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                RaiseOnAdd(dlg.NewEntity);
            }
            else
            {
                RaiseOnCancel(dlg.NewEntity);
            }
        }*/

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        public override void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItems.Count == 0)
            {
                if (EditControl != null) EditControl.IsEnabled = false;
                if (DeleteControl != null) DeleteControl.IsEnabled = false;
            }
            else
            {
                if (DeleteControl != null) DeleteControl.IsEnabled = true;

                if (SelectedItems.Count > 1)
                {
                    if (EditControl != null) EditControl.IsEnabled = false;
                }
                else
                {
                    if (EditControl != null) EditControl.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Method called on section default checkbox changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void CheckBoxDefault_Checked<V>(object sender, RoutedEventArgs e) where V : U
        {
            V item = (V)((CheckBox)sender).Tag;
            RaiseDefaultChanged(item);
        }

        /// <summary>
        /// <para>Method to select item by an extra checkbox field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void CheckBox_Checked<V>(object sender, RoutedEventArgs e) where V : U
        {
            V item = (V)((CheckBox)sender).Tag;

            if (!SelectedItems.Contains(item))
            {
                SelectedItems.Add(item);
            }
        }

        /// <summary>
        /// <para>Method to remove item by an extra checkbox field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void CheckBox_UnChecked<V>(object sender, RoutedEventArgs e) where V : U
        {
            V item = (V)((CheckBox)sender).Tag;

            if (SelectedItems.Contains(item))
            {
                SelectedItems.Remove(item);
            }
        }

        #endregion
    }
}
