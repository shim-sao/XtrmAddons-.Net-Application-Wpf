namespace Fotootof.Libraries.Interfaces
{
    /// <summary>
    /// <para>Interface Fotootof Libraries Auto Loader.</para>
    /// <para></para>
    /// </summary>
    public interface IAutoLoader
    {
        #region Properties

        /// <summary>
        /// Property to define if the default object context can be loaded at construct.
        /// </summary>
        bool IsAutoloadEnabled { get; }

        #endregion



        #region Methods

        /// <summary>
        /// Method to initialize the object.
        /// </summary>
        /// <param name="autoLoad">Is auto load enabled ?.</param>
        void Initialize(bool autoLoad);

        /// <summary>
        /// Method to load the object default context.
        /// </summary>
        void Load();

        #endregion
    }
}
