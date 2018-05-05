using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using System.Windows;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Culture
{
    public static class Translation
    {
        #region Variables

        /// <summary>
        /// Variable resources manager for generic words translations.
        /// </summary>
        private static Dictionary<string, ResourceManager> rms 
            = new Dictionary<string, ResourceManager>()
            {
                { "Words", new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly()) },
                { "Logs", new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.ResourcesLogs", System.Reflection.Assembly.GetExecutingAssembly()) }
            };

        /// <summary>
        /// Variable resources manager for generic words translations.
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        public static dynamic DLogs
            => Logs.ToExpando();

        /// <summary>
        /// 
        /// </summary>
        public static dynamic DWords
            => Words.ToExpando();

        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
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