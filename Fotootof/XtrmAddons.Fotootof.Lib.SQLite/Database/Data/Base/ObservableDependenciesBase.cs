using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System;
using XtrmAddons.Net.Common.Extensions;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Base
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries SQLite Database Data Base Observable Dependencies.
    /// </summary>
    [JsonArray()]
    public class ObservableDependenciesBase<T> : ObservableCollection<T>
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
        private List<int> depPKs = new List<int>();

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the dependency property primary key name.
        /// </summary>
        public string DepPKName { get; private set; } = "";

        /// <summary>
        /// Property to access to a list of dependency items primary keys.
        /// </summary>
        public IEnumerable<int> DependenciesPrimaryKeys
        {
            get => depPKs;
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
                ArgumentNullException e = ArgumentNullException(nameof(dependenciesPrimaryKeysName));
                log.Fatal(e.Output());
                throw e;
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
        /// Method to find an element by a property value.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The property value to search.</param>
        /// <returns>The first founded element otherwise, default value of type T, or null if type T is nullable.</returns>
        protected int FindIndexInDepPks(int primaryKey)
        {
            return depPKs.ToList().FindIndex(x => x == primaryKey);
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
                        if (FindIndexInDepPks(id) == -1)
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
                        if (FindIndexInDepPks(id) == -1)
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



        #region Methods Exceptions

        /// <summary>
        /// Method to get a formated argument null exception.
        /// </summary>
        /// <returns>A formated argument null exception.</returns>
        protected static ArgumentNullException ArgumentNullException(string propertyName)
        {
            return new ArgumentNullException($"The argument {propertyName} must be not null, empty or whitespace !");
        }

        #endregion
    }
}
