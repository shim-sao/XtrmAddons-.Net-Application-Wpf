namespace XtrmAddons.Fotootof.AddInsContracts.Interfaces
{
    /// <summary>
    /// Interface XtrmAddons Fotootof AddIns Contracts Interfaces Process.
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// Method to run an AddIn process.
        /// </summary>
        void Run();
    }
}
