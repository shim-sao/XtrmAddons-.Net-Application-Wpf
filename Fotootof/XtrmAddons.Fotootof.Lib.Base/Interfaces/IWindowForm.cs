namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
