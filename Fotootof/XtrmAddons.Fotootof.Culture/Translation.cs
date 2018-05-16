using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using System.Windows;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Culture
{
    /// <summary>
    /// Class XtrmAddons Fotootof Culture Translation.
    /// </summary>
    public static class Translation
    {
        #region Variables

        /// <summary>
        /// Variable dictionary of resources manager for culture translations.
        /// </summary>
        private static Dictionary<string, ResourceManager> rms 
            = new Dictionary<string, ResourceManager>()
            {
                { "Words", new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly()) },
                { "Logs", new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.ResourcesLogs", System.Reflection.Assembly.GetExecutingAssembly()) }
            };

        /// <summary>
        /// Variable dictionary of resources dictionary of culture translations.
        /// </summary>
        private static Dictionary<string, ResourceDictionary> rds 
            = new Dictionary<string, ResourceDictionary>()
            {
                { "Words", null },
                { "Logs", null }
            };

        #endregion



        #region Properties

        /// <summary>
        /// Property to access to the logs culture translation as resource dictionary.
        /// </summary>
        public static ResourceDictionary Logs
        {
            get
            {
                if (rds["Logs"] == null)
                {
                    Translate("Logs");
                }

                return rds["Logs"];
            }
        }

        /// <summary>
        /// Property to access to the generic words culture translation as resource dictionary.
        /// </summary>
        public static ResourceDictionary Words
        {
            get
            {
                if (rds["Words"] == null)
                {
                    Translate("Words");
                }

                return rds["Words"];
            }
        }

        /// <summary>
        /// Property to access to the logs culture translation as object with dynamic properties.
        /// </summary>
        public static dynamic DLogs
            => Logs.ToExpando();

        /// <summary>
        /// Property to access to the generic words culture translation as object with dynamic properties.
        /// </summary>
        public static dynamic DWords
            => Words.ToExpando();

        #endregion



        #region Methods

        /// <summary>
        /// Method to get a given source translation.
        /// </summary>
        /// <param name="name">The name of the resources file to load.</param>
        private static void Translate(string name)
        {
            rds[name] = new ResourceDictionary();
            var rs = rms[name].GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
            foreach (DictionaryEntry de in rs)
            {
                rds[name].Add(de.Key.ToString(), de.Value.ToString());
            }
        }

        #endregion
    }
}