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
        private List<int> depPKs = new List<int>();

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string DepPKName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<int> DependenciesPrimaryKeys
        {
            get { return depPKs; }
            private set
            {
                if (value != depPKs)
                {
                    depPKs = new List<int>(value);
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

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add :
                    foreach(var item in e.NewItems)
                    {
                        int id = (int)item.GetPropertyValue(DepPKName);
                        if (!(depPKs.ToList().FindIndex(x => x == id) > 0))
                        {
                            depPKs.Add((int)item.GetPropertyValue(DepPKName));
                        }
                    }
                    NotifyPropertyChanged("DependenciesPrimaryKeys");
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
                        }
                    }
                    NotifyPropertyChanged("DependenciesPrimaryKeys");
                    break;

                case NotifyCollectionChangedAction.Replace :
                    break;

                case NotifyCollectionChangedAction.Reset :
                    if (depPKs.Count > 0)
                    {
                        depPKs.Clear();
                    }
                    NotifyPropertyChanged("DependenciesPrimaryKeys");
                    break;
            }
        }

        #endregion
    }
}
