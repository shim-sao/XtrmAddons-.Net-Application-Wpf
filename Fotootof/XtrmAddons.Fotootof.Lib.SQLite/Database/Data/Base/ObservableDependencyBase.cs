using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies.
    /// </summary>
    /// <typeparam name="T">The Type of the items of the dependency.</typeparam>
    /// <typeparam name="O">The Type of the entity item to observe.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    public class ObservableDependencyBase<T, O, E> : ObservableCollection<T> where O : class where E : class
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable list of dependency primary keys.
        /// </summary>
        private ObservableCollection<int> depPKs = new ObservableCollection<int>();

        /// <summary>
        /// Variable list of dependency primary keys removed.
        /// </summary>
        private ObservableCollection<int> depPKsRemoved = new ObservableCollection<int>();

        /// <summary>
        /// Variable list of dependency primary keys.
        /// </summary>
        private ObservableCollection<E> depRef = new ObservableCollection<E>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the dependency property Primary Key name.
        /// </summary>
        public string DepPKName { get; private set; }

        /// <summary>
        /// Property to access to a list of dependency items Primary Keys.
        /// </summary>
        public ObservableCollection<int> DepPKeys
        {
            get => depPKs;
            private set
            {
                if (value != depPKs)
                {
                    depPKs.ClearAndAdd(value);
                }
            }
        }

        /// <summary>
        /// Property to access to a list of dependency items Primary Keys removed.
        /// </summary>
        public ObservableCollection<int> DepPKeysRemoved
        {
            get => depPKsRemoved;
            private set
            {
                if (value != depPKsRemoved)
                {
                    depPKsRemoved.ClearAndAdd(value);
                }
            }
        }

        /// <summary>
        /// Property to access to the dependency References.
        /// </summary>
        public ObservableCollection<E> DepReferences
        {
            get => depRef;
            private set
            {
                log.Debug($"+ Setter {GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {value.Count}");
                if (value != depRef)
                {
                    log.Debug($"+ Setter {GetType().Name}.{MethodBase.GetCurrentMethod().Name} : ClearAndAdd");
                    depRef.ClearAndAdd(value);
                }
            }
        }

        /// <summary>
        /// Property to get or set to 
        /// </summary>
        [System.Obsolete("BlockReentrancy() is used.")]
        public bool LockChanges { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool LockDepReferencesChanges { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool LockDepPKeysChanges { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool IsPopulated { get; private set; } = true;

        #endregion



        #region Property Changed Event Handler

        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler DelegatePropertyChanged = delegate { };

        /// <summary>
        /// This method is called by the Set accessor of each property.
        /// The CallerMemberName attribute that is applied to the optional propertyName
        /// parameter causes the property name of the caller to be substituted as an argument.
        /// </summary>
        /// <param name="propertyName">The name of the property to raise notify changes event.</param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            DelegatePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        public ObservableDependencyBase()
        {
            InitializeCollection();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableDependencyBase(List<T> list) : base(list)
        {
            InitializeCollection();
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableDependencyBase(IEnumerable<T> collection) : base(collection)
        {
            InitializeCollection();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called by the constructor to initialize the collection.
        /// </summary>
        private void InitializeCollection()
        {
            DepPKName = typeof(E).Name.Replace("Entity", "Id");
            DepReferences.CollectionChanged += DepReferences_CollectionChanged;
            DepPKeys.CollectionChanged += DepPKeys_CollectionChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int GetPkValue(object item)
        {
            int pkValue = 0;

            if (item is int)
                pkValue = (int)item;
            else
                pkValue = (int)item.GetPropertyValue(DepPKName);
            return pkValue;
        }

        #endregion



        #region Methods Find

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public int FindIndexInDepPKeys(int primaryKey)
        {
            return depPKs.ToList().FindIndex(x => x == primaryKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool ExistsDepPKeys(int primaryKey)
        {
            return depPKs.ToList().FindIndex(x => x == primaryKey) > -1;
        }

        /// <summary>
        /// Method to find an element by a property value.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value to search.</param>
        /// <returns>The first founded element otherwise, default value of type T, or null if type T is nullable.</returns>
        public int FindIndexInDepPKeysRemoved(int primaryKey)
        {
            return depPKsRemoved.ToList().FindIndex(x => x == primaryKey);
        }

        /// <summary>
        /// Method to find an element by a property value.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value to search.</param>
        /// <returns>The first founded element otherwise, default value of type T, or null if type T is nullable.</returns>
        public int FindIndexInDepReferences(int primaryKey)
        {
            return depRef.ToList().FindIndex(x => (int)x.GetPropertyValue("PrimaryKey") == primaryKey);
        }

        /// <summary>
        /// Method to find an element by a property value.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value to search.</param>
        /// <returns>The first founded element otherwise, default value of type T, or null if type T is nullable.</returns>
        public E FindDepReferences(int primaryKey)
        {
            return depRef.ToList().Find(x => (int)x.GetPropertyValue("PrimaryKey") == primaryKey) as E;
        }

        #endregion



        #region Methods Events Handlers

        /// <summary>
        /// Method called on collection changed event.
        /// </summary>
        /// <param name="e">Notify collection changed event arguments.</param>
        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");
            

        //    // Check if the collection changes is not locked.
        //    // Check if we must populate data on construct.
        //    if (LockChanges == true || IsPopulated == false)
        //    {
        //        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => {LockChanges} || {IsPopulated}");
        //        LockChanges = false;
        //        return;
        //    }

        //    // Check if a dependency primary key name is valid.
        //    if (DepPKName.IsNullOrWhiteSpace())
        //    {
        //        DepPKName = typeof(E).Name.Replace("Entity", "Id");
        //        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKName => {DepPKName}");
        //    }

        //    LockDepPKeysChanges = true;
        //    LockDepReferencesChanges = true;

        //    // Switch for the action to do.
        //    switch (e.Action)
        //    {
        //        // Occurs on add new item into the collection.
        //        case NotifyCollectionChangedAction.Add:
        //            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Add");
        //            NotifyCollectionAdd_DepPKeys(this, e);
        //            NotifyCollectionAdd_DepReferences(this, e);
        //            break;

        //        case NotifyCollectionChangedAction.Move :
        //            break;

        //        case NotifyCollectionChangedAction.Remove:
        //            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Remove");
        //            NotifyCollectionRemove_DepPKeys(this, e);
        //            NotifyCollectionRemove_DepReferences(this, e);
        //            break;

        //        case NotifyCollectionChangedAction.Replace :
        //            break;

        //        case NotifyCollectionChangedAction.Reset :
        //            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Reset");
        //            NotifyCollectionReset_DepPKeys(this, e);
        //            DepReferences.Clear();
        //            break;
        //    }

        //    LockDepPKeysChanges = false;
        //    LockDepReferencesChanges = false;
        //}

        /// <summary>
        /// Method called on collection changed event.
        /// </summary>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");
            
            // Check if a dependency primary key name is valid.
            if (DepPKName.IsNullOrWhiteSpace())
            {
                DepPKName = typeof(E).Name.Replace("Entity", "Id");
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKName => {DepPKName}");
            }


            // Check if the collection changes is not locked.
            // Check if we must populate data on construct.
            using (var block = this.BlockReentrancy())
            {
                LockDepPKeysChanges = true;
                LockDepReferencesChanges = true;

                // Switch for the action to do.
                switch (e.Action)
                {
                    // Occurs on add new item into the collection.
                    case NotifyCollectionChangedAction.Add:
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Add Dependency");
                        NotifyCollectionAdd_DepPKeys(this, e);
                        NotifyCollectionAdd_DepReferences(this, e);
                        break;

                    case NotifyCollectionChangedAction.Move:
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Remove Dependency");
                        NotifyCollectionRemove_DepPKeys(this, e);
                        NotifyCollectionRemove_DepReferences(this, e);
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Reset Dependency");
                        NotifyCollectionReset_DepPKeys(this, e);
                        NotifyCollectionReset_DepReferences(this, e);
                        break;
                }

                LockDepPKeysChanges = false;
                LockDepReferencesChanges = false;
            }
        }

        /// <summary>
        /// Method called on collection dependency references changed event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        private void DepReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");

            if (LockDepReferencesChanges == true)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyDepReferencesChanges => {LockDepReferencesChanges}");
                LockDepReferencesChanges = false;
                return;
            }
            
            LockDepPKeysChanges = true;

            // Switch for the action to do.
            switch (e.Action)
            {
                // Occurs on add new item into the collection.
                case NotifyCollectionChangedAction.Add:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Add Dependency References");
                    NotifyCollectionAdd(sender, e);
                    NotifyCollectionAdd_DepPKeys(sender, e);
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Remove Dependency References");
                    NotifyCollectionRemove(sender, e);
                    NotifyCollectionRemove_DepPKeys(sender, e);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Action Reset Dependency References");
                    NotifyCollectionReset(sender, e);
                    NotifyCollectionReset_DepPKeys(sender, e);
                    break;
            }
            
            LockDepPKeysChanges = false;
        }

        /// <summary>
        /// Method called on collection dependency Primary Keys list changed event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        private void DepPKeys_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");

            if(LockDepPKeysChanges == true)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : LockDepPKeysChanges => {LockDepPKeysChanges}");
                LockDepPKeysChanges = false;
                return;
            }
            
            LockDepReferencesChanges = true;

            // Switch for the action to do.
            switch (e.Action)
            {
                // Occurs on add new item into the collection.
                case NotifyCollectionChangedAction.Add:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKeysChanged => Action Add");
                    NotifyCollectionAdd(sender, e);
                    NotifyCollectionAdd_DepReferences(sender, e);
                    NotifyCollectionAdd_DepPKeysRemoved(sender, e); // Remove
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKeysChanged => Action Remove");
                    NotifyCollectionRemove(sender, e);
                    NotifyCollectionRemove_DepReferences(sender, e);
                    NotifyCollectionRemove_DepPKeysRemoved(sender, e); // Add
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKeysChanged => Action Reset");
                    NotifyCollectionReset(sender, e);
                    NotifyCollectionReset_DepReferences(sender, e);
                    break;
            }
            
            LockDepReferencesChanges = false;
        }

        #endregion



        #region Methods Notify Add

        /// <summary>
        /// <para>Method to notify an add event to the dependency link collection.</para>
        /// <para>Originaly from the Primary Keys and References collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionAdd(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    int pkValue = GetPkValue(item);

                    if (pkValue > 0)
                    {
                        //T obj1 = (T)Activator.CreateInstance(typeof(T));
                        //var b = obj1.GetType();
                        try
                        {
                            T obj = this.ToList().Find(x => (int)x.GetPropertyValue(DepPKName) == pkValue);
                            if (obj == null)
                            {
                                T newObj = (T)Activator.CreateInstance(typeof(T));
                                newObj.SetPropertyValue(DepPKName, pkValue);
                              
                                Add(newObj);
                                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {newObj?.GetType()?.Name} {pkValue} to observable dependency link.");
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Output(), ex);
                            throw ex;
                        }
                    }
                    else
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding to observable dependency link => Can't add 0");
                    }
                }
            }
        }

        /// <summary>
        /// <para>Method to notify an add event to the dependency References collection.</para>
        /// <para>Originaly from the Primary Keys and Links collections.</para>
        /// <para>
        /// WARNING : REFERENCES can't be auto populated.
        /// SQLite lock table on Save, concurrent Select cause SQLiteException.
        /// Method still here to try to find a solution.
        /// Use a call to Populate() when needed.
        /// </para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionAdd_DepReferences(object sender, NotifyCollectionChangedEventArgs e)
        {
            // REFERENCES can't be auto populated.
            // SQLite lock table on Save, concurrent Select cause SQLiteException.
            // Method still here to try to find a solution.
            return;

            // Process action on each new item
            //if (e.NewItems?.Count != null)
            //{
            //    foreach (var item in e.NewItems)
            //    {
            //        int pkValue = 0;

            //        if (item is int)
            //            pkValue = (int)item;
            //        else
            //            pkValue = (int)item.GetPropertyValue(DepPKName);

            //        if (pkValue > 0)
            //        {
            //            if (FindIndexInDepReferences(pkValue) == -1 && IsPopulated == true)
            //            {
            //                E reference = EntityBase.Db.Context.Find<E>(pkValue);

            //                if (reference != null && !DepReferences.Contains(reference))
            //                {
            //                    DepReferences.Add(reference);
            //                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {reference.GetType().Name} {(reference as EntityBase).PrimaryKey} to observable dependency reference.");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding to observable dependency reference => Can't add 0");
            //        }                    
            //    }
            //}
        }

        /// <summary>
        /// <para>Method to notify an add event to the dependency Primary Keys collection</para>
        /// <para>Originaly from the References and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionAdd_DepPKeys(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    int pkValue = GetPkValue(item);
                    if (pkValue != 0)
                    depPKs.AddIfNotExists(pkValue);

                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    //int pkValue = GetPkValue(item);
                    //if (pkValue != 0 && !ExistsDepPKeys(pkValue))
                    //{
                    //    depPKs.Add(pkValue);
                    //    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {pkValue} to observable dependency primary key.");
                    //}
                    //else
                    //{
                    //    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding to observable dependency primary keys => Can't add {pkValue}");
                    //}
                }
            }
        }

        /// <summary>
        /// <para>Method to notify an add event to the dependency Primary Keys collection</para>
        /// <para>Originaly from the References and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionAdd_DepPKeysRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    // Search for the value of the primary key name of the item.
                    int pkValue = GetPkValue(item);
                    if (FindIndexInDepPKeysRemoved(pkValue) != -1)
                    {
                        depPKsRemoved.Remove(pkValue);
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing {pkValue} from observable removed dependency primary key.");
                    }
                    else
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing from observable removed dependency primary keys => Key not found {pkValue}");
                    }
                }
            }
        }

        #endregion



        #region Methods Notify Remove

        /// <summary>
        /// <para>Method to notify a remove event to the dependency link collection.</para>
        /// <para>Originaly from the Primary Keys and References collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionRemove(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old items
                foreach (var item in e.OldItems)
                {
                    try
                    {
                        int pkValue = GetPkValue(item);

                        var a = this.ToList();

                        T obj = this.ToList().Find(x => (int)x.GetPropertyValue(DepPKName) == pkValue);
                        if (obj != null)
                        {
                            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing {obj?.GetType()?.Name} {(obj as EntityBase)?.PrimaryKey} to observable list.");
                            Remove(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Output(), ex);
                        throw ex;
                    }
                }
            }
        }
        
        /// <summary>
        /// <para>Method to notify a remove event to the dependency Primary Keys collection</para>
        /// <para>Originaly from the References and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionRemove_DepPKeys(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old items
                foreach (var item in e.OldItems)
                {
                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    int pkValue = GetPkValue(item);
                    if (FindIndexInDepPKeys(pkValue) != -1)
                    {
                        depPKs.Remove(pkValue);
                    }
                }
            }
        }
        
        /// <summary>
        /// <para>Method to notify a remove event to the dependency Primary Keys collection</para>
        /// <para>Originaly from the References and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionRemove_DepPKeysRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old items
                foreach (var item in e.OldItems)
                {
                    // Search for the value of the primary key name of the item.
                    int pkValue = GetPkValue(item);
                    if (pkValue != 0 && FindIndexInDepPKeysRemoved(pkValue) == -1)
                    {
                        depPKsRemoved.Add(pkValue);
                    }
                }
            }
        }

        /// <summary>
        /// <para>Method to notify a remove event to the dependency References collection</para>
        /// <para>Originaly from the Primary Keys and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionRemove_DepReferences(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old item
                foreach (var item in e.OldItems)
                {
                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    int pkValue = GetPkValue(item);
                    if (LockDepReferencesChanges)
                    {
                        if (FindIndexInDepReferences(pkValue) != -1)
                        {
                            E reference = FindDepReferences(pkValue);
                            DepReferences.Remove(reference);
                            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing {reference?.GetType()?.Name} {(reference as EntityBase)?.PrimaryKey} to observable list.");
                        }
                    }
                }
            }
        }

        #endregion



        #region Methods Notify Reset

        /// <summary>
        /// <para>Method to notify a remove event to the dependency link collection.</para>
        /// <para>Originaly from the Primary Keys and References collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionReset(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Count > 0)
            {
                Clear();
            }
        }

        /// <summary>
        /// <para>Method to notify a remove event to the dependency Primary Keys collection</para>
        /// <para>Originaly from the References and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionReset_DepPKeys(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (depPKs.Count > 0)
            {
                depPKs.Clear();
            }
        }
        
        /// <summary>
        /// <para>Method to notify a reset event to the dependency References collection</para>
        /// <para>Originaly from the Primary Keys and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void NotifyCollectionReset_DepReferences(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (depRef.Count > 0)
            {
                depRef.Clear();
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method to populate observable collection of Primary Keys and References.
        /// </summary>
        public virtual void Populate()
        {
            if (!IsPopulated)
            {
                IsPopulated = true;

                // Convert dependencies into a list of Primary Keys.
                if (DepPKeys.Count != Count)
                {
                    foreach (var o in this)
                    {
                        DepPKeys.AddIfNotExists((int)((T)o).GetPropertyValue(DepPKName));
                    }
                }
            }

            // Convert dependencies into a list of Entity References.
            if (DepReferences.Count != Count)
            {
                Populate_DepReferences();
            }
        }

        /// <summary>
        /// <para>Method to notify an add event to the dependency References collection</para>
        /// <para>Originaly from the Primary Keys and Links collections.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Notify collection changed event arguments.</param>
        protected virtual void Populate_DepReferences()
        {
            // Process action on each new item
            if (Items?.Count != null)
            {
                foreach (var item in Items)
                {
                    int pkValue = 0;

                    pkValue = (int)item.GetPropertyValue(DepPKName);

                    if (pkValue > 0)
                    {
                        if (FindIndexInDepReferences(pkValue) == -1 && IsPopulated == true)
                        {
                            E reference = EntityBase.Db.Context.Find<E>(pkValue);

                            if (reference != null && !DepReferences.Contains(reference))
                            {
                                ApplicationBase.BeginInvokeIfRequired(new Action(() =>
                                {
                                    DepReferences.Add(reference);
                                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {reference.GetType().Name} {(reference as EntityBase).PrimaryKey} to REFERENCES.");
                                }));
                            }
                        }
                    }
                    else
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding to REFERENCES => Can't add 0");
                    }
                }
            }
        }

        #endregion
    }
}
