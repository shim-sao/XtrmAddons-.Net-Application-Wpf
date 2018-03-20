namespace XtrmAddons.Fotootof.Libraries.Common.Interfaces.Windows
{
    [System.Obsolete("Use => XtrmAddons.Fotootof.Lib.Base.Interfaces.IWindowForm", true)]
    public interface IWindowForm<T>
    {
        /// <summary>
        /// Variable old form informations.
        /// </summary>
        T OldForm { get; set; }

        /// <summary>
        /// Variable new form informations.
        /// </summary>
        T NewForm { get; set; }
    }
}
