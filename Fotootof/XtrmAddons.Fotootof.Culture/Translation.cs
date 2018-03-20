using System.Collections;
using System.Resources;
using System.Threading;
using System.Windows;
using XtrmAddons.Net.Common.Extensions;

namespace XtrmAddons.Fotootof.Culture
{
    public static class Translation
    {
        private static ResourceManager RmWords = new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
        private static ResourceManager RmLogs = new ResourceManager("XtrmAddons.Fotootof.Culture.Properties.ResourcesLogs", System.Reflection.Assembly.GetExecutingAssembly());

        private static ResourceDictionary words;
        private static ResourceDictionary logs;

        /// <summary>
        /// 
        /// </summary>
        public static ResourceDictionary Words
        {
            get
            {
                if (words == null)
                {
                    Translate();
                }

                return words;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static dynamic DWords => Words.ToExpando();

        /// <summary>
        /// 
        /// </summary>
        public static ResourceDictionary Logs
        {
            get
            {
                if (logs == null)
                {
                    Translate();
                }

                return logs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static dynamic DLogs => Logs.ToExpando();

        /// <summary>
        /// 
        /// </summary>
        public static void Translate()
        {
            words = new ResourceDictionary();
            ResourceSet rsWords = RmWords.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
            foreach (DictionaryEntry de in rsWords)
            {
                words.Add(de.Key.ToString(), de.Value.ToString());
            }
            
            logs = new ResourceDictionary();
            rsWords = RmLogs.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
            foreach (DictionaryEntry de in rsWords)
            {
                logs.Add(de.Key.ToString(), de.Value.ToString());
            }
        }
    }
}