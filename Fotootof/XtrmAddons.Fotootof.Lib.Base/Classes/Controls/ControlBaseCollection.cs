using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Event;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Component Base Controls.
    /// </summary>
    public abstract class ControlBaseCollection : ControlBase, IControlCollection
    {
        #region Events

        /// <summary>
        /// Delegate property event add handler for an AclGroup.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnAdd = delegate { };

        /// <summary>
        /// Delegate property event changes handler for an AclGroup.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnChange = delegate { };

        /// <summary>
        /// Delegate property event cancel handler for an AclGroup.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnCancel = delegate { };

        /// <summary>
        /// Delegate property event delete handler for AclGroup.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> OnDelete = delegate { };
        
        #endregion
        


        #region Properties
        
        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public abstract Control AddControl { get; }

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public abstract Control EditControl { get; }

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public abstract Control DeleteControl { get; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Base Controls Constuctor.
        /// </summary>
        public ControlBaseCollection() : base() { }

        #endregion



        #region Methods

        /// <summary>
        /// Method to raise the on add event.
        /// </summary>
        protected void RaiseOnAdd<T>(T entity) where T : class
        {
            OnAdd?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method to raise the on changes event.
        /// </summary>
        protected void RaiseOnChange<T>(T entity, T old = null) where T : class
        {
            OnChange?.Invoke(this, new EntityChangesEventArgs(entity, old));
        }

        /// <summary>
        /// Method to raise the on cancel event.
        /// </summary>
        protected void RaiseOnCancel<T>(T entity) where T : class
        {
            OnCancel?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method to raise the on delete event.
        /// </summary>
        protected void RaiseOnDelete<T>(T entity) where T : class
        {
            OnDelete?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method called on add new item click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void OnAddNewItem_Click(object sender, RoutedEventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Method called on edit item click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void OnEditItem_Click(object sender, RoutedEventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Method called on delete items click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void OnDeleteItems_Click(object sender, RoutedEventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        public virtual void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        #endregion
    }
}
