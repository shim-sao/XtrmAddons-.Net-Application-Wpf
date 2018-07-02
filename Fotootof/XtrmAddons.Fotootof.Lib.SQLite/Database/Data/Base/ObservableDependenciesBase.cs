using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies.
    /// </summary>
    /// <typeparam name="T">The Type of the items of the dependency.</typeparam>
    /// <typeparam name="E">The Type of the entity items destination of the dependency.</typeparam>
    public class ObservableDependenciesBase<T, E> : ObservableCollection<T> where E : class
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
        /// Variable list of dependency primary keys.
        /// </summary>
        private ObservableCollection<E> depRef = new ObservableCollection<E>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the dependency property primary key name.
        /// </summary>
        public string DepPKName { get; private set; }

        /// <summary>
        /// Property to access to a list of dependency items primary keys.
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
        /// Property to access to
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
        /// 
        /// </summary>
        public bool NotifyChanges { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool NotifyDepReferencesChanges { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool NotifyDepPKeysChanges { get; set; } = true;

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
        public ObservableDependenciesBase()
        {
            DepPKName = typeof(E).Name.Replace("Entity", "Id");
            DepReferences.CollectionChanged += DepReferences_CollectionChanged;
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        /// <param name="list">A list of items to add at the collection initialization. </param>
        public ObservableDependenciesBase(List<T> list) : base(list)
        {
            DepPKName = typeof(E).Name.Replace("Entity", "Id");
        }

        /// <summary>
        /// Class XtrmAddons Fotootof Lib SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        /// <param name="collection">A enumerable collection of items to add at the collection initialization.</param>
        public ObservableDependenciesBase(IEnumerable<T> collection) : base(collection)
        {
            DepPKName = typeof(E).Name.Replace("Entity", "Id");
        }

        #endregion



        #region Methods Find

        /// <summary>
        /// Method to find an element by a property value.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value to search.</param>
        /// <returns>The first founded element otherwise, default value of type T, or null if type T is nullable.</returns>
        public int FindIndexInDepPks(int primaryKey)
        {
            return depPKs.ToList().FindIndex(x => x == primaryKey);
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
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");

            // Check if a dependency primary key name is valid.
            if (DepPKName.IsNullOrWhiteSpace())
            {
                DepPKName = typeof(E).Name.Replace("Entity", "Id");
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : DepPKName => {DepPKName}");
            }

            if (NotifyChanges == false)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => {NotifyChanges}");
                NotifyChanges = true;
                return;
            }

            NotifyDepPKeysChanges = false;
            NotifyDepReferencesChanges = false;

            // Switch for the action to do.
            switch (e.Action)
            {
                // Occurs on add new item into the collection.
                case NotifyCollectionChangedAction.Add:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Add");
                    NotifyCollectionDepPKeysAdd(this, e);
                    NotifyCollectionDepReferencesAdd(this, e);
                    break;

                case NotifyCollectionChangedAction.Move :
                    break;

                case NotifyCollectionChangedAction.Remove:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Remove");
                    NotifyCollectionRemove_DepPKeys(this, e);
                    NotifyCollectionRemove_DepReferences(this, e);
                    break;

                case NotifyCollectionChangedAction.Replace :
                    break;

                case NotifyCollectionChangedAction.Reset :
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyChanges => Action Reset");
                    NotifyCollectionReset(e);
                    DepReferences.Clear();
                    break;
            }

            NotifyDepPKeysChanges = true;
            NotifyDepReferencesChanges = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Sender => {GetType().Name}");

            if(NotifyDepReferencesChanges == false)
            {
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyDepReferencesChanges => {NotifyDepReferencesChanges}");
                NotifyDepReferencesChanges = true;
                return;
            }

            NotifyChanges = false;
            NotifyDepPKeysChanges = false;

            // Switch for the action to do.
            switch (e.Action)
            {
                // Occurs on add new item into the collection.
                case NotifyCollectionChangedAction.Add:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyDepReferencesChanges => Action Add");
                    NotifyCollectionAdd(sender, e);
                    NotifyCollectionDepPKeysAdd(sender, e);
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyDepReferencesChanges => Action Remove");
                    NotifyCollectionRemove(sender, e);
                    NotifyCollectionRemove_DepPKeys(sender, e);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : NotifyDepReferencesChanges => Action Reset");
                    DepPKeys.Clear();
                    Clear();
                    break;
            }

            NotifyChanges = true;
            NotifyDepPKeysChanges = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionAdd(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    T obj = this.ToList().Find(x => (int)x.GetPropertyValue(DepPKName) == pkValue);
                    if (obj == null)
                    {
                        T newObj = (T)Activator.CreateInstance(typeof(T));
                        newObj.SetPropertyValue(DepPKName, pkValue);
                        Add(newObj);
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {newObj.GetType().Name} {(newObj as EntityBase).PrimaryKey} to observable list.");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionDepReferencesAdd(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    if (FindIndexInDepReferences(pkValue) == -1)
                    {
                        E reference = EntityBase.Db.Context.Find<E>(pkValue);

                        if (reference != null && !DepReferences.Contains(reference))
                        {
                            DepReferences.Add(reference);
                            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Adding {reference.GetType().Name} {(reference as EntityBase).PrimaryKey} to observable list.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionDepPKeysAdd(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Process action on each new item
            if (e.NewItems?.Count != null)
            {
                foreach (var item in e.NewItems)
                {
                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    if (FindIndexInDepPks(pkValue) == -1)
                    {
                        depPKs.Add((int)item.GetPropertyValue(DepPKName));
                    }
                }
            }
        }

        #endregion



        #region Methods Notify Remove

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionRemove(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old items
                foreach (var item in e.OldItems)
                {
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    T obj = this.ToList().Find(x => (int)x.GetPropertyValue(DepPKName) == pkValue);
                    if (obj != null)
                    {
                        log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing {obj?.GetType()?.Name} {(obj as EntityBase)?.PrimaryKey} to observable list.");
                        Remove(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionRemove_DepPKeys(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old items
                foreach (var item in e.OldItems)
                {
                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    if (FindIndexInDepPks(pkValue) != -1)
                    {
                        depPKs.Remove(pkValue);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionRemove_DepReferences(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != null)
            {
                // Process action on each old item
                foreach (var item in e.OldItems)
                {
                    // Try to find if the dependency is already set.
                    // Search for the value of the primary key name of the item.
                    int pkValue = (int)item.GetPropertyValue(DepPKName);
                    if (NotifyDepReferencesChanges)
                    {
                        if (FindIndexInDepReferences(pkValue) != -1)
                        {
                            E reference = FindDepReferences(pkValue);
                            DepReferences.Remove(reference);
                            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : Removing {reference.GetType().Name} {(reference as EntityBase).PrimaryKey} to observable list.");
                        }
                    }
                }
            }
        }

        #endregion



        #region Methods Notify Reset

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void NotifyCollectionReset(NotifyCollectionChangedEventArgs e)
        {
            if (depPKs.Count > 0)
            {
                depPKs.Clear();
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureId"></param>
        public void Link(int itemPK, bool isNew = true)
        {
            try
            {
                int index = this.ToList().FindIndex(x => (int)x.GetPropertyValue(DepPKName) == itemPK);

                if (index < 0)
                {
                    if (isNew)
                    {
                        T item = (T)Activator.CreateInstance(typeof(T));
                        item.SetPropertyValue(DepPKName, itemPK);
                        this.Add(item);
                    }
                    else
                    {
                        //this.Add(new PicturesInAlbums { PictureId = pictureId, AlbumId = PrimaryKey });
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}
