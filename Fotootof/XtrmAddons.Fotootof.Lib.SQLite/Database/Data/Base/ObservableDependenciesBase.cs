using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System;
using XtrmAddons.Net.Common.Extensions;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Base Observable Dependencies.
    /// </summary>
    public class ObservableDependenciesBase<T> : ObservableCollection<T>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private bool isCollectionWritable = true;

        /// <summary>
        /// 
        /// </summary>
        private bool isdepPKsWritable = true;

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<int> depPKs = new ObservableCollection<int>();

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string DepPKName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public IList<int> DependenciesPrimaryKeys
        {
            get { return depPKs; }
            set
            {
                if (value != depPKs)
                {
                    depPKs = new ObservableCollection<int>(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCollectionWritable
        {
            get { return isCollectionWritable; }
            private set
            {
                if (value != isCollectionWritable)
                {
                    isCollectionWritable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsdepPKsWritable
        {
            get { return isdepPKsWritable; }
            private set
            {
                if (value != isdepPKsWritable)
                {
                    isdepPKsWritable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Event Handler

        /// <summary>
        /// Delegate property changed event handler of the model.
        /// </summary>
        public event PropertyChangedEventHandler DelegatePropertyChanged = delegate { };

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries SQLite Database Data Base Observable Dependencies Constructor.
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName"></param>
        public ObservableDependenciesBase(string dependenciesPrimaryKeysName) : base()
        {
            if(dependenciesPrimaryKeysName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(dependenciesPrimaryKeysName));
            }
            DepPKName = dependenciesPrimaryKeysName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependenciesPrimaryKeysName"></param>
        //public ObservableDependenciesBase(string dependenciesPrimaryKeysName) : base()
        //{
        //    if(dependenciesPrimaryKeysName.IsNullOrWhiteSpace())
        //    {
        //        throw new ArgumentNullException(nameof(dependenciesPrimaryKeysName));
        //    }
        //    DepPKName = dependenciesPrimaryKeysName;
        //}

        #endregion



        #region Methods

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (DepPKName.IsNullOrWhiteSpace())
            {
                return;
            }

            IsCollectionWritable = false;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add :
                    foreach(var item in e.NewItems)
                    {
                        int id = (int)item.GetPropertyValue(DepPKName);
                        if (!(depPKs.ToList().FindIndex(x => x == id) > 0))
                        {
                            depPKs.Add((int)item.GetPropertyValue(DepPKName));
                            NotifyPropertyChanged();
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move :
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.NewItems)
                    {
                        int id = (int)item.GetPropertyValue(DepPKName);
                        if (!(depPKs.ToList().FindIndex(x => x == id) > 0))
                        {
                            depPKs.Remove((int)item.GetPropertyValue(DepPKName));
                            NotifyPropertyChanged();
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace :
                    break;

                case NotifyCollectionChangedAction.Reset :
                    if (depPKs.Count > 0)
                    {
                        depPKs.Clear();
                        NotifyPropertyChanged();
                    }
                    break;
            }

            IsCollectionWritable = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OndepPKsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (DepPKName.IsNullOrWhiteSpace())
            {
                return;
            }

            IsdepPKsWritable = false;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (int item in e.NewItems)
                    {
                        if (!(Items.ToList().FindIndex(x => (int)x.GetPropertyValue(DepPKName) == item) >= 0))
                        {
                            T newItem = (T)Activator.CreateInstance(typeof(T));
                            newItem.SetPropertyValue(DepPKName, item);
                            Add(newItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (int item in e.NewItems)
                    {
                        int index = Items.ToList().FindIndex(x => (int)x.GetPropertyValue(DepPKName) == item);
                        if (index >= 0)
                        {
                            RemoveAt(index);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    if (Count > 0)
                    {
                        Clear();
                    }
                    break;
            }

            IsdepPKsWritable = true;
        }

        #endregion
    }
}
