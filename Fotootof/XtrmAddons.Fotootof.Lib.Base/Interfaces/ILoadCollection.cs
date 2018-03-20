namespace XtrmAddons.Fotootof.Lib.Base.Interfaces
{
    /// <summary>
    /// <para>Interface XtrmAddons Fotootof Libraries Base Load Collection</para>
    /// <para></para>
    /// </summary>
    public interface ILoadCollection
    {
        #region Properties

        /// <summary>
        /// Property to define if the default items of the collection can be loaded.
        /// </summary>
        bool IsAutoloadEnabled { get; }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the collection.
        /// </summary>
        /// <param name="autoLoad">Auto load default items of the collection.</param>
        void Initialize(bool autoLoad);

        /// <summary>
        /// Method to load default items of the collection.
        /// </summary>
        void Load();

        #endregion
    }
}
