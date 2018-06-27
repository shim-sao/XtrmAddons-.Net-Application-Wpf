using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Event;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls.ListViews
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Controls ListViews.
    /// </summary>
    /// <typeparam name="T">The collection items type.</typeparam>
    /// <typeparam name="U">The item type.</typeparam>
    public abstract class ListViewBase<T, U> : ControlBaseCollection, IListItems<T>
    {
        #region Properties Events Handlers

        /// <summary>
        /// Delegate property event changes handler for an Section.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> DefaultChanged = delegate { };

        /// <summary>
        /// Delegate property event datagrid selection changed.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add => ItemsCollection.SelectionChanged += value;
            remove => ItemsCollection.SelectionChanged -= value;
        }

        #endregion



        #region Properties Controls

        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public override Control AddControl => (Control)FindName("Button_Add");

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditControl => (Control)FindName("Button_Edit");

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteControl => (Control)FindName("Button_Delete");

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public virtual T Items { get => (T)GetValue(PropertyItems); set => SetValue(PropertyItems, value); }

        /// <summary>
        /// Property Using a DependencyProperty as the backing store for Entities.
        /// </summary>
        public static readonly DependencyProperty PropertyItems =
            DependencyProperty.Register
            (
                "Items",
                typeof(T),
                typeof(ListViewBase<T, U>),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// Property to access to the main items data grid.
        /// </summary>
        public virtual ListView ItemsCollection => (ListView)FindName("ListViewCollection");

        /// <summary>
        /// Property to access to the datagrid selected item.
        /// </summary>
        public U SelectedItem => (U)ItemsCollection.SelectedItem;

        /// <summary>
        /// Property to access to the datagrid selected items.
        /// </summary>
        public List<U> SelectedItems => new List<U>(ItemsCollection.SelectedItems.Cast<U>());

        #endregion



        #region Methods Events Notifier

        /// <summary>
        /// Method to raise on default change event.
        /// </summary>
        protected void NotifyDefaultChanged<V>(V item) where V : U
        {
            DefaultChanged?.Invoke(this, new EntityChangesEventArgs(item));
        }

        #endregion



        #region Methods

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
            V entity = (V)((CheckBox)sender).Tag;
            NotifyDefaultChanged(entity);
        }

        /// <summary>
        /// <para>Method to select item by an extra checkbox field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void CheckBoxSelectItem_Checked<V>(object sender, RoutedEventArgs e) where V : U
        {
            V entity = (V)((CheckBox)sender).Tag;

            if (!SelectedItems.Contains(entity))
            {
                SelectedItems.Add(entity);
            }
        }

        /// <summary>
        /// <para>Method to remove item by an extra checkbox field.</para>
        /// <para>Add it to the data grid selected items.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void CheckBoxSelectItem_UnChecked<V>(object sender, RoutedEventArgs e) where V : U
        {
            V entity = (V)((CheckBox)sender).Tag;

            if (SelectedItems.Contains(entity))
            {
                SelectedItems.Remove(entity);
            }
        }

        #endregion
    }
}