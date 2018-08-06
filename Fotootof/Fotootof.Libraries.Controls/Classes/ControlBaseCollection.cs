using Fotootof.Layouts.Interfaces;
using Fotootof.Libraries.System.Messages;
using Fotootof.SQLite.EntityManager.Event;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Libraries.Controls
{
    /// <summary>
    /// Class Fotootof Libraries Control Collection Base.
    /// </summary>
    public abstract class ControlBaseCollection : ControlBase, ILayoutCollection
    {
        #region Events

        /// <summary>
        /// Delegate property added event handler.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> Added = delegate { };

        /// <summary>
        /// Delegate property changed event handler.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> Changed = delegate { };

        /// <summary>
        /// Delegate property canceled event handler.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> Canceled = delegate { };

        /// <summary>
        /// Delegate property deleted event handler.
        /// </summary>
        public event EventHandler<EntityChangesEventArgs> Deleted = delegate { };
        
        #endregion
        


        #region Properties
        
        /// <summary>
        /// Property to access to the main add to collection control.
        /// </summary>
        public virtual Control AddCtrl
            => FindName("ButtonAddName") as Button;

        /// <summary>
        /// Property to access to the main edit item control.
        /// </summary>
        public virtual Control EditCtrl
            => FindName("ButtonEditName") as Button;

        /// <summary>
        /// Property to access to the main delete items control.
        /// </summary>
        public virtual Control DeleteCtrl
            => FindName("ButtonDeleteName") as Button;

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Component Base Controls Constuctor.
        /// </summary>
        public ControlBaseCollection() : base() { }

        #endregion



        #region Methods Event Handler

        /// <summary>
        /// Method to raise the added item event.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="object"/></typeparam>
        /// <param name="entity">The new entity to paste into the event <see cref="EntityChangesEventArgs"/></param>
        protected void NotifyAdded<T>(T entity) where T : class
        {
            Added?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method to raise the changed item event.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="object"/></typeparam>
        /// <param name="newEntity">The new entity to paste into the event <see cref="EntityChangesEventArgs"/></param>
        /// <param name="oldEntity">The old entity to paste into the event <see cref="EntityChangesEventArgs"/></param>
        protected void NotifyChanged<T>(T newEntity, T oldEntity = null) where T : class
        {
            Changed?.Invoke(this, new EntityChangesEventArgs(newEntity, oldEntity));
        }

        /// <summary>
        /// Method to raise the canceled event.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="object"/></typeparam>
        /// <param name="entity">The entity to paste into the event <see cref="EntityChangesEventArgs"/></param>
        protected void NotifyCanceled<T>(T entity) where T : class
        {
            Canceled?.Invoke(this, new EntityChangesEventArgs(entity));
        }

        /// <summary>
        /// Method to raise the deleted item event.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="object"/></typeparam>
        /// <param name="entity">The old entity to paste into the event <see cref="EntityChangesEventArgs"/></param>
        protected void NotifyDeleted<T>(T entity) where T : class
        {
            Deleted?.Invoke(this, new EntityChangesEventArgs(null, entity));
        }

        /// <summary>
        /// Method to raise the deleted items event.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="object"/></typeparam>
        /// <param name="entities">The old entities to paste into the event <see cref="EntityChangesEventArgs"/></param>
        protected void NotifyDeleted<T>(T[] entities) where T : class
        {
            Deleted?.Invoke(this, new EntityChangesEventArgs(null, entities));
        }

        #endregion



        #region Methods Click Handler

        /// <summary>
        /// Method called on add new item click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented(Local.Properties.Exceptions.NotImplementedAddItemClick);
        }

        /// <summary>
        /// Method called on edit item click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void EditItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented(Local.Properties.Exceptions.NotImplementedEditItemClick);
        }

        /// <summary>
        /// Method called on delete items click event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        public virtual void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBase.NotImplemented(Local.Properties.Exceptions.NotImplementedDeleteItemClick);
        }

        /// <summary>
        /// Method called on items collection selection changed.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Selection changed arguments.</param>
        public virtual void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBase.NotImplemented(Local.Properties.Exceptions.NotImplementedItemsSelectionChangedClick);
        }

        #endregion
    }
}
