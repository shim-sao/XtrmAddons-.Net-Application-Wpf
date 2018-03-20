namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Server Libraries Base Interface Page Base.</para>
    /// <para>This Interface inplement some properties and method usefull for managing custom user control.</para>
    /// </summary>
    public interface IContentInit
    {
        /// <summary>
        /// Method to initialize and display data context.
        /// </summary>
        void InitializeContent();

        /// <summary>
        /// Method to initialize and display data context asynchronous.
        /// </summary>
        void InitializeContentAsync();
    }
}