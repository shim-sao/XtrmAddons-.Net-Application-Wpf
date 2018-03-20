using System.Dynamic;
using System.Windows;
using System.Windows.Controls;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Controls
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Classes Controls Control Base.</para>
    /// <para>This Class is design to define some application custom properties and methods for user controls.</para>
    /// </summary>
    public class ControlBase : UserControl
    {
        #region Methods

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Control Base Constructor.
        /// </summary>
        public ControlBase() : base()
        {
            // Merge application resources.
            Resources.MergedDictionaries.Add(Culture.Translation.Words);
            Resources.MergedDictionaries.Add(Culture.Translation.Logs);
        }

        #endregion
    }
}