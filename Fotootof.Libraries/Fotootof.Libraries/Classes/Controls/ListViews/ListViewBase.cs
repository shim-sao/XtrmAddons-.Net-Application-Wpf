using Fotootof.SQLite.EntityManager.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Libraries.Controls.ListViews
{
    /// <summary>
    /// Class Fotootof Libraries Control ListView Base.
    /// </summary>
    /// <typeparam name="T">The collection items type.</typeparam>
    /// <typeparam name="U">The item type.</typeparam>
    public abstract class ListViewBase<T, U> : ControlLayoutCollection
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
        public override Control AddCtrl => FindName("ButtonAddName") as Control;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public override Control EditCtrl => FindName("ButtonEditName") as Control;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public override Control DeleteCtrl => FindName("ButtonDeleteName") as Control;

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the items collection.
        /// </summary>
        public virtual T Items
        {
            get
            {
                Debug.WriteLine($"* Getter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {typeof(T)}");

                return (T)GetValue(PropertyItems);
            }
            set
            {
                Debug.WriteLineIf(value == null, $"+ Setter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => null");
                Debug.WriteLineIf(value != null, $"+ Setter : {GetType().Name}.{MethodBase.GetCurrentMethod().Name} => {(value as IEnumerable<U>).Count()}");

                SetValue(PropertyItems, value);
            }
        }

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
        public virtual ListView ItemsCollection => FindName("ListViewCollectionName") as ListView;

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
                if (EditCtrl != null) EditCtrl.IsEnabled = false;
                if (DeleteCtrl != null) DeleteCtrl.IsEnabled = false;
            }
            else
            {
                if (DeleteCtrl != null) DeleteCtrl.IsEnabled = true;

                if (SelectedItems.Count > 1)
                {
                    if (EditCtrl != null) EditCtrl.IsEnabled = false;
                }
                else
                {
                    if (EditCtrl != null) EditCtrl.IsEnabled = true;
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